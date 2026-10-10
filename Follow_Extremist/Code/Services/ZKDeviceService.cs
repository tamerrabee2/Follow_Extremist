using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Follow_Extremist.Core;

namespace Follow_Extremist.Code.Services
{
    public class ZKDeviceService : IZKDeviceService, IDisposable
    {
        private object zkInstance;
        private Type zkType;
        private bool isConnected;
        private FingerprintDevice currentDevice;
        private System.Windows.Forms.Timer pollTimer;
        private readonly object lockObj = new();
        private bool isDisposed;
        private DateTime lastSeenLogTime = DateTime.MinValue;
        private int lastRecordCount = -1;

        public bool IsConnected => isConnected;
        public FingerprintDevice CurrentDevice => currentDevice;

        public event EventHandler<AttendanceEventArgs> OnAttendanceReceived;
        public event EventHandler<string> OnStatusChanged;
        public event EventHandler<string> OnErrorOccurred;

        public ZKDeviceService()
        {
            InitializeZKObject();
        }

        private bool InitializeZKObject()
        {
            if (zkInstance != null) return true;

            try
            {
                zkType = Type.GetTypeFromProgID("zkemkeeper.ZKEM");
                if (zkType != null)
                {
                    zkInstance = Activator.CreateInstance(zkType);
                    return true;
                }
                else
                {
                    OnStatusChanged?.Invoke(this, "مكتبة zkemkeeper.dll غير مسجلة في النظام.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke(this, $"فشل تهيئة مكتبة البصمة: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ConnectAsync(FingerprintDevice device)
        {
            if (device == null) return false;
            currentDevice = device;

            return await Task.Run(() =>
            {
                lock (lockObj)
                {
                    if (!InitializeZKObject())
                    {
                        OnErrorOccurred?.Invoke(this, "تعذر إنشاء كائن zkemkeeper. تأكد من تثبيت وتشغيل regsvr32 zkemkeeper.dll");
                        return false;
                    }

                    try
                    {
                        if (isConnected)
                        {
                            Disconnect();
                        }

                        // Set communication password if provided (MUST be set BEFORE Connect_Net)
                        if (!string.IsNullOrEmpty(device.CommPassword) && int.TryParse(device.CommPassword, out int commKey))
                        {
                            zkType.InvokeMember("SetCommPassword",
                                BindingFlags.InvokeMethod, null, zkInstance, new object[] { commKey });
                        }

                        // Connect_Net(ip, port)
                        object[] args = new object[] { device.IpAddress, device.Port };
                        bool result = (bool)zkType.InvokeMember("Connect_Net",
                            BindingFlags.InvokeMethod, null, zkInstance, args);

                        if (result)
                        {
                            isConnected = true;

                            // RegEvent(machineNumber, 65535)
                            try
                            {
                                zkType.InvokeMember("RegEvent",
                                    BindingFlags.InvokeMethod, null, zkInstance, new object[] { device.MachineNumber, 65535 });
                            }
                            catch { }

                            // Synchronize Device Time automatically on connection
                            try
                            {
                                SyncDeviceTimeInternal();
                            }
                            catch { }

                            // Read actual device time to align lastSeenLogTime
                            var devTime = GetDeviceTimeInternal() ?? DateTime.Now;
                            lastSeenLogTime = devTime.AddSeconds(-10);

                            // Read hardware Serial Number from device
                            try
                            {
                                string sn = GetDeviceSerialNumberInternal(device.MachineNumber);
                                if (!string.IsNullOrEmpty(sn))
                                {
                                    device.SerialNumber = sn;
                                    currentDevice.SerialNumber = sn;
                                    _ = UpdateDeviceSerialNumberInDbAsync(device.Id, sn);
                                }
                            }
                            catch { }

                            // Initial seed for device log count to prevent redundant initial dumps
                            lastRecordCount = GetDeviceRecordCountInternal(device.MachineNumber);

                            // Start background polling timer on UI thread
                            StartPolling();

                            OnStatusChanged?.Invoke(this, $"تم الاتصال بنجاح بالجهاز: {device.DeviceName} ({device.IpAddress})");
                            return true;
                        }
                        else
                        {
                            isConnected = false;
                            object[] errArgs = new object[] { 0 };
                            zkType.InvokeMember("GetLastError",
                                BindingFlags.InvokeMethod, null, zkInstance, errArgs);
                            int errorCode = (int)errArgs[0];

                            OnErrorOccurred?.Invoke(this, $"فشل الاتصال بالجهاز {device.IpAddress} (رمز الخطأ: {errorCode})");
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        isConnected = false;
                        OnErrorOccurred?.Invoke(this, $"استثناء أثناء الاتصال بالجهاز: {ex.Message}");
                        return false;
                    }
                }
            });
        }

        public int GetDeviceRecordCountInternal(int machineNum)
        {
            if (!isConnected || zkInstance == null || zkType == null) return -1;
            try
            {
                object[] statusArgs = new object[] { machineNum, 6, 0 };
                ParameterModifier pm = new ParameterModifier(3);
                pm[2] = true;
                bool ok = (bool)zkType.InvokeMember("GetDeviceStatus",
                    BindingFlags.InvokeMethod, null, zkInstance, statusArgs, new ParameterModifier[] { pm }, null, null);
                if (ok && statusArgs[2] != null)
                {
                    return Convert.ToInt32(statusArgs[2]);
                }
            }
            catch { }
            return -1;
        }

        public void Disconnect()
        {
            lock (lockObj)
            {
                StopPolling();

                if (isConnected && zkInstance != null && zkType != null)
                {
                    try
                    {
                        zkType.InvokeMember("Disconnect",
                            BindingFlags.InvokeMethod, null, zkInstance, null);
                    }
                    catch { }
                }

                isConnected = false;
                lastRecordCount = -1;
                OnStatusChanged?.Invoke(this, "تم قطع الاتصال بجهاز البصمة.");
            }
        }

        private void StartPolling()
        {
            if (System.Windows.Forms.Application.OpenForms.Count > 0)
            {
                var mainForm = System.Windows.Forms.Application.OpenForms[0];
                mainForm.BeginInvoke(new Action(() =>
                {
                    if (pollTimer == null)
                    {
                        pollTimer = new System.Windows.Forms.Timer();
                        pollTimer.Interval = 2000; // فحص خفيف وسريع كل ثانيتين بدون إجهاد المعالج
                        bool isBusy = false;
                        pollTimer.Tick += async (s, e) =>
                        {
                            if (isConnected && !isBusy)
                            {
                                isBusy = true;
                                try
                                {
                                    int machineNum = currentDevice?.MachineNumber ?? 1;
                                    int currentCount = GetDeviceRecordCountInternal(machineNum);

                                    // إذا لم يتغير عدد الحركات في ذاكرة الجهاز، فالجهاز خامل ولا توجد أي بصمة جديدة -> تخطي فوري في 1ms
                                    // هذا يمنع إرهاق معالج الماكينة وتجميد حساس البصمة نهائياً
                                    if (currentCount >= 0 && lastRecordCount >= 0 && currentCount <= lastRecordCount)
                                    {
                                        return;
                                    }

                                    // هناك حركة جديدة تم تسجيلها في الجهاز -> قراءة الحركات الجديدة فقط
                                    await ReadNewLogsAsync();
                                    if (currentCount >= 0)
                                    {
                                        lastRecordCount = currentCount;
                                    }
                                }
                                catch { }
                                finally
                                {
                                    isBusy = false;
                                }
                            }
                        };
                    }
                    pollTimer.Start();
                }));
            }
        }

        private void StopPolling()
        {
            if (pollTimer != null)
            {
                pollTimer.Stop();
                pollTimer.Dispose();
                pollTimer = null;
            }
        }

        public async Task<List<AttendanceEventArgs>> ReadNewLogsAsync()
        {
            if (!isConnected || zkInstance == null) return new List<AttendanceEventArgs>();

            return await Task.Run(() =>
            {
                var logs = new List<AttendanceEventArgs>();

                lock (lockObj)
                {
                    try
                    {
                        int machineNum = currentDevice?.MachineNumber ?? 1;

                        // ReadGeneralLogData(machineNumber)
                        bool readOk = (bool)zkType.InvokeMember("ReadGeneralLogData",
                            BindingFlags.InvokeMethod, null, zkInstance, new object[] { machineNum });

                        if (!readOk) return logs;

                        // Loop through logs using SSR_GetGeneralLogData or GetGeneralLogData
                        while (true)
                        {
                            object[] logArgs = new object[]
                            {
                                machineNum,
                                string.Empty, // sdwEnrollNumber
                                0,            // idwVerifyMode
                                0,            // idwInOutMode
                                0,            // idwYear
                                0,            // idwMonth
                                0,            // idwDay
                                0,            // idwHour
                                0,            // idwMinute
                                0,            // idwSecond
                                0             // idwWorkCode
                            };

                            ParameterModifier pm = new ParameterModifier(11);
                            for (int p = 1; p <= 10; p++) pm[p] = true;

                            bool hasRecord = false;
                            try
                            {
                                hasRecord = (bool)zkType.InvokeMember("SSR_GetGeneralLogData",
                                    BindingFlags.InvokeMethod, null, zkInstance, logArgs, new ParameterModifier[] { pm }, null, null);
                            }
                            catch { }

                            if (!hasRecord)
                            {
                                // Fallback: try standard GetGeneralLogData (with integer enroll number)
                                object[] stdArgs = new object[]
                                {
                                    machineNum,
                                    0, // idwEnrollNumber (int)
                                    0, // idwVerifyMode
                                    0, // idwInOutMode
                                    0, // idwYear
                                    0, // idwMonth
                                    0, // idwDay
                                    0, // idwHour
                                    0, // idwMinute
                                    0, // idwSecond
                                    0  // idwWorkCode
                                };
                                ParameterModifier pmStd = new ParameterModifier(11);
                                for (int p = 1; p <= 10; p++) pmStd[p] = true;

                                bool hasStd = false;
                                try
                                {
                                    hasStd = (bool)zkType.InvokeMember("GetGeneralLogData",
                                        BindingFlags.InvokeMethod, null, zkInstance, stdArgs, new ParameterModifier[] { pmStd }, null, null);
                                }
                                catch { }

                                if (!hasStd) break;

                                logArgs[1] = stdArgs[1]?.ToString();
                                for (int p = 2; p <= 10; p++) logArgs[p] = stdArgs[p];
                            }

                            string enrollNum = logArgs[1]?.ToString()?.Trim();
                            int verifyMode = Convert.ToInt32(logArgs[2]);
                            int year = Convert.ToInt32(logArgs[4]);
                            int month = Convert.ToInt32(logArgs[5]);
                            int day = Convert.ToInt32(logArgs[6]);
                            int hour = Convert.ToInt32(logArgs[7]);
                            int minute = Convert.ToInt32(logArgs[8]);
                            int second = Convert.ToInt32(logArgs[9]);

                            if (int.TryParse(enrollNum, out int elementId) && year > 2000)
                            {
                                DateTime attTime = new DateTime(year, month, day, hour, minute, second);
                                if (attTime > lastSeenLogTime)
                                {
                                    lastSeenLogTime = attTime;
                                    var attEvent = new AttendanceEventArgs
                                    {
                                        ElementId = elementId,
                                        DeviceEnrollId = elementId,
                                        DeviceId = currentDevice?.Id,
                                        DeviceSerialNumber = currentDevice?.SerialNumber,
                                        AttendanceTime = attTime,
                                        VerifyMethod = verifyMode
                                    };
                                    logs.Add(attEvent);

                                    // Raise event for real-time handling
                                    OnAttendanceReceived?.Invoke(this, attEvent);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred?.Invoke(this, $"خطأ أثناء قراءة سجلات البصمة: {ex.Message}");
                    }
                }

                return logs;
            });
        }

        public async Task<bool> UploadUserAsync(int elementId, string elementName)
        {
            if (!isConnected || zkInstance == null) return false;

            return await Task.Run(() =>
            {
                lock (lockObj)
                {
                    try
                    {
                        int machineNum = currentDevice?.MachineNumber ?? 1;
                        string enrollNum = elementId.ToString();

                        // SSR_SetUserInfo(machineNumber, enrollNum, name, password, privilege, enabled)
                        bool result = (bool)zkType.InvokeMember("SSR_SetUserInfo",
                            BindingFlags.InvokeMethod, null, zkInstance,
                            new object[] { machineNum, enrollNum, elementName, "", 0, true });

                        if (result)
                        {
                            // Refresh data in device
                            zkType.InvokeMember("RefreshData",
                                BindingFlags.InvokeMethod, null, zkInstance, new object[] { machineNum });
                        }

                        return result;
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred?.Invoke(this, $"خطأ أثناء رفع بيانات العنصر للجهاز: {ex.Message}");
                        return false;
                    }
                }
            });
        }

        public async Task<bool> UploadFingerprintTemplateAsync(int elementId, int fingerIndex, string templateBase64)
        {
            if (!isConnected || zkInstance == null || string.IsNullOrEmpty(templateBase64)) return false;

            return await Task.Run(() =>
            {
                lock (lockObj)
                {
                    try
                    {
                        int machineNum = currentDevice?.MachineNumber ?? 1;
                        string enrollNum = elementId.ToString();

                        // Try 1: SSR_SetUserTmpStr (Standard for TFT and color screen devices)
                        try
                        {
                            bool result = (bool)zkType.InvokeMember("SSR_SetUserTmpStr",
                                BindingFlags.InvokeMethod, null, zkInstance,
                                new object[] { machineNum, enrollNum, fingerIndex, templateBase64 });

                            if (result)
                            {
                                zkType.InvokeMember("RefreshData",
                                    BindingFlags.InvokeMethod, null, zkInstance, new object[] { machineNum });
                                return true;
                            }
                        }
                        catch { }

                        // Try 2: SetUserTmpStr (Numeric ID fallback)
                        try
                        {
                            bool result = (bool)zkType.InvokeMember("SetUserTmpStr",
                                BindingFlags.InvokeMethod, null, zkInstance,
                                new object[] { machineNum, elementId, fingerIndex, templateBase64 });

                            if (result)
                            {
                                zkType.InvokeMember("RefreshData",
                                    BindingFlags.InvokeMethod, null, zkInstance, new object[] { machineNum });
                                return true;
                            }
                        }
                        catch { }

                        return false;
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred?.Invoke(this, $"خطأ أثناء رفع قالب البصمة: {ex.Message}");
                        return false;
                    }
                }
            });
        }

        public async Task<string> ReadFingerprintTemplateAsync(int elementId, int fingerIndex, string alternateEnrollNum = null)
        {
            if (!isConnected || zkInstance == null) return null;

            return await Task.Run(() =>
            {
                lock (lockObj)
                {
                    try
                    {
                        int machineNum = currentDevice?.MachineNumber ?? 1;
                        string enrollNum = elementId.ToString();

                        // Must read all users and templates from device into SDK memory buffer first
                        try
                        {
                            zkType.InvokeMember("ReadAllUserID",
                                BindingFlags.InvokeMethod, null, zkInstance, new object[] { machineNum });
                        }
                        catch { }

                        try
                        {
                            zkType.InvokeMember("ReadAllTemplate",
                                BindingFlags.InvokeMethod, null, zkInstance, new object[] { machineNum });
                        }
                        catch { }

                        string tpl = TryGetTemplateInternal(machineNum, enrollNum, elementId, fingerIndex);
                        if (!string.IsNullOrEmpty(tpl))
                        {
                            return tpl;
                        }

                        if (!string.IsNullOrEmpty(alternateEnrollNum) && alternateEnrollNum != enrollNum)
                        {
                            int altId = int.TryParse(alternateEnrollNum, out int pAlt) ? pAlt : 0;
                            string altTpl = TryGetTemplateInternal(machineNum, alternateEnrollNum, altId, fingerIndex);
                            if (!string.IsNullOrEmpty(altTpl))
                            {
                                return altTpl;
                            }
                        }

                        return null;
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred?.Invoke(this, $"خطأ أثناء قراءة قالب البصمة: {ex.Message}");
                        return null;
                    }
                }
            });
        }

        private string TryGetTemplateInternal(int machineNum, string enrollNum, int elementId, int fingerIndex)
        {
            // Try 1: SSR_GetUserTmpStr
            try
            {
                object[] argsSSR = new object[] { machineNum, enrollNum, fingerIndex, string.Empty, 0 };
                ParameterModifier pmSSR = new ParameterModifier(5);
                pmSSR[3] = true;
                pmSSR[4] = true;
                bool okSSR = (bool)zkType.InvokeMember("SSR_GetUserTmpStr",
                    BindingFlags.InvokeMethod, null, zkInstance, argsSSR, new ParameterModifier[] { pmSSR }, null, null);
                if (okSSR && !string.IsNullOrEmpty(argsSSR[3]?.ToString()))
                {
                    return argsSSR[3].ToString();
                }
            }
            catch { }

            // Try 1.5: GetUserTmpExStr (Standard for Biokey 10.0 and TFT devices)
            try
            {
                object[] argsEx = new object[] { machineNum, enrollNum, fingerIndex, 0, string.Empty, 0 };
                ParameterModifier pmEx = new ParameterModifier(6);
                pmEx[3] = true; // flag
                pmEx[4] = true; // tmpStr
                pmEx[5] = true; // tmpLen
                bool okEx = (bool)zkType.InvokeMember("GetUserTmpExStr",
                    BindingFlags.InvokeMethod, null, zkInstance, argsEx, new ParameterModifier[] { pmEx }, null, null);
                if (okEx && !string.IsNullOrEmpty(argsEx[4]?.ToString()))
                {
                    return argsEx[4].ToString();
                }
            }
            catch { }

            // Try 2: GetUserTmpStr
            if (elementId > 0)
            {
                try
                {
                    object[] argsNum = new object[] { machineNum, elementId, fingerIndex, string.Empty, 0 };
                    ParameterModifier pmNum = new ParameterModifier(5);
                    pmNum[3] = true;
                    pmNum[4] = true;
                    bool okNum = (bool)zkType.InvokeMember("GetUserTmpStr",
                        BindingFlags.InvokeMethod, null, zkInstance, argsNum, new ParameterModifier[] { pmNum }, null, null);
                    if (okNum && !string.IsNullOrEmpty(argsNum[3]?.ToString()))
                    {
                        return argsNum[3].ToString();
                    }
                }
                catch { }
            }

            // Try 3: GetUserTmpStr64
            if (elementId > 0)
            {
                try
                {
                    object[] args64 = new object[] { machineNum, elementId, fingerIndex, string.Empty, 0 };
                    ParameterModifier pm64 = new ParameterModifier(5);
                    pm64[3] = true;
                    pm64[4] = true;
                    bool ok64 = (bool)zkType.InvokeMember("GetUserTmpStr64",
                        BindingFlags.InvokeMethod, null, zkInstance, args64, new ParameterModifier[] { pm64 }, null, null);
                    if (ok64 && !string.IsNullOrEmpty(args64[3]?.ToString()))
                    {
                        return args64[3].ToString();
                    }
                }
                catch { }
            }

            return null;
        }

        public async Task<bool> ClearLogsAsync()
        {
            if (!isConnected || zkInstance == null) return false;

            return await Task.Run(() =>
            {
                lock (lockObj)
                {
                    try
                    {
                        int machineNum = currentDevice?.MachineNumber ?? 1;
                        return (bool)zkType.InvokeMember("ClearGLog",
                            BindingFlags.InvokeMethod, null, zkInstance, new object[] { machineNum });
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred?.Invoke(this, $"خطأ أثناء مسح سجلات الجهاز: {ex.Message}");
                        return false;
                    }
                }
            });
        }

        private DateTime? GetDeviceTimeInternal()
        {
            try
            {
                int machineNum = currentDevice?.MachineNumber ?? 1;
                object[] args = new object[] { machineNum, 0, 0, 0, 0, 0, 0 };
                ParameterModifier pm = new ParameterModifier(7);
                for (int i = 1; i < 7; i++) pm[i] = true;

                bool result = (bool)zkType.InvokeMember("GetDeviceTime",
                    BindingFlags.InvokeMethod, null, zkInstance, args, new ParameterModifier[] { pm }, null, null);

                if (result)
                {
                    return new DateTime(
                        Convert.ToInt32(args[1]),
                        Convert.ToInt32(args[2]),
                        Convert.ToInt32(args[3]),
                        Convert.ToInt32(args[4]),
                        Convert.ToInt32(args[5]),
                        Convert.ToInt32(args[6]));
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<DateTime?> GetDeviceTimeAsync()
        {
            if (!isConnected || zkInstance == null) return null;

            return await Task.Run<DateTime?>(() =>
            {
                lock (lockObj)
                {
                    return GetDeviceTimeInternal();
                }
            });
        }

        private bool SyncDeviceTimeInternal(DateTime? customTime = null)
        {
            try
            {
                int machineNum = currentDevice?.MachineNumber ?? 1;
                bool ok = false;
                if (customTime.HasValue)
                {
                    var dt = customTime.Value;
                    ok = (bool)zkType.InvokeMember("SetDeviceTime2",
                        BindingFlags.InvokeMethod, null, zkInstance,
                        new object[] { machineNum, dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second });
                }
                else
                {
                    try
                    {
                        ok = (bool)zkType.InvokeMember("SetDeviceTime",
                            BindingFlags.InvokeMethod, null, zkInstance, new object[] { machineNum });
                    }
                    catch { }

                    if (!ok)
                    {
                        var now = DateTime.Now;
                        ok = (bool)zkType.InvokeMember("SetDeviceTime2",
                            BindingFlags.InvokeMethod, null, zkInstance,
                            new object[] { machineNum, now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second });
                    }
                }

                if (ok)
                {
                    try
                    {
                        zkType.InvokeMember("RefreshData",
                            BindingFlags.InvokeMethod, null, zkInstance, new object[] { machineNum });
                    }
                    catch { }
                    OnStatusChanged?.Invoke(this, $"تمت مزامنة وقت جهاز البصمة مع وقت الحاسوب بنجاح ({DateTime.Now:yyyy-MM-dd HH:mm:ss})");
                }
                return ok;
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke(this, $"خطأ أثناء مزامنة وقت الجهاز: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SyncDeviceTimeAsync(DateTime? customTime = null)
        {
            if (!isConnected || zkInstance == null) return false;

            return await Task.Run(() =>
            {
                lock (lockObj)
                {
                    return SyncDeviceTimeInternal(customTime);
                }
            });
        }

        public async Task<bool> StartEnrollAsync(int elementId, int fingerIndex)
        {
            if (!isConnected || zkInstance == null) return false;

            return await Task.Run(() =>
            {
                lock (lockObj)
                {
                    try
                    {
                        string enrollNum = elementId.ToString();
                        try
                        {
                            bool res = (bool)zkType.InvokeMember("StartEnrollEx",
                                BindingFlags.InvokeMethod, null, zkInstance, new object[] { enrollNum, fingerIndex, 1 });
                            if (res)
                            {
                                OnStatusChanged?.Invoke(this, $"بدأ تسجيل بصمة العنصر {elementId} (الإصبع #{fingerIndex}) على شاشة الجهاز...");
                                return true;
                            }
                        }
                        catch { }

                        // Fallback to StartEnroll
                        bool resFallback = (bool)zkType.InvokeMember("StartEnroll",
                            BindingFlags.InvokeMethod, null, zkInstance, new object[] { enrollNum, fingerIndex });
                        if (resFallback)
                        {
                            OnStatusChanged?.Invoke(this, $"بدأ تسجيل بصمة العنصر {elementId} (الإصبع #{fingerIndex}) على شاشة الجهاز...");
                        }
                        return resFallback;
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred?.Invoke(this, $"خطأ أثناء بدء تسجيل البصمة على الجهاز: {ex.Message}");
                        return false;
                    }
                }
            });
        }

        public async Task<bool> DeleteFingerprintAsync(int elementId, int fingerIndex)
        {
            if (!isConnected || zkInstance == null) return false;

            return await Task.Run(() =>
            {
                lock (lockObj)
                {
                    try
                    {
                        int machineNum = currentDevice?.MachineNumber ?? 1;
                        string enrollNum = elementId.ToString();

                        bool result = (bool)zkType.InvokeMember("SSR_DeleteEnrollData",
                            BindingFlags.InvokeMethod, null, zkInstance, new object[] { machineNum, enrollNum, fingerIndex });

                        if (result)
                        {
                            zkType.InvokeMember("RefreshData",
                                BindingFlags.InvokeMethod, null, zkInstance, new object[] { machineNum });
                        }
                        return result;
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred?.Invoke(this, $"خطأ أثناء حذف بصمة العنصر من الجهاز: {ex.Message}");
                        return false;
                    }
                }
            });
        }

        public async Task<bool> DeleteUserAsync(int elementId)
        {
            if (!isConnected || zkInstance == null) return false;

            return await Task.Run(() =>
            {
                lock (lockObj)
                {
                    try
                    {
                        int machineNum = currentDevice?.MachineNumber ?? 1;
                        string enrollNum = elementId.ToString();

                        // 12 deletes user and all biometric data
                        bool result = (bool)zkType.InvokeMember("SSR_DeleteEnrollData",
                            BindingFlags.InvokeMethod, null, zkInstance, new object[] { machineNum, enrollNum, 12 });

                        if (result)
                        {
                            zkType.InvokeMember("RefreshData",
                                BindingFlags.InvokeMethod, null, zkInstance, new object[] { machineNum });
                        }
                        return result;
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred?.Invoke(this, $"خطأ أثناء حذف العنصر من الجهاز: {ex.Message}");
                        return false;
                    }
                }
            });
        }

        public async Task<Dictionary<int, string>> ReadAllUserTemplatesAsync(int elementId, string alternateEnrollNum = null)
        {
            var dict = new Dictionary<int, string>();
            if (!isConnected || zkInstance == null) return dict;

            return await Task.Run(() =>
            {
                lock (lockObj)
                {
                    try
                    {
                        int machineNum = currentDevice?.MachineNumber ?? 1;
                        string enrollNum = elementId.ToString();

                        // Load users and templates into internal buffer
                        try
                        {
                            zkType.InvokeMember("ReadAllUserID",
                                BindingFlags.InvokeMethod, null, zkInstance, new object[] { machineNum });
                        }
                        catch { }

                        try
                        {
                            zkType.InvokeMember("ReadAllTemplate",
                                BindingFlags.InvokeMethod, null, zkInstance, new object[] { machineNum });
                        }
                        catch { }

                        for (int fingerIndex = 0; fingerIndex <= 9; fingerIndex++)
                        {
                            string tpl = TryGetTemplateInternal(machineNum, enrollNum, elementId, fingerIndex);
                            if (!string.IsNullOrEmpty(tpl))
                            {
                                dict[fingerIndex] = tpl;
                            }
                        }

                        // Fallback to alternateEnrollNum if no templates found
                        if (dict.Count == 0 && !string.IsNullOrEmpty(alternateEnrollNum) && alternateEnrollNum != enrollNum)
                        {
                            int altId = int.TryParse(alternateEnrollNum, out int pAlt) ? pAlt : 0;
                            for (int fingerIndex = 0; fingerIndex <= 9; fingerIndex++)
                            {
                                string tpl = TryGetTemplateInternal(machineNum, alternateEnrollNum, altId, fingerIndex);
                                if (!string.IsNullOrEmpty(tpl))
                                {
                                    dict[fingerIndex] = tpl;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred?.Invoke(this, $"خطأ أثناء قراءة قوالب البصمات: {ex.Message}");
                    }
                }
                return dict;
            });
        }

        public async Task<List<(string EnrollNum, string Name, int FingerCount)>> GetAllDeviceUsersWithFingerprintsAsync()
        {
            var list = new List<(string EnrollNum, string Name, int FingerCount)>();
            if (!isConnected || zkInstance == null) return list;

            return await Task.Run(() =>
            {
                lock (lockObj)
                {
                    try
                    {
                        int machineNum = currentDevice?.MachineNumber ?? 1;

                        try
                        {
                            zkType.InvokeMember("ReadAllUserID",
                                BindingFlags.InvokeMethod, null, zkInstance, new object[] { machineNum });
                        }
                        catch { }

                        try
                        {
                            zkType.InvokeMember("ReadAllTemplate",
                                BindingFlags.InvokeMethod, null, zkInstance, new object[] { machineNum });
                        }
                        catch { }

                        while (true)
                        {
                            object[] userArgs = new object[] { machineNum, string.Empty, string.Empty, string.Empty, 0, false };
                            ParameterModifier pm = new ParameterModifier(6);
                            pm[1] = true; // enrollNum
                            pm[2] = true; // name
                            pm[3] = true; // password
                            pm[4] = true; // privilege
                            pm[5] = true; // enabled

                            bool hasUser = (bool)zkType.InvokeMember("SSR_GetAllUserInfo",
                                BindingFlags.InvokeMethod, null, zkInstance, userArgs, new ParameterModifier[] { pm }, null, null);

                            if (!hasUser) break;

                            string enrollNum = userArgs[1]?.ToString()?.Trim() ?? "";
                            string name = userArgs[2]?.ToString()?.Trim() ?? "";

                            if (string.IsNullOrEmpty(enrollNum)) continue;

                            int fingerCount = 0;
                            int elId = int.TryParse(enrollNum, out int id) ? id : 0;
                            for (int f = 0; f <= 9; f++)
                            {
                                string tpl = TryGetTemplateInternal(machineNum, enrollNum, elId, f);
                                if (!string.IsNullOrEmpty(tpl))
                                {
                                    fingerCount++;
                                }
                            }

                            list.Add((enrollNum, name, fingerCount));
                        }
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred?.Invoke(this, $"خطأ أثناء فحص مستخدمي الجهاز: {ex.Message}");
                    }
                }
                return list;
            });
        }

        public async Task<List<(int ElementId, string Name)>> GetAllDeviceUsersAsync()
        {
            var list = new List<(int ElementId, string Name)>();
            if (!isConnected || zkInstance == null) return list;

            return await Task.Run(() =>
            {
                lock (lockObj)
                {
                    try
                    {
                        int machineNum = currentDevice?.MachineNumber ?? 1;

                        zkType.InvokeMember("ReadAllUserID",
                            BindingFlags.InvokeMethod, null, zkInstance, new object[] { machineNum });

                        while (true)
                        {
                            object[] userArgs = new object[] { machineNum, string.Empty, string.Empty, string.Empty, 0, false };
                            ParameterModifier pm = new ParameterModifier(6);
                            pm[1] = true; // enrollNum
                            pm[2] = true; // name
                            pm[3] = true; // password
                            pm[4] = true; // privilege
                            pm[5] = true; // enabled

                            bool hasUser = (bool)zkType.InvokeMember("SSR_GetAllUserInfo",
                                BindingFlags.InvokeMethod, null, zkInstance, userArgs, new ParameterModifier[] { pm }, null, null);

                            if (!hasUser) break;

                            string enrollNum = userArgs[1]?.ToString()?.Trim();
                            string name = userArgs[2]?.ToString()?.Trim();

                            if (int.TryParse(enrollNum, out int elId))
                            {
                                list.Add((elId, name));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred?.Invoke(this, $"خطأ أثناء قراءة مستخدمي الجهاز: {ex.Message}");
                    }
                }
                return list;
            });
        }

        private static string GetConnectionString()
        {
            if (!string.IsNullOrEmpty(Follow_Extremist.Data.SqlServer.SqlCon.SqlConnection))
                return Follow_Extremist.Data.SqlServer.SqlCon.SqlConnection;

            if (!string.IsNullOrEmpty(Properties.Settings.Default.SqServerConString))
                return Properties.Settings.Default.SqServerConString;

            return @"Server=DESKTOP-2B87UHT\MSSQLSERVER2019;Database=FollowExtremistDatabase;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        public async Task<int> GetNextAvailableEnrollIdAsync()
        {
            return await Task.Run(async () =>
            {
                int maxFromDevice = 0;
                if (isConnected && zkInstance != null)
                {
                    try
                    {
                        var deviceUsers = await GetAllDeviceUsersAsync();
                        if (deviceUsers != null && deviceUsers.Count > 0)
                        {
                            maxFromDevice = deviceUsers.Max(u => u.ElementId);
                        }
                    }
                    catch { }
                }

                int maxFromDb = 0;
                try
                {
                    using var conn = new Microsoft.Data.SqlClient.SqlConnection(GetConnectionString());
                    await conn.OpenAsync();
                    using var cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT ISNULL(MAX(DeviceEnrollId), 0) FROM ElementInfo";
                    var val = await cmd.ExecuteScalarAsync();
                    if (val != null && val != DBNull.Value)
                    {
                        maxFromDb = Convert.ToInt32(val);
                    }
                }
                catch { }

                int nextId = Math.Max(maxFromDevice, maxFromDb) + 1;
                return nextId;
            });
        }

        public async Task<string> GetDeviceSerialNumberAsync()
        {
            if (!isConnected || zkInstance == null) return currentDevice?.SerialNumber ?? string.Empty;
            return await Task.Run(() =>
            {
                lock (lockObj)
                {
                    int machineNum = currentDevice?.MachineNumber ?? 1;
                    return GetDeviceSerialNumberInternal(machineNum);
                }
            });
        }

        private string GetDeviceSerialNumberInternal(int machineNum)
        {
            try
            {
                object[] snArgs = new object[] { machineNum, string.Empty };
                ParameterModifier pm = new ParameterModifier(2);
                pm[1] = true;
                bool ok = (bool)zkType.InvokeMember("GetDeviceStrInfo", BindingFlags.InvokeMethod, null, zkInstance, snArgs, new ParameterModifier[] { pm }, null, null);
                if (ok && !string.IsNullOrWhiteSpace(snArgs[1]?.ToString()))
                {
                    return snArgs[1].ToString().Trim();
                }
            }
            catch { }

            try
            {
                object[] snArgs2 = new object[] { machineNum, string.Empty };
                ParameterModifier pm2 = new ParameterModifier(2);
                pm2[1] = true;
                bool ok2 = (bool)zkType.InvokeMember("GetSerialNumber", BindingFlags.InvokeMethod, null, zkInstance, snArgs2, new ParameterModifier[] { pm2 }, null, null);
                if (ok2 && !string.IsNullOrWhiteSpace(snArgs2[1]?.ToString()))
                {
                    return snArgs2[1].ToString().Trim();
                }
            }
            catch { }

            return string.Empty;
        }

        private async Task UpdateDeviceSerialNumberInDbAsync(int deviceId, string serialNumber)
        {
            if (deviceId <= 0 || string.IsNullOrEmpty(serialNumber)) return;
            try
            {
                using var conn = new Microsoft.Data.SqlClient.SqlConnection(GetConnectionString());
                await conn.OpenAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE FingerprintDevice SET SerialNumber = @sn WHERE Id = @devId AND (SerialNumber IS NULL OR SerialNumber <> @sn)";
                cmd.Parameters.AddWithValue("@sn", serialNumber);
                cmd.Parameters.AddWithValue("@devId", deviceId);
                await cmd.ExecuteNonQueryAsync();
            }
            catch { }
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                Disconnect();
                isDisposed = true;
            }
        }
    }
}

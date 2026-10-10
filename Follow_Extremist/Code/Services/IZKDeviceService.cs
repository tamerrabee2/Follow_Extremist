using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Follow_Extremist.Core;

namespace Follow_Extremist.Code.Services
{
    public class AttendanceEventArgs : EventArgs
    {
        public int ElementId { get; set; }
        public int? DeviceEnrollId { get; set; }
        public int? DeviceId { get; set; }
        public string DeviceSerialNumber { get; set; }
        public DateTime AttendanceTime { get; set; }
        public int VerifyMethod { get; set; }
    }

    public interface IZKDeviceService
    {
        bool IsConnected { get; }
        FingerprintDevice CurrentDevice { get; }

        event EventHandler<AttendanceEventArgs> OnAttendanceReceived;
        event EventHandler<string> OnStatusChanged;
        event EventHandler<string> OnErrorOccurred;

        Task<bool> ConnectAsync(FingerprintDevice device);
        void Disconnect();
        Task<List<AttendanceEventArgs>> ReadNewLogsAsync();
        Task<bool> UploadUserAsync(int elementId, string elementName);
        Task<bool> UploadFingerprintTemplateAsync(int elementId, int fingerIndex, string templateBase64);
        Task<bool> ClearLogsAsync();
        Task<DateTime?> GetDeviceTimeAsync();
        Task<bool> SyncDeviceTimeAsync(DateTime? customTime = null);
        Task<bool> StartEnrollAsync(int elementId, int fingerIndex);
        Task<bool> DeleteFingerprintAsync(int elementId, int fingerIndex);
        Task<bool> DeleteUserAsync(int elementId);
        Task<string> ReadFingerprintTemplateAsync(int elementId, int fingerIndex, string alternateEnrollNum = null);
        Task<Dictionary<int, string>> ReadAllUserTemplatesAsync(int elementId, string alternateEnrollNum = null);
        Task<List<(int ElementId, string Name)>> GetAllDeviceUsersAsync();
        Task<List<(string EnrollNum, string Name, int FingerCount)>> GetAllDeviceUsersWithFingerprintsAsync();
        Task<int> GetNextAvailableEnrollIdAsync();
        Task<string> GetDeviceSerialNumberAsync();
    }
}

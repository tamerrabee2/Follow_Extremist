using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Follow_Extremist.Code
{
    public static class AppIconHelper
    {
        private static Icon _appIcon;

        public static Icon AppIcon
        {
            get
            {
                if (_appIcon != null) return _appIcon;

                string[] possibleFiles = new[]
                {
                    "app_icon.ico",
                    "شعار_قطاع_الأمن_الوطني_(مصر).ico"
                };

                foreach (var fileName in possibleFiles)
                {
                    try
                    {
                        string path = Path.Combine(Application.StartupPath, fileName);
                        if (File.Exists(path))
                        {
                            _appIcon = new Icon(path);
                            return _appIcon;
                        }

                        path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                        if (File.Exists(path))
                        {
                            _appIcon = new Icon(path);
                            return _appIcon;
                        }
                    }
                    catch { }
                }

                try
                {
                    if (!string.IsNullOrEmpty(Application.ExecutablePath) && File.Exists(Application.ExecutablePath))
                    {
                        var icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                        if (icon != null)
                        {
                            _appIcon = icon;
                            return _appIcon;
                        }
                    }
                }
                catch { }

                return null;
            }
        }

        public static void ApplyFormIcon(Form form)
        {
            if (form == null) return;
            try
            {
                var icon = AppIcon;
                if (icon != null)
                {
                    form.Icon = icon;
                    form.ShowIcon = true;
                }
            }
            catch { }
        }
    }
}

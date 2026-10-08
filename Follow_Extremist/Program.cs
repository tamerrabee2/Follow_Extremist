using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Follow_Extremist.Code;
using Follow_Extremist.Data.SqlServer;


namespace Follow_Extremist
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DependencyInjection.AddDependencyValues();
            string conString = Properties.Settings.Default.SqServerConString;
            if (!string.IsNullOrEmpty(conString) && !conString.Contains("TrustServerCertificate", StringComparison.OrdinalIgnoreCase))
            {
                conString = conString.TrimEnd(';') + ";TrustServerCertificate=True;";
                Properties.Settings.Default.SqServerConString = conString;
                Properties.Settings.Default.Save();
            }
            if (string.IsNullOrEmpty(Properties.Settings.Default.Server))
            {
                Properties.Settings.Default.Server = @".";
                Properties.Settings.Default.Database = "FollowExtremistDatabase";
                Properties.Settings.Default.Save();
            }
            SqlCon.SqlConnection = conString;
            Application.Run(new StartForm1());
        }
    }
}

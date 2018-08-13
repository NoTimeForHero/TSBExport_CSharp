using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Security.Permissions;
using System.Windows.Forms;

namespace TSBExport_CSharp
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ConfigSettings settings = new ConfigSettings();
            if (settings.GridSettings == null) DefaultData.InitTableSettings(settings);

            FormMain frmMain = new FormMain(settings);
            frmMain.AddTableControls += DefaultData.AddTableControls;
            frmMain.AddStyles += DefaultData.AddStyles;

            var path = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
            Console.WriteLine("CONFIGURATION: " + path);

            Application.Run(frmMain);
        }
    }
}

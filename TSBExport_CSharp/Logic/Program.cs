using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;
using TSBExport_CSharp.Grid;
using TSBExport_CSharp.Logic.Export;
using TSBExport_CSharp.Other;

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
            ConfigSettings settings = new ConfigSettings();
            if (settings.GridSettings == null) DefaultData.InitTableSettings(settings);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FormMain frmMain = new FormMain(settings);
            frmMain.AddTableControls += DefaultData.AddTableControls;
            frmMain.AddStyles += DefaultData.AddStyles;
            frmMain.ExportExcel += DefaultData.ExportExcel;

            var path = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
            Console.WriteLine("CONFIGURATION: " + path);

            Application.Run(frmMain);
        }

        static void StartExcel(ConfigSettings settings)
        {
            var data = settings.GridSettings?.getActualData();
            var styles = new List<GridCellsAppearance>();
            DefaultData.AddStyles(styles);

            var rainbow = styles.FirstOrDefault(x => x.name.Equals("RAINBOW"));
            ToExcel test = new ToExcel(data);
            test.AsyncFill().Wait();
            test.Visible = true;
        }
    }
}

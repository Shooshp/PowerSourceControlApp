using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using Serilog;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using Serilog.Sinks.MSSqlServer;
using System.DirectoryServices.AccountManagement;

namespace PowerSourceControlApp
{
    class Program
    {

        [STAThread]
        static void Main()
        {
            var connectionString = new SqlConnectionStringBuilder
            {
                DataSource = "S14",
                UserID = "PowerSource",
                Password = "123",
                InitialCatalog = "Barinov"
            };

            const string tablename = "Logs";
            var columnOptions = new ColumnOptions
            {
                AdditionalDataColumns = new Collection<DataColumn>
                {
                    new DataColumn{DataType = typeof(string), ColumnName = "User"},
                    new DataColumn{DataType = typeof(string), ColumnName = "Host"}
                }
            };

            Log.Logger = new LoggerConfiguration().WriteTo
                .MSSqlServer(connectionString.ToString(), tablename, columnOptions: columnOptions)
                .MinimumLevel.Debug()
                .CreateLogger();



            Global.UserName = UserPrincipal.Current.DisplayName;
            Global.DomainName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            Global.User = Global.DomainName + " (" + Global.UserName + ")";

            Log.Information("Application is started on {User} at {TimeStamp}", Global.User, DateTime.Now);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
            Application.Run(new Form1());
        }
    }
}

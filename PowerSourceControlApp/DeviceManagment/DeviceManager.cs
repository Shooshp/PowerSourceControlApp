using Dapper;
using PowerSourceControlApp.DapperDTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using Serilog;

namespace PowerSourceControlApp.DeviceManagment
{
    public static class DeviceManager
    {
        public static List<PowerSource.PowerSource> DeviceList;
        public static List<Logs> LogList;
        public static bool IsBusy;
        public static string SelectedPowerSourceIp;
        public static string SelectedChanelUUID;

        private static Thread _hashThread;
        private static int _hash;
        private static int _loghash;

        public static event EventHandler LogUpdate;
        public static event EventHandler DeviceListUpdate;
        public static event EventHandler DeviceUpdate;
        public delegate void EventHandler();

        public static void Init()
        {
            DeviceList = new List<PowerSource.PowerSource>();
            LogList = new List<Logs>();
            IsBusy = false;
            SelectedPowerSourceIp = "";
            _hash = 0;
            _loghash = 0;
        }

        public static void StartHashTread()
        {
            _hashThread = new Thread(CheckHash)
            {
                IsBackground = true,
                Priority = ThreadPriority.Normal,
                Name = "Device Manager Thread"
            };
            _hashThread.Start();
        }

        public static void NewDeviceDetectorHanler(string address)
        {
            if (!IsBusy) //  Check if object is Busy
            {
                IsBusy = true; //  Mark object as Busy
                var powerSourceListIsEmpty = !DeviceList.Any();

                if (!powerSourceListIsEmpty) //  If there are devices on the list
                {
                    if (DeviceList.Any(p => p.IpAddress == address)) //  If device is already on list
                    {
                        if (DeviceList.Single(p => p.IpAddress == address).IsOnline == false) // If device is offline by any reason
                        {
                            DeviceList.Single(p => p.IpAddress == address).IsOnline = true;
                            DeviceList.Single(p => p.IpAddress == address).Pinger.Start();

                            Log.Information("{User} reconected with {Host} at {TimeStamp}", Global.User, DeviceList.Single(p => p.IpAddress == address).DisplayName, DateTime.Now);
                        }
                    }
                    else // If device is not on the list than add device to list
                    {
                        DeviceList.Add(new PowerSource.PowerSource(address));
                        Log.Information("{User} detected new {Host} at {TimeStamp}", Global.User, DeviceList.Single(p => p.IpAddress == address).DisplayName, DateTime.Now);
                    }
                }
                else // No devices on the list so we will add new one
                {
                    DeviceList.Add(new PowerSource.PowerSource(address));
                    Log.Information("{User} detected new {Host} at {TimeStamp}", Global.User, DeviceList.Single(p => p.IpAddress == address).DisplayName, DateTime.Now);
                }
                DeviceUpdate?.Invoke();
                IsBusy = false; //  Remove Busy Flag
            }
        }

        private static void CheckHash()
        {
            while (true)
            {
                Thread.Sleep(100);

                var currentHash = 0;
                int logCount;

                var connectionString = new SqlConnectionStringBuilder
                {
                    DataSource = "S14",
                    UserID = "PowerSource",
                    Password = "123",
                    InitialCatalog = "Barinov"
                };

                using (var connection = new SqlConnection(connectionString.ToString()))
                {
                    const string queryString = "SELECT COUNT(*) FROM Logs";
                    var command = new SqlCommand(queryString, connection);
                    connection.Open();
                    var reader = command.ExecuteScalar();

                    logCount = Convert.ToInt32(reader);

                    connection.Close();
                }

                if (DeviceList.Count != 0)
                {
                    currentHash += DeviceList.GetHashCode();
                }

                if (SelectedPowerSourceIp != null)
                {
                    currentHash += SelectedPowerSourceIp.GetHashCode();
                }

                if (SelectedChanelUUID != null)
                {
                    currentHash += SelectedChanelUUID.GetHashCode();
                }

                if (_hash != currentHash)
                {
                    _hash = currentHash;
                    DeviceListUpdate?.Invoke();
                }

                if (_loghash != logCount)
                {
                    _loghash = logCount;
                    using (var connection = new SqlConnection(connectionString.ToString()))
                    {
                        SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLServer);
                        connection.Open();
                        LogList = connection.GetList<Logs>().ToList();
                        connection.Close();
                    }

                    LogUpdate?.Invoke();
                }
            }
        }

        public static void DeviceRefresh(string ipaddress)
        {
            if (SelectedPowerSourceIp == null && ipaddress != null)
            {
                DeviceListUpdate?.Invoke();
            }
            if (SelectedPowerSourceIp == ipaddress)
            {
                if (DeviceList.Single(source => source.IpAddress == ipaddress).IsOnline == false)
                {
                    DeviceListUpdate?.Invoke();
                }
                DeviceUpdate?.Invoke();
            }
        }
    }
}

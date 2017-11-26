using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using Dapper;
using MySql.Data.MySqlClient;
using PowerSourceControlApp.DapperDTO;

namespace PowerSourceControlApp.PowerSource.Tasks
{
    public class Task
    {
        public int TaskId;
        public uint Progress;
        public uint TaskNumber;
        public decimal Argument;
        public string TaskName;
        private readonly PowerSource _parentPowerSource;
        public Chanel TargetChanel;
        public bool IsActive;
        public bool IsComplited;
        public bool MarkForDelite;
        public string DisplayName { get; }

        public Task(Chanel chanel, string name, decimal argument, uint assignedTaskNumber)
        {
            TaskId = 0;
            Progress = 0;
            Argument = argument;
            TaskName = name;
            TargetChanel = chanel;
            _parentPowerSource = TargetChanel.ParentPowerSource;
            IsActive = false;
            IsComplited = false;
            MarkForDelite = false;
            TaskNumber = assignedTaskNumber;

            if (Argument != 0)
            {
                DisplayName = string.Concat(TaskName, ": ", Argument.ToString(CultureInfo.InvariantCulture), " to Chanel:", TargetChanel.ChanelId.ToString());  
            }
        }

        public void Run()
        {
            IsActive = true;
            CommitToDeviceDb();
            if (_parentPowerSource.Collection.SelectedPowerSourceIp == _parentPowerSource.IpAddress)
            {
                _parentPowerSource.Collection.IsUpdated = true;
            }
            SendMessage();           
            while (_parentPowerSource.IsOnline && Progress != 100)
            {
                ChekProgress();
            }
            TargetChanel.SyncSettings(); //Re Update Settings in chanel from DB
            
            IsActive = false;
            IsComplited = true;
        }

        private void SendMessage()
        {
            _parentPowerSource.Message = string.Concat("Task:", TaskId.ToString());
        }

        private void CommitToDeviceDb()
        {
            try
            {
                using (var connection = new MySqlConnection(_parentPowerSource.MsqlConnectionString.ToString()))
                {
                    SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
                    connection.Open();
                    var id = connection.Insert(new CurrentTasks
                    {
                        UUID = TargetChanel.ChanelUUID,
                        TaskName = TaskName,
                        TaskArgument = Argument,
                        IsComplited = false
                    });

                    if (id != null)
                    {
                        TaskId = (int) id;
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
                //TODO: Need to implement some kind of SQL disconnect handler
            }
        }

        private void ChekProgress()
        {
            bool state = false;

            try
            {
                using (var connection = new MySqlConnection(_parentPowerSource.MsqlConnectionString.ToString()))
                {
                    SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
                    connection.Open();
                    state = connection.Get<CurrentTasks>(TaskId).IsComplited;
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
                //TODO: Need to implement some kind of SQL disconnect handler
            }
            if (!state)
            {
                if (_parentPowerSource.Status.Contains("task") && _parentPowerSource.Status.Contains("progress"))
                {
                    var message = _parentPowerSource.Status.Split(':');
                    if (Convert.ToUInt32(message[1]) == TaskId)
                    {
                        Progress = Convert.ToUInt32(message[3]);
                    }
                }
            }
            else
            {
                Progress = 100;
            }
        }
    }
}

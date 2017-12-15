using System;
using System.Globalization;
using System.Threading;
using Dapper;
using MySql.Data.MySqlClient;
using PowerSourceControlApp.DapperDTO;
using PowerSourceControlApp.DeviceManagment;

namespace PowerSourceControlApp.PowerSource.Tasks
{
    public class Task : IDisposable
    {
        public int TaskId;
        public uint Progress;
        public uint TaskNumber;
        public decimal Argument;
        public string TaskName;
        private readonly PowerSource _parentPowerSource;
        private readonly TaskManager _parentTaskManager;
        public Chanel TargetChanel;
        public bool IsExecuting;
        public bool IsComplited;
        public string DisplayName { get; }


        public Task(Chanel chanel, string name, decimal argument, uint assignedTaskNumber)
        {
            TaskId = 0;
            Progress = 0;
            Argument = argument;
            TaskName = name;
            TargetChanel = chanel;
            _parentPowerSource = TargetChanel.ParentPowerSource;
            _parentTaskManager = _parentPowerSource.DutyManager;
            IsExecuting = false;
            IsComplited = false;
            TaskNumber = assignedTaskNumber;

            if (Argument != 0)
            {
                DisplayName = string.Concat(TaskName, ": ", Argument.ToString(CultureInfo.InvariantCulture), " to Chanel:", TargetChanel.ChanelId.ToString());  
            }
            else
            {
                DisplayName = string.Concat(TaskName, " Chanel:", TargetChanel.ChanelId.ToString());
            }
        }

        public void Run(int timetoexecute)
        {
            var wd = new Watchdog(timetoexecute);
            wd.TimeToDie += Dispose;
            wd.Start();

            IsExecuting = true;
            CommitToDeviceDb();
            SendMessage();           
            while (_parentPowerSource.IsOnline && Progress != 100)
            {
                ChekProgress();
            }
            Thread.Sleep(810); //Wait for powersource to reupdate settings table
            
            IsExecuting = false;
            IsComplited = true;
            wd.Dispose();
            _parentTaskManager.StartNextTask();
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

            Thread.Sleep(100);
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
                //TODO: Need to implement some kind of SQL disconnect handler
            }
            if (!state)
            {
                if (_parentPowerSource.Status.Contains("task") && _parentPowerSource.Status.Contains("progress"))
                {
                    var message = _parentPowerSource.Status.Split(':');
                    var curTask = (int)Char.GetNumericValue(message[1][1]);
                    if (curTask == TaskId)
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            Progress = 100;
        }
    }
}

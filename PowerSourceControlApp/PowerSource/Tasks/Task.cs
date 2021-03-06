﻿using System;
using System.Globalization;
using System.Threading;
using Dapper;
using MySql.Data.MySqlClient;
using PowerSourceControlApp.DapperDTO;
using PowerSourceControlApp.DeviceManagment;
using Serilog;

namespace PowerSourceControlApp.PowerSource.Tasks
{
    public class Task : IDisposable
    {
        public int TaskId;
        public uint Progress;
        public uint TaskNumber;
        private int _ttl;
        public decimal Argument;
        public string TaskName;
        private readonly PowerSource _parentPowerSource;
        private readonly TaskManager _parentTaskManager;
        public Chanel TargetChanel;
        public bool IsExecuting;
        public bool IsComplited;
        public string DisplayName { get; }


        public Task(Chanel chanel, string name, decimal argument, uint assignedTaskNumber, int ttl)
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
            _ttl = ttl;

            if (Argument != 0)
            {
                DisplayName = string.Concat(TaskName, ": ", Argument.ToString(CultureInfo.InvariantCulture), " to Chanel:", TargetChanel.ChanelId.ToString());  
            }
            else
            {
                DisplayName = string.Concat(TaskName, " Chanel:", TargetChanel.ChanelId.ToString());
            }
        }

        public void Run()
        {
            var wd = new Watchdog(_ttl);
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
            Log.Debug("{Host} complited task {LogEvent} created by {User} at {TimeStamp}", _parentPowerSource.DisplayName, DisplayName, Global.User, DateTime.Now);

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
                Log.Error("SQL Error accured by {User} to {Host}, while commiting to DB task: {LogEvent}. With parametrs {Exception} at {TimeStamp}",
                    Global.User, _parentPowerSource.DisplayName, DisplayName, e, DateTime.Now);
                Console.WriteLine(e);
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
                Log.Error("SQL Error accured by {User} to {Host}, cheking progress of task: {LogEvent}. With parametrs {Exception} at {TimeStamp}",
                    Global.User, _parentPowerSource.DisplayName, DisplayName, e, DateTime.Now);
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
            if (disposing)
            {
                Progress = 100;
            }
        }
    }
}

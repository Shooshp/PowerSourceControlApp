using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Serilog;

namespace PowerSourceControlApp.PowerSource.Tasks
{
    public class TaskManager
    {
        public List<Task> TaskList { get; }
        private readonly PowerSource _parentPowerSource;

        private bool RunningTaskExist
        {
            get
            {
                if (!IsEmpty)
                {
                    if (TaskList.Exists(e => e.IsExecuting))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }

        private bool IsEmpty
        {
            get
            {
                if (TaskList.Count == 0)
                {
                    return true;
                }
                return false;
            }
        }

        public TaskManager(PowerSource parent)
        {
            _parentPowerSource = parent;
            TaskList = new List<Task>();
        }

        public void StartNextTask()
        {
            RemoveComplitedTasks();

            if (_parentPowerSource.IsOnline && !RunningTaskExist && _parentPowerSource.Status == "Idle")
            {
                if (!IsEmpty)
                {
                    var nextTask = TaskList.OrderBy(o => o.TaskNumber).First();
                    var taskThread = new Thread(() => nextTask.Run()) {IsBackground = true};
                    Thread.MemoryBarrier();
                    taskThread.Start();
                }
            }
        }

        private void Add(Chanel chanel, string name, decimal argument, int ttl)
        {
            if (!IsEmpty)
            {
                var nextNumber = TaskList.Count + 1;
                if (!TaskList.Exists(x => x.TargetChanel == chanel && x.TaskName == name && x.Argument == argument))
                {
                    if (TaskList.Exists(x => x.TargetChanel == chanel && x.TaskName == name && x.IsExecuting == false && x.IsComplited == false))
                    { // If There is exist task to the same chanel with the same duty 
                      // but with different argument, then just update thet task
                        TaskList.Single(x =>
                            x.TargetChanel == chanel && 
                            x.TaskName == name && 
                            x.IsExecuting == false &&
                            x.IsComplited == false).Argument = argument;
                    }
                    else
                    {
                        TaskList.Add(new Task(chanel, name, argument, Convert.ToUInt32(nextNumber), ttl));
                    }
                }
            }
            else
            {
                TaskList.Add(new Task(chanel, name, argument, 1, ttl));
            }
            StartNextTask();
        }

        private void RemoveComplitedTasks()
        {
            var tasks = TaskList.Count.ToString();

            Log.Debug("{User} remove {LogEvent} tasks to host {Host} at {TimeStamp}",
                Global.User, tasks, _parentPowerSource.DisplayName, DateTime.Now);

            TaskList.RemoveAll(x => x.IsComplited && x.IsExecuting == false);
        }

        public void SetVoltage(Chanel chanel, decimal value)
        {
            Add(chanel, "SetVoltage", value, 30000);

            var taskDiscription = "Set Voltage: " + value + " to Chanel: " + chanel.Address;

            Log.Debug("{User} give task to {Host}, {LogEvent} at {TimeStamp}", 
                Global.User, _parentPowerSource.DisplayName, taskDiscription, DateTime.Now);
        }

        public void SetCurrent(Chanel chanel, decimal value)
        {
            Add(chanel, "SetCurrent", value, 5000);

            var taskDiscription = "Set Current: " + value + " to Chanel: " + chanel.Address;

            Log.Debug("{User} give task to {Host}, {LogEvent} at {TimeStamp}",
                Global.User, _parentPowerSource.DisplayName, taskDiscription, DateTime.Now);
        }

        public void Calibrate(Chanel chanel)
        {
            Add(chanel, "Calibrate", 0, 370000);

            var taskDiscription = "Calibrating Chanel: " + chanel.Address;

            Log.Debug("{User} give task to {Host}, {LogEvent} at {TimeStamp}",
                Global.User, _parentPowerSource.DisplayName, taskDiscription, DateTime.Now);
        }

        public void ShutDown(Chanel chanel)
        {
            Add(chanel, "ShutDown", 0, 5000);

            var taskDiscription = "Shut Down Chanel: " + chanel.Address;

            Log.Debug("{User} give task to {Host}, {LogEvent} at {TimeStamp}",
                Global.User, _parentPowerSource.DisplayName, taskDiscription, DateTime.Now);
        }

        public void TurnOn(Chanel chanel)
        {
            Add(chanel, "TurnOn", 0, 5000);

            var taskDiscription = "Turn On Chanel: " + chanel.Address;

            Log.Debug("{User} give task to {Host}, {LogEvent} at {TimeStamp}",
                Global.User, _parentPowerSource.DisplayName, taskDiscription, DateTime.Now);
        }

        public void ClearAllTasks()
        {
            foreach (var task in TaskList)
            {
                task.Dispose();
            }
        }
    }
}



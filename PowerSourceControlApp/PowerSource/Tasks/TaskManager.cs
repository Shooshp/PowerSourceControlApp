using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PowerSourceControlApp.PowerSource.Tasks;

namespace PowerSourceControlApp.PowerSource
{
    public class TaskManager
    {        
        public bool IsActive;
        public List<Task> TaskList;
        private Thread _taskManagerThread;
        private readonly Random _randomNumberGenerator;
        private PowerSource _parentPowerSource;

        public bool RunningTaskExist
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
            _randomNumberGenerator = new Random();
            _parentPowerSource = parent;
            TaskList = new List<Task>();
            IsActive = true;
        }

        public void StartTaskManagerThread()
        {
            var threadName = string.Concat("TaskManager:", _parentPowerSource.IpAddress);
            _taskManagerThread = new Thread(TaskManagerThread)
            {
                Name = threadName,
            };
            _taskManagerThread.Start();
        }

        public void StopTaskManagerThread()
        {
            IsActive = false;
            GC.Collect();
        }

        public void ReStart()
        {
            TaskList.Clear();
            StartTaskManagerThread();
        }

        private void TaskManagerThread()
        {
            Thread.Sleep(300);
            while (IsActive)
            {               
                //RemoveMarked();
                if (_parentPowerSource.IsOnline)
                {
                    if (!RunningTaskExist && _parentPowerSource.Status == "Idle")
                    {
                        RemoveComplited();
                        if (!IsEmpty)
                        {
                            var nextTask = TaskList.OrderBy(o => o.TaskNumber).First();
                            nextTask.Run();
                        }
                    }
                }              
            }
        }     

        private void Add(Chanel chanel, string name, decimal argument)
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
                    // Add task only if its on exist already
                    TaskList.Add(new Task(chanel, name, argument, Convert.ToUInt32(nextNumber)));
                }
            }
            else
            {
                TaskList.Add(new Task(chanel, name, argument, 1));
            }
        }

        private void RemoveComplited()
        {
            if (!IsEmpty)
            {
                TaskList.RemoveAll(x => x.IsComplited && x.IsExecuting == false);
            }
        }

        public void SetVoltage(Chanel chanel, decimal value)
        {
            Add(chanel, "SetVoltage", value);
        }

        public void SetCurrent(Chanel chanel, decimal value)
        {
            Add(chanel, "SetCurrent", value);
        }

        public void Calibrate(Chanel chanel)
        {
            Add(chanel, "Calibrate", 0);
        }

        public void ShutDown(Chanel chanel)
        {
            Add(chanel, "ShutDown", 0);
        }

        public void TurnOn(Chanel chanel)
        {
            Add(chanel, "TurnOn", 0);
        }
    }
}



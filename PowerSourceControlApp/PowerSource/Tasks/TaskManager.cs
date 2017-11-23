using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PowerSourceControlApp.PowerSource.Tasks;

namespace PowerSourceControlApp.PowerSource
{
    public class TaskManager
    {        
        public bool Halt;
        public List<Task> TaskList;
        private Thread _taskManagerThread;
        private readonly Random _randomNumberGenerator;
        private PowerSource _parentPowerSource;

        public bool RunningTaskExist
        {
            get
            {
                if (TaskList.Exists(e => e.IsActive))
                {
                    return true;
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
            Halt = false;
        }

        public void Start()
        {
            var threadName = string.Concat("TaskManager:", _parentPowerSource.IpAddress);
            _taskManagerThread = new Thread(TaskManagerThread)
            {
                Name = threadName,
                IsBackground = true,
                Priority = ThreadPriority.Highest
            };
            _taskManagerThread.Start();
        }

        public void ReStart()
        {
            TaskList.Clear();
            Start();
        }

        private void TaskManagerThread()
        { 
            while (true)
            {
                Thread.Sleep(_randomNumberGenerator.Next(400, 500));
                if (!Halt)
                {
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
                    else
                    { 
                        Halt = true;
                    }
                }               
            }
        }     

        private void Add(Chanel chanel, string name, decimal argument)
        {
            if (!IsEmpty)
            {
                var nextNumber = TaskList.Count + 1;
                TaskList.Add(new Task(chanel, name, argument, Convert.ToUInt32(nextNumber)));
            }
            else
            {
                TaskList.Add(new Task(chanel, name, argument, 1));
            }
            if (_parentPowerSource.Collection.SelectedPowerSourceIp == _parentPowerSource.IpAddress)
            {
                _parentPowerSource.Collection.IsUpdated = true;
            }
        }

        private void RemoveComplited()
        {
            TaskList.RemoveAll(x => x.IsComplited && x.IsActive == false);
            if (_parentPowerSource.Collection.SelectedPowerSourceIp == _parentPowerSource.IpAddress)
            {
                _parentPowerSource.Collection.IsUpdated = true;
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



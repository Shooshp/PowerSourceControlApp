using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PowerSourceControlApp.PowerSource
{
    public class DutyManager
    {        
        public bool Halt;
        public List<Duty> TaskList;
        private Thread _taskManagerThread;
        private readonly Random _randomNumberGenerator;
        private Device _parentDevice;

        public bool RunningDutyExist
        {
            get
            {
                if (TaskList.Exists(e => e.IsActive))
                {
                    return true;
                }
            else
                {
                    return false;
                }
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
                else
                {
                    return false;
                }
            }
        }

        public DutyManager(Device parent)
        {
            _randomNumberGenerator = new Random();
            _parentDevice = parent;
            TaskList = new List<Duty>();
            Halt = false;

        }

        public void Start()
        {
            var threadName = string.Concat("TaskManager:", _parentDevice.IpAddress);
            _taskManagerThread = new Thread(DutyManagerThread)
            {
                Name = threadName,
                IsBackground = true,
                Priority = ThreadPriority.Normal
            };
            _taskManagerThread.Start();
        }

        public void ReStart()
        {
            TaskList.Clear();
            Start();
        }

        private void DutyManagerThread()
        { 
            while (!Halt)
            {
                Thread.Sleep(_randomNumberGenerator.Next(400, 500));

                if (_parentDevice.IsOnline)
                {
                    if (!RunningDutyExist && _parentDevice.Status == "Idle")
                    {
                        RemoveComplited();
                        if (!IsEmpty)
                        {                           
                            var nextDuty = TaskList.OrderBy(o => o.DutyNumber).First();
                            nextDuty.Run();
                        }
                    }
                }
                else
                {
                    Halt = true;
                }
            }
        }     

        private void Add(Chanel chanel, string name, decimal argument)
        {
            if (!IsEmpty)
            {
                var nextNumber = TaskList.Count + 1;
                TaskList.Add(new Duty(chanel, name, argument, Convert.ToUInt32(nextNumber)));
            }
            else
            {
                TaskList.Add(new Duty(chanel, name, argument, 1));
            }
        }

        private void RemoveComplited()
        {
            TaskList.RemoveAll(x => x.IsComplited);
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



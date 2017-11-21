using System;
using System.Threading;
using Dapper;
using DevExpress.Utils.Extensions;
using PowerSourceControlApp.DapperDTO;

namespace PowerSourceControlApp.PowerSource
{
    public class Duty
    {
        public int DutyId;
        public uint Progress;
        public uint DutyNumber;
        public decimal Argument;
        public string DutyName;
        private Device _parentDevice;
        public Chanel TargetChanel;
        public bool IsActive;
        public bool IsComplited;

        public Duty(Chanel chanel, string name, decimal argument, uint assignedDutyNumber)
        {
            DutyId = 0;
            Progress = 0;
            Argument = argument;
            DutyName = name;
            TargetChanel = chanel;
            _parentDevice = TargetChanel.ParentDevice;
            IsActive = false;
            IsComplited = false;
            DutyNumber = assignedDutyNumber;
        }

        public void Run()
        {
            CommitToDeviceDb();
            SendMessage();
            IsActive = true;
            while (_parentDevice.IsOnline && Progress != 100)
            {
                ChekProgress();
            }
            IsActive = false;
            IsComplited = true;
        }

        private void SendMessage()
        {
            _parentDevice.Message = string.Concat("Task:", DutyId.ToString());
        }

        private void CommitToDeviceDb()
        {
            while (_parentDevice.SqlIsBusy)
            {
                Thread.Sleep(1);
            }
            using (var connection = _parentDevice.GetConnection())
            {
                connection.Open();
                var Id = connection.Insert(new CurrentTasks
                    {
                        UUID = TargetChanel.ChanelUUID,
                        TaskName = DutyName,
                        TaskArgument = Argument,
                        IsComplited = false
                    });


                DutyId = (int) Id;

                connection.Close();
            }
            _parentDevice.SqlIsBusy = false;
        }

        private void ChekProgress()
        {           
            if (_parentDevice.Status.Contains("task") && _parentDevice.Status.Contains("progress"))
            {
                var message = _parentDevice.Status.Split(':');
                if (Convert.ToUInt32(message[1]) == DutyId)
                {
                    Progress = Convert.ToUInt32(message[3]);
                }
            }
        }
    }
}

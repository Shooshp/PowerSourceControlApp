using System.Collections.Generic;
using System.Linq;

namespace PowerSourceControlApp.DeviceManagment
{
    public class DeviceManager
    {
        public List<PowerSource.PowerSource> DetectedPowerSources;
        public bool IsUpdated;
        public bool IsBusy;
        public string SelectedPowerSourceIp;

        public DeviceManager()
        {
            DetectedPowerSources = new List<PowerSource.PowerSource>();
            IsUpdated = false;
            IsBusy = false;
            SelectedPowerSourceIp = "";
        }

        public void NewDeviceDetectorHanler(string address)
        {
            if (!IsBusy) //  Check if object is Busy
            {
                IsBusy = true; //  Mark object as Busy
                var powerSourceListIsEmpty = !DetectedPowerSources.Any();

                if (!powerSourceListIsEmpty) //  If there are devices on the list
                {
                    if (DetectedPowerSources.Any(p => p.IpAddress == address)) //  If device is already on list
                    {
                        if (DetectedPowerSources.Single(p => p.IpAddress == address).IsOnline == false) // If device is offline by any reason
                        {
                            DetectedPowerSources.Single(p => p.IpAddress == address).IsOnline = true;
                            DetectedPowerSources.Single(p => p.IpAddress == address).Pinger.Start();
                            DetectedPowerSources.Single(p => p.IpAddress == address).DutyManager.ReStart();
                            IsUpdated = true;
                        }
                    }
                    else // If device is not on the list than add device to list
                    {
                        DetectedPowerSources.Add(new PowerSource.PowerSource(address, this));
                        IsUpdated = true;
                    }
                }
                else // No devices on the list so we will add new one
                {
                    DetectedPowerSources.Add(new PowerSource.PowerSource(address, this));
                    IsUpdated = true;
                }
                IsBusy = false; //  Remove Busy Flag
            }
        }
    }
}

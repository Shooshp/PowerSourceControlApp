using System.Collections.Generic;
using System.Linq;
using PowerSourceControlApp.PowerSource;

namespace PowerSourceControlApp
{
    public class DeviceManager
    {
        public List<Device> DetectedPowerSources;
        public bool isUpdated;
        public bool isBusy;

        public DeviceManager()
        {
            DetectedPowerSources = new List<Device>();
            isUpdated = false;
            isBusy = false;
        }

        public void NewDeviceDetectorHanler(string address)
        {
            if (!isBusy) //  Check if object is Busy
            {
                isBusy = true; //  Mark object as Busy
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
                            isUpdated = true;
                        }
                    }
                    else // If device is not on the list than add device to list
                    {
                        DetectedPowerSources.Add(new Device(address, this));
                        isUpdated = true;
                    }
                }
                else // No devices on the list so we will add new one
                {
                    DetectedPowerSources.Add(new Device(address, this));
                    isUpdated = true;
                }
                isBusy = false; //  Remove Busy Flag
            }
        }
    }
}

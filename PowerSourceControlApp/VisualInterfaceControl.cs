using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;

namespace PowerSourceControlApp
{
    public class VisualInterfaceControl
    {
        public int FocusedPowerSourceIndex;
        public string FocusedPowerSourceIP;
        public int FocusedChanelIndex;
        public int FocusedChanelID;
        public List<PowerSource> DetectedPowerSources;
        public DevExpress.XtraGrid.GridControl PowerSourceListGrid;
        public DevExpress.XtraGrid.GridControl ChanelListGrid;

        public VisualInterfaceControl()
        {
            

            DetectedPowerSources = new List<PowerSource>();

            FocusedPowerSourceIndex = 0;
            FocusedPowerSourceIP = null;
            FocusedChanelIndex = 0;
            FocusedChanelID = 0;
        }

        public void ConnectToGrids(object powersourcegrid, object chanellistgrid)
        {
            PowerSourceListGrid = (GridControl)powersourcegrid;
            ChanelListGrid = (GridControl)chanellistgrid;
            PowerSourceListGrid.DataSource = DetectedPowerSources;
        }


        public void ScanForPowerSources()
        {
            var powerSourceListIsEmpty = !DetectedPowerSources.Any();
            var scaner = new NetworkScaner("192.168.1.", 10236);
            bool update = false;


            if (!powerSourceListIsEmpty)
            {
                foreach (var powerSource in DetectedPowerSources)
                {
                    var checkIfStillOnline = new Thread(powerSource.Ping);
                    checkIfStillOnline.Start();
                }

                foreach (var pinger in scaner.PingerList)
                {
                    if (!DetectedPowerSources.Any(x => x.Server == pinger.address))
                    {
                        DetectedPowerSources.Add(new PowerSource(pinger.address));
                        update = true;
                    }
                }
            }
            else
            {
                foreach (var pinger in scaner.PingerList)
                {
                    DetectedPowerSources.Add(new PowerSource(pinger.address));
                    update = true;
                }
            }

            powerSourceListIsEmpty = !DetectedPowerSources.Any();

            if (update)
            {
                if (FocusedPowerSourceIP == null)
                {
                    ChanelListGrid.DataSource = DetectedPowerSources.ElementAt(0).ChanelList;
                }
                PowerSourceListGrid.RefreshDataSource();
                ChanelListGrid.RefreshDataSource();
            }

            if (!powerSourceListIsEmpty)
            {
                if (!DetectedPowerSources.ElementAt(FocusedPowerSourceIndex).isOnline)
                {
                    ChanelListGrid.DataSource = null;                    
                }
                else
                {
                    ChanelListGrid.DataSource = DetectedPowerSources.ElementAt(FocusedPowerSourceIndex).ChanelList;
                }
                ChanelListGrid.RefreshDataSource();
            }

            scaner.PingerList.Clear();
            GC.Collect();
        }

        public void CurrentPowerSourceChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var powerSourceListIsEmpty = !DetectedPowerSources.Any();
            var arguments = e;
            FocusedPowerSourceIndex = arguments.FocusedRowHandle;       

            if (!powerSourceListIsEmpty)
            {
                FocusedPowerSourceIP = DetectedPowerSources.ElementAt(FocusedPowerSourceIndex).Server;

                if (DetectedPowerSources.ElementAt(FocusedPowerSourceIndex).isOnline)
                    ChanelListGrid.DataSource = DetectedPowerSources.ElementAt(FocusedPowerSourceIndex).ChanelList;
                else
                    ChanelListGrid.DataSource = null;
            }
            else
            {
                ChanelListGrid.DataSource = null;
            }          
            ChanelListGrid.RefreshDataSource();
        }

        public void CurrentChanelChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var arguments = e;
            FocusedChanelIndex = arguments.FocusedRowHandle;
        }
    }
}

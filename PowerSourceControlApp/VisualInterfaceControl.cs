using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Layout;
using PowerSourceControlApp.PowerSource;

namespace PowerSourceControlApp
{
    public class VisualInterfaceControl
    {
        public bool IsBusy;
        private int _focusedPowerSourceIndex;
        private string _focusedPowerSourceIp;
        private int _focusedChanelIndex;
        private int _focusedChanelId;
        public List<Device> DetectedPowerSources;
        private GridControl _powerSourceListGridControl;
        private GridControl _chanelListGridControl;
        private GridView _powerSourceListGridView;
        private LayoutView _chanelListLayoutView;


        public VisualInterfaceControl(List<Device> deviceList)
        {
            DetectedPowerSources = deviceList;

            _focusedPowerSourceIndex = 0;
            _focusedPowerSourceIp = null;
            _focusedChanelIndex = 0;
            _focusedChanelId = 0;

            IsBusy = false;
        }

        public void ConnectToGrids(object powersourcegrid, object chanellistgrid, object powersourcelistgridview, object chanellistlayoutview)
        {
            _powerSourceListGridControl = (GridControl)powersourcegrid;
            _chanelListGridControl = (GridControl)chanellistgrid;

            _powerSourceListGridView = (GridView) powersourcelistgridview;
            _chanelListLayoutView = (LayoutView) chanellistlayoutview;

            _powerSourceListGridControl.DataSource = DetectedPowerSources;

            _powerSourceListGridView.FocusedRowChanged += CurrentPowerSourceChanged;
            _chanelListLayoutView.FocusedRowChanged += CurrentChanelChanged;
        }

        public void UpdateForms()
        {
            var powerSourceListIsEmpty = !DetectedPowerSources.Any();

            _powerSourceListGridControl.RefreshDataSource();
            if (!powerSourceListIsEmpty)
            {
                if (!DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).IsOnline)
                {
                    _chanelListGridControl.DataSource = null;
                }
                else
                {
                    if (_focusedPowerSourceIp == null)
                    {
                        _chanelListGridControl.DataSource = DetectedPowerSources.ElementAt(0).ChanelList;
                    }
                    else
                    {
                        _chanelListGridControl.DataSource =
                            DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).ChanelList;
                    }
                }     
            }
            
            _chanelListGridControl.RefreshDataSource();
        }
  
        public void CurrentPowerSourceChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var powerSourceListIsEmpty = !DetectedPowerSources.Any();
            var arguments = e;
            _focusedPowerSourceIndex = arguments.FocusedRowHandle;

            if (!IsBusy)  //  Check if object is Busy
            {
                IsBusy = true; //  Mark object as Busy

                if (!powerSourceListIsEmpty) // If there are devices on the list
                {
                    _focusedPowerSourceIp = DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).IpAddress; // Update IP of currently selected device

                    if (DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).IsOnline) // If device is online we use its chanellist to display
                        _chanelListGridControl.DataSource = DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).ChanelList;
                    else // If device is offline we show nothing
                        _chanelListGridControl.DataSource = null;
                }
                else // If there are no devices on the list perhaps event was called on start of application
                {
                    _chanelListGridControl.DataSource = null;
                }
                _chanelListGridControl.RefreshDataSource();

                IsBusy = false; //  Remove Busy Flag
            }
        }

        public void CurrentChanelChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var arguments = e;
            _focusedChanelIndex = arguments.FocusedRowHandle;
        }
    }
}

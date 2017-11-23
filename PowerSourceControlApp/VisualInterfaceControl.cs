using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Layout;
using PowerSourceControlApp.DeviceManagment;

namespace PowerSourceControlApp
{
    public class VisualInterfaceControl
    {
        public bool IsBusy;
        private int _focusedPowerSourceIndex;
        private string _focusedPowerSourceIp;
        private int _focusedChanelIndex;
        private int _focusedChanelId;

        public List<PowerSource.PowerSource> DetectedPowerSources;
        private GridControl _taskListGridControl;
        private GridControl _powerSourceListGridControl;
        private GridControl _chanelListGridControl;
        private ChartControl _voltageGraphChartControl;
        private ChartControl _currentGraphChartControl;
        private GridView _powerSourceListGridView;
        private GridView _taskListGridView;
        private LayoutView _chanelListLayoutView;
        private DeviceManager PowerSourceCollection;


        public VisualInterfaceControl(DeviceManager powersourcecollection)
        {
            PowerSourceCollection = powersourcecollection;
            DetectedPowerSources = PowerSourceCollection.DetectedPowerSources;

            _focusedPowerSourceIndex = 0;
            _focusedPowerSourceIp = null;
            _focusedChanelIndex = 0;
            _focusedChanelId = 0;

            IsBusy = false;
        }

        public void ConnectToGrids(
            object powersourcegrid, 
            object chanellistgrid, 
            object tasklistgrid, 
            object voltagechart,
            object currentchart,
            object powersourcelistgridview, 
            object chanellistlayoutview, 
            object tasklistgridview)
        {
            _powerSourceListGridControl = (GridControl)powersourcegrid;
            _chanelListGridControl = (GridControl)chanellistgrid;
            _taskListGridControl = (GridControl) tasklistgrid;
            _voltageGraphChartControl = (ChartControl) voltagechart;
            _currentGraphChartControl = (ChartControl) currentchart;
            _powerSourceListGridView = (GridView) powersourcelistgridview;
            _chanelListLayoutView = (LayoutView) chanellistlayoutview;
            _taskListGridView = (GridView) tasklistgridview;

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
                    _taskListGridControl.DataSource = null;
                    _currentGraphChartControl.DataSource = null;
                    _voltageGraphChartControl.DataSource = null;
                    PowerSourceCollection.SelectedPowerSourceIp = null;
                }
                else
                {
                    if (_focusedPowerSourceIp == null)
                    {
                        _chanelListGridControl.DataSource = DetectedPowerSources.ElementAt(0).ChanelList;
                        _taskListGridControl.DataSource = DetectedPowerSources.ElementAt(0).DutyManager.TaskList;
                        _voltageGraphChartControl.DataSource = DetectedPowerSources.ElementAt(0).ChanelList.ElementAt(0).ResultsList;
                        _currentGraphChartControl.DataSource = DetectedPowerSources.ElementAt(0).ChanelList.ElementAt(0).ResultsList;
                        PowerSourceCollection.SelectedPowerSourceIp = DetectedPowerSources.ElementAt(0).IpAddress;

                    }
                    else
                    {
                        _chanelListGridControl.DataSource =
                            DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).ChanelList;

                        _taskListGridControl.DataSource =
                            DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).DutyManager.TaskList;

                        _voltageGraphChartControl.DataSource =
                            DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).ChanelList.ElementAt(_focusedChanelIndex).ResultsList;

                        _currentGraphChartControl.DataSource =
                            DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).ChanelList.ElementAt(_focusedChanelIndex).ResultsList;

                        PowerSourceCollection.SelectedPowerSourceIp = DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).IpAddress;
                    }
                }     
            }
            _voltageGraphChartControl.RefreshData();
            _currentGraphChartControl.RefreshData();
            _chanelListGridControl.RefreshDataSource();
            _taskListGridControl.RefreshDataSource();
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

                    if (DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).IsOnline
                    ) // If device is online we use its chanellist to display
                    {
                        _chanelListGridControl.DataSource =
                            DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).ChanelList;

                        _taskListGridControl.DataSource =
                            DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).DutyManager.TaskList;

                        _voltageGraphChartControl.DataSource =
                            DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).
                                ChanelList.ElementAt(0).ResultsList;

                        _currentGraphChartControl.DataSource =
                            DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).
                                ChanelList.ElementAt(0).ResultsList;

                        PowerSourceCollection.SelectedPowerSourceIp = DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).IpAddress;
                    }
                    else // If device is offline we show nothing
                    {
                        _voltageGraphChartControl.DataSource = null;
                        _currentGraphChartControl.DataSource = null;
                        _chanelListGridControl.DataSource = null;
                        _taskListGridControl.DataSource = null;
                        PowerSourceCollection.SelectedPowerSourceIp = null;
                    }      
                }
                else // If there are no devices on the list perhaps event was called on start of application
                {
                    _voltageGraphChartControl.DataSource = null;
                    _currentGraphChartControl.DataSource = null;
                    _chanelListGridControl.DataSource = null;
                    _taskListGridControl.DataSource = null;
                    PowerSourceCollection.SelectedPowerSourceIp = null;
                }
                _chanelListGridControl.RefreshDataSource();
                _taskListGridControl.RefreshDataSource();
                _voltageGraphChartControl.RefreshData();
                _voltageGraphChartControl.RefreshData();
                IsBusy = false; //  Remove Busy Flag
            }
        }

        public void CurrentChanelChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var powerSourceListIsEmpty = !DetectedPowerSources.Any();
            var arguments = e;
            _focusedChanelIndex = arguments.FocusedRowHandle;

            if (!IsBusy)  //  Check if object is Busy
            {
                IsBusy = true; //  Mark object as Busy

                if (!powerSourceListIsEmpty) // If there are devices on the list
                {
                    if (DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).IsOnline
                    ) // If device is online we use its chanellist to display
                    {
                        _voltageGraphChartControl.DataSource =
                            DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).ChanelList.
                                ElementAt(0).ResultsList;

                        _currentGraphChartControl.DataSource =
                            DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).ChanelList.
                                ElementAt(0).ResultsList;
                    }
                    else // If device is offline we show nothing
                    {
                        _voltageGraphChartControl.DataSource = null;
                        _currentGraphChartControl.DataSource = null;
                    }
                }
                else // If there are no devices on the list perhaps event was called on start of application
                {
                    _voltageGraphChartControl.DataSource = null;
                    _currentGraphChartControl.DataSource = null;
                }
                _voltageGraphChartControl.RefreshData();
                _currentGraphChartControl.RefreshData();

                IsBusy = false; //  Remove Busy Flag
            }
        }
    }
}

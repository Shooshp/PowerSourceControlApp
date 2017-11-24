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

        private TextEdit _voltageEdit;
        private TextEdit _currentEdit;

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
            object tasklistgridview,
            object voltageedit,
            object currentedit)
        {
            _powerSourceListGridControl = (GridControl)powersourcegrid;
            _chanelListGridControl = (GridControl)chanellistgrid;
            _taskListGridControl = (GridControl) tasklistgrid;

            _voltageGraphChartControl = (ChartControl) voltagechart;
            _currentGraphChartControl = (ChartControl) currentchart;

            _powerSourceListGridView = (GridView) powersourcelistgridview;
            _chanelListLayoutView = (LayoutView) chanellistlayoutview;
            _taskListGridView = (GridView) tasklistgridview;

            _voltageEdit = (TextEdit) voltageedit;
            _currentEdit = (TextEdit) currentedit;

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
                    TaskAndChanelListDs(-1);
                    ChartsDS(-1, -1);
                    _voltageEdit.EditValue = null;
                    _currentEdit.EditValue = null;
                    PowerSourceCollection.SelectedPowerSourceIp = null;
                }
                else
                {
                    if (_focusedPowerSourceIp == null)
                    {
                        TaskAndChanelListDs(0);
                        ChartsDS(0, 0);
                        _voltageEdit.EditValue = DetectedPowerSources.ElementAt(0).ChanelList.ElementAt(0).Voltage; 
                        _currentEdit.EditValue = DetectedPowerSources.ElementAt(0).ChanelList.ElementAt(0).Current; 
                    }
                    else
                    {
                        TaskAndChanelListDs(_focusedPowerSourceIndex);
                        ChartsDS(_focusedPowerSourceIndex, _focusedChanelIndex);
                        _voltageEdit.EditValue = DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).ChanelList.ElementAt(_focusedChanelIndex).Voltage;
                        _currentEdit.EditValue = DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).ChanelList.ElementAt(_focusedChanelIndex).Current;
                        PowerSourceCollection.SelectedPowerSourceIp = DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).IpAddress;
                    }
                }     
            }
            RefreshCharts();
            RefreshLists();
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
                        TaskAndChanelListDs(_focusedPowerSourceIndex);
                        ChartsDS(_focusedPowerSourceIndex, 0);
                        _voltageEdit.EditValue = DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).ChanelList.ElementAt(0).Voltage;
                        _currentEdit.EditValue = DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).ChanelList.ElementAt(0).Current;
                        PowerSourceCollection.SelectedPowerSourceIp = DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).IpAddress;
                    }
                    else // If device is offline we show nothing
                    {
                        ChartsDS(-1, -1);
                        TaskAndChanelListDs(-1);
                        _voltageEdit.EditValue = null;
                        _currentEdit.EditValue = null;
                        PowerSourceCollection.SelectedPowerSourceIp = null;
                    }      
                }
                else // If there are no devices on the list perhaps event was called on start of application
                {
                    ChartsDS(-1, -1);
                    TaskAndChanelListDs(-1);
                    _voltageEdit.EditValue = null;
                    _currentEdit.EditValue = null;
                    PowerSourceCollection.SelectedPowerSourceIp = null;
                }

                RefreshLists();
                RefreshCharts();
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
                        ChartsDS(_focusedPowerSourceIndex, _focusedChanelIndex);
                        _voltageEdit.EditValue = DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).ChanelList.ElementAt(_focusedChanelIndex).Voltage;
                        _currentEdit.EditValue = DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).ChanelList.ElementAt(_focusedChanelIndex).Current;
                    }
                    else // If device is offline we show nothing
                    {
                        ChartsDS(-1, -1);
                        _voltageEdit.EditValue = null;
                        _currentEdit.EditValue = null;
                    }
                }
                else // If there are no devices on the list perhaps event was called on start of application
                {
                    ChartsDS(-1, -1);
                    _voltageEdit.EditValue = null;
                    _currentEdit.EditValue = null;
                }
                RefreshCharts();

                IsBusy = false; //  Remove Busy Flag
            }
        }

        public void UpdateButtonClick()
        {
            var powerSourceListIsEmpty = !DetectedPowerSources.Any();

            if (!powerSourceListIsEmpty)
            {
                if (DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).IsOnline)
                {
                     
                }
            }
        }

        private void TaskAndChanelListDs(int powersourceindex)
        {
            if (powersourceindex == -1)
            {
                _chanelListGridControl.DataSource = null;
                _taskListGridControl.DataSource = null;
            }
            else
            {
                _chanelListGridControl.DataSource = DetectedPowerSources.ElementAt(powersourceindex).ChanelList;
                _taskListGridControl.DataSource = DetectedPowerSources.ElementAt(powersourceindex).DutyManager.TaskList;
            }
        }

        private void ChartsDS(int powersourceindex, int chanelindex)
        {
            if (powersourceindex == -1)
            {
                _voltageGraphChartControl.DataSource = null;
                _currentGraphChartControl.DataSource = null;
            }
            else
            {
                _voltageGraphChartControl.DataSource =
                    DetectedPowerSources.ElementAt(powersourceindex).ChanelList.
                        ElementAt(chanelindex).ResultsList;

                _currentGraphChartControl.DataSource =
                    DetectedPowerSources.ElementAt(powersourceindex).ChanelList.
                        ElementAt(chanelindex).ResultsList;
            }
        }

        private void RefreshLists()
        {
            _chanelListGridControl.RefreshDataSource();
            _taskListGridControl.RefreshDataSource();
        }

        private void RefreshCharts()
        {
            _voltageGraphChartControl.RefreshData();
            _voltageGraphChartControl.RefreshData();
        }
    }
}

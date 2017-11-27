using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        private SpinEdit _voltageEdit;
        private SpinEdit _currentEdit;

        private SimpleButton _updateButton;
        private ToggleSwitch _onOffSwitch;

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
            object currentedit,
            object updatebutton,
            object onoffswitch)
        {
            _powerSourceListGridControl = (GridControl)powersourcegrid;
            _chanelListGridControl = (GridControl)chanellistgrid;
            _taskListGridControl = (GridControl) tasklistgrid;

            _voltageGraphChartControl = (ChartControl) voltagechart;
            _currentGraphChartControl = (ChartControl) currentchart;

            _powerSourceListGridView = (GridView) powersourcelistgridview;
            _chanelListLayoutView = (LayoutView) chanellistlayoutview;
            _taskListGridView = (GridView) tasklistgridview;

            _voltageEdit = (SpinEdit) voltageedit;
            _currentEdit = (SpinEdit) currentedit;

            _updateButton = (SimpleButton) updatebutton;

            _onOffSwitch = (ToggleSwitch) onoffswitch;

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
                    ChartsDs(-1, -1);
                    EditorsDs(-1, -1);
                    UpdateButtonIsVisible(false);
                    OnOffSwitchIsVisible(false);
                    PowerSourceCollection.SelectedPowerSourceIp = null;
                }
                else
                {
                    if (_focusedPowerSourceIp == null)
                    {   // If no PowerSource was selected before select first in list
                        TaskAndChanelListDs(0);
                        ChartsDs(0, 0);
                        EditorsDs(0, 0);
                        UpdateButtonIsVisible(true);
                        OnOffSwitchIsVisible(true);
                        OnOffSwitchState(0, 0);
                        PowerSourceCollection.SelectedPowerSourceIp = DetectedPowerSources.ElementAt(0).IpAddress;
                    }
                    else
                    {   // Use selected PowerSource fo DAtaSources
                        TaskAndChanelListDs(_focusedPowerSourceIndex);
                        ChartsDs(_focusedPowerSourceIndex, _focusedChanelIndex);
                        EditorsDs(_focusedPowerSourceIndex, _focusedChanelIndex);
                        UpdateButtonIsVisible(true);
                        OnOffSwitchIsVisible(true);
                        OnOffSwitchState(_focusedPowerSourceIndex, _focusedChanelIndex);
                        PowerSourceCollection.SelectedPowerSourceIp = DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).IpAddress;
                    }
                }     
            }
            else
            {   // If PowerSource List is Empty we draw nothing
                TaskAndChanelListDs(-1);
                ChartsDs(-1, -1);
                EditorsDs(-1, -1);
                UpdateButtonIsVisible(false);
                OnOffSwitchIsVisible(false);
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
                        ChartsDs(_focusedPowerSourceIndex, 0);
                        EditorsDs(_focusedPowerSourceIndex, 0);
                        UpdateButtonIsVisible(true);
                        OnOffSwitchIsVisible(true);
                        OnOffSwitchState(_focusedPowerSourceIndex, 0);
                        PowerSourceCollection.SelectedPowerSourceIp = DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).IpAddress;
                    }
                    else // If device is offline we show nothing
                    {
                        ChartsDs(-1, -1);
                        TaskAndChanelListDs(-1);
                        EditorsDs(-1, -1);
                        UpdateButtonIsVisible(false);
                        OnOffSwitchIsVisible(false);
                        PowerSourceCollection.SelectedPowerSourceIp = null;
                    }      
                }
                else // If there are no devices on the list perhaps event was called on start of application
                {
                    ChartsDs(-1, -1);
                    TaskAndChanelListDs(-1);
                    EditorsDs(-1, -1);
                    UpdateButtonIsVisible(false);
                    OnOffSwitchIsVisible(false);
                    PowerSourceCollection.SelectedPowerSourceIp = null;
                }

                RefreshLists();
                RefreshCharts();
                RefreshEditors();
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
                        ChartsDs(_focusedPowerSourceIndex, _focusedChanelIndex);
                        EditorsDs(_focusedPowerSourceIndex, _focusedChanelIndex);
                        UpdateButtonIsVisible(true);
                        OnOffSwitchIsVisible(true);
                        OnOffSwitchState(_focusedPowerSourceIndex, _focusedChanelIndex);
                    }
                    else // If device is offline we show nothing
                    {
                        ChartsDs(-1, -1);
                        EditorsDs(-1, -1);
                        UpdateButtonIsVisible(false);
                        OnOffSwitchIsVisible(false);
                    }
                }
                else // If there are no devices on the list perhaps event was called on start of application
                {
                    ChartsDs(-1, -1);
                    EditorsDs(-1, -1);
                    UpdateButtonIsVisible(false);
                    OnOffSwitchIsVisible(false);
                }
                RefreshCharts();

                IsBusy = false; //  Remove Busy Flag
            }
        }

        /// <summary>
        /// Working private routines
        /// </summary>

        public void UpdateButtonClick()
        {
            decimal voltage = Convert.ToDecimal(_voltageEdit.EditValue);
            decimal current = Convert.ToDecimal(_currentEdit.EditValue);
            uint chanelId = DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).ChanelList.ElementAt(_focusedChanelIndex).ChanelId;

            if (DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).IsOnline)
            {
                PowerSourceCollection.DetectedPowerSources.Single(p => p.IpAddress == _focusedPowerSourceIp).Update(voltage, current, chanelId);
            }                  
        }

        public void OnOffSwitchToggled()
        {
            uint chanelId = DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).ChanelList.ElementAt(_focusedChanelIndex).ChanelId;
            bool state = _onOffSwitch.IsOn;

            if (DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).IsOnline)
            {
                PowerSourceCollection.DetectedPowerSources.Single(p => p.IpAddress == _focusedPowerSourceIp).Switch(chanelId, state);
            }
            while (DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).ChanelList.ElementAt(_focusedChanelIndex).OnOff != state)
            {
                DetectedPowerSources.ElementAt(_focusedPowerSourceIndex).ChanelList.ElementAt(_focusedChanelIndex).SyncSettings();
                Thread.Sleep(1);
            }
        }

        private void OnOffSwitchState(int powersourceindex, int chanelindex)
        {
            _onOffSwitch.EditValue = DetectedPowerSources.ElementAt(powersourceindex).ChanelList.ElementAt(chanelindex).OnOff;
        }

        private void TaskAndChanelListDs(int powersourceindex)
        {
            if (powersourceindex == -1)
            {
                _chanelListGridControl.DataSource = null;
                _taskListGridControl.DataSource = null;
                TaskListIsVisible(false);
            }
            else
            {
                _chanelListGridControl.DataSource = DetectedPowerSources.ElementAt(powersourceindex).ChanelList;
                _taskListGridControl.DataSource = DetectedPowerSources.ElementAt(powersourceindex).DutyManager.TaskList;
                TaskListIsVisible(true);
            }
        }
 
        private void EditorsDs(int powersourceindex, int chanelindex)
        {
            if (powersourceindex == -1)
            { //Empty Chanel List or empty PowerSource List
               /* _voltageEdit.EditValue = null;
                _currentEdit.EditValue = null;*/
                EditorsIsVisible(false);
            }
            else
            {
              /*  _voltageEdit.EditValue = DetectedPowerSources.ElementAt(powersourceindex).ChanelList.ElementAt(chanelindex).Voltage;
                _currentEdit.EditValue = DetectedPowerSources.ElementAt(powersourceindex).ChanelList.ElementAt(chanelindex).Current;
               */ EditorsIsVisible(true);
            }
        }

        private void ChartsDs(int powersourceindex, int chanelindex)
        {
            if (powersourceindex == -1)
            {
                _voltageGraphChartControl.DataSource = null;
                _currentGraphChartControl.DataSource = null;
                ChartsIsVisible(false);
            }
            else
            {
                _voltageGraphChartControl.DataSource =
                    DetectedPowerSources.ElementAt(powersourceindex).ChanelList.
                        ElementAt(chanelindex).ResultsList;

                _currentGraphChartControl.DataSource =
                    DetectedPowerSources.ElementAt(powersourceindex).ChanelList.
                        ElementAt(chanelindex).ResultsList;

                ChartsIsVisible(true);
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

        private void RefreshEditors()
        {
            _voltageEdit.Refresh();
            _currentEdit.Refresh();
        }

        private void TaskListIsVisible(bool state)
        {
            _taskListGridControl.Visible = state;
        }

        private void EditorsIsVisible(bool state)
        {
            _voltageEdit.Visible = state;
            _currentEdit.Visible = state;
        }

        private void ChartsIsVisible(bool state)
        {
            _voltageGraphChartControl.Visible = state;
            _currentGraphChartControl.Visible = state;
        }

        private void UpdateButtonIsVisible(bool state)
        {
            _updateButton.Visible = state;
        }

        private void OnOffSwitchIsVisible(bool state)
        {
            _onOffSwitch.Visible = state;
        }
    }
}

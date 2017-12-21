using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Layout;
using PowerSourceControlApp.DeviceManagment;

namespace PowerSourceControlApp
{
    public static class VisualInterfaceControl
    {
        private static int _focusedPowerSourceIndex;
        private static string _focusedPowerSourceIp;
        private static int _focusedChanelIndex;
        private static int _focusedChanelId;

        private static GridControl _taskListGridControl;
        private static GridControl _powerSourceListGridControl;
        private static GridControl _chanelListGridControl;
        private static ChartControl _voltageGraphChartControl;
        private static ChartControl _currentGraphChartControl;
        private static GridView _powerSourceListGridView;
        private static GridView _taskListGridView;
        private static LayoutView _chanelListLayoutView;
        private static SpinEdit _voltageEdit;
        private static SpinEdit _currentEdit;
        private static SimpleButton _updateButton;
        private static SimpleButton _onoffButton;
        private static List<Label> _labelList;

        private static bool PowerSourceListIsEmpty
        {
            get
            {
                if (DeviceManager.DeviceList.Count == 0)
                {
                    return true;
                }
                return false;
            }
        }

        public static void ConnectToGrids(
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
            object onoffbutton,
            object labellist)
        {
            _focusedPowerSourceIndex = 0;
            _focusedPowerSourceIp = null;
            _focusedChanelIndex = 0;
            _focusedChanelId = 0;

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
            _onoffButton = (SimpleButton) onoffbutton;

            _powerSourceListGridControl.DataSource = DeviceManager.DeviceList;

            _powerSourceListGridView.FocusedRowChanged += CurrentPowerSourceChanged;
            _chanelListLayoutView.FocusedRowChanged += CurrentChanelChanged;

            _labelList = (List<Label>) labellist;
        }

        public static void UpdateForms()
        {
            _powerSourceListGridControl?.RefreshDataSource();

            if (!PowerSourceListIsEmpty)
            {
                if (!DeviceManager.DeviceList.ElementAt(_focusedPowerSourceIndex).IsOnline)
                {
                    SelectPowerSource(-1);
                }
                else
                {
                    if (_focusedPowerSourceIp == null)
                    {
                        SelectPowerSource(0);
                    }
                    else
                    {
                        SelectPowerSource(_focusedPowerSourceIndex);
                        SelectChanel(_focusedChanelIndex);
                    }
                }
            }
            else
            {
                SelectPowerSource(-1);
            }

        }

        public static void UpdateDevice()
        {
            if (!PowerSourceListIsEmpty && _focusedPowerSourceIp != null && DeviceManager.DeviceList.ElementAt(_focusedPowerSourceIndex).IsOnline)
            {
                RefreshLists();
                SelectChanel(_focusedChanelIndex);
            }
        }

        private static void SelectPowerSource(int powersourceindex)
        {
            if (powersourceindex > -1)
            {
                _focusedPowerSourceIp = DeviceManager.DeviceList.ElementAt(_focusedPowerSourceIndex).IpAddress;
                DeviceManager.SelectedPowerSourceIp = _focusedPowerSourceIp;
                TaskAndChanelListDs(powersourceindex);
                SelectChanel(0);
            }
            else
            {
                TaskAndChanelListDs(-1);
                SelectChanel(-1);
                DeviceManager.SelectedPowerSourceIp = null;
            }
            RefreshLists();
        }

        private static void SelectChanel(int chanelindex)
        {
            if (chanelindex > -1)
            {
                ChartsDs(_focusedPowerSourceIndex, chanelindex);
                EditorsDs(_focusedPowerSourceIndex, chanelindex);
                ButtonsIsVisible(true);
                LableIsVisible(true);
                DeviceManager.SelectedChanelUUID = DeviceManager.DeviceList.
                    ElementAt(_focusedPowerSourceIndex).ChanelList.ElementAt(chanelindex).ChanelUUID;
            }
            else
            {
                ChartsDs(-1, -1);
                EditorsDs(-1, -1);
                ButtonsIsVisible(false);
                LableIsVisible(false);
                DeviceManager.SelectedChanelUUID = null;
            }
            RefreshCharts();
            RefreshEditors();
        }

        public static void UpdateChanelData()
        {
            _chanelListGridControl.RefreshDataSource();
            RefreshCharts();
        }

        public static void UpdateTaskList()
        {
            _taskListGridControl.RefreshDataSource();
        }

        public static void CurrentPowerSourceChanged(object sender, FocusedRowChangedEventArgs arguments)
        {
            _focusedPowerSourceIndex = arguments.FocusedRowHandle;

            if (!PowerSourceListIsEmpty) 
            {
                if (DeviceManager.DeviceList.ElementAt(_focusedPowerSourceIndex).IsOnline)
                {   
                    SelectPowerSource(_focusedPowerSourceIndex);
                }
                else 
                {
                    SelectPowerSource(-1);
                }      
            }
            else 
            {
                SelectPowerSource(-1);
            }
        }

        public static void CurrentChanelChanged(object sender, FocusedRowChangedEventArgs arguments)
        {
            _focusedChanelIndex = arguments.FocusedRowHandle;

            if (!PowerSourceListIsEmpty)
            { 
                if (DeviceManager.DeviceList.ElementAt(_focusedPowerSourceIndex).IsOnline)             
                {
                    SelectChanel(_focusedChanelIndex);
                }
                else 
                {
                    SelectChanel(-1);
                }
            }
            else 
            {
                SelectChanel(-1);
            }
        }

        /// <summary>
        /// Working private routines
        /// </summary>

        public static void UpdateButtonClick()
        {
            decimal voltage = Convert.ToDecimal(_voltageEdit.EditValue);
            decimal current = Convert.ToDecimal(_currentEdit.EditValue);
            uint chanelId = DeviceManager.DeviceList.ElementAt(_focusedPowerSourceIndex).ChanelList.ElementAt(_focusedChanelIndex).ChanelId;

            if (DeviceManager.DeviceList.ElementAt(_focusedPowerSourceIndex).IsOnline)
            {
                DeviceManager.DeviceList.Single(p => p.IpAddress == _focusedPowerSourceIp).Update(voltage, current, chanelId);
                ChartsDs(_focusedPowerSourceIndex, _focusedChanelIndex);
                RefreshCharts();
                UpdateChanelData();
            }                  
        }

        public static void OnOffButtonClick()
        {
            uint chanelId = DeviceManager.DeviceList.ElementAt(_focusedPowerSourceIndex).ChanelList.ElementAt(_focusedChanelIndex).ChanelId;
            if (DeviceManager.DeviceList.ElementAt(_focusedPowerSourceIndex).IsOnline)
            {
                DeviceManager.DeviceList.Single(p => p.IpAddress == _focusedPowerSourceIp).Switch(chanelId);
            }
        }

        private static void TaskAndChanelListDs(int powersourceindex)
        {
            if (powersourceindex < 0 )
            {
                _chanelListGridControl.DataSource = null;
                _taskListGridControl.DataSource = null;
                TaskListIsVisible(false);
            }
            else
            {
                _chanelListGridControl.DataSource = DeviceManager.DeviceList.ElementAt(powersourceindex).ChanelList;
                _taskListGridControl.DataSource = DeviceManager.DeviceList.ElementAt(powersourceindex).DutyManager.TaskList;
                TaskListIsVisible(true);
            }
        }

        private static void EditorsDs(int powersourceindex, int chanelindex)
        {
            if (powersourceindex < 0 || chanelindex < 0)
            { //Empty Chanel List or empty PowerSource List
                _voltageEdit.EditValue = null;
                _currentEdit.EditValue = null;
                EditorsIsVisible(false);
            }
            else
            {
                 EditorsIsVisible(true);
            }
        }

        private static void ChartsDs(int powersourceindex, int chanelindex)
        {
            if (powersourceindex < 0 || chanelindex < 0)
            {
                _voltageGraphChartControl.DataSource = null;
                _currentGraphChartControl.DataSource = null;
                ChartsIsVisible(false);
            }
            else
            {
                _voltageGraphChartControl.DataSource =
                    DeviceManager.DeviceList.ElementAt(powersourceindex).ChanelList.
                        ElementAt(chanelindex).ResultsList;

                _currentGraphChartControl.DataSource =
                    DeviceManager.DeviceList.ElementAt(powersourceindex).ChanelList.
                        ElementAt(chanelindex).ResultsList;

                ChartsIsVisible(true);
            }
        }

        private static void RefreshLists()
        {
            _chanelListGridControl.RefreshDataSource();
            _taskListGridControl.RefreshDataSource();
        }

        private static void RefreshCharts()
        {

            _voltageGraphChartControl.RefreshData();
            _currentGraphChartControl.RefreshData();
        }

        private static void RefreshEditors()
        {
            _voltageEdit.Refresh();
            _currentEdit.Refresh();
        }

        private static void TaskListIsVisible(bool state)
        {
            _taskListGridControl.Visible = state;
        }

        private static void EditorsIsVisible(bool state)
        {
            _voltageEdit.Visible = state;
            _currentEdit.Visible = state;
        }

        private static void ChartsIsVisible(bool state)
        {
            _voltageGraphChartControl.Visible = state;
            _currentGraphChartControl.Visible = state;
        }

        private static void ButtonsIsVisible(bool state)
        {
            _updateButton.Visible = state;
            _onoffButton.Visible = state;
        }

        private static void LableIsVisible(bool state)
        {
            foreach (var label in _labelList)
            {
                label.Visible = state;
            }
        }
    }
}

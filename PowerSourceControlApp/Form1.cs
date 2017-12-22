using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.CustomEditor;
using DevExpress.XtraGauges.Win;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Layout.Events;
using PowerSourceControlApp.DeviceManagment;
using PowerSourceControlApp.DeviceManagment.Log;

namespace PowerSourceControlApp
{
    public partial class Form1: XtraForm
    {
        private readonly NetworkDeviceDetector _deviceDetector;

        public Form1()
        {
            DeviceManager.Init();
            _deviceDetector = new NetworkDeviceDetector();
            _deviceDetector.OnDataReceived += NewDeviceHandler;

            InitializeComponent();

            var labelList = new List<Label> {CurrentLabel, VoltageLabel, OnOffLabel, UpdateLabel};

            VisualInterfaceControl.ConnectToGrids(
                powersourcegrid: PowerSourceList,
                chanellistgrid: PowerSourceChanelList,
                tasklistgrid: TaskListControl,
                voltagechart: VoltageChart,
                currentchart: CurrentChart,
                powersourcelistgridview: gridView1,
                chanellistlayoutview: layoutView1,
                tasklistgridview: gridView2,
                voltageedit: VoltageEdit,
                currentedit: CurrentEdit,
                updatebutton: UpdateButton,
                onoffbutton: OnButton,
                labellist: labelList,
                loggridcontrol:LogGridControl
                );

            var version = Assembly.GetExecutingAssembly().GetName().Version;
            Text = @"PowerSource Control " + version.Major + @"." + version.Minor + @" (build " + version.Build + @")";

            CreateGauge(layoutView1.Columns["Status"], StatusGauge);
            CreateGauge(layoutView1.Columns["RecentVoltageDisplay"], VoltageGauge);
            CreateGauge(layoutView1.Columns["RecentCurrentDisplay"], CurrentGauge);
            _deviceDetector.CreateUdpReadThread();
            layoutView1.FieldValueClick += ChanelStatusRightClick;
            gridView1.RowClick += PowerSourceList_DoubleClick;
            DeviceManager.StartHashTread();

            DeviceManager.DeviceListUpdate += UpdateFormsHandler;
            DeviceManager.DeviceUpdate += UpdateDeviceHandler;
            DeviceManager.LogUpdate += UpdateLogHandler;

            EventLog.Add("Global", "App started!");
        }

        

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private static void CreateGauge(GridColumn column, GaugeControl gauge)
        {
            var ri = new RepositoryItemAnyControl {Control = gauge};
            column.View.GridControl.RepositoryItems.Add(ri);
            column.ColumnEdit = ri;
        }

        public void UpdateOnChange()
        {
            layoutView1.PostEditor();
        }

        private void UpdateLogHandler()
        {

            Invoke((MethodInvoker)VisualInterfaceControl.UpdateLog);
        }

        private void UpdateFormsHandler()
        {
            Invoke((MethodInvoker)VisualInterfaceControl.UpdateForms);
        }

        private void UpdateDeviceHandler()
        {
            Invoke((MethodInvoker)VisualInterfaceControl.UpdateDevice);
        }

        private void NewDeviceHandler(object sender, DataEventArgs e)
        {
            var senderip = e.IpAddress.ToString();
            var sendermessage = Encoding.UTF8.GetString(e.Data);

            if (sendermessage == "Hello!")
            {
                if (!DeviceManager.IsBusy)
                {
                    _deviceDetector.SuspendThread = true;
                    var handleDevice =
                        new Thread(() => DeviceManager.NewDeviceDetectorHanler(senderip)) {IsBackground = true};
                    handleDevice.Start();
                    _deviceDetector.SuspendThread = false;
                }
            } 
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)VisualInterfaceControl.UpdateButtonClick);
        }

        private void OnButton_Click(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)VisualInterfaceControl.OnOffButtonClick);
        }

        private void ChanelStatusRightClick(object sender, FieldValueClickEventArgs e)
        {
            var arguments = e;
            if (arguments.Button == MouseButtons.Right)
            {
                if (arguments.Column.Caption == @"Status")
                {
                    
                    var powersourceIp = DeviceManager.SelectedPowerSourceIp;
                    var powersource = DeviceManager.DeviceList.Single(source => source.IpAddress == powersourceIp);

                    if (powersource.IsOnline)
                    {
                        var chanelUUID = DeviceManager.SelectedChanelUUID;
                        var chanel = powersource.ChanelList.Single(chanel1 => chanel1.ChanelUUID == chanelUUID);
                        var address = "0x" + chanel.Address.ToString("X");
                        var calibrationDate = chanel.CalibratedAt;
                        string text;

                        if (chanel.Calibration)
                        {
                            text = "Chanel " + address +
                                   " is already calibrated at "+ calibrationDate + ". Start recalibration?";
                        }
                        else
                        {
                            text = "Chanel " + address +
                                   ". Start calibration?";
                        }

                        const string caption = "Calibrate?";
                        const MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        const MessageBoxIcon icon = MessageBoxIcon.Question;
                        var result = XtraMessageBox.Show(text, buttons: buttons, caption: caption, icon: icon);

                        if (result == DialogResult.Yes) 
                        {
                            powersource.DutyManager.Calibrate(chanel);
                        }
                    }              
                }
            }
        }

        private void PowerSourceList_DoubleClick(object sender, RowClickEventArgs e)
        {
            var arguments = e;

            if (arguments.Button == MouseButtons.Right)
            {
                var powersource = DeviceManager.DeviceList[arguments.HitInfo.VisibleIndex];
                var column = arguments.HitInfo.Column.Caption;
                if (column == "Адрес") 
                {
                    if (powersource.IsOnline)
                    {
                        var nameEdit = new TextEdit
                        {
                            Text = powersource.Hostname,
                            Dock = DockStyle.Fill
                        };
                        var caption = "Change Hostname for PowerSource " + powersource.IpAddress;
                        const MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                        var result = XtraDialog.Show(content: nameEdit, caption: caption, buttons: buttons);

                        if (result == DialogResult.Yes)
                        {
                            var newName = nameEdit.Text;

                            if (newName != powersource.Hostname)
                            {
                                var thread = new Thread(() => powersource.SetHostname(newName)) { IsBackground = true };
                                thread.Start();
                            }
                        }
                    }
                }
            }
        }
    }
}

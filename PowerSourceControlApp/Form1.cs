using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using DevExpress.XtraEditors.CustomEditor;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGauges.Win;
using DevExpress.XtraGrid.Columns;
using PowerSourceControlApp.DeviceManagment;

namespace PowerSourceControlApp
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        private VisualInterfaceControl MainViewControl;
        private NetworkDeviceDetector DeviceDetector;
        private DeviceManager PowerSourceCollection;
        bool _listIsEmpty;

        public Form1()
        {
            _listIsEmpty = true;
            PowerSourceCollection = new DeviceManager();
            MainViewControl = new VisualInterfaceControl(PowerSourceCollection);
            DeviceDetector = new NetworkDeviceDetector();
            DeviceDetector.OnDataReceived += JustSimpleHandler;

            InitializeComponent();


            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = "PowerSource Control " + version.Major + "." + version.Minor + " (build " + version.Build + ")";



            MainViewControl.ConnectToGrids(
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
                onoffswitch: OnOffSwitch);

            CreateGauge(layoutView1.Columns["Status"], StatusGauge);
            CreateGauge(layoutView1.Columns["RecentVoltageDisplay"], VoltageGauge);
            CreateGauge(layoutView1.Columns["RecentCurrentDisplay"], CurrentGauge);


            MainViewControl.UpdateForms();
            DeviceDetector.CreateUdpReadThread();
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void Edit_EditValueChanged(object sender, EventArgs e)
        {
            layoutView1.PostEditor();
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

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (PowerSourceCollection.IsUpdated)
            {
                MainViewControl.UpdateForms();
                PowerSourceCollection.IsUpdated = false;
            }
        }

        private  void JustSimpleHandler(object sender, DataEventArgs e)
        {
            var senderip = e.IpAddress.ToString();
            var sendermessage = Encoding.UTF8.GetString(e.Data);

            if (sendermessage == "Hello!")
            {
                if (!PowerSourceCollection.IsBusy)
                {
                    DeviceDetector.SuspendThread = true;
                    var handleDevice =
                        new Thread(() => PowerSourceCollection.NewDeviceDetectorHanler(senderip)) {IsBackground = true};
                    handleDevice.Start();
                    DeviceDetector.SuspendThread = false;
                }
            } 
        }

        private void PingTime_Tick(object sender, EventArgs e)
        {          
            if (PowerSourceCollection.DetectedPowerSources.Any())
            {
                if (_listIsEmpty)
                {
                    CreateGauge(gridView1.Columns["IsOnline"], isOnlineGauge);
                    _listIsEmpty = false;
                }
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            MainViewControl.UpdateButtonClick();
        }

        private void ToggleSwitch2_Toggled(object sender, EventArgs e)
        {
            MainViewControl.OnOffSwitchToggled();
        }
    }
}

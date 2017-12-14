using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors.CustomEditor;
using DevExpress.XtraGauges.Win;
using DevExpress.XtraGrid.Columns;
using PowerSourceControlApp.DeviceManagment;

namespace PowerSourceControlApp
{
    public partial  class Form1: DevExpress.XtraEditors.XtraForm
    {
        private NetworkDeviceDetector DeviceDetector;

        public Form1()
        {
            DeviceManager.Init();
            DeviceDetector = new NetworkDeviceDetector();
            DeviceDetector.OnDataReceived += NewDeviceHandler;

            InitializeComponent();

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
                onoffbutton: OnButton);

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = "PowerSource Control " + version.Major + "." + version.Minor + " (build " + version.Build + ")";

            CreateGauge(layoutView1.Columns["Status"], StatusGauge);
            CreateGauge(layoutView1.Columns["RecentVoltageDisplay"], VoltageGauge);
            CreateGauge(layoutView1.Columns["RecentCurrentDisplay"], CurrentGauge);

            DeviceDetector.CreateUdpReadThread();
            DeviceManager.StartHashTread();

            DeviceManager.DeviceListUpdate += UpdateFormsHandler;
            DeviceManager.DeviceUpdate += UpdateDeviceHandler;
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

        public void UpdateFormsHandler()
        {
            Invoke((MethodInvoker)VisualInterfaceControl.UpdateForms);
        }

        public void UpdateDeviceHandler()
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
                    DeviceDetector.SuspendThread = true;
                    var handleDevice =
                        new Thread(() => DeviceManager.NewDeviceDetectorHanler(senderip)) {IsBackground = true};
                    handleDevice.Start();
                    DeviceDetector.SuspendThread = false;
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
    }
}

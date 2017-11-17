using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.XtraEditors.CustomEditor;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGauges.Win;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace PowerSourceControlApp
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public VisualInterfaceControl MainViewControl;
        public NetworkDeviceDetector DeviceDetector;
        public DeviceManager PowerSourceCollection;
        bool ListIsEmpty;

        public Form1()
        {
            ListIsEmpty = true;
            PowerSourceCollection = new DeviceManager();
            MainViewControl = new VisualInterfaceControl(PowerSourceCollection.DetectedPowerSources);
            DeviceDetector = new NetworkDeviceDetector();
            DeviceDetector.OnDataReceived += JustSimpleHandler;
            DeviceDetector.CreateUdpReadThread();
            
            InitializeComponent();
            MainViewControl.ConnectToGrids(PowerSourceList, PowerSourceChanelList, gridView1, layoutView1);
            CreateGauge(layoutView1.Columns["Status"], StatusGauge);
            CreateGauge(layoutView1.Columns["Voltage"], VoltageGauge);
            CreateGauge(layoutView1.Columns["Current"], CurrentGauge);
            
            var edit = new RepositoryItemToggleSwitch();
            layoutView1.Columns["OnOff"].ColumnEdit = edit;
            edit.EditValueChanged += Edit_EditValueChanged;

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (PowerSourceCollection.isUpdated)
            {
                MainViewControl.UpdateForms();
                PowerSourceCollection.isUpdated = false;
            }      
        }

        private  void JustSimpleHandler(object sender, DataEventArgs e)
        {
            var senderip = e.IpAddress.ToString();
            var sendermessage = Encoding.UTF8.GetString(e.Data);

            if (sendermessage == "Hello!")
            {
                if (!PowerSourceCollection.isBusy)
                {
                    DeviceDetector.SuspendThread = true;
                    Thread handleDevice = new Thread(() => PowerSourceCollection.NewDeviceDetectorHanler(senderip));
                    handleDevice.IsBackground = true;
                    handleDevice.Start();
                    DeviceDetector.SuspendThread = false;
                }
            } 
        }

        private void PingTime_Tick(object sender, EventArgs e)
        {          
            if (PowerSourceCollection.DetectedPowerSources.Any())
            {
                if (ListIsEmpty)
                {
                    CreateGauge(gridView1.Columns["IsOnline"], isOnlineGauge);
                    ListIsEmpty = false;
                }
            }
        }
    }
}

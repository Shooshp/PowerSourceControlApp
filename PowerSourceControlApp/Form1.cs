using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors.CustomEditor;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGauges.Win;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;

namespace PowerSourceControlApp
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public VisualInterfaceControl MainViewControl;
        public NetworkDeviceDetector DeviceDetector;

        public Form1()
        {
            MainViewControl = new VisualInterfaceControl();
            DeviceDetector = new NetworkDeviceDetector();
            DeviceDetector.OnDataReceived += JustSimpleHandler;
            DeviceDetector.CreateUdpReadThread();
            
            InitializeComponent();
            MainViewControl.ConnectToGrids(PowerSourceList, PowerSourceChanelList);
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
          //TODO: Create ping method for all currently connected devices
        }

        public void JustSimpleHandler(object sender, DataEventArgs e)
        {
            var senderip = e.IpAddress.ToString();
            var sendermessage = System.Text.Encoding.UTF8.GetString(e.Data);
            Thread.Sleep(15);
            var message = string.Concat("Get message from ip: ", senderip, " message is: ", sendermessage);
            Console.WriteLine(message);
        }
    }
}

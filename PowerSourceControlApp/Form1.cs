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
        private NowFocused _currentlySelectedChanel;
        private NowFocused _currentlySelectedPowerSource;
        private List<PowerSource> _powerSourceScanResultList;

        public Form1()
        {
            _currentlySelectedChanel = new NowFocused();
            _currentlySelectedPowerSource = new NowFocused();
            _powerSourceScanResultList = new List<PowerSource>();
            ScanForPowerSources();
            InitializeComponent();
            CreateGauge(layoutView1.Columns["Status"], StatusGauge);
            CreateGauge(layoutView1.Columns["Voltage"], VoltageGauge);
            CreateGauge(layoutView1.Columns["Current"], CurrentGauge);
            var edit = new RepositoryItemToggleSwitch();
            layoutView1.Columns["OnOff"].ColumnEdit = edit;
            edit.EditValueChanged += Edit_EditValueChanged;

            PowerSourceList.DataSource = _powerSourceScanResultList;
            PowerSourceChanelList.DataSource =
                _powerSourceScanResultList.ElementAt(_currentlySelectedPowerSource.Row).ChanelList;
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

        private void ChanelColomnFocus(object sender, FocusedColumnChangedEventArgs e)
        {
            var arguments = e;
            if (arguments.FocusedColumn == null)
                return;

            _currentlySelectedChanel.Column = arguments.FocusedColumn.Name;
            Console.WriteLine(_currentlySelectedChanel.Row);
            Console.WriteLine(_currentlySelectedChanel.Column);
        }

        private void ChanelRowFocus(object sender, FocusedRowChangedEventArgs e)
        {
            var arguments = e;
            _currentlySelectedChanel.Row = arguments.FocusedRowHandle;
        }

        private void PowerSourceRowFocus(object sender, FocusedRowChangedEventArgs e)
        {
            var arguments = e;
            _currentlySelectedPowerSource.Row = arguments.FocusedRowHandle;
            layoutView1.Refresh();
        }

        public void UpdateOnChange()
        {
            layoutView1.PostEditor();
        }

        private void ScanForPowerSources()
        {
            var scaner = new NetworkScaner("192.168.1.", 10236);
            var scanResult = new List<PowerSource>();
  
            foreach (var pinger in scaner.PingerList)
            {
                scanResult.Add(new PowerSource(pinger.address));
            }
            _powerSourceScanResultList.Clear();
            _powerSourceScanResultList = scanResult.ToList();
            scanResult.Clear();
            scaner.PingerList.Clear();
            GC.Collect();
        }

        private class NowFocused
        {
            public int Row { get; set; }
            public string Column { get; set; }

            public NowFocused()
            {
                Row = 0;
                Column = null;
            }
        }
    }
}

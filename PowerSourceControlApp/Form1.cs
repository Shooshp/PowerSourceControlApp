using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.CustomEditor;
using DevExpress.XtraGauges.Win;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;

namespace PowerSourceControlApp
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        private Focus CurentFocus;

        public Form1()
        {
            CurentFocus = new Focus();
            var powerSource = new PowerSource("192.168.43.207");
            InitializeComponent();
            CreateGauge(layoutView1.Columns["Status"], StatusGauge);
            CreateGauge(layoutView1.Columns["Voltage"], VoltageGauge);
            CreateGauge(layoutView1.Columns["Current"], CurrentGauge);
            gridControl1.DataSource = powerSource.ChanelList;
        }

        private static void CreateGauge(GridColumn column, GaugeControl gauge)
        {
            var ri = new RepositoryItemAnyControl { Control = gauge };
            column.View.GridControl.RepositoryItems.Add(ri);
            column.ColumnEdit = ri;
        }

        private void ColomnFocus(object sender, FocusedColumnChangedEventArgs e)
        {
            var arguments = e;
            if (arguments.FocusedColumn == null)
                return;

            CurentFocus.Column = arguments.FocusedColumn.Name;
            Console.WriteLine(CurentFocus.Row);
            Console.WriteLine(CurentFocus.Column);
        }

        private void RowFocus(object sender, FocusedRowChangedEventArgs e)
        {
            var arguments = e;
            CurentFocus.Row = arguments.FocusedRowHandle;
        }

        public class Focus
        {
            public int Row { get; set; }
            public string Column { get; set; }
        }
    }
}

using DevExpress.XtraGrid.Views.Base;

namespace PowerSourceControlApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState1 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
            DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState2 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
            DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState3 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
            DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState4 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.layoutView1 = new DevExpress.XtraGrid.Views.Layout.LayoutView();
            this.CalibrationColumn = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn1_5 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.PowerColumn = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn1_4 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.CurrentColumn = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn1_3 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.AddressColumn = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn1_2 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.ChanelIdColumn = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn1_1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.VoltageColumn = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.OnOffColumn = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn1_6 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.StatusColumn = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn1_7 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
            this.item1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.StatusGauge = new DevExpress.XtraGauges.Win.GaugeControl();
            this.stateIndicatorGauge1 = new DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorGauge();
            this.stateIndicatorComponent1 = new DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorComponent();
            this.VoltageGauge = new DevExpress.XtraGauges.Win.GaugeControl();
            this.dGauge1 = new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalGauge();
            this.digitalBackgroundLayerComponent1 = new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent();
            this.CurrentGauge = new DevExpress.XtraGauges.Win.GaugeControl();
            this.digitalGauge1 = new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalGauge();
            this.digitalBackgroundLayerComponent2 = new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.item1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateIndicatorGauge1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateIndicatorComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGauge1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.digitalBackgroundLayerComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.digitalGauge1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.digitalBackgroundLayerComponent2)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.layoutView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(863, 357);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutView1});
            // 
            // layoutView1
            // 
            this.layoutView1.ActiveFilterEnabled = false;
            this.layoutView1.CardMinSize = new System.Drawing.Size(258, 252);
            this.layoutView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.CalibrationColumn,
            this.PowerColumn,
            this.CurrentColumn,
            this.AddressColumn,
            this.ChanelIdColumn,
            this.VoltageColumn,
            this.OnOffColumn,
            this.StatusColumn});
            this.layoutView1.GridControl = this.gridControl1;
            this.layoutView1.Name = "layoutView1";
            this.layoutView1.OptionsCustomization.AllowFilter = false;
            this.layoutView1.OptionsCustomization.AllowSort = false;
            this.layoutView1.TemplateCard = this.layoutViewCard1;
            this.layoutView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.RowFocus);
            this.layoutView1.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.ColomnFocus);
            // 
            // CalibrationColumn
            // 
            this.CalibrationColumn.Caption = "Calibration";
            this.CalibrationColumn.FieldName = "Calibration";
            this.CalibrationColumn.LayoutViewField = this.layoutViewField_layoutViewColumn1_5;
            this.CalibrationColumn.Name = "CalibrationColumn";
            // 
            // layoutViewField_layoutViewColumn1_5
            // 
            this.layoutViewField_layoutViewColumn1_5.EditorPreferredWidth = 174;
            this.layoutViewField_layoutViewColumn1_5.Location = new System.Drawing.Point(0, 146);
            this.layoutViewField_layoutViewColumn1_5.Name = "layoutViewField_layoutViewColumn1_5";
            this.layoutViewField_layoutViewColumn1_5.Size = new System.Drawing.Size(238, 24);
            this.layoutViewField_layoutViewColumn1_5.TextSize = new System.Drawing.Size(55, 13);
            // 
            // PowerColumn
            // 
            this.PowerColumn.Caption = "W";
            this.PowerColumn.FieldName = "Power";
            this.PowerColumn.LayoutViewField = this.layoutViewField_layoutViewColumn1_4;
            this.PowerColumn.Name = "PowerColumn";
            // 
            // layoutViewField_layoutViewColumn1_4
            // 
            this.layoutViewField_layoutViewColumn1_4.EditorPreferredWidth = 174;
            this.layoutViewField_layoutViewColumn1_4.Location = new System.Drawing.Point(0, 122);
            this.layoutViewField_layoutViewColumn1_4.Name = "layoutViewField_layoutViewColumn1_4";
            this.layoutViewField_layoutViewColumn1_4.Size = new System.Drawing.Size(238, 24);
            this.layoutViewField_layoutViewColumn1_4.TextSize = new System.Drawing.Size(55, 13);
            // 
            // CurrentColumn
            // 
            this.CurrentColumn.Caption = "I";
            this.CurrentColumn.FieldName = "Current";
            this.CurrentColumn.LayoutViewField = this.layoutViewField_layoutViewColumn1_3;
            this.CurrentColumn.Name = "CurrentColumn";
            // 
            // layoutViewField_layoutViewColumn1_3
            // 
            this.layoutViewField_layoutViewColumn1_3.EditorPreferredWidth = 174;
            this.layoutViewField_layoutViewColumn1_3.Location = new System.Drawing.Point(0, 98);
            this.layoutViewField_layoutViewColumn1_3.Name = "layoutViewField_layoutViewColumn1_3";
            this.layoutViewField_layoutViewColumn1_3.Size = new System.Drawing.Size(238, 24);
            this.layoutViewField_layoutViewColumn1_3.TextSize = new System.Drawing.Size(55, 13);
            // 
            // AddressColumn
            // 
            this.AddressColumn.Caption = "Address";
            this.AddressColumn.FieldName = "Address";
            this.AddressColumn.LayoutViewField = this.layoutViewField_layoutViewColumn1_2;
            this.AddressColumn.Name = "AddressColumn";
            // 
            // layoutViewField_layoutViewColumn1_2
            // 
            this.layoutViewField_layoutViewColumn1_2.EditorPreferredWidth = 174;
            this.layoutViewField_layoutViewColumn1_2.Location = new System.Drawing.Point(0, 50);
            this.layoutViewField_layoutViewColumn1_2.Name = "layoutViewField_layoutViewColumn1_2";
            this.layoutViewField_layoutViewColumn1_2.Size = new System.Drawing.Size(238, 24);
            this.layoutViewField_layoutViewColumn1_2.TextSize = new System.Drawing.Size(55, 13);
            // 
            // ChanelIdColumn
            // 
            this.ChanelIdColumn.Caption = "ID";
            this.ChanelIdColumn.FieldName = "ChanelId";
            this.ChanelIdColumn.LayoutViewField = this.layoutViewField_layoutViewColumn1_1;
            this.ChanelIdColumn.Name = "ChanelIdColumn";
            // 
            // layoutViewField_layoutViewColumn1_1
            // 
            this.layoutViewField_layoutViewColumn1_1.EditorPreferredWidth = 174;
            this.layoutViewField_layoutViewColumn1_1.Location = new System.Drawing.Point(0, 26);
            this.layoutViewField_layoutViewColumn1_1.Name = "layoutViewField_layoutViewColumn1_1";
            this.layoutViewField_layoutViewColumn1_1.Size = new System.Drawing.Size(238, 24);
            this.layoutViewField_layoutViewColumn1_1.TextSize = new System.Drawing.Size(55, 13);
            // 
            // VoltageColumn
            // 
            this.VoltageColumn.Caption = "V";
            this.VoltageColumn.FieldName = "Voltage";
            this.VoltageColumn.LayoutViewField = this.layoutViewField_layoutViewColumn1;
            this.VoltageColumn.Name = "VoltageColumn";
            // 
            // layoutViewField_layoutViewColumn1
            // 
            this.layoutViewField_layoutViewColumn1.EditorPreferredWidth = 174;
            this.layoutViewField_layoutViewColumn1.Location = new System.Drawing.Point(0, 74);
            this.layoutViewField_layoutViewColumn1.Name = "layoutViewField_layoutViewColumn1";
            this.layoutViewField_layoutViewColumn1.Size = new System.Drawing.Size(238, 24);
            this.layoutViewField_layoutViewColumn1.TextSize = new System.Drawing.Size(55, 13);
            // 
            // OnOffColumn
            // 
            this.OnOffColumn.Caption = "On";
            this.OnOffColumn.FieldName = "OnOff";
            this.OnOffColumn.LayoutViewField = this.layoutViewField_layoutViewColumn1_6;
            this.OnOffColumn.Name = "OnOffColumn";
            // 
            // layoutViewField_layoutViewColumn1_6
            // 
            this.layoutViewField_layoutViewColumn1_6.EditorPreferredWidth = 174;
            this.layoutViewField_layoutViewColumn1_6.Location = new System.Drawing.Point(0, 170);
            this.layoutViewField_layoutViewColumn1_6.Name = "layoutViewField_layoutViewColumn1_6";
            this.layoutViewField_layoutViewColumn1_6.Size = new System.Drawing.Size(238, 44);
            this.layoutViewField_layoutViewColumn1_6.TextSize = new System.Drawing.Size(55, 13);
            // 
            // StatusColumn
            // 
            this.StatusColumn.Caption = "Status";
            this.StatusColumn.FieldName = "Status";
            this.StatusColumn.LayoutViewField = this.layoutViewField_layoutViewColumn1_7;
            this.StatusColumn.Name = "StatusColumn";
            // 
            // layoutViewField_layoutViewColumn1_7
            // 
            this.layoutViewField_layoutViewColumn1_7.EditorPreferredWidth = 174;
            this.layoutViewField_layoutViewColumn1_7.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_layoutViewColumn1_7.Name = "layoutViewField_layoutViewColumn1_7";
            this.layoutViewField_layoutViewColumn1_7.Size = new System.Drawing.Size(238, 24);
            this.layoutViewField_layoutViewColumn1_7.TextSize = new System.Drawing.Size(55, 13);
            // 
            // layoutViewCard1
            // 
            this.layoutViewCard1.CustomizationFormText = "TemplateCard";
            this.layoutViewCard1.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutViewCard1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_layoutViewColumn1_1,
            this.layoutViewField_layoutViewColumn1_4,
            this.layoutViewField_layoutViewColumn1_5,
            this.layoutViewField_layoutViewColumn1_6,
            this.layoutViewField_layoutViewColumn1,
            this.layoutViewField_layoutViewColumn1_3,
            this.layoutViewField_layoutViewColumn1_2,
            this.layoutViewField_layoutViewColumn1_7,
            this.item1});
            this.layoutViewCard1.Name = "layoutViewCard1";
            this.layoutViewCard1.OptionsItemText.TextToControlDistance = 5;
            this.layoutViewCard1.Text = "TemplateCard";
            // 
            // item1
            // 
            this.item1.AllowHotTrack = false;
            this.item1.CustomizationFormText = "item1";
            this.item1.Location = new System.Drawing.Point(0, 24);
            this.item1.Name = "item1";
            this.item1.Size = new System.Drawing.Size(238, 2);
            // 
            // StatusGauge
            // 
            this.StatusGauge.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[] {
            this.stateIndicatorGauge1});
            this.StatusGauge.Location = new System.Drawing.Point(85, 64);
            this.StatusGauge.Name = "StatusGauge";
            this.StatusGauge.Size = new System.Drawing.Size(50, 50);
            this.StatusGauge.TabIndex = 1;
            this.StatusGauge.Visible = false;
            // 
            // stateIndicatorGauge1
            // 
            this.stateIndicatorGauge1.Bounds = new System.Drawing.Rectangle(6, 6, 38, 38);
            this.stateIndicatorGauge1.Indicators.AddRange(new DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorComponent[] {
            this.stateIndicatorComponent1});
            this.stateIndicatorGauge1.Name = "stateIndicatorGauge1";
            // 
            // stateIndicatorComponent1
            // 
            this.stateIndicatorComponent1.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(124F, 124F);
            this.stateIndicatorComponent1.Name = "stateIndicatorComponent1";
            this.stateIndicatorComponent1.Size = new System.Drawing.SizeF(200F, 200F);
            this.stateIndicatorComponent1.StateIndex = 3;
            indicatorState1.Name = "State1";
            indicatorState1.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight1;
            indicatorState2.Name = "State2";
            indicatorState2.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight2;
            indicatorState3.Name = "State3";
            indicatorState3.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight3;
            indicatorState4.Name = "State4";
            indicatorState4.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight4;
            this.stateIndicatorComponent1.States.AddRange(new DevExpress.XtraGauges.Core.Model.IIndicatorState[] {
            indicatorState1,
            indicatorState2,
            indicatorState3,
            indicatorState4});
            // 
            // VoltageGauge
            // 
            this.VoltageGauge.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[] {
            this.dGauge1});
            this.VoltageGauge.Location = new System.Drawing.Point(35, 120);
            this.VoltageGauge.Name = "VoltageGauge";
            this.VoltageGauge.Size = new System.Drawing.Size(150, 60);
            this.VoltageGauge.TabIndex = 2;
            this.VoltageGauge.Visible = false;
            // 
            // dGauge1
            // 
            this.dGauge1.AppearanceOff.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#0FFF5000");
            this.dGauge1.AppearanceOn.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#FF5000");
            this.dGauge1.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent[] {
            this.digitalBackgroundLayerComponent1});
            this.dGauge1.Bounds = new System.Drawing.Rectangle(6, 6, 138, 48);
            this.dGauge1.DigitCount = 5;
            this.dGauge1.Name = "dGauge1";
            this.dGauge1.Text = "00,000";
            // 
            // digitalBackgroundLayerComponent1
            // 
            this.digitalBackgroundLayerComponent1.BottomRight = new DevExpress.XtraGauges.Core.Base.PointF2D(259.8125F, 99.9625F);
            this.digitalBackgroundLayerComponent1.Name = "bg1";
            this.digitalBackgroundLayerComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.DigitalBackgroundShapeSetType.Style3;
            this.digitalBackgroundLayerComponent1.TopLeft = new DevExpress.XtraGauges.Core.Base.PointF2D(20F, 0F);
            this.digitalBackgroundLayerComponent1.ZOrder = 1000;
            // 
            // CurrentGauge
            // 
            this.CurrentGauge.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[] {
            this.digitalGauge1});
            this.CurrentGauge.Location = new System.Drawing.Point(35, 186);
            this.CurrentGauge.Name = "CurrentGauge";
            this.CurrentGauge.Size = new System.Drawing.Size(150, 60);
            this.CurrentGauge.TabIndex = 3;
            this.CurrentGauge.Visible = false;
            // 
            // digitalGauge1
            // 
            this.digitalGauge1.AppearanceOff.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#0FFF5000");
            this.digitalGauge1.AppearanceOn.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#FF5000");
            this.digitalGauge1.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent[] {
            this.digitalBackgroundLayerComponent2});
            this.digitalGauge1.Bounds = new System.Drawing.Rectangle(6, 6, 138, 48);
            this.digitalGauge1.DigitCount = 5;
            this.digitalGauge1.Name = "digitalGauge1";
            this.digitalGauge1.Text = "00,000";
            // 
            // digitalBackgroundLayerComponent2
            // 
            this.digitalBackgroundLayerComponent2.BottomRight = new DevExpress.XtraGauges.Core.Base.PointF2D(259.8125F, 99.9625F);
            this.digitalBackgroundLayerComponent2.Name = "bg1";
            this.digitalBackgroundLayerComponent2.ShapeType = DevExpress.XtraGauges.Core.Model.DigitalBackgroundShapeSetType.Style3;
            this.digitalBackgroundLayerComponent2.TopLeft = new DevExpress.XtraGauges.Core.Base.PointF2D(20F, 0F);
            this.digitalBackgroundLayerComponent2.ZOrder = 1000;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 357);
            this.Controls.Add(this.CurrentGauge);
            this.Controls.Add(this.VoltageGauge);
            this.Controls.Add(this.StatusGauge);
            this.Controls.Add(this.gridControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.item1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateIndicatorGauge1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateIndicatorComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGauge1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.digitalBackgroundLayerComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.digitalGauge1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.digitalBackgroundLayerComponent2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Layout.LayoutView layoutView1;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn VoltageColumn;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn CalibrationColumn;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn PowerColumn;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn CurrentColumn;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn AddressColumn;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn ChanelIdColumn;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn OnOffColumn;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn StatusColumn;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn1_5;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn1_4;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn1_3;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn1_2;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn1_1;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn1;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn1_6;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn1_7;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCard1;
        private DevExpress.XtraLayout.SimpleSeparator item1;
        private DevExpress.XtraGauges.Win.GaugeControl StatusGauge;
        private DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorGauge stateIndicatorGauge1;
        private DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorComponent stateIndicatorComponent1;
        private DevExpress.XtraGauges.Win.GaugeControl VoltageGauge;
        private DevExpress.XtraGauges.Win.Gauges.Digital.DigitalGauge dGauge1;
        private DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent digitalBackgroundLayerComponent1;
        private DevExpress.XtraGauges.Win.GaugeControl CurrentGauge;
        private DevExpress.XtraGauges.Win.Gauges.Digital.DigitalGauge digitalGauge1;
        private DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent digitalBackgroundLayerComponent2;
    }
}


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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState1 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
            DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState2 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
            DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState3 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
            DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState4 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState5 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
            DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState6 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
            DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState7 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
            DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState8 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
            this.PowerSourceChanelList = new DevExpress.XtraGrid.GridControl();
            this.layoutView1 = new DevExpress.XtraGrid.Views.Layout.LayoutView();
            this.PowerColumn = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn1_4 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.CurrentColumn = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.AddressColumn = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.ChanelIdColumn = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.VoltageColumn = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.StatusColumn = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.StatusGauge = new DevExpress.XtraGauges.Win.GaugeControl();
            this.stateIndicatorGauge1 = new DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorGauge();
            this.stateIndicatorComponent1 = new DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorComponent();
            this.VoltageGauge = new DevExpress.XtraGauges.Win.GaugeControl();
            this.digitalBackgroundLayerComponent1 = new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent();
            this.CurrentGauge = new DevExpress.XtraGauges.Win.GaugeControl();
            this.digitalBackgroundLayerComponent2 = new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent();
            this.PowerSourceList = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.PowerSourceIPColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ifOnlineStateColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.ChanelTabs = new DevExpress.XtraTab.XtraTabControl();
            this.ChanelStatus = new DevExpress.XtraTab.XtraTabPage();
            this.CurrentLabel = new System.Windows.Forms.Label();
            this.VoltageLabel = new System.Windows.Forms.Label();
            this.UpdateLabel = new System.Windows.Forms.Label();
            this.CurrentEdit = new DevExpress.XtraEditors.SpinEdit();
            this.OnOffLabel = new System.Windows.Forms.Label();
            this.VoltageEdit = new DevExpress.XtraEditors.SpinEdit();
            this.CurrentChart = new DevExpress.XtraCharts.ChartControl();
            this.VoltageChart = new DevExpress.XtraCharts.ChartControl();
            this.OnButton = new DevExpress.XtraEditors.SimpleButton();
            this.TaskListControl = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.TaskNameColomn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.UpdateButton = new DevExpress.XtraEditors.SimpleButton();
            this.ChanelLog = new DevExpress.XtraTab.XtraTabPage();
            this.LogGridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Level = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Message = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TimeStamp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.User = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Host = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ChanelSettings = new DevExpress.XtraTab.XtraTabPage();
            this.isOnlineGauge = new DevExpress.XtraGauges.Win.GaugeControl();
            this.stateIndicatorGauge = new DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorGauge();
            this.OnLineGauge = new DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorComponent();
            this.dGauge1 = new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalGauge();
            this.digitalGauge1 = new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalGauge();
            this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
            this.layoutViewField_layoutViewColumn1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewField_layoutViewColumn1_3 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewField_layoutViewColumn1_7 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.item1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.item2 = new DevExpress.XtraLayout.SimpleSeparator();
            this.layoutViewField_layoutViewColumn1_1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewField_layoutViewColumn1_2 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            ((System.ComponentModel.ISupportInitialize)(this.PowerSourceChanelList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateIndicatorGauge1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateIndicatorComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.digitalBackgroundLayerComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.digitalBackgroundLayerComponent2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PowerSourceList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChanelTabs)).BeginInit();
            this.ChanelTabs.SuspendLayout();
            this.ChanelStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoltageEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoltageChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TaskListControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.ChanelLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateIndicatorGauge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OnLineGauge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGauge1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.digitalGauge1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.item1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.item2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_2)).BeginInit();
            this.SuspendLayout();
            // 
            // PowerSourceChanelList
            // 
            this.PowerSourceChanelList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PowerSourceChanelList.Location = new System.Drawing.Point(333, 0);
            this.PowerSourceChanelList.MainView = this.layoutView1;
            this.PowerSourceChanelList.Name = "PowerSourceChanelList";
            this.PowerSourceChanelList.Size = new System.Drawing.Size(1177, 425);
            this.PowerSourceChanelList.TabIndex = 0;
            this.PowerSourceChanelList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutView1});
            // 
            // layoutView1
            // 
            this.layoutView1.ActiveFilterEnabled = false;
            this.layoutView1.Appearance.Card.BorderColor = System.Drawing.Color.Black;
            this.layoutView1.Appearance.Card.Options.UseBorderColor = true;
            this.layoutView1.Appearance.FocusedCardCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.layoutView1.Appearance.FocusedCardCaption.Options.UseFont = true;
            this.layoutView1.CardCaptionFormat = " Канал [ {0} из {1} ]";
            this.layoutView1.CardMinSize = new System.Drawing.Size(165, 114);
            this.layoutView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.PowerColumn,
            this.CurrentColumn,
            this.AddressColumn,
            this.ChanelIdColumn,
            this.VoltageColumn,
            this.StatusColumn});
            this.layoutView1.GridControl = this.PowerSourceChanelList;
            this.layoutView1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_layoutViewColumn1_1,
            this.layoutViewField_layoutViewColumn1_2});
            this.layoutView1.Name = "layoutView1";
            this.layoutView1.OptionsBehavior.AllowExpandCollapse = false;
            this.layoutView1.OptionsCustomization.AllowFilter = false;
            this.layoutView1.OptionsCustomization.AllowSort = false;
            this.layoutView1.OptionsView.CardsAlignment = DevExpress.XtraGrid.Views.Layout.CardsAlignment.Near;
            this.layoutView1.OptionsView.DefaultColumnCount = 4;
            this.layoutView1.OptionsView.ShowCardExpandButton = false;
            this.layoutView1.OptionsView.ShowHeaderPanel = false;
            this.layoutView1.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.MultiRow;
            this.layoutView1.TemplateCard = this.layoutViewCard1;
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
            this.layoutViewField_layoutViewColumn1_4.EditorPreferredWidth = 117;
            this.layoutViewField_layoutViewColumn1_4.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_layoutViewColumn1_4.Name = "layoutViewField_layoutViewColumn1_4";
            this.layoutViewField_layoutViewColumn1_4.Size = new System.Drawing.Size(165, 26);
            this.layoutViewField_layoutViewColumn1_4.TextSize = new System.Drawing.Size(43, 20);
            // 
            // CurrentColumn
            // 
            this.CurrentColumn.Caption = "I";
            this.CurrentColumn.FieldName = "RecentCurrentDisplay";
            this.CurrentColumn.LayoutViewField = this.layoutViewField_layoutViewColumn1_3;
            this.CurrentColumn.Name = "CurrentColumn";
            // 
            // AddressColumn
            // 
            this.AddressColumn.Caption = "Address";
            this.AddressColumn.FieldName = "Address";
            this.AddressColumn.LayoutViewField = this.layoutViewField_layoutViewColumn1_2;
            this.AddressColumn.Name = "AddressColumn";
            // 
            // ChanelIdColumn
            // 
            this.ChanelIdColumn.Caption = "ID";
            this.ChanelIdColumn.FieldName = "ChanelId";
            this.ChanelIdColumn.LayoutViewField = this.layoutViewField_layoutViewColumn1_1;
            this.ChanelIdColumn.Name = "ChanelIdColumn";
            // 
            // VoltageColumn
            // 
            this.VoltageColumn.Caption = "V";
            this.VoltageColumn.FieldName = "RecentVoltageDisplay";
            this.VoltageColumn.LayoutViewField = this.layoutViewField_layoutViewColumn1;
            this.VoltageColumn.Name = "VoltageColumn";
            // 
            // StatusColumn
            // 
            this.StatusColumn.Caption = "Status";
            this.StatusColumn.FieldName = "Status";
            this.StatusColumn.LayoutViewField = this.layoutViewField_layoutViewColumn1_7;
            this.StatusColumn.Name = "StatusColumn";
            // 
            // StatusGauge
            // 
            this.StatusGauge.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[] {
            this.stateIndicatorGauge1});
            this.StatusGauge.Location = new System.Drawing.Point(1319, 42);
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
            this.VoltageGauge.Location = new System.Drawing.Point(1319, 98);
            this.VoltageGauge.Name = "VoltageGauge";
            this.VoltageGauge.Size = new System.Drawing.Size(150, 60);
            this.VoltageGauge.TabIndex = 2;
            this.VoltageGauge.Visible = false;
            // 
            // digitalBackgroundLayerComponent1
            // 
            this.digitalBackgroundLayerComponent1.BottomRight = new DevExpress.XtraGauges.Core.Base.PointF2D(211.85F, 99.9625F);
            this.digitalBackgroundLayerComponent1.Name = "bg1";
            this.digitalBackgroundLayerComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.DigitalBackgroundShapeSetType.Style3;
            this.digitalBackgroundLayerComponent1.TopLeft = new DevExpress.XtraGauges.Core.Base.PointF2D(20F, 0F);
            this.digitalBackgroundLayerComponent1.ZOrder = 1000;
            // 
            // CurrentGauge
            // 
            this.CurrentGauge.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[] {
            this.digitalGauge1});
            this.CurrentGauge.Location = new System.Drawing.Point(1319, 164);
            this.CurrentGauge.Name = "CurrentGauge";
            this.CurrentGauge.Size = new System.Drawing.Size(150, 60);
            this.CurrentGauge.TabIndex = 3;
            this.CurrentGauge.Visible = false;
            // 
            // digitalBackgroundLayerComponent2
            // 
            this.digitalBackgroundLayerComponent2.BottomRight = new DevExpress.XtraGauges.Core.Base.PointF2D(211.85F, 99.9625F);
            this.digitalBackgroundLayerComponent2.Name = "bg1";
            this.digitalBackgroundLayerComponent2.ShapeType = DevExpress.XtraGauges.Core.Model.DigitalBackgroundShapeSetType.Style3;
            this.digitalBackgroundLayerComponent2.TopLeft = new DevExpress.XtraGauges.Core.Base.PointF2D(20F, 0F);
            this.digitalBackgroundLayerComponent2.ZOrder = 1000;
            // 
            // PowerSourceList
            // 
            this.PowerSourceList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PowerSourceList.Location = new System.Drawing.Point(-1, 0);
            this.PowerSourceList.MainView = this.gridView1;
            this.PowerSourceList.Name = "PowerSourceList";
            this.PowerSourceList.Size = new System.Drawing.Size(303, 651);
            this.PowerSourceList.TabIndex = 5;
            this.PowerSourceList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.PowerSourceIPColumn,
            this.ifOnlineStateColumn});
            this.gridView1.GridControl = this.PowerSourceList;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // PowerSourceIPColumn
            // 
            this.PowerSourceIPColumn.Caption = "Адрес";
            this.PowerSourceIPColumn.FieldName = "DisplayName";
            this.PowerSourceIPColumn.Name = "PowerSourceIPColumn";
            this.PowerSourceIPColumn.OptionsColumn.AllowEdit = false;
            this.PowerSourceIPColumn.Visible = true;
            this.PowerSourceIPColumn.VisibleIndex = 0;
            // 
            // ifOnlineStateColumn
            // 
            this.ifOnlineStateColumn.Caption = "Статус";
            this.ifOnlineStateColumn.FieldName = "IsOnline";
            this.ifOnlineStateColumn.Name = "ifOnlineStateColumn";
            this.ifOnlineStateColumn.OptionsColumn.ReadOnly = true;
            this.ifOnlineStateColumn.Visible = true;
            this.ifOnlineStateColumn.VisibleIndex = 1;
            // 
            // separatorControl1
            // 
            this.separatorControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.separatorControl1.LineOrientation = System.Windows.Forms.Orientation.Vertical;
            this.separatorControl1.LineThickness = 3;
            this.separatorControl1.Location = new System.Drawing.Point(308, 0);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(19, 651);
            this.separatorControl1.TabIndex = 6;
            // 
            // ChanelTabs
            // 
            this.ChanelTabs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChanelTabs.Location = new System.Drawing.Point(333, 431);
            this.ChanelTabs.Name = "ChanelTabs";
            this.ChanelTabs.SelectedTabPage = this.ChanelStatus;
            this.ChanelTabs.Size = new System.Drawing.Size(1177, 220);
            this.ChanelTabs.TabIndex = 7;
            this.ChanelTabs.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.ChanelStatus,
            this.ChanelLog,
            this.ChanelSettings});
            // 
            // ChanelStatus
            // 
            this.ChanelStatus.Controls.Add(this.CurrentLabel);
            this.ChanelStatus.Controls.Add(this.VoltageLabel);
            this.ChanelStatus.Controls.Add(this.UpdateLabel);
            this.ChanelStatus.Controls.Add(this.CurrentEdit);
            this.ChanelStatus.Controls.Add(this.OnOffLabel);
            this.ChanelStatus.Controls.Add(this.VoltageEdit);
            this.ChanelStatus.Controls.Add(this.CurrentChart);
            this.ChanelStatus.Controls.Add(this.VoltageChart);
            this.ChanelStatus.Controls.Add(this.OnButton);
            this.ChanelStatus.Controls.Add(this.TaskListControl);
            this.ChanelStatus.Controls.Add(this.UpdateButton);
            this.ChanelStatus.Name = "ChanelStatus";
            this.ChanelStatus.Size = new System.Drawing.Size(1171, 192);
            this.ChanelStatus.Text = "Статус";
            // 
            // CurrentLabel
            // 
            this.CurrentLabel.AutoSize = true;
            this.CurrentLabel.Font = new System.Drawing.Font("Nunito Sans", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentLabel.Location = new System.Drawing.Point(13, 112);
            this.CurrentLabel.Name = "CurrentLabel";
            this.CurrentLabel.Size = new System.Drawing.Size(33, 54);
            this.CurrentLabel.TabIndex = 13;
            this.CurrentLabel.Text = "I";
            // 
            // VoltageLabel
            // 
            this.VoltageLabel.AutoSize = true;
            this.VoltageLabel.Font = new System.Drawing.Font("Nunito Sans", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VoltageLabel.Location = new System.Drawing.Point(4, 27);
            this.VoltageLabel.Name = "VoltageLabel";
            this.VoltageLabel.Size = new System.Drawing.Size(51, 54);
            this.VoltageLabel.TabIndex = 12;
            this.VoltageLabel.Text = "V";
            // 
            // UpdateLabel
            // 
            this.UpdateLabel.AutoSize = true;
            this.UpdateLabel.Font = new System.Drawing.Font("Nunito Sans", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateLabel.Location = new System.Drawing.Point(573, 121);
            this.UpdateLabel.Name = "UpdateLabel";
            this.UpdateLabel.Size = new System.Drawing.Size(133, 46);
            this.UpdateLabel.TabIndex = 10;
            this.UpdateLabel.Text = "Update";
            // 
            // CurrentEdit
            // 
            this.CurrentEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CurrentEdit.Location = new System.Drawing.Point(367, 95);
            this.CurrentEdit.Name = "CurrentEdit";
            this.CurrentEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 52F);
            this.CurrentEdit.Properties.Appearance.Options.UseFont = true;
            this.CurrentEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CurrentEdit.Properties.Mask.EditMask = "\\d{1}(\\R.\\d{0,3})";
            this.CurrentEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.CurrentEdit.Properties.MaxValue = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.CurrentEdit.Size = new System.Drawing.Size(200, 90);
            this.CurrentEdit.TabIndex = 8;
            // 
            // OnOffLabel
            // 
            this.OnOffLabel.AutoSize = true;
            this.OnOffLabel.Font = new System.Drawing.Font("Nunito Sans", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OnOffLabel.Location = new System.Drawing.Point(573, 27);
            this.OnOffLabel.Name = "OnOffLabel";
            this.OnOffLabel.Size = new System.Drawing.Size(141, 46);
            this.OnOffLabel.TabIndex = 11;
            this.OnOffLabel.Text = "On / Off";
            // 
            // VoltageEdit
            // 
            this.VoltageEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.VoltageEdit.Location = new System.Drawing.Point(367, 3);
            this.VoltageEdit.Name = "VoltageEdit";
            this.VoltageEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 52F);
            this.VoltageEdit.Properties.Appearance.Options.UseFont = true;
            this.VoltageEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.VoltageEdit.Properties.Mask.EditMask = "\\d{1,2}(\\R.\\d{0,3})";
            this.VoltageEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.VoltageEdit.Properties.MaxValue = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.VoltageEdit.Size = new System.Drawing.Size(200, 90);
            this.VoltageEdit.TabIndex = 7;
            // 
            // CurrentChart
            // 
            this.CurrentChart.DataBindings = null;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.CurrentChart.Diagram = xyDiagram1;
            this.CurrentChart.Legend.Name = "Default Legend";
            this.CurrentChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.CurrentChart.Location = new System.Drawing.Point(61, 95);
            this.CurrentChart.Name = "CurrentChart";
            series1.ArgumentDataMember = "MeasuredAt";
            series1.CrosshairHighlightPoints = DevExpress.Utils.DefaultBoolean.False;
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series1.Name = "VoltageLine";
            series1.ValueDataMembersSerializable = "Current";
            series1.View = lineSeriesView1;
            this.CurrentChart.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.CurrentChart.Size = new System.Drawing.Size(300, 90);
            this.CurrentChart.TabIndex = 2;
            // 
            // VoltageChart
            // 
            this.VoltageChart.DataBindings = null;
            xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            this.VoltageChart.Diagram = xyDiagram2;
            this.VoltageChart.Legend.Name = "Default Legend";
            this.VoltageChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.VoltageChart.Location = new System.Drawing.Point(61, 2);
            this.VoltageChart.Name = "VoltageChart";
            series2.ArgumentDataMember = "MeasuredAt";
            series2.CrosshairHighlightPoints = DevExpress.Utils.DefaultBoolean.False;
            series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series2.Name = "VoltageLine";
            series2.ValueDataMembersSerializable = "Voltage";
            series2.View = lineSeriesView2;
            this.VoltageChart.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2};
            this.VoltageChart.Size = new System.Drawing.Size(300, 90);
            this.VoltageChart.TabIndex = 1;
            // 
            // OnButton
            // 
            this.OnButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("OnButton.ImageOptions.Image")));
            this.OnButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.OnButton.Location = new System.Drawing.Point(720, 29);
            this.OnButton.Name = "OnButton";
            this.OnButton.Size = new System.Drawing.Size(45, 45);
            this.OnButton.TabIndex = 9;
            this.OnButton.Click += new System.EventHandler(this.OnButton_Click);
            // 
            // TaskListControl
            // 
            this.TaskListControl.Location = new System.Drawing.Point(812, 4);
            this.TaskListControl.MainView = this.gridView2;
            this.TaskListControl.Name = "TaskListControl";
            this.TaskListControl.Size = new System.Drawing.Size(356, 185);
            this.TaskListControl.TabIndex = 0;
            this.TaskListControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.TaskNameColomn});
            this.gridView2.GridControl = this.TaskListControl;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsCustomization.AllowFilter = false;
            this.gridView2.OptionsCustomization.AllowSort = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // TaskNameColomn
            // 
            this.TaskNameColomn.Caption = "Текущие задачи";
            this.TaskNameColomn.FieldName = "DisplayName";
            this.TaskNameColomn.Name = "TaskNameColomn";
            this.TaskNameColomn.Visible = true;
            this.TaskNameColomn.VisibleIndex = 0;
            // 
            // UpdateButton
            // 
            this.UpdateButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("UpdateButton.ImageOptions.Image")));
            this.UpdateButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.UpdateButton.Location = new System.Drawing.Point(720, 121);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(45, 45);
            this.UpdateButton.TabIndex = 5;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // ChanelLog
            // 
            this.ChanelLog.Controls.Add(this.LogGridControl);
            this.ChanelLog.Name = "ChanelLog";
            this.ChanelLog.Size = new System.Drawing.Size(1171, 192);
            this.ChanelLog.Text = "Лог";
            // 
            // LogGridControl
            // 
            this.LogGridControl.Location = new System.Drawing.Point(3, 3);
            this.LogGridControl.MainView = this.gridView3;
            this.LogGridControl.Name = "LogGridControl";
            this.LogGridControl.Size = new System.Drawing.Size(1165, 186);
            this.LogGridControl.TabIndex = 0;
            this.LogGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Level,
            this.Message,
            this.TimeStamp,
            this.User,
            this.Host});
            this.gridView3.GridControl = this.LogGridControl;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsCustomization.AllowGroup = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // Level
            // 
            this.Level.Caption = "Тип события";
            this.Level.FieldName = "Level";
            this.Level.Name = "Level";
            this.Level.Visible = true;
            this.Level.VisibleIndex = 0;
            // 
            // Message
            // 
            this.Message.Caption = "Событие";
            this.Message.FieldName = "Message";
            this.Message.Name = "Message";
            this.Message.Visible = true;
            this.Message.VisibleIndex = 1;
            // 
            // TimeStamp
            // 
            this.TimeStamp.Caption = "Дата";
            this.TimeStamp.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.TimeStamp.FieldName = "TimeStamp";
            this.TimeStamp.Name = "TimeStamp";
            this.TimeStamp.Visible = true;
            this.TimeStamp.VisibleIndex = 2;
            // 
            // User
            // 
            this.User.Caption = "Пользователь";
            this.User.FieldName = "User";
            this.User.Name = "User";
            this.User.Visible = true;
            this.User.VisibleIndex = 3;
            // 
            // Host
            // 
            this.Host.Caption = "Устройство";
            this.Host.FieldName = "Host";
            this.Host.Name = "Host";
            this.Host.Visible = true;
            this.Host.VisibleIndex = 4;
            // 
            // ChanelSettings
            // 
            this.ChanelSettings.Name = "ChanelSettings";
            this.ChanelSettings.Size = new System.Drawing.Size(1171, 192);
            this.ChanelSettings.Text = "Настройки";
            // 
            // isOnlineGauge
            // 
            this.isOnlineGauge.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[] {
            this.stateIndicatorGauge});
            this.isOnlineGauge.Location = new System.Drawing.Point(117, 118);
            this.isOnlineGauge.Name = "isOnlineGauge";
            this.isOnlineGauge.Size = new System.Drawing.Size(35, 35);
            this.isOnlineGauge.TabIndex = 8;
            this.isOnlineGauge.Visible = false;
            // 
            // stateIndicatorGauge
            // 
            this.stateIndicatorGauge.Bounds = new System.Drawing.Rectangle(6, 6, 23, 23);
            this.stateIndicatorGauge.Indicators.AddRange(new DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorComponent[] {
            this.OnLineGauge});
            this.stateIndicatorGauge.Name = "stateIndicatorGauge";
            // 
            // OnLineGauge
            // 
            this.OnLineGauge.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(124F, 124F);
            this.OnLineGauge.Name = "stateIndicatorComponent1";
            this.OnLineGauge.Size = new System.Drawing.SizeF(200F, 200F);
            this.OnLineGauge.StateIndex = 3;
            indicatorState5.Name = "State1";
            indicatorState5.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight1;
            indicatorState6.Name = "State2";
            indicatorState6.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight2;
            indicatorState7.Name = "State3";
            indicatorState7.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight3;
            indicatorState8.Name = "State4";
            indicatorState8.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight4;
            this.OnLineGauge.States.AddRange(new DevExpress.XtraGauges.Core.Model.IIndicatorState[] {
            indicatorState5,
            indicatorState6,
            indicatorState7,
            indicatorState8});
            // 
            // dGauge1
            // 
            this.dGauge1.AppearanceOff.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#0FFF5000");
            this.dGauge1.AppearanceOn.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#FF5000");
            this.dGauge1.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent[] {
            this.digitalBackgroundLayerComponent1});
            this.dGauge1.Bounds = new System.Drawing.Rectangle(6, 6, 138, 48);
            this.dGauge1.DigitCount = 4;
            this.dGauge1.Name = "dGauge1";
            this.dGauge1.Text = "00,00";
            // 
            // digitalGauge1
            // 
            this.digitalGauge1.AppearanceOff.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#0FFF5000");
            this.digitalGauge1.AppearanceOn.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#FF5000");
            this.digitalGauge1.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent[] {
            this.digitalBackgroundLayerComponent2});
            this.digitalGauge1.Bounds = new System.Drawing.Rectangle(6, 6, 138, 48);
            this.digitalGauge1.DigitCount = 4;
            this.digitalGauge1.Name = "digitalGauge1";
            this.digitalGauge1.Text = "0,000";
            // 
            // layoutViewCard1
            // 
            this.layoutViewCard1.CustomizationFormText = "TemplateCard";
            this.layoutViewCard1.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutViewCard1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_layoutViewColumn1,
            this.layoutViewField_layoutViewColumn1_3,
            this.layoutViewField_layoutViewColumn1_7,
            this.item1,
            this.item2});
            this.layoutViewCard1.Name = "layoutViewTemplateCard";
            this.layoutViewCard1.OptionsItemText.TextToControlDistance = 5;
            this.layoutViewCard1.Text = "TemplateCard";
            // 
            // layoutViewField_layoutViewColumn1
            // 
            this.layoutViewField_layoutViewColumn1.EditorPreferredWidth = 101;
            this.layoutViewField_layoutViewColumn1.Location = new System.Drawing.Point(0, 28);
            this.layoutViewField_layoutViewColumn1.Name = "layoutViewField_layoutViewColumn1";
            this.layoutViewField_layoutViewColumn1.Size = new System.Drawing.Size(145, 24);
            this.layoutViewField_layoutViewColumn1.TextSize = new System.Drawing.Size(35, 13);
            // 
            // layoutViewField_layoutViewColumn1_3
            // 
            this.layoutViewField_layoutViewColumn1_3.EditorPreferredWidth = 101;
            this.layoutViewField_layoutViewColumn1_3.Location = new System.Drawing.Point(0, 52);
            this.layoutViewField_layoutViewColumn1_3.Name = "layoutViewField_layoutViewColumn1_3";
            this.layoutViewField_layoutViewColumn1_3.Size = new System.Drawing.Size(145, 24);
            this.layoutViewField_layoutViewColumn1_3.TextSize = new System.Drawing.Size(35, 13);
            // 
            // layoutViewField_layoutViewColumn1_7
            // 
            this.layoutViewField_layoutViewColumn1_7.EditorPreferredWidth = 101;
            this.layoutViewField_layoutViewColumn1_7.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_layoutViewColumn1_7.Name = "layoutViewField_layoutViewColumn1_7";
            this.layoutViewField_layoutViewColumn1_7.Size = new System.Drawing.Size(145, 24);
            this.layoutViewField_layoutViewColumn1_7.TextSize = new System.Drawing.Size(35, 13);
            // 
            // item1
            // 
            this.item1.AllowHotTrack = false;
            this.item1.CustomizationFormText = "item1";
            this.item1.Location = new System.Drawing.Point(0, 24);
            this.item1.Name = "item1";
            this.item1.Size = new System.Drawing.Size(145, 2);
            // 
            // item2
            // 
            this.item2.AllowHotTrack = false;
            this.item2.CustomizationFormText = "item2";
            this.item2.Location = new System.Drawing.Point(0, 26);
            this.item2.Name = "item2";
            this.item2.Size = new System.Drawing.Size(145, 2);
            // 
            // layoutViewField_layoutViewColumn1_1
            // 
            this.layoutViewField_layoutViewColumn1_1.EditorPreferredWidth = 20;
            this.layoutViewField_layoutViewColumn1_1.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_layoutViewColumn1_1.Name = "layoutViewField_layoutViewColumn1_1";
            this.layoutViewField_layoutViewColumn1_1.Size = new System.Drawing.Size(145, 76);
            this.layoutViewField_layoutViewColumn1_1.TextSize = new System.Drawing.Size(55, 13);
            // 
            // layoutViewField_layoutViewColumn1_2
            // 
            this.layoutViewField_layoutViewColumn1_2.EditorPreferredWidth = 20;
            this.layoutViewField_layoutViewColumn1_2.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_layoutViewColumn1_2.Name = "layoutViewField_layoutViewColumn1_2";
            this.layoutViewField_layoutViewColumn1_2.Size = new System.Drawing.Size(145, 76);
            this.layoutViewField_layoutViewColumn1_2.TextSize = new System.Drawing.Size(55, 13);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1508, 651);
            this.Controls.Add(this.isOnlineGauge);
            this.Controls.Add(this.ChanelTabs);
            this.Controls.Add(this.separatorControl1);
            this.Controls.Add(this.PowerSourceList);
            this.Controls.Add(this.CurrentGauge);
            this.Controls.Add(this.VoltageGauge);
            this.Controls.Add(this.StatusGauge);
            this.Controls.Add(this.PowerSourceChanelList);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Glow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.PowerSourceChanelList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateIndicatorGauge1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateIndicatorComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.digitalBackgroundLayerComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.digitalBackgroundLayerComponent2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PowerSourceList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChanelTabs)).EndInit();
            this.ChanelTabs.ResumeLayout(false);
            this.ChanelStatus.ResumeLayout(false);
            this.ChanelStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoltageEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoltageChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TaskListControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ChanelLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateIndicatorGauge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OnLineGauge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGauge1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.digitalGauge1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.item1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.item2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl PowerSourceChanelList;
        private DevExpress.XtraGrid.Views.Layout.LayoutView layoutView1;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn VoltageColumn;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn PowerColumn;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn CurrentColumn;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn AddressColumn;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn ChanelIdColumn;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn StatusColumn;
        private DevExpress.XtraGauges.Win.GaugeControl StatusGauge;
        private DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorGauge stateIndicatorGauge1;
        private DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorComponent stateIndicatorComponent1;
        private DevExpress.XtraGauges.Win.GaugeControl VoltageGauge;
        private DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent digitalBackgroundLayerComponent1;
        private DevExpress.XtraGauges.Win.GaugeControl CurrentGauge;
        private DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent digitalBackgroundLayerComponent2;
        private DevExpress.XtraGrid.GridControl PowerSourceList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn PowerSourceIPColumn;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn1_4;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraTab.XtraTabControl ChanelTabs;
        private DevExpress.XtraTab.XtraTabPage ChanelStatus;
        private DevExpress.XtraTab.XtraTabPage ChanelLog;
        private DevExpress.XtraTab.XtraTabPage ChanelSettings;
        private DevExpress.XtraGrid.Columns.GridColumn ifOnlineStateColumn;
        private DevExpress.XtraGauges.Win.GaugeControl isOnlineGauge;
        private DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorGauge stateIndicatorGauge;
        private DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorComponent OnLineGauge;
        private DevExpress.XtraGrid.GridControl TaskListControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn TaskNameColomn;
        private DevExpress.XtraCharts.ChartControl VoltageChart;
        private DevExpress.XtraCharts.ChartControl CurrentChart;
        private DevExpress.XtraEditors.SimpleButton UpdateButton;
        private DevExpress.XtraEditors.SpinEdit VoltageEdit;
        private DevExpress.XtraEditors.SpinEdit CurrentEdit;
        private DevExpress.XtraEditors.SimpleButton OnButton;
        private System.Windows.Forms.Label UpdateLabel;
        private System.Windows.Forms.Label OnOffLabel;
        private System.Windows.Forms.Label CurrentLabel;
        private System.Windows.Forms.Label VoltageLabel;
        private DevExpress.XtraGrid.GridControl LogGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn Level;
        private DevExpress.XtraGrid.Columns.GridColumn Message;
        private DevExpress.XtraGrid.Columns.GridColumn TimeStamp;
        private DevExpress.XtraGrid.Columns.GridColumn User;
        private DevExpress.XtraGrid.Columns.GridColumn Host;
        private DevExpress.XtraGauges.Win.Gauges.Digital.DigitalGauge dGauge1;
        private DevExpress.XtraGauges.Win.Gauges.Digital.DigitalGauge digitalGauge1;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn1_3;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn1_2;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn1_1;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn1;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn1_7;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCard1;
        private DevExpress.XtraLayout.SimpleSeparator item1;
        private DevExpress.XtraLayout.SimpleSeparator item2;
    }
}

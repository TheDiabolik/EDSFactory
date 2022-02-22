namespace EDSFactory 
{
    partial class SettingsModal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsModal));
            this.formAssistant1 = new DevExpress.XtraBars.FormAssistant();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.m_treeViewEDSType = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.ımageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.m_checkEditDeleteLog = new DevExpress.XtraEditors.CheckEdit();
            this.m_checkEditAutoStart = new DevExpress.XtraEditors.CheckEdit();
            this.m_checkEditAlwaysSearching = new DevExpress.XtraEditors.CheckEdit();
            this.m_comboBoxEditDeleteLogPeriod = new DevExpress.XtraEditors.ComboBoxEdit();
            this.m_comboBoxEditAutoStartProgram = new DevExpress.XtraEditors.ComboBoxEdit();
            this.m_comboBoxEditAlwaysSearching = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_checkedListBoxControlAutoStartHelperModuls = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.m_simpleButtonApply = new DevExpress.XtraEditors.SimpleButton();
            this.m_simpleButtonSave = new DevExpress.XtraEditors.SimpleButton();
            this.workPlanControl1 = new WorkPlanSchema.WorkPlanControl();
            ((System.ComponentModel.ISupportInitialize)(this.m_treeViewEDSType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ımageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_checkEditDeleteLog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_checkEditAutoStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_checkEditAlwaysSearching.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_comboBoxEditDeleteLogPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_comboBoxEditAutoStartProgram.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_comboBoxEditAlwaysSearching.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_checkedListBoxControlAutoStartHelperModuls)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Blueprint";
            // 
            // m_treeViewEDSType
            // 
            this.m_treeViewEDSType.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.m_treeViewEDSType.Location = new System.Drawing.Point(12, 220);
            this.m_treeViewEDSType.Name = "m_treeViewEDSType";
            this.m_treeViewEDSType.BeginUnboundLoad();
            this.m_treeViewEDSType.AppendNode(new object[] {
            "Park İhlal Elektronik Denetleme Sistemi"}, -1, -1, 0, 1);
            this.m_treeViewEDSType.AppendNode(new object[] {
            "Sabit"}, 0, -1, 0, 3);
            this.m_treeViewEDSType.AppendNode(new object[] {
            "Mobil"}, 0, 0, 0, 2);
            this.m_treeViewEDSType.AppendNode(new object[] {
            "Koridor Hız Elektronik Denetleme Sistemi"}, -1, 0, 0, 6);
            this.m_treeViewEDSType.AppendNode(new object[] {
            "Dar"}, 3, 0, 0, 4);
            this.m_treeViewEDSType.AppendNode(new object[] {
            "Geniş"}, 3, 0, 0, 5);
            this.m_treeViewEDSType.AppendNode(new object[] {
            "Emniyet Şeridi Elektronik Denetleme Sistemi"}, -1, 0, 0, 0);
            this.m_treeViewEDSType.AppendNode(new object[] {
            "Sabit"}, 6, -1, 0, 3);
            this.m_treeViewEDSType.AppendNode(new object[] {
            "Mobil"}, 6, 0, 0, 2);
            this.m_treeViewEDSType.AppendNode(new object[] {
            "Duraklama Elektronik Denetleme Sistemi"}, -1, 0, 0, 7);
            this.m_treeViewEDSType.AppendNode(new object[] {
            "Ofset Tarama Elektronik Denetleme Sistemi"}, -1, 0, 0, 8);
            this.m_treeViewEDSType.EndUnboundLoad();
            this.m_treeViewEDSType.OptionsView.ExpandButtonCentered = false;
            this.m_treeViewEDSType.OptionsView.ShowColumns = false;
            this.m_treeViewEDSType.OptionsView.ShowIndicator = false;
            this.m_treeViewEDSType.Size = new System.Drawing.Size(281, 206);
            this.m_treeViewEDSType.StateImageList = this.ımageCollection1;
            this.m_treeViewEDSType.TabIndex = 0;
            this.m_treeViewEDSType.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.m_treeViewEDSType_FocusedNodeChanged);
            this.m_treeViewEDSType.SelectionChanged += new System.EventHandler(this.m_treeViewEDSType_SelectionChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "treeListColumn1";
            this.treeListColumn1.FieldName = "treeListColumn1";
            this.treeListColumn1.MinWidth = 69;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.AllowEdit = false;
            this.treeListColumn1.OptionsColumn.AllowMove = false;
            this.treeListColumn1.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.treeListColumn1.OptionsColumn.ReadOnly = true;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            this.treeListColumn1.Width = 129;
            // 
            // ımageCollection1
            // 
            this.ımageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("ımageCollection1.ImageStream")));
            this.ımageCollection1.Images.SetKeyName(0, "Highway1.png");
            this.ımageCollection1.Images.SetKeyName(1, "parking.jpg");
            this.ımageCollection1.Images.SetKeyName(2, "mobile.jpg");
            this.ımageCollection1.Images.SetKeyName(3, "fixed.jpg");
            this.ımageCollection1.Images.SetKeyName(4, "narrow.png");
            this.ımageCollection1.Images.SetKeyName(5, "wide.png");
            this.ımageCollection1.Images.SetKeyName(6, "corridor.png");
            this.ımageCollection1.Images.SetKeyName(7, "standing.jpg");
            this.ımageCollection1.Images.SetKeyName(8, "PixelKit_road_cone_icon.png");
            // 
            // m_checkEditDeleteLog
            // 
            this.m_checkEditDeleteLog.Location = new System.Drawing.Point(309, 220);
            this.m_checkEditDeleteLog.Name = "m_checkEditDeleteLog";
            this.m_checkEditDeleteLog.Properties.Caption = "Loglar Silinsin :";
            this.m_checkEditDeleteLog.Size = new System.Drawing.Size(106, 19);
            this.m_checkEditDeleteLog.TabIndex = 34;
            this.m_checkEditDeleteLog.CheckedChanged += new System.EventHandler(this.m_checkBoxEdits_CheckedChanged);
            // 
            // m_checkEditAutoStart
            // 
            this.m_checkEditAutoStart.Location = new System.Drawing.Point(309, 245);
            this.m_checkEditAutoStart.Name = "m_checkEditAutoStart";
            this.m_checkEditAutoStart.Properties.Caption = "Otomatik Başlat :";
            this.m_checkEditAutoStart.Size = new System.Drawing.Size(106, 19);
            this.m_checkEditAutoStart.TabIndex = 35;
            this.m_checkEditAutoStart.CheckedChanged += new System.EventHandler(this.m_checkBoxEdits_CheckedChanged);
            // 
            // m_checkEditAlwaysSearching
            // 
            this.m_checkEditAlwaysSearching.Location = new System.Drawing.Point(309, 270);
            this.m_checkEditAlwaysSearching.Name = "m_checkEditAlwaysSearching";
            this.m_checkEditAlwaysSearching.Properties.Caption = "Sürekli Tarama";
            this.m_checkEditAlwaysSearching.Size = new System.Drawing.Size(106, 19);
            this.m_checkEditAlwaysSearching.TabIndex = 36;
            this.m_checkEditAlwaysSearching.CheckedChanged += new System.EventHandler(this.m_checkBoxEdits_CheckedChanged);
            // 
            // m_comboBoxEditDeleteLogPeriod
            // 
            this.m_comboBoxEditDeleteLogPeriod.Location = new System.Drawing.Point(444, 220);
            this.m_comboBoxEditDeleteLogPeriod.Name = "m_comboBoxEditDeleteLogPeriod";
            this.m_comboBoxEditDeleteLogPeriod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_comboBoxEditDeleteLogPeriod.Properties.Items.AddRange(new object[] {
            "Günlük",
            "Haftalık",
            "Aylık",
            "Yıllık"});
            this.m_comboBoxEditDeleteLogPeriod.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.m_comboBoxEditDeleteLogPeriod.Size = new System.Drawing.Size(125, 20);
            this.m_comboBoxEditDeleteLogPeriod.TabIndex = 37;
            // 
            // m_comboBoxEditAutoStartProgram
            // 
            this.m_comboBoxEditAutoStartProgram.Location = new System.Drawing.Point(444, 244);
            this.m_comboBoxEditAutoStartProgram.Name = "m_comboBoxEditAutoStartProgram";
            this.m_comboBoxEditAutoStartProgram.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_comboBoxEditAutoStartProgram.Properties.Items.AddRange(new object[] {
            "Emniyet Şeridi EDS - Sabit",
            "Emniyet Şeridi EDS - Mobil",
            "Park EDS - Sabit",
            "Park EDS - Mobil ",
            "Hız Koridor EDS - Dar",
            "Hız Koridor EDS - Geniş",
            "Duraklama EDS",
            "Ofset Tarama EDS"});
            this.m_comboBoxEditAutoStartProgram.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.m_comboBoxEditAutoStartProgram.Size = new System.Drawing.Size(125, 20);
            this.m_comboBoxEditAutoStartProgram.TabIndex = 38;
            // 
            // m_comboBoxEditAlwaysSearching
            // 
            this.m_comboBoxEditAlwaysSearching.Location = new System.Drawing.Point(444, 269);
            this.m_comboBoxEditAlwaysSearching.Name = "m_comboBoxEditAlwaysSearching";
            this.m_comboBoxEditAlwaysSearching.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_comboBoxEditAlwaysSearching.Properties.Items.AddRange(new object[] {
            "Emniyet Şeridi EDS - Sabit",
            "Emniyet Şeridi EDS - Mobil",
            "Park EDS - Sabit",
            "Park EDS - Mobil ",
            "Hız Koridor EDS - Dar",
            "Hız Koridor EDS - Geniş",
            "Duraklama EDS",
            "Ofset Tarama EDS"});
            this.m_comboBoxEditAlwaysSearching.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.m_comboBoxEditAlwaysSearching.Size = new System.Drawing.Size(125, 20);
            this.m_comboBoxEditAlwaysSearching.TabIndex = 39;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.m_checkedListBoxControlAutoStartHelperModuls);
            this.groupControl1.Location = new System.Drawing.Point(311, 295);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(258, 131);
            this.groupControl1.TabIndex = 40;
            this.groupControl1.Text = "Yardımcı Modüller";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(17, 27);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(82, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Otomatik Başlat :";
            // 
            // m_checkedListBoxControlAutoStartHelperModuls
            // 
            this.m_checkedListBoxControlAutoStartHelperModuls.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Kamera Kontrol"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Zaman Eşitleyici"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Video Oluşturucu")});
            this.m_checkedListBoxControlAutoStartHelperModuls.Location = new System.Drawing.Point(133, 24);
            this.m_checkedListBoxControlAutoStartHelperModuls.Name = "m_checkedListBoxControlAutoStartHelperModuls";
            this.m_checkedListBoxControlAutoStartHelperModuls.Size = new System.Drawing.Size(120, 95);
            this.m_checkedListBoxControlAutoStartHelperModuls.TabIndex = 0;
            // 
            // m_simpleButtonApply
            // 
            this.m_simpleButtonApply.Image = ((System.Drawing.Image)(resources.GetObject("m_simpleButtonApply.Image")));
            this.m_simpleButtonApply.Location = new System.Drawing.Point(494, 432);
            this.m_simpleButtonApply.Name = "m_simpleButtonApply";
            this.m_simpleButtonApply.Size = new System.Drawing.Size(75, 41);
            this.m_simpleButtonApply.TabIndex = 42;
            this.m_simpleButtonApply.Text = "Uygula";
            this.m_simpleButtonApply.Click += new System.EventHandler(this.m_simpleButtons_Click);
            // 
            // m_simpleButtonSave
            // 
            this.m_simpleButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("m_simpleButtonSave.Image")));
            this.m_simpleButtonSave.Location = new System.Drawing.Point(413, 432);
            this.m_simpleButtonSave.Name = "m_simpleButtonSave";
            this.m_simpleButtonSave.Size = new System.Drawing.Size(75, 41);
            this.m_simpleButtonSave.TabIndex = 41;
            this.m_simpleButtonSave.Text = "Kaydet";
            this.m_simpleButtonSave.Click += new System.EventHandler(this.m_simpleButtons_Click);
            // 
            // workPlanControl1
            // 
            this.workPlanControl1.BackColor = System.Drawing.Color.White;
            this.workPlanControl1.ForeColor = System.Drawing.Color.Black;
            this.workPlanControl1.Location = new System.Drawing.Point(12, 12);
            this.workPlanControl1.Name = "workPlanControl1";
            this.workPlanControl1.Size = new System.Drawing.Size(557, 202);
            this.workPlanControl1.TabIndex = 43;
            this.workPlanControl1.WorkingDates = ((System.Collections.Generic.List<string>)(resources.GetObject("workPlanControl1.WorkingDates")));
            // 
            // SettingsModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(581, 484);
            this.Controls.Add(this.workPlanControl1);
            this.Controls.Add(this.m_simpleButtonApply);
            this.Controls.Add(this.m_simpleButtonSave);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.m_comboBoxEditAlwaysSearching);
            this.Controls.Add(this.m_comboBoxEditAutoStartProgram);
            this.Controls.Add(this.m_comboBoxEditDeleteLogPeriod);
            this.Controls.Add(this.m_checkEditAlwaysSearching);
            this.Controls.Add(this.m_checkEditAutoStart);
            this.Controls.Add(this.m_checkEditDeleteLog);
            this.Controls.Add(this.m_treeViewEDSType);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayarlar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsModal_FormClosing);
            this.Load += new System.EventHandler(this.SettingsModal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_treeViewEDSType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ımageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_checkEditDeleteLog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_checkEditAutoStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_checkEditAlwaysSearching.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_comboBoxEditDeleteLogPeriod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_comboBoxEditAutoStartProgram.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_comboBoxEditAlwaysSearching.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_checkedListBoxControlAutoStartHelperModuls)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.FormAssistant formAssistant1;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraTreeList.TreeList m_treeViewEDSType;
        private DevExpress.XtraEditors.CheckEdit m_checkEditDeleteLog;
        private DevExpress.XtraEditors.CheckEdit m_checkEditAutoStart;
        private DevExpress.XtraEditors.CheckEdit m_checkEditAlwaysSearching;
        private DevExpress.XtraEditors.ComboBoxEdit m_comboBoxEditDeleteLogPeriod;
        private DevExpress.XtraEditors.ComboBoxEdit m_comboBoxEditAutoStartProgram;
        private DevExpress.XtraEditors.ComboBoxEdit m_comboBoxEditAlwaysSearching;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckedListBoxControl m_checkedListBoxControlAutoStartHelperModuls;
        private DevExpress.XtraEditors.SimpleButton m_simpleButtonApply;
        private DevExpress.XtraEditors.SimpleButton m_simpleButtonSave;
        private WorkPlanSchema.WorkPlanControl workPlanControl1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.Utils.ImageCollection ımageCollection1;
    }
}
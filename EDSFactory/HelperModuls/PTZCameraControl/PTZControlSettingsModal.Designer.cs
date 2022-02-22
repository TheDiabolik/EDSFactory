namespace EDSFactory 
{
    partial class PTZControlSettingsModal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PTZControlSettingsModal));
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.formAssistant1 = new DevExpress.XtraBars.FormAssistant();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.m_comboBoxEditStopBits = new DevExpress.XtraEditors.ComboBoxEdit();
            this.m_comboBoxEditParity = new DevExpress.XtraEditors.ComboBoxEdit();
            this.m_comboBoxEditDataBits = new DevExpress.XtraEditors.ComboBoxEdit();
            this.m_comboBoxEditBaudRate = new DevExpress.XtraEditors.ComboBoxEdit();
            this.m_comboBoxEditPortName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.m_labelControlStopBits = new DevExpress.XtraEditors.LabelControl();
            this.m_labelControlParity = new DevExpress.XtraEditors.LabelControl();
            this.m_labelControlDataBits = new DevExpress.XtraEditors.LabelControl();
            this.m_labelControlBaudRate = new DevExpress.XtraEditors.LabelControl();
            this.m_labelControlPortName = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.m_spinEditWaitingTime = new DevExpress.XtraEditors.SpinEdit();
            this.m_checkEditStartAuto = new DevExpress.XtraEditors.CheckEdit();
            this.m_radioGroupWaitTimeType = new DevExpress.XtraEditors.RadioGroup();
            this.m_labelControlWaitingTime = new DevExpress.XtraEditors.LabelControl();
            this.m_simpleButtonSave = new DevExpress.XtraEditors.SimpleButton();
            this.m_simpleButtonApply = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_comboBoxEditStopBits.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_comboBoxEditParity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_comboBoxEditDataBits.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_comboBoxEditBaudRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_comboBoxEditPortName.Properties)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_spinEditWaitingTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_checkEditStartAuto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_radioGroupWaitTimeType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Blueprint";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(12, 9);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(300, 206);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.m_comboBoxEditStopBits);
            this.xtraTabPage1.Controls.Add(this.m_comboBoxEditParity);
            this.xtraTabPage1.Controls.Add(this.m_comboBoxEditDataBits);
            this.xtraTabPage1.Controls.Add(this.m_comboBoxEditBaudRate);
            this.xtraTabPage1.Controls.Add(this.m_comboBoxEditPortName);
            this.xtraTabPage1.Controls.Add(this.m_labelControlStopBits);
            this.xtraTabPage1.Controls.Add(this.m_labelControlParity);
            this.xtraTabPage1.Controls.Add(this.m_labelControlDataBits);
            this.xtraTabPage1.Controls.Add(this.m_labelControlBaudRate);
            this.xtraTabPage1.Controls.Add(this.m_labelControlPortName);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(295, 177);
            this.xtraTabPage1.Text = "Seri Port";
            // 
            // m_comboBoxEditStopBits
            // 
            this.m_comboBoxEditStopBits.Location = new System.Drawing.Point(158, 128);
            this.m_comboBoxEditStopBits.Name = "m_comboBoxEditStopBits";
            this.m_comboBoxEditStopBits.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_comboBoxEditStopBits.Properties.Items.AddRange(new object[] {
            "Yok",
            "1",
            "1,5",
            "2"});
            this.m_comboBoxEditStopBits.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.m_comboBoxEditStopBits.Size = new System.Drawing.Size(100, 20);
            this.m_comboBoxEditStopBits.TabIndex = 8;
            // 
            // m_comboBoxEditParity
            // 
            this.m_comboBoxEditParity.Location = new System.Drawing.Point(158, 102);
            this.m_comboBoxEditParity.Name = "m_comboBoxEditParity";
            this.m_comboBoxEditParity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_comboBoxEditParity.Properties.Items.AddRange(new object[] {
            "Yok",
            "Tek",
            "Çift",
            "İşaret",
            "Boşluk"});
            this.m_comboBoxEditParity.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.m_comboBoxEditParity.Size = new System.Drawing.Size(100, 20);
            this.m_comboBoxEditParity.TabIndex = 7;
            // 
            // m_comboBoxEditDataBits
            // 
            this.m_comboBoxEditDataBits.Location = new System.Drawing.Point(158, 76);
            this.m_comboBoxEditDataBits.Name = "m_comboBoxEditDataBits";
            this.m_comboBoxEditDataBits.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_comboBoxEditDataBits.Properties.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.m_comboBoxEditDataBits.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.m_comboBoxEditDataBits.Size = new System.Drawing.Size(100, 20);
            this.m_comboBoxEditDataBits.TabIndex = 6;
            // 
            // m_comboBoxEditBaudRate
            // 
            this.m_comboBoxEditBaudRate.Location = new System.Drawing.Point(158, 50);
            this.m_comboBoxEditBaudRate.Name = "m_comboBoxEditBaudRate";
            this.m_comboBoxEditBaudRate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_comboBoxEditBaudRate.Properties.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.m_comboBoxEditBaudRate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.m_comboBoxEditBaudRate.Size = new System.Drawing.Size(100, 20);
            this.m_comboBoxEditBaudRate.TabIndex = 5;
            // 
            // m_comboBoxEditPortName
            // 
            this.m_comboBoxEditPortName.Location = new System.Drawing.Point(158, 24);
            this.m_comboBoxEditPortName.Name = "m_comboBoxEditPortName";
            this.m_comboBoxEditPortName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_comboBoxEditPortName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.m_comboBoxEditPortName.Size = new System.Drawing.Size(100, 20);
            this.m_comboBoxEditPortName.TabIndex = 1;
            // 
            // m_labelControlStopBits
            // 
            this.m_labelControlStopBits.Location = new System.Drawing.Point(50, 131);
            this.m_labelControlStopBits.Name = "m_labelControlStopBits";
            this.m_labelControlStopBits.Size = new System.Drawing.Size(53, 13);
            this.m_labelControlStopBits.TabIndex = 4;
            this.m_labelControlStopBits.Text = "Dur Bitleri :";
            // 
            // m_labelControlParity
            // 
            this.m_labelControlParity.Location = new System.Drawing.Point(65, 105);
            this.m_labelControlParity.Name = "m_labelControlParity";
            this.m_labelControlParity.Size = new System.Drawing.Size(35, 13);
            this.m_labelControlParity.TabIndex = 3;
            this.m_labelControlParity.Text = "Parity :";
            // 
            // m_labelControlDataBits
            // 
            this.m_labelControlDataBits.Location = new System.Drawing.Point(49, 79);
            this.m_labelControlDataBits.Name = "m_labelControlDataBits";
            this.m_labelControlDataBits.Size = new System.Drawing.Size(54, 13);
            this.m_labelControlDataBits.TabIndex = 2;
            this.m_labelControlDataBits.Text = "Veri Bitleri :";
            // 
            // m_labelControlBaudRate
            // 
            this.m_labelControlBaudRate.Location = new System.Drawing.Point(46, 53);
            this.m_labelControlBaudRate.Name = "m_labelControlBaudRate";
            this.m_labelControlBaudRate.Size = new System.Drawing.Size(57, 13);
            this.m_labelControlBaudRate.TabIndex = 1;
            this.m_labelControlBaudRate.Text = "Baud Rate :";
            // 
            // m_labelControlPortName
            // 
            this.m_labelControlPortName.Location = new System.Drawing.Point(73, 27);
            this.m_labelControlPortName.Name = "m_labelControlPortName";
            this.m_labelControlPortName.Size = new System.Drawing.Size(27, 13);
            this.m_labelControlPortName.TabIndex = 0;
            this.m_labelControlPortName.Text = "Port :";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.m_spinEditWaitingTime);
            this.xtraTabPage2.Controls.Add(this.m_checkEditStartAuto);
            this.xtraTabPage2.Controls.Add(this.m_radioGroupWaitTimeType);
            this.xtraTabPage2.Controls.Add(this.m_labelControlWaitingTime);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(295, 177);
            this.xtraTabPage2.Text = "Diğer";
            // 
            // m_spinEditWaitingTime
            // 
            this.m_spinEditWaitingTime.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_spinEditWaitingTime.Location = new System.Drawing.Point(176, 40);
            this.m_spinEditWaitingTime.Name = "m_spinEditWaitingTime";
            this.m_spinEditWaitingTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_spinEditWaitingTime.Size = new System.Drawing.Size(100, 20);
            this.m_spinEditWaitingTime.TabIndex = 6;
            // 
            // m_checkEditStartAuto
            // 
            this.m_checkEditStartAuto.Location = new System.Drawing.Point(176, 129);
            this.m_checkEditStartAuto.Name = "m_checkEditStartAuto";
            this.m_checkEditStartAuto.Properties.Caption = "Açılışta Başlat";
            this.m_checkEditStartAuto.Size = new System.Drawing.Size(100, 19);
            this.m_checkEditStartAuto.TabIndex = 5;
            // 
            // m_radioGroupWaitTimeType
            // 
            this.m_radioGroupWaitTimeType.Location = new System.Drawing.Point(176, 66);
            this.m_radioGroupWaitTimeType.Name = "m_radioGroupWaitTimeType";
            this.m_radioGroupWaitTimeType.Properties.Columns = 2;
            this.m_radioGroupWaitTimeType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "sn"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "dk")});
            this.m_radioGroupWaitTimeType.Size = new System.Drawing.Size(100, 24);
            this.m_radioGroupWaitTimeType.TabIndex = 4;
            // 
            // m_labelControlWaitingTime
            // 
            this.m_labelControlWaitingTime.Location = new System.Drawing.Point(14, 43);
            this.m_labelControlWaitingTime.Name = "m_labelControlWaitingTime";
            this.m_labelControlWaitingTime.Size = new System.Drawing.Size(135, 13);
            this.m_labelControlWaitingTime.TabIndex = 2;
            this.m_labelControlWaitingTime.Text = "Tur Sonrası Bekleme Süresi :";
            // 
            // m_simpleButtonSave
            // 
            this.m_simpleButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("m_simpleButtonSave.Image")));
            this.m_simpleButtonSave.Location = new System.Drawing.Point(156, 235);
            this.m_simpleButtonSave.Name = "m_simpleButtonSave";
            this.m_simpleButtonSave.Size = new System.Drawing.Size(75, 41);
            this.m_simpleButtonSave.TabIndex = 1;
            this.m_simpleButtonSave.Text = "Kaydet";
            this.m_simpleButtonSave.Click += new System.EventHandler(this.m_simpleButtons_Click);
            // 
            // m_simpleButtonApply
            // 
            this.m_simpleButtonApply.Image = ((System.Drawing.Image)(resources.GetObject("m_simpleButtonApply.Image")));
            this.m_simpleButtonApply.Location = new System.Drawing.Point(237, 235);
            this.m_simpleButtonApply.Name = "m_simpleButtonApply";
            this.m_simpleButtonApply.Size = new System.Drawing.Size(75, 41);
            this.m_simpleButtonApply.TabIndex = 2;
            this.m_simpleButtonApply.Text = "Uygula";
            this.m_simpleButtonApply.Click += new System.EventHandler(this.m_simpleButtons_Click);
            // 
            // PTZControlSettingsModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(324, 286);
            this.Controls.Add(this.m_simpleButtonApply);
            this.Controls.Add(this.m_simpleButtonSave);
            this.Controls.Add(this.xtraTabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PTZControlSettingsModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PTZ Kamera Kontrol - Ayarlar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PTZControlSettingsModal_FormClosing);
            this.Load += new System.EventHandler(this.PTZControlSettingsModal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_comboBoxEditStopBits.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_comboBoxEditParity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_comboBoxEditDataBits.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_comboBoxEditBaudRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_comboBoxEditPortName.Properties)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_spinEditWaitingTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_checkEditStartAuto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_radioGroupWaitTimeType.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraBars.FormAssistant formAssistant1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraEditors.ComboBoxEdit m_comboBoxEditStopBits;
        private DevExpress.XtraEditors.ComboBoxEdit m_comboBoxEditParity;
        private DevExpress.XtraEditors.ComboBoxEdit m_comboBoxEditDataBits;
        private DevExpress.XtraEditors.ComboBoxEdit m_comboBoxEditBaudRate;
        private DevExpress.XtraEditors.ComboBoxEdit m_comboBoxEditPortName;
        private DevExpress.XtraEditors.LabelControl m_labelControlStopBits;
        private DevExpress.XtraEditors.LabelControl m_labelControlParity;
        private DevExpress.XtraEditors.LabelControl m_labelControlDataBits;
        private DevExpress.XtraEditors.LabelControl m_labelControlBaudRate;
        private DevExpress.XtraEditors.LabelControl m_labelControlPortName;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.CheckEdit m_checkEditStartAuto;
        private DevExpress.XtraEditors.RadioGroup m_radioGroupWaitTimeType;
        private DevExpress.XtraEditors.LabelControl m_labelControlWaitingTime;
        private DevExpress.XtraEditors.SimpleButton m_simpleButtonSave;
        private DevExpress.XtraEditors.SimpleButton m_simpleButtonApply;
        private DevExpress.XtraEditors.SpinEdit m_spinEditWaitingTime;
    }
}
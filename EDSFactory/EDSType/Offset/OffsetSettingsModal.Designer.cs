namespace EDSFactory 
{
    partial class OffsetSettingsModal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OffsetSettingsModal));
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.formAssistant1 = new DevExpress.XtraBars.FormAssistant();
            this.m_folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.m_labelControlImagesPath = new DevExpress.XtraEditors.LabelControl();
            this.m_buttonEditImagesPath = new DevExpress.XtraEditors.ButtonEdit();
            this.m_labelControlThumbNailImagesPath = new DevExpress.XtraEditors.LabelControl();
            this.m_labelControlViolationImagesPath = new DevExpress.XtraEditors.LabelControl();
            this.m_buttonEditThumbNailImagesPath = new DevExpress.XtraEditors.ButtonEdit();
            this.m_buttonEditViolationImagesPath = new DevExpress.XtraEditors.ButtonEdit();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.m_checkEditImageDelete = new DevExpress.XtraEditors.CheckEdit();
            this.m_labelControlProtectViolationTime = new DevExpress.XtraEditors.LabelControl();
            this.m_labelControlScanImageTime = new DevExpress.XtraEditors.LabelControl();
            this.m_spinEditProtectViolationTime = new DevExpress.XtraEditors.SpinEdit();
            this.m_spinEditScanImageTime = new DevExpress.XtraEditors.SpinEdit();
            this.m_simpleButtonDeleteAllRecord = new DevExpress.XtraEditors.SimpleButton();
            this.m_simpleButtonApply = new DevExpress.XtraEditors.SimpleButton();
            this.m_simpleButtonSave = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_buttonEditImagesPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_buttonEditThumbNailImagesPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_buttonEditViolationImagesPath.Properties)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_checkEditImageDelete.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_spinEditProtectViolationTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_spinEditScanImageTime.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Blueprint";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(12, 12);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(479, 180);
            this.xtraTabControl1.TabIndex = 7;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.m_labelControlImagesPath);
            this.xtraTabPage1.Controls.Add(this.m_buttonEditImagesPath);
            this.xtraTabPage1.Controls.Add(this.m_labelControlThumbNailImagesPath);
            this.xtraTabPage1.Controls.Add(this.m_labelControlViolationImagesPath);
            this.xtraTabPage1.Controls.Add(this.m_buttonEditThumbNailImagesPath);
            this.xtraTabPage1.Controls.Add(this.m_buttonEditViolationImagesPath);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(474, 151);
            this.xtraTabPage1.Text = "Dosya";
            // 
            // m_labelControlImagesPath
            // 
            this.m_labelControlImagesPath.Location = new System.Drawing.Point(67, 32);
            this.m_labelControlImagesPath.Name = "m_labelControlImagesPath";
            this.m_labelControlImagesPath.Size = new System.Drawing.Size(103, 13);
            this.m_labelControlImagesPath.TabIndex = 9;
            this.m_labelControlImagesPath.Text = "Resimler Dosya Yolu :";
            // 
            // m_buttonEditImagesPath
            // 
            this.m_buttonEditImagesPath.Location = new System.Drawing.Point(230, 29);
            this.m_buttonEditImagesPath.Name = "m_buttonEditImagesPath";
            this.m_buttonEditImagesPath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_buttonEditImagesPath.Properties.ReadOnly = true;
            this.m_buttonEditImagesPath.Size = new System.Drawing.Size(207, 20);
            this.m_buttonEditImagesPath.TabIndex = 8;
            this.m_buttonEditImagesPath.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.m_buttonEdits_ButtonClick);
            // 
            // m_labelControlThumbNailImagesPath
            // 
            this.m_labelControlThumbNailImagesPath.Location = new System.Drawing.Point(36, 84);
            this.m_labelControlThumbNailImagesPath.Name = "m_labelControlThumbNailImagesPath";
            this.m_labelControlThumbNailImagesPath.Size = new System.Drawing.Size(134, 13);
            this.m_labelControlThumbNailImagesPath.TabIndex = 3;
            this.m_labelControlThumbNailImagesPath.Text = "Küçük Resimler Dosya Yolu :";
            // 
            // m_labelControlViolationImagesPath
            // 
            this.m_labelControlViolationImagesPath.Location = new System.Drawing.Point(42, 58);
            this.m_labelControlViolationImagesPath.Name = "m_labelControlViolationImagesPath";
            this.m_labelControlViolationImagesPath.Size = new System.Drawing.Size(128, 13);
            this.m_labelControlViolationImagesPath.TabIndex = 2;
            this.m_labelControlViolationImagesPath.Text = "İhlal Resimleri Dosya Yolu :";
            // 
            // m_buttonEditThumbNailImagesPath
            // 
            this.m_buttonEditThumbNailImagesPath.Location = new System.Drawing.Point(230, 81);
            this.m_buttonEditThumbNailImagesPath.Name = "m_buttonEditThumbNailImagesPath";
            this.m_buttonEditThumbNailImagesPath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_buttonEditThumbNailImagesPath.Size = new System.Drawing.Size(207, 20);
            this.m_buttonEditThumbNailImagesPath.TabIndex = 1;
            this.m_buttonEditThumbNailImagesPath.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.m_buttonEdits_ButtonClick);
            // 
            // m_buttonEditViolationImagesPath
            // 
            this.m_buttonEditViolationImagesPath.Location = new System.Drawing.Point(230, 55);
            this.m_buttonEditViolationImagesPath.Name = "m_buttonEditViolationImagesPath";
            this.m_buttonEditViolationImagesPath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_buttonEditViolationImagesPath.Size = new System.Drawing.Size(207, 20);
            this.m_buttonEditViolationImagesPath.TabIndex = 1;
            this.m_buttonEditViolationImagesPath.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.m_buttonEdits_ButtonClick);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.m_checkEditImageDelete);
            this.xtraTabPage2.Controls.Add(this.m_labelControlProtectViolationTime);
            this.xtraTabPage2.Controls.Add(this.m_labelControlScanImageTime);
            this.xtraTabPage2.Controls.Add(this.m_spinEditProtectViolationTime);
            this.xtraTabPage2.Controls.Add(this.m_spinEditScanImageTime);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(474, 151);
            this.xtraTabPage2.Text = "Sistem";
            // 
            // m_checkEditImageDelete
            // 
            this.m_checkEditImageDelete.Location = new System.Drawing.Point(297, 120);
            this.m_checkEditImageDelete.Name = "m_checkEditImageDelete";
            this.m_checkEditImageDelete.Properties.Caption = "Resim Silinme Uygulansın";
            this.m_checkEditImageDelete.Size = new System.Drawing.Size(140, 19);
            this.m_checkEditImageDelete.TabIndex = 11;
            // 
            // m_labelControlProtectViolationTime
            // 
            this.m_labelControlProtectViolationTime.Location = new System.Drawing.Point(44, 70);
            this.m_labelControlProtectViolationTime.Name = "m_labelControlProtectViolationTime";
            this.m_labelControlProtectViolationTime.Size = new System.Drawing.Size(143, 13);
            this.m_labelControlProtectViolationTime.TabIndex = 7;
            this.m_labelControlProtectViolationTime.Text = "Ceza Korunma Süresi (saat) : ";
            // 
            // m_labelControlScanImageTime
            // 
            this.m_labelControlScanImageTime.Location = new System.Drawing.Point(42, 44);
            this.m_labelControlScanImageTime.Name = "m_labelControlScanImageTime";
            this.m_labelControlScanImageTime.Size = new System.Drawing.Size(145, 13);
            this.m_labelControlScanImageTime.TabIndex = 6;
            this.m_labelControlScanImageTime.Text = "Resimleri Tarama Süresi (sn) : ";
            // 
            // m_spinEditProtectViolationTime
            // 
            this.m_spinEditProtectViolationTime.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_spinEditProtectViolationTime.Location = new System.Drawing.Point(230, 67);
            this.m_spinEditProtectViolationTime.Name = "m_spinEditProtectViolationTime";
            this.m_spinEditProtectViolationTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_spinEditProtectViolationTime.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.m_spinEditProtectViolationTime.Size = new System.Drawing.Size(207, 20);
            this.m_spinEditProtectViolationTime.TabIndex = 4;
            // 
            // m_spinEditScanImageTime
            // 
            this.m_spinEditScanImageTime.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_spinEditScanImageTime.Location = new System.Drawing.Point(230, 41);
            this.m_spinEditScanImageTime.Name = "m_spinEditScanImageTime";
            this.m_spinEditScanImageTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_spinEditScanImageTime.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.m_spinEditScanImageTime.Size = new System.Drawing.Size(207, 20);
            this.m_spinEditScanImageTime.TabIndex = 5;
            // 
            // m_simpleButtonDeleteAllRecord
            // 
            this.m_simpleButtonDeleteAllRecord.Image = ((System.Drawing.Image)(resources.GetObject("m_simpleButtonDeleteAllRecord.Image")));
            this.m_simpleButtonDeleteAllRecord.Location = new System.Drawing.Point(12, 201);
            this.m_simpleButtonDeleteAllRecord.Name = "m_simpleButtonDeleteAllRecord";
            this.m_simpleButtonDeleteAllRecord.Size = new System.Drawing.Size(171, 41);
            this.m_simpleButtonDeleteAllRecord.TabIndex = 12;
            this.m_simpleButtonDeleteAllRecord.Text = "Bütün İhlal Kayıtları Silinsin!";
            this.m_simpleButtonDeleteAllRecord.Click += new System.EventHandler(this.m_simpleButtonDeleteAllRecord_Click);
            // 
            // m_simpleButtonApply
            // 
            this.m_simpleButtonApply.Image = ((System.Drawing.Image)(resources.GetObject("m_simpleButtonApply.Image")));
            this.m_simpleButtonApply.Location = new System.Drawing.Point(416, 201);
            this.m_simpleButtonApply.Name = "m_simpleButtonApply";
            this.m_simpleButtonApply.Size = new System.Drawing.Size(75, 41);
            this.m_simpleButtonApply.TabIndex = 11;
            this.m_simpleButtonApply.Text = "Uygula";
            this.m_simpleButtonApply.Click += new System.EventHandler(this.m_simpleButtons_Click);
            // 
            // m_simpleButtonSave
            // 
            this.m_simpleButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("m_simpleButtonSave.Image")));
            this.m_simpleButtonSave.Location = new System.Drawing.Point(335, 201);
            this.m_simpleButtonSave.Name = "m_simpleButtonSave";
            this.m_simpleButtonSave.Size = new System.Drawing.Size(75, 41);
            this.m_simpleButtonSave.TabIndex = 10;
            this.m_simpleButtonSave.Text = "Kaydet";
            this.m_simpleButtonSave.Click += new System.EventHandler(this.m_simpleButtons_Click);
            // 
            // OffsetSettingsModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(506, 250);
            this.Controls.Add(this.m_simpleButtonDeleteAllRecord);
            this.Controls.Add(this.m_simpleButtonApply);
            this.Controls.Add(this.m_simpleButtonSave);
            this.Controls.Add(this.xtraTabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "OffsetSettingsModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ofset Tarama Elektronik Denetleme Sistemi - Ayarlar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OffsetSettingsModal_FormClosing);
            this.Load += new System.EventHandler(this.OffsetSettingsModal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_buttonEditImagesPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_buttonEditThumbNailImagesPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_buttonEditViolationImagesPath.Properties)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_checkEditImageDelete.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_spinEditProtectViolationTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_spinEditScanImageTime.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraBars.FormAssistant formAssistant1;
        private System.Windows.Forms.FolderBrowserDialog m_folderBrowserDialog;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraEditors.LabelControl m_labelControlImagesPath;
        private DevExpress.XtraEditors.ButtonEdit m_buttonEditImagesPath;
        private DevExpress.XtraEditors.LabelControl m_labelControlThumbNailImagesPath;
        private DevExpress.XtraEditors.LabelControl m_labelControlViolationImagesPath;
        private DevExpress.XtraEditors.ButtonEdit m_buttonEditThumbNailImagesPath;
        private DevExpress.XtraEditors.ButtonEdit m_buttonEditViolationImagesPath;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.CheckEdit m_checkEditImageDelete;
        private DevExpress.XtraEditors.LabelControl m_labelControlProtectViolationTime;
        private DevExpress.XtraEditors.LabelControl m_labelControlScanImageTime;
        private DevExpress.XtraEditors.SpinEdit m_spinEditProtectViolationTime;
        private DevExpress.XtraEditors.SpinEdit m_spinEditScanImageTime;
        private DevExpress.XtraEditors.SimpleButton m_simpleButtonDeleteAllRecord;
        private DevExpress.XtraEditors.SimpleButton m_simpleButtonApply;
        private DevExpress.XtraEditors.SimpleButton m_simpleButtonSave;
    }
}
namespace EDSFactory
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.formAssistant1 = new DevExpress.XtraBars.FormAssistant();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.m_labelControlProductName = new DevExpress.XtraEditors.LabelControl();
            this.m_labelControlVersion = new DevExpress.XtraEditors.LabelControl();
            this.m_labelControlCopyright = new DevExpress.XtraEditors.LabelControl();
            this.m_labelControlCompanyName = new DevExpress.XtraEditors.LabelControl();
            this.m_simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.m_textEditDescription = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_textEditDescription.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Blueprint";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::EDSFactory.Properties.Resources.isbak_beyaz;
            this.pictureEdit1.Location = new System.Drawing.Point(12, 3);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(460, 276);
            this.pictureEdit1.TabIndex = 0;
            // 
            // m_labelControlProductName
            // 
            this.m_labelControlProductName.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.m_labelControlProductName.Location = new System.Drawing.Point(12, 285);
            this.m_labelControlProductName.Name = "m_labelControlProductName";
            this.m_labelControlProductName.Size = new System.Drawing.Size(145, 14);
            this.m_labelControlProductName.TabIndex = 1;
            this.m_labelControlProductName.Text = "Ürün Adı : EDS Fabrikası";
            // 
            // m_labelControlVersion
            // 
            this.m_labelControlVersion.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.m_labelControlVersion.Location = new System.Drawing.Point(12, 304);
            this.m_labelControlVersion.Name = "m_labelControlVersion";
            this.m_labelControlVersion.Size = new System.Drawing.Size(108, 14);
            this.m_labelControlVersion.TabIndex = 1;
            this.m_labelControlVersion.Text = "Versiyon : 1.5.6.4";
            // 
            // m_labelControlCopyright
            // 
            this.m_labelControlCopyright.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.m_labelControlCopyright.Location = new System.Drawing.Point(12, 323);
            this.m_labelControlCopyright.Name = "m_labelControlCopyright";
            this.m_labelControlCopyright.Size = new System.Drawing.Size(236, 14);
            this.m_labelControlCopyright.TabIndex = 1;
            this.m_labelControlCopyright.Text = "Telif Hakkı : © 2015 Tüm Hakkı Saklıdır";
            // 
            // m_labelControlCompanyName
            // 
            this.m_labelControlCompanyName.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.False;
            this.m_labelControlCompanyName.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.m_labelControlCompanyName.Location = new System.Drawing.Point(12, 342);
            this.m_labelControlCompanyName.Name = "m_labelControlCompanyName";
            this.m_labelControlCompanyName.Size = new System.Drawing.Size(404, 14);
            this.m_labelControlCompanyName.TabIndex = 1;
            this.m_labelControlCompanyName.Text = "Şirket Adı : İSBAK - İstanbul Bilişim ve Akıllı Kent Teknolojileri A.Ş.";
            // 
            // m_simpleButtonOK
            // 
            this.m_simpleButtonOK.Location = new System.Drawing.Point(397, 491);
            this.m_simpleButtonOK.Name = "m_simpleButtonOK";
            this.m_simpleButtonOK.Size = new System.Drawing.Size(75, 23);
            this.m_simpleButtonOK.TabIndex = 2;
            this.m_simpleButtonOK.Text = "Tamam";
            this.m_simpleButtonOK.Click += new System.EventHandler(this.m_simpleButtonOK_Click);
            // 
            // m_textEditDescription
            // 
            this.m_textEditDescription.EditValue = "Açıklama";
            this.m_textEditDescription.Location = new System.Drawing.Point(12, 362);
            this.m_textEditDescription.Name = "m_textEditDescription";
            this.m_textEditDescription.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.m_textEditDescription.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.m_textEditDescription.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.m_textEditDescription.Properties.AutoHeight = false;
            this.m_textEditDescription.Properties.ReadOnly = true;
            this.m_textEditDescription.Size = new System.Drawing.Size(460, 123);
            this.m_textEditDescription.TabIndex = 3;
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(485, 522);
            this.Controls.Add(this.m_textEditDescription);
            this.Controls.Add(this.m_simpleButtonOK);
            this.Controls.Add(this.m_labelControlCompanyName);
            this.Controls.Add(this.m_labelControlCopyright);
            this.Controls.Add(this.m_labelControlVersion);
            this.Controls.Add(this.m_labelControlProductName);
            this.Controls.Add(this.pictureEdit1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hakkında";
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_textEditDescription.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraBars.FormAssistant formAssistant1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl m_labelControlProductName;
        private DevExpress.XtraEditors.LabelControl m_labelControlVersion;
        private DevExpress.XtraEditors.LabelControl m_labelControlCopyright;
        private DevExpress.XtraEditors.LabelControl m_labelControlCompanyName;
        private DevExpress.XtraEditors.SimpleButton m_simpleButtonOK;
        private DevExpress.XtraEditors.TextEdit m_textEditDescription;
    }
}
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDSFactory 
{
    public partial class StandingSettingsModal : DevExpress.XtraEditors.XtraForm
    {
        private static StandingSettingsModal m_mhssm;
        public Standing m_mhs;

        Settings.StandingSettings m_settings;

        public StandingSettingsModal()
        {
            InitializeComponent();

            m_settings = Settings.StandingSettings.Singleton();
            m_settings = m_settings.DeSerialize(m_settings);

            m_buttonEditImagesPath.Text = m_settings.m_imagePath;
            m_buttonEditViolationImagesPath.Text = m_settings.m_violationImagesPath;
            m_buttonEditThumbNailImagesPath.Text = m_settings.m_thumbNailImagesPath;
            m_checkEditImageDelete.Checked = m_settings.m_deleteImages;
            m_spinEditScanImageTime.Value = m_settings.m_scanImageTime;
            m_spinEditProtectViolationTime.Value = m_settings.m_protectViolationTime;
            m_checkEditViolationWithVideo.Checked = m_settings.m_videoMode;
        }


        public static StandingSettingsModal Singleton(Standing mhs)
        {
            if (m_mhssm == null)
                m_mhssm = new StandingSettingsModal(mhs);

            return m_mhssm;
        }

        private StandingSettingsModal(Standing mhs)
            : this()
        {
            m_mhs = mhs;
        }

        private void m_buttonEdits_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit myButton = (ButtonEdit)sender;

            if (myButton == m_buttonEditImagesPath)
            {
                if (m_folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    m_buttonEditImagesPath.Text = m_folderBrowserDialog.SelectedPath;
            }

            if (myButton == m_buttonEditViolationImagesPath)
            {
                if (m_folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    m_buttonEditViolationImagesPath.Text = m_folderBrowserDialog.SelectedPath;
            }

            if (myButton == m_buttonEditThumbNailImagesPath)
            {
                if (m_folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    m_buttonEditThumbNailImagesPath.Text = m_folderBrowserDialog.SelectedPath;
            }
        }

        private void m_simpleButtons_Click(object sender, EventArgs e)
        {
            m_settings.m_imagePath = m_buttonEditImagesPath.Text;
            //m_buttonEditViolationImagesPath.Text = m_settings.m_imagePath;
            m_settings.m_violationImagesPath = m_buttonEditViolationImagesPath.Text;
            m_settings.m_thumbNailImagesPath = m_buttonEditThumbNailImagesPath.Text;
            m_settings.m_deleteImages = m_checkEditImageDelete.Checked;
            m_settings.m_scanImageTime = Convert.ToInt32(m_spinEditScanImageTime.Value);
            m_settings.m_protectViolationTime = Convert.ToInt32(m_spinEditProtectViolationTime.Value);
            m_settings.m_videoMode = m_checkEditViolationWithVideo.Checked;

            m_settings.Serilize(m_settings);
            m_settings = m_settings.DeSerialize(m_settings);

            SimpleButton myButton = (SimpleButton)sender;

            if (myButton == m_simpleButtonApply)
                this.Close();
        }

        private void m_simpleButtonDeleteAllRecord_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(UserMessages.DeleteQuestionMessage, UserMessages.MessageCaptionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                Task<int> returnValueOfDelete = DatabaseOperation.Standing.Singleton().AsyncDelete();

                returnValueOfDelete.Wait();

                if (returnValueOfDelete.Result > 0)
                    MessageBox.Show(UserMessages.DeleteMessage, UserMessages.MessageCaptionMessage, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(UserMessages.DeleteErrorMessage, UserMessages.MessageCaptionMessage, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void StandingSettingsModal_Load(object sender, EventArgs e)
        {
            MainForm.m_mediator.StopScan();
        }

        private void StandingSettingsModal_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_mhssm = null;
            MainForm.m_mediator.StartAlwaysScanProgram();
        }
    }
}

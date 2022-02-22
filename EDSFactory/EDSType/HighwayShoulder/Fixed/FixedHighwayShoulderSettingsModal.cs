using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace EDSFactory 
{
    public partial class FixedHighwayShoulderSettingsModal : DevExpress.XtraEditors.XtraForm
    {
        private static FixedHighwayShoulderSettingsModal m_fhssm;
        public FixedHighwayShoulder m_fhs; 
        Settings.FixedHighwayShoulderSettings m_settings; 

        public static FixedHighwayShoulderSettingsModal Singleton(FixedHighwayShoulder fhs)
        {
            if (m_fhssm == null)
                m_fhssm = new FixedHighwayShoulderSettingsModal(fhs);

            return m_fhssm;
        }

        private FixedHighwayShoulderSettingsModal(FixedHighwayShoulder fhs)
            : this()
        {
            m_fhs = fhs;
        }

        public FixedHighwayShoulderSettingsModal()
        {
            InitializeComponent();

            m_settings = Settings.FixedHighwayShoulderSettings.Singleton();
            m_settings = m_settings.DeSerialize(m_settings); 

            m_buttonEditImagesPath.Text = m_settings.m_imagePath;
            m_buttonEditViolationImagesPath.Text = m_settings.m_violationImagesPath;
            m_buttonEditThumbNailImagesPath.Text = m_settings.m_thumbNailImagesPath;
            m_checkEditImageDelete.Checked = m_settings.m_deleteImages;
            m_spinEditDistance.Value = m_settings.m_distance;
            m_spinEditSpeedLimit.Value = m_settings.m_speed;
            m_spinEditEnforcementTolerance.Value = m_settings.m_tolerancePercentage;
            m_spinEditScanImageTime.Value = m_settings.m_scanImageTime;
            m_spinEditProtectViolationTime.Value = m_settings.m_protectViolationTime;
            m_checkEditEnforcementTolerance.Checked = m_settings.m_applyTolerance;
            m_checkEditViolationWithVideo.Checked = m_settings.m_videoMode;
          
        }
 
        private void m_buttonEditViolationImagesPath_EditValueChanged(object sender, EventArgs e)
        {

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
            m_settings.m_violationImagesPath = m_buttonEditViolationImagesPath.Text;
            m_settings.m_thumbNailImagesPath = m_buttonEditThumbNailImagesPath.Text;
            m_settings.m_deleteImages = m_checkEditImageDelete.Checked;
            m_settings.m_distance = Convert.ToInt32(m_spinEditDistance.Value);
            m_settings.m_speed = Convert.ToInt32(m_spinEditSpeedLimit.Value);
            m_settings.m_tolerancePercentage = Convert.ToInt32(m_spinEditEnforcementTolerance.Value);
            m_settings.m_scanImageTime = Convert.ToInt32(m_spinEditScanImageTime.Value);
            m_settings.m_protectViolationTime = Convert.ToInt32(m_spinEditProtectViolationTime.Value); 
            m_settings.m_applyTolerance = m_checkEditEnforcementTolerance.Checked;
            m_settings.m_videoMode = m_checkEditViolationWithVideo.Checked;
            

            m_settings.Serilize(m_settings);
            m_settings = m_settings.DeSerialize(m_settings);

            SimpleButton myButton = (SimpleButton)sender;

            if (myButton == m_simpleButtonApply)
                this.Close();
        }

        private void FixedHighwayShoulderSettingsModal_Load(object sender, EventArgs e)
        {
            MainForm.m_mediator.StopScan();
        }

        private void FixedHighwayShoulderSettingsModal_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_fhssm = null;

            MainForm.m_mediator.StartAlwaysScanProgram();
        }

        private void m_simpleButtonDeleteAllRecord_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(UserMessages.DeleteQuestionMessage, UserMessages.MessageCaptionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == System.Windows.Forms.DialogResult.Yes)
            { 
                Task<int> returnValueOfDelete = DatabaseOperation.FixedHighwayShoulder.Singleton().AsyncDelete();
                returnValueOfDelete.Wait();

                if (returnValueOfDelete.Result > 0)
                    MessageBox.Show(UserMessages.DeleteMessage, UserMessages.MessageCaptionMessage, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(UserMessages.DeleteErrorMessage, UserMessages.MessageCaptionMessage, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

    }
}

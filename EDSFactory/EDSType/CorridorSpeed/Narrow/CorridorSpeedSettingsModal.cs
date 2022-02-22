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
    public partial class CorridorSpeedSettingsModal : DevExpress.XtraEditors.XtraForm
    {
        private static CorridorSpeedSettingsModal m_fhssm;
        public CorridorSpeed  m_fhs;

        Settings.CorridorSpeedSettings m_settings; 


        public CorridorSpeedSettingsModal()
        {
            InitializeComponent();
            
            m_settings = Settings.CorridorSpeedSettings.Singleton();
            m_settings = m_settings.DeSerialize(m_settings);

            m_buttonEditEntryTagImagesPath.Text = m_settings.m_entryTagPath;
            m_buttonEditImagesPath.Text = m_settings.m_imagePath;
            m_buttonEditViolationImagesPath.Text = m_settings.m_violationImagesPath;
            m_buttonEditThumbNailImagesPath.Text = m_settings.m_thumbNailImagesPath;

            if(m_settings.m_workingType)
                m_radioGroupWorkingType.SelectedIndex = 0;
            else
                m_radioGroupWorkingType.SelectedIndex = 1;

            m_checkEditImageDelete.Checked = m_settings.m_deleteImages;
            m_spinEditDistance.Value = m_settings.m_distance;
            m_spinEditSpeedLimit.Value = m_settings.m_speed;
            m_spinEditEnforcementTolerance.Value = m_settings.m_tolerancePercentage; 

            m_spinEditProtectViolationTime.Value = m_settings.m_protectViolationTime;
            m_checkEditEnforcementTolerance.Checked = m_settings.m_applyTolerance;

            m_ipAddressControlEntryTag.Text = m_settings.m_entryTagIP;
            m_textEditPort.Text = m_settings.m_entryTagPort;

            m_textEditEntryTagPort.Text = m_settings.m_entryTagListenPort;
        }

        public static CorridorSpeedSettingsModal Singleton(CorridorSpeed fhs)
        {
            if (m_fhssm == null)
                m_fhssm = new CorridorSpeedSettingsModal(fhs);

            return m_fhssm;
        }

        private CorridorSpeedSettingsModal(CorridorSpeed fhs)
            : this()
        {
            m_fhs = fhs;
        }

        private void CorridorSpeedSettingsModal_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_fhssm = null;

            MainForm.m_mediator.StartAlwaysScanProgram();
        }

        private void m_simpleButtonSave_Click(object sender, EventArgs e)
        {
            m_settings.m_entryTagPath = m_buttonEditEntryTagImagesPath.Text;
            m_settings.m_imagePath = m_buttonEditImagesPath.Text;
             m_settings.m_violationImagesPath = m_buttonEditViolationImagesPath.Text;
             m_settings.m_thumbNailImagesPath = m_buttonEditThumbNailImagesPath.Text;

            if (m_radioGroupWorkingType.SelectedIndex == 0)
                m_settings.m_workingType = true;
            else
                m_settings.m_workingType = false;

            m_settings.m_deleteImages = m_checkEditImageDelete.Checked;
            m_settings.m_distance = Convert.ToInt32(m_spinEditDistance.Value);
            m_settings.m_speed = Convert.ToInt32(m_spinEditSpeedLimit.Value);
            m_settings.m_tolerancePercentage = Convert.ToInt32(m_spinEditEnforcementTolerance.Value);

            m_settings.m_protectViolationTime = Convert.ToInt32(m_spinEditProtectViolationTime.Value);
            m_settings.m_applyTolerance = m_checkEditEnforcementTolerance.Checked;

            m_settings.m_entryTagIP =m_ipAddressControlEntryTag.Text;
            m_settings.m_entryTagPort = m_textEditPort.Text;
            m_settings.m_entryTagListenPort = m_textEditEntryTagPort.Text;

            m_settings.Serilize(m_settings);

            m_settings = m_settings.DeSerialize(m_settings);

            SimpleButton button = (SimpleButton)sender;

            if(button == m_simpleButtonApply)
            {
                this.Close();
            }
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

            if (myButton == m_buttonEditEntryTagImagesPath)
            {
                if (m_folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    m_buttonEditEntryTagImagesPath.Text = m_folderBrowserDialog.SelectedPath;
            }



            
        }

        private void CorridorSpeedSettingsModal_Load(object sender, EventArgs e)
        {
            MainForm.m_mediator.StopScan();
        }

        private void m_simpleButtonDeleteAllRecord_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(UserMessages.DeleteQuestionMessage, UserMessages.MessageCaptionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                Task<int> returnValueOfDelete = DatabaseOperation.CorridorSpeed.Singleton().AsyncDelete();
                returnValueOfDelete.Wait();

                if (returnValueOfDelete.Result > 0)
                    MessageBox.Show(UserMessages.DeleteMessage, UserMessages.MessageCaptionMessage, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(UserMessages.DeleteErrorMessage, UserMessages.MessageCaptionMessage, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }
    }
}

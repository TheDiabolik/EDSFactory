using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDSFactory 
{
    public partial class PTZControlSettingsModal : DevExpress.XtraEditors.XtraForm
    {
        private static PTZControlSettingsModal m_PTZcsm;
        public PTZCameraControl m_PTZc;

        Settings.PTZControlSettings m_PTZcs;


        public static PTZControlSettingsModal Singleton(PTZCameraControl PTZc)
        {
            if (m_PTZcsm == null)
                m_PTZcsm = new PTZControlSettingsModal(PTZc);

            return m_PTZcsm;
        }

        private PTZControlSettingsModal(PTZCameraControl PTZc)
            : this()
        {
            m_PTZc = PTZc;
        }

        public PTZControlSettingsModal()
        {
            InitializeComponent();

            m_PTZcs = Settings.PTZControlSettings.Singleton();

            m_PTZcs = m_PTZcs.DeSerialize(m_PTZcs);

            try
            {
                foreach (string i in SerialPort.GetPortNames())
                {
                    m_comboBoxEditPortName.Properties.Items.Add(i);
                }
            }
            catch
            {
                MessageBox.Show("Seri Portlar Yüklemenedi !?");
            }
        }

        private void PTZControlSettingsModal_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_PTZcsm = null;
        }
         

        private void m_simpleButtons_Click(object sender, EventArgs e)
        {
            m_PTZcs.m_autoStart = m_checkEditStartAuto.Checked;

            if (!string.IsNullOrEmpty(m_comboBoxEditPortName.SelectedItem.ToString()))
                m_PTZcs.m_portName = m_comboBoxEditPortName.SelectedItem.ToString();

            m_PTZcs.m_baudRate = int.Parse(m_comboBoxEditBaudRate.SelectedItem.ToString());
            m_PTZcs.m_dataBits = m_comboBoxEditDataBits.SelectedItem.ToString();


            switch (m_comboBoxEditParity.SelectedIndex)
            {
                case 0:
                    {
                        m_PTZcs.m_parity = Parity.None;
                        break;
                    }
                case 1:
                    {
                        m_PTZcs.m_parity = Parity.Odd;
                        break;
                    }
                case 2:
                    {
                        m_PTZcs.m_parity = Parity.Even;
                        break;
                    }
                case 3:
                    {
                        m_PTZcs.m_parity = Parity.Mark;
                        break;
                    }
                case 4:
                    {
                        m_PTZcs.m_parity = Parity.Space;
                        break;
                    }
            }

            switch (m_comboBoxEditStopBits.SelectedIndex)
            {
                case 0:
                    {
                        m_PTZcs.m_stopBits = StopBits.None;
                        break;
                    }
                case 1:
                    {
                        m_PTZcs.m_stopBits = StopBits.One;
                        break;
                    }
                case 2:
                    {
                        m_PTZcs.m_stopBits = StopBits.OnePointFive;
                        break;
                    }
                case 3:
                    {
                        m_PTZcs.m_stopBits = StopBits.Two;
                        break;
                    }
            }


            m_PTZcs.m_waitingTime = int.Parse(m_spinEditWaitingTime.Value.ToString());


            if (m_radioGroupWaitTimeType.SelectedIndex == 0)
                m_PTZcs.m_waitingTimeUnit = true;
            else
                m_PTZcs.m_waitingTimeUnit = false;


            m_PTZcs.Serialize(m_PTZcs);

            SimpleButton button = (SimpleButton)sender;

            if (button == m_simpleButtonApply)
                this.Close();


        }

        private void PTZControlSettingsModal_Load(object sender, EventArgs e)
        {
            m_spinEditWaitingTime.Value = m_PTZcs.m_waitingTime;

            if (m_PTZcs.m_waitingTimeUnit)
                m_radioGroupWaitTimeType.SelectedIndex = 0;
            else
                m_radioGroupWaitTimeType.SelectedIndex = 1;


            m_checkEditStartAuto.Checked = m_PTZcs.m_autoStart;


            m_comboBoxEditPortName.Text = m_PTZcs.m_portName;

            m_comboBoxEditBaudRate.Text = m_PTZcs.m_baudRate.ToString();
            //m_PTZcs.m_baudRate = int.Parse(m_comboBoxExBaudRate.SelectedItem.ToString());
            m_comboBoxEditDataBits.Text = m_PTZcs.m_dataBits.ToString();


            switch (m_PTZcs.m_parity)
            {
                case Parity.None:
                    {
                        m_comboBoxEditParity.SelectedIndex = 0;
                        break;
                    }
                case Parity.Odd:
                    {
                        m_comboBoxEditParity.SelectedIndex = 1;
                        break;
                    }
                case Parity.Even:
                    {
                        m_comboBoxEditParity.SelectedIndex = 2;
                        break;
                    }
                case Parity.Mark:
                    {
                        m_comboBoxEditParity.SelectedIndex = 3;
                        break;
                    }
                case Parity.Space:
                    {
                        m_comboBoxEditParity.SelectedIndex = 4;
                        break;
                    }
            }

            switch (m_PTZcs.m_stopBits)
            {
                case StopBits.None:
                    {
                        m_comboBoxEditStopBits.SelectedIndex = 0;
                        break;
                    }
                case StopBits.One:
                    {
                        m_comboBoxEditStopBits.SelectedIndex = 1;
                        break;
                    }
                case StopBits.OnePointFive:
                    {
                        m_comboBoxEditStopBits.SelectedIndex = 2;
                        break;
                    }
                case StopBits.Two:
                    {
                        m_comboBoxEditStopBits.SelectedIndex = 3;
                        break;
                    }
            }

        }
    }
}

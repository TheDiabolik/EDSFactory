using DirectX.Capture;
using DirectX;
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
using System.Threading;
using DevExpress.XtraBars;
using System.IO.Ports;

namespace EDSFactory 
{

    public partial class PTZCameraControl : DevExpress.XtraEditors.XtraForm
    {
        private static PTZCameraControl m_PTZc;
        public MainForm m_mf;
        D pelco_d;
        //private Capture capture = null;
        public Filters filters;
        Settings.PTZControlSettings m_PTZControlSettings;

        int sayac = 0, comboBoxSelectedIndex = 0;
        byte presetin;
        uint addressin;
        byte[] msgSend;
        bool m_flagOfCircle;
        int sleepTime = 1000;

        enum presetChoice { Manuel, Grup }
        enum presetOperation { Add, Delete, Call }
        enum recordsPreset { All, Name }
        enum Zoom { ZoomIn, ZoomOut, Search }
        enum PTZRotation { Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight, Stop }

        public static PTZCameraControl Singleton(MainForm mf)
        {
            if (m_PTZc == null)
                m_PTZc = new PTZCameraControl(mf);

            return m_PTZc;
        }

        private PTZCameraControl(MainForm mf)
            : this()
        {
            m_mf = mf;
        }

        public PTZCameraControl()
        {
            InitializeComponent();

            m_PTZControlSettings = Settings.PTZControlSettings.Singleton();
            m_PTZControlSettings = m_PTZControlSettings.DeSerialize(m_PTZControlSettings);

            pelco_d = D.GiveD();
        }

        private void checkButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void m_settingsPopup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PTZControlSettingsModal settingsModal = PTZControlSettingsModal.Singleton(this);
            settingsModal.Owner = this;
            settingsModal.ShowDialog();
        }


        List<string> sa = new List<string>();

        private void updateMenu()
        {
            //ToolStripMenuItem m;
            //Filter f;
            //Control oldPreviewWindow = null;


            //// Disable preview to avoid additional flashes (optional)
            //if (capture != null)
            //{
            //    oldPreviewWindow = capture.PreviewWindow;
            //    capture.PreviewWindow = null;
            //}

            //// Load video devices
            //Filter videoDevice = null;
            //if (capture != null)
            //    videoDevice = capture.VideoDevice;
            //m_devicesVideoDevicesItem.DropDownItems.Clear();
            //m = new ToolStripMenuItem("(None)", null, new EventHandler(m_devicesVideoDevicesItem_Click));


            //m.Checked = (videoDevice == null);
            //m_devicesVideoDevicesItem.DropDownItems.Add(m);
            //for (int c = 0; c < filters.VideoInputDevices.Count; c++)
            //{
            //    f = filters.VideoInputDevices[c];
            //    m = new ToolStripMenuItem(f.Name, null, new EventHandler(m_devicesVideoDevicesItem_Click));
            //    m.Checked = (videoDevice == f);
            //    m_devicesVideoDevicesItem.DropDownItems.Add(m);
            //    sa.Add(m.Text);

            //}
            //m_devicesVideoDevicesItem.Enabled = (filters.VideoInputDevices.Count > 0);


            //// Check Preview menu option
            //m_devicesPreviewItem.Checked = (oldPreviewWindow != null);
            //m_devicesPreviewItem.Enabled = (capture != null);

            //// Reenable preview if it was enabled before
            //if (capture != null)
            //    capture.PreviewWindow = oldPreviewWindow;

        }

         



        private void PTZCameraControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_PTZc = null;
        }

        private void m_simpleButtonRightUp_Click(object sender, EventArgs e)
        {
            SimpleButton mybutton = (SimpleButton)sender;

            if (mybutton == m_simpleButtonUp)
                Rotation(PTZRotation.Up);
            else if (mybutton == m_simpleButtonDown)
                Rotation(PTZRotation.Down);
            else if (mybutton == m_simpleButtonLeft)
                Rotation(PTZRotation.Left);
            else if (mybutton == m_simpleButtonRight)
                Rotation(PTZRotation.Right);
            else if (mybutton == m_simpleButtonRightUp)
                Rotation(PTZRotation.UpRight);
            else if (mybutton == m_simpleButtonRightDown)
                Rotation(PTZRotation.DownRight);
            else if (mybutton == m_simpleButtonLeftDown)
                Rotation(PTZRotation.DownLeft);
            else if (mybutton == m_simpleButtonLeftUp)
                Rotation(PTZRotation.UpLeft);
            else if (mybutton == m_simpleButtonStop)
                Rotation(PTZRotation.Stop);
        }

        private void Rotation(PTZRotation ptz)
        {
            addressin = Byte.Parse(m_textEditAddress.Text);

            try
            {
                switch (ptz)
                {
                    case PTZRotation.Up:
                        {
                            msgSend = pelco_d.CameraTilt(addressin, D.Tilt.Up, 1);
                            m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                            m_serialPortPreset.DiscardInBuffer();
                            m_serialPortPreset.DiscardOutBuffer();
                        }
                        break;
                    case PTZRotation.Down:
                        {
                            msgSend = pelco_d.CameraTilt(addressin, D.Tilt.Down, 1);
                            m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                            m_serialPortPreset.DiscardInBuffer();
                            m_serialPortPreset.DiscardOutBuffer();
                        }
                        break;
                    case PTZRotation.Left:
                        {
                            msgSend = pelco_d.CameraPan(addressin, D.Pan.Left, 1);
                            m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                            m_serialPortPreset.DiscardInBuffer();
                            m_serialPortPreset.DiscardOutBuffer();
                        }
                        break;
                    case PTZRotation.Right:
                        {
                            msgSend = pelco_d.CameraPan(addressin, D.Pan.Right, 1);
                            m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                            m_serialPortPreset.DiscardInBuffer();
                            m_serialPortPreset.DiscardOutBuffer();
                        }
                        break;
                    case PTZRotation.UpLeft:
                        {
                            msgSend = pelco_d.CameraPanTilt(addressin, D.Pan.Left, 1, D.Tilt.Up, 1);
                            m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                            m_serialPortPreset.DiscardInBuffer();
                            m_serialPortPreset.DiscardOutBuffer();
                        }
                        break;
                    case PTZRotation.UpRight:
                        {
                            msgSend = pelco_d.CameraPanTilt(addressin, D.Pan.Right, 1, D.Tilt.Up, 1);
                            m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                            m_serialPortPreset.DiscardInBuffer();
                            m_serialPortPreset.DiscardOutBuffer();
                        }
                        break;
                    case PTZRotation.DownLeft:
                        {
                            msgSend = pelco_d.CameraPanTilt(addressin, D.Pan.Left, 1, D.Tilt.Down, 1);
                            m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                            m_serialPortPreset.DiscardInBuffer();
                            m_serialPortPreset.DiscardOutBuffer();
                        }
                        break;
                    case PTZRotation.DownRight:
                        {
                            msgSend = pelco_d.CameraPanTilt(addressin, D.Pan.Right, 1, D.Tilt.Down, 1);
                            m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                            m_serialPortPreset.DiscardInBuffer();
                            m_serialPortPreset.DiscardOutBuffer();
                        }
                        break;
                    case PTZRotation.Stop:
                        {
                            msgSend = pelco_d.CameraStop(addressin);
                            m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                            m_serialPortPreset.DiscardInBuffer();
                            m_serialPortPreset.DiscardOutBuffer();
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void ZoomCamera(Zoom zoom)
        {

            //buralara discardlican

            addressin = Byte.Parse(m_textEditAddress.Text);

            try
            {
                switch (zoom)
                {
                    case Zoom.ZoomIn:
                        {
                            msgSend = pelco_d.SetZoomLensSpeed(addressin, m_PTZControlSettings.m_lensSpeed);
                            m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                            Thread.Sleep(sleepTime);
                            msgSend = pelco_d.CameraZoom(addressin, D.Zoom.Tele);
                            m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                            Thread.Sleep(sleepTime);
                            msgSend = pelco_d.CameraStop(addressin);
                            m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                            m_serialPortPreset.DiscardInBuffer();
                            m_serialPortPreset.DiscardOutBuffer();
                        }
                        break;
                    case Zoom.ZoomOut:
                        {
                            msgSend = pelco_d.SetZoomLensSpeed(addressin, m_PTZControlSettings.m_lensSpeed);
                            m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                            Thread.Sleep(sleepTime);
                            msgSend = pelco_d.CameraZoom(addressin, D.Zoom.Wide);
                            m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                            Thread.Sleep(sleepTime);
                            msgSend = pelco_d.CameraStop(addressin);
                            m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                            m_serialPortPreset.DiscardInBuffer();
                            m_serialPortPreset.DiscardOutBuffer();
                        }
                        break;
                    case Zoom.Search:
                        {
                            addressin = Byte.Parse(m_textEditAddress.Text);
                            msgSend = pelco_d.RemoteReset(addressin);
                            m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                            m_serialPortPreset.DiscardInBuffer();
                            m_serialPortPreset.DiscardOutBuffer();
                        }
                        break;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void m_simpleButtonZoomIn_Click(object sender, EventArgs e)
        {
            SimpleButton mybutton = (SimpleButton)sender;

            if (mybutton == m_simpleButtonZoomIn)
                ZoomCamera(Zoom.ZoomIn);
            else if (mybutton == m_simpleButtonZoomOut)
                ZoomCamera(Zoom.ZoomOut);
            else if (mybutton == m_simpleButtonSearch)
                ZoomCamera(Zoom.Search);
        }

        private void m_comboBoxEditNameOfPresetGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if( m_radioGroupPreset.SelectedIndex == 1)
            {
                ListAllPreset(recordsPreset.Name);
            }
        }
        private void ListAllPreset(recordsPreset rp)
        {
            switch (rp)
            {

                case recordsPreset.Name:
                    {

                        switch (m_comboBoxEditNameOfPresetGroup.SelectedIndex)
                        {


                            case 0:
                                {
                                    m_listViewPreset.Items.Clear();
                                    int i = 0;
                                    foreach (KeyValuePair<int, string> pair in m_PTZControlSettings.TourPreset1)
                                    {
                                        m_listViewPreset.Items.Add(pair.Key.ToString());
                                        m_listViewPreset.Items[i].SubItems.Add(pair.Value.ToString());
                                        i++;
                                    }

                                }
                                break;
                            case 1:
                                {
                                    m_listViewPreset.Items.Clear();
                                    int i = 0;
                                    foreach (KeyValuePair<int, string> pair in m_PTZControlSettings.TourPreset2)
                                    {

                                        m_listViewPreset.Items.Add(pair.Key.ToString());
                                        m_listViewPreset.Items[i].SubItems.Add(pair.Value.ToString());
                                        i++;
                                    }

                                }
                                break;
                            case 2:
                                {
                                    m_listViewPreset.Items.Clear();
                                    int i = 0;
                                    foreach (KeyValuePair<int, string> pair in m_PTZControlSettings.TourPreset3)
                                    {

                                        m_listViewPreset.Items.Add(pair.Key.ToString());
                                        m_listViewPreset.Items[i].SubItems.Add(pair.Value.ToString());
                                        i++;
                                    }
                                }
                                break;
                            case 3:
                                {
                                    m_listViewPreset.Items.Clear();
                                    int i = 0;
                                    foreach (KeyValuePair<int, string> pair in m_PTZControlSettings.TourPreset4)
                                    {

                                        m_listViewPreset.Items.Add(pair.Key.ToString());
                                        m_listViewPreset.Items[i].SubItems.Add(pair.Value.ToString());
                                        i++;
                                    }

                                }
                                break;
                            case 4:
                                {
                                    m_listViewPreset.Items.Clear();
                                    int i = 0;
                                    foreach (KeyValuePair<int, string> pair in m_PTZControlSettings.TourPreset5)
                                    {

                                        m_listViewPreset.Items.Add(pair.Key.ToString());
                                        m_listViewPreset.Items[i].SubItems.Add(pair.Value.ToString());
                                        i++;
                                    }
                                }
                                break;
                            case 5:
                                {
                                    m_listViewPreset.Items.Clear();
                                    int i = 0;
                                    foreach (KeyValuePair<int, string> pair in m_PTZControlSettings.TourPreset6)
                                    {

                                        m_listViewPreset.Items.Add(pair.Key.ToString());
                                        m_listViewPreset.Items[i].SubItems.Add(pair.Value.ToString());
                                        i++;
                                    }

                                }
                                break;
                            case 6:
                                {
                                    m_listViewPreset.Items.Clear();
                                    int i = 0;
                                    foreach (KeyValuePair<int, string> pair in m_PTZControlSettings.TourPreset7)
                                    {

                                        m_listViewPreset.Items.Add(pair.Key.ToString());
                                        m_listViewPreset.Items[i].SubItems.Add(pair.Value.ToString());
                                        i++;
                                    }
                                }
                                break;
                            case 7:
                                {
                                    m_listViewPreset.Items.Clear();
                                    int i = 0;
                                    foreach (KeyValuePair<int, string> pair in m_PTZControlSettings.TourPreset8)
                                    {

                                        m_listViewPreset.Items.Add(pair.Key.ToString());
                                        m_listViewPreset.Items[i].SubItems.Add(pair.Value.ToString());
                                        i++;
                                    }
                                }
                                break;
                            case 8:
                                {
                                    m_listViewPreset.Items.Clear();
                                    int i = 0;
                                    foreach (KeyValuePair<int, string> pair in m_PTZControlSettings.TourPreset9)
                                    {

                                        m_listViewPreset.Items.Add(pair.Key.ToString());
                                        m_listViewPreset.Items[i].SubItems.Add(pair.Value.ToString());
                                        i++;
                                    }
                                }
                                break;
                            case 9:
                                {
                                    m_listViewPreset.Items.Clear();
                                    int i = 0;
                                    foreach (KeyValuePair<int, string> pair in m_PTZControlSettings.TourPreset10)
                                    {

                                        m_listViewPreset.Items.Add(pair.Key.ToString());
                                        m_listViewPreset.Items[i].SubItems.Add(pair.Value.ToString());
                                        i++;
                                    }
                                }
                                break;

                        }
                    }
                    break;

            }
        }

        private void m_radioGroupPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_listViewPreset.Items.Clear();

            if(m_radioGroupPreset.SelectedIndex == 0)
            {
                m_spinEditAllPreset.Enabled = true;
                m_spinEditAllPreset_EditValueChanged(sender, e);
                m_PTZControlSettings.m_presetChoice = true;
                m_comboBoxEditNameOfPresetGroup.Enabled = false;
                m_PTZControlSettings.Serialize(m_PTZControlSettings);

            }
            else
            {
                m_spinEditAllPreset.Enabled = false;
                m_PTZControlSettings.m_presetChoice = false;

                m_comboBoxEditNameOfPresetGroup.Enabled = true;
                m_comboBoxEditNameOfPresetGroup.SelectedIndex = comboBoxSelectedIndex;
                m_comboBoxEditNameOfPresetGroup_SelectedIndexChanged(sender, e);

                m_PTZControlSettings.Serialize(m_PTZControlSettings);

                //m_listViewPreset.Items.Clear();
            }
        }

        private void m_spinEditAllPreset_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                m_PTZControlSettings.m_totalPresetNumber = int.Parse(m_spinEditAllPreset.Value.ToString());
                m_PTZControlSettings.Serialize(m_PTZControlSettings);

                m_listViewPreset.Items.Clear();

                for (int i = 0; i < int.Parse(m_spinEditAllPreset.Value.ToString()); i++)
                {
                    m_listViewPreset.Items.Add((i + 1).ToString());
                    m_listViewPreset.Items[i].SubItems.Add("Kayıtlı Grup Yok!");
                }
            }
            catch
            {
                MessageBox.Show("Toplam Preset Alınamadı !?");
            }
        }

        private void m_comboBoxEditTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_PTZControlSettings.m_presetChangingTime = m_comboBoxEditTime.SelectedIndex;
            m_PTZControlSettings.Serialize(m_PTZControlSettings);
        }

        private void PTZCameraControl_Load(object sender, EventArgs e)
        {
            try
            {
                m_barStaticItemConnectedPortName.Caption = "";

                if (!string.IsNullOrEmpty(m_PTZControlSettings.m_portName))
                {
                    if (m_PTZControlSettings.m_autoStart)
                    {
                        m_openSerialPortItem.Checked = true;


                        m_serialPortItems_Click(m_openSerialPortItem, null);

                        TurnIt();
                    }
                    else
                    {
                        m_barStaticItemConnectedPortName.Caption = m_PTZControlSettings.m_portName;
                        m_barStaticItemConnectedPortName.ItemAppearance.Normal.BackColor = Color.Red;
                    }
                }
                else
                {
                    m_barStaticItemConnectedPortName.Caption = "Seri Port Bulunamadı!";
                    m_barStaticItemConnectedPortName.ItemAppearance.Normal.BackColor = Color.Red;
                }

                //program ayarları
                if (m_PTZControlSettings.m_totalPresetNumber < 0 && m_PTZControlSettings.m_totalPresetNumber > 256)
                {
                    m_spinEditAllPreset.Value = 1;
                    m_PTZControlSettings.m_totalPresetNumber = 1;
                    m_PTZControlSettings.Serialize(m_PTZControlSettings);
                }
                else
                    m_spinEditAllPreset.Value = m_PTZControlSettings.m_totalPresetNumber;//burada ne problem var?


                //presetleri dolduruyor
                for (int i = 0; i < 256; i++)
                    m_comboBoxEditPresetNo.Properties.Items.Add((i + 1));

                //kayıtlı lens ayarı getiriyor.
                switch (m_PTZControlSettings.m_lensSpeed)
                {
                    case D.LensSpeed.Low:
                        {
                            m_radioGroupZoomSpeed.SelectedIndex = 0;
                        }
                        break;
                    case D.LensSpeed.Medium:
                        {
                            m_radioGroupZoomSpeed.SelectedIndex = 1;
                        }
                        break;
                    case D.LensSpeed.High:
                        {
                            m_radioGroupZoomSpeed.SelectedIndex = 2;
                        }
                        break;
                    case D.LensSpeed.Turbo:
                        {
                            m_radioGroupZoomSpeed.SelectedIndex = 3;
                        }
                        break;
                }

                //preset geçiş zaman ayarı
                m_comboBoxEditTime.SelectedIndex = m_PTZControlSettings.m_presetChangingTime;

                //manuel mı kayıtlı preset mi?
                if (m_PTZControlSettings.m_presetChoice)
                    m_radioGroupPreset.SelectedIndex = 0;
                else
                    m_radioGroupPreset.SelectedIndex = 1;

                //checkboxin üzerine gidince açılan yazı
                //m_toolTipNewPreset.SetToolTip(m_checkBoxNewPreset, "Seçilirse Kameraya Preset Kaydeder/Siler !");
            }
            catch
            {
                MessageBox.Show("Program Yüklenirken Problem ile Karşılaşıldı !?");
            }
        }

        private void m_serialPortPreset_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

        }

       

        private void m_serialPortItems_Click(object sender, ItemClickEventArgs e)
        {  
            BarCheckItem m = sender as BarCheckItem; 

            if(m==null)
                m = e.Item as BarCheckItem; 

            if (m == m_openSerialPortItem)
            {
                m_openSerialPortItem.Checked = true;
                m_closeSerialPortItem.Checked = false;
            }
            else if (m == m_closeSerialPortItem)
            {
                m_openSerialPortItem.Checked = false;
                m_closeSerialPortItem.Checked = true;
            }

            serialPortController(m_openSerialPortItem.Checked);
        }

        private void m_openSerialPortItem_CheckedChanged(object sender, ItemClickEventArgs e)
        {

            //BarCheckItem m = (BarCheckItem)sender; 


            //if (m == m_openSerialPortItem)
            //{
            //    m_openSerialPortItem.Checked = true;
            //    m_closeSerialPortItem.Checked = false;
            //}
            //else if (m == m_closeSerialPortItem)
            //{
            //    m_openSerialPortItem.Checked = false;
            //    m_closeSerialPortItem.Checked = true;
            //}
        }

        private void m_timerPreset_Tick(object sender, EventArgs e)
        {
            m_timerPreset.Interval = int.Parse(m_comboBoxEditTime.Text) * 1000;
            addressin = Byte.Parse(m_textEditAddress.Text);

            try
            {
                if (sayac >= m_listViewPreset.Items.Count)
                {

                    sayac = 0;
                }

                if (m_radioGroupPreset.SelectedIndex == 0)
                {
                    if (m_listViewPreset.Items.Count >= sayac)
                    {
                        presetin = Byte.Parse(m_listViewPreset.Items[sayac].Text);
                        msgSend = pelco_d.Preset(addressin, presetin, D.PresetAction.Goto);
                        m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                        m_serialPortPreset.DiscardInBuffer();
                        m_serialPortPreset.DiscardOutBuffer();
                    }
                }
                else
                {
                    if (m_listViewPreset.Items.Count >= sayac)
                    {
                        presetin = Byte.Parse(m_listViewPreset.Items[sayac].Text);
                        msgSend = pelco_d.Preset(addressin, presetin, D.PresetAction.Goto);
                        m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                        m_serialPortPreset.DiscardInBuffer();
                        m_serialPortPreset.DiscardOutBuffer();
                    }
                }

                m_listViewPreset.Select();
                m_listViewPreset.Items[sayac].Selected = true;
                sayac++;


                if (sayac >= m_listViewPreset.Items.Count)
                {
                    m_timerPreset.Stop();

                    if (m_PTZControlSettings.m_waitingTimeUnit)
                        m_timerWaiter.Interval = m_PTZControlSettings.m_waitingTime * 1000;
                    else
                        m_timerWaiter.Interval = m_PTZControlSettings.m_waitingTime * 10000;

                    m_timerWaiter.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void m_timerWaiter_Tick(object sender, EventArgs e)
        {
            m_timerWaiter.Stop();
            m_timerPreset.Start();
        }



        private void TurnIt()
        {
            try
            {
                if (!m_serialPortPreset.IsOpen)
                {
                    MessageBox.Show("Seri Port Bağlantı Noktasını Açınız !?");
                    return;
                }
                if (m_timerPreset.Enabled)
                {
                    m_timerPreset.Interval = 1;
                }
                if (!m_flagOfCircle)
                {
                    m_timerPreset.Start();
                    m_simpleButtonCircle.BackColor = Color.Red;
                    m_simpleButtonCircle.Text = "Dur";

                    sayac = 0;

                    m_spinEditAllPreset.Enabled = m_flagOfCircle;
                    m_comboBoxEditTime.Enabled = m_flagOfCircle;
                    m_textEditAddress.Enabled = m_flagOfCircle;
                    m_radioGroupPreset.Enabled = m_flagOfCircle;
                    m_comboBoxEditNameOfPresetGroup.Enabled = m_flagOfCircle;
                    m_simpleButtonAddPresetToGroup.Enabled = m_flagOfCircle;
                    m_simpleButtonDeletePresetToGroup.Enabled = m_flagOfCircle;
                    m_checkEditNewPreset.Enabled = m_flagOfCircle;
                    m_comboBoxEditPresetNo.Enabled = m_flagOfCircle;
                }

                if (m_flagOfCircle)
                {
                    m_simpleButtonCircle.BackColor = Color.Green;
                    m_simpleButtonCircle.Text = "Döndür";
                    m_timerPreset.Stop();

                    m_spinEditAllPreset.Enabled = m_flagOfCircle;
                    m_comboBoxEditTime.Enabled = m_flagOfCircle;
                    m_textEditAddress.Enabled = m_flagOfCircle;
                    m_radioGroupPreset.Enabled = m_flagOfCircle;
                    m_comboBoxEditNameOfPresetGroup.Enabled = m_flagOfCircle;
                    m_simpleButtonAddPresetToGroup.Enabled = m_flagOfCircle;
                    m_simpleButtonDeletePresetToGroup.Enabled = m_flagOfCircle;
                    m_checkEditNewPreset.Enabled = m_flagOfCircle;
                    m_comboBoxEditPresetNo.Enabled = m_flagOfCircle;

                }

                if (!m_flagOfCircle)
                {
                    m_flagOfCircle = true;
                }
                else
                {
                    m_flagOfCircle = false;
                }

            }
            catch
            {
                MessageBox.Show("PTZ İşlemi Başlatılamadı !?");
            }
        }

        public void serialPortController(bool serialPortStatus)
        {
            m_PTZControlSettings = m_PTZControlSettings.DeSerialize(m_PTZControlSettings);


            List<string> serialPorts = new List<string>(SerialPort.GetPortNames());
            bool hasSerialPort = serialPorts.Contains(m_PTZControlSettings.m_portName);

            if (!hasSerialPort)
            {
                m_PTZControlSettings.m_portName = "";
                //m_s.m_serialPortNameIndex = -1;

                m_PTZControlSettings.Serialize(m_PTZControlSettings);
                m_PTZControlSettings = m_PTZControlSettings.DeSerialize(m_PTZControlSettings);
            }


            if (!string.IsNullOrEmpty(m_PTZControlSettings.m_portName))
            {
                if (m_serialPortPreset.IsOpen && !serialPortStatus)
                {
                    m_serialPortPreset.Close();
                    m_barStaticItemConnectedPortName.Caption  = m_PTZControlSettings.m_portName;
                    m_barStaticItemConnectedPortName.ItemAppearance.Normal.BackColor = Color.Red;

                }
                else if (!m_serialPortPreset.IsOpen && serialPortStatus)
                {
                    m_serialPortPreset.PortName = m_PTZControlSettings.m_portName;
                    m_serialPortPreset.Open();
                    m_barStaticItemConnectedPortName.Caption = m_PTZControlSettings.m_portName;
                    m_barStaticItemConnectedPortName.ItemAppearance.Normal.BackColor = Color.Green;

                }
            }
            else
            {
                m_barStaticItemConnectedPortName.Caption = "Seri Port Bulunamadı!";
                m_barStaticItemConnectedPortName.ItemAppearance.Normal.BackColor = Color.Red;
            }
        }

        private void m_simpleButtonCircle_Click(object sender, EventArgs e)
        {
            TurnIt();
        }

        private void m_listViewPreset_DoubleClick(object sender, EventArgs e)
        {
            RecordPresets(presetOperation.Call);
        }


        private void RecordPresets(presetOperation po)
        {
            switch (po)
            {
                case presetOperation.Add:
                    {
                        presetin = Byte.Parse(m_comboBoxEditPresetNo.Text);
                        addressin = Byte.Parse(m_textEditAddress.Text);
                        msgSend = pelco_d.Preset(addressin, presetin, D.PresetAction.Set);
                        m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                        m_serialPortPreset.DiscardInBuffer();
                        m_serialPortPreset.DiscardOutBuffer();
                    }
                    break;
                case presetOperation.Delete:
                    {
                        presetin = Byte.Parse(m_comboBoxEditPresetNo.Text);
                        addressin = Byte.Parse(m_textEditAddress.Text);
                        msgSend = pelco_d.Preset(addressin, presetin, D.PresetAction.Clear);
                        m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                        m_serialPortPreset.DiscardInBuffer();
                        m_serialPortPreset.DiscardOutBuffer();
                    }
                    break;
                case presetOperation.Call:
                    {
                        presetin = Byte.Parse(m_listViewPreset.SelectedItems[0].Text);
                        addressin = Byte.Parse(m_textEditAddress.Text);
                        msgSend = pelco_d.Preset(addressin, presetin, D.PresetAction.Goto);
                        m_serialPortPreset.Write(msgSend, 0, msgSend.Length);
                        m_serialPortPreset.DiscardInBuffer();
                        m_serialPortPreset.DiscardOutBuffer();
                    }
                    break;
            }

        }

        private void m_simpleButtonAddPresetToGroup_Click(object sender, EventArgs e)
        {
            try
            {

                if (m_comboBoxEditPresetNo.Text == "")
                {
                    MessageBox.Show("Seçilebilecek Preset No Yoktur !?");
                    return;
                }


                switch (m_comboBoxEditNameOfPresetGroup.SelectedIndex)
                {
                    case 0:
                        {
                            if (!m_PTZControlSettings.TourPreset1.ContainsKey(int.Parse(m_comboBoxEditPresetNo.Text)))
                            {
                                m_PTZControlSettings.TourPreset1.Add(int.Parse(m_comboBoxEditPresetNo.Text), m_comboBoxEditNameOfPresetGroup.Text);
                            }
                            else
                            {
                                MessageBox.Show("Kayıt Mevcut !");
                                return;
                            }
                        }
                        break;
                    case 1:
                        {
                            if (!m_PTZControlSettings.TourPreset2.ContainsKey(int.Parse(m_comboBoxEditPresetNo.Text)))
                            {
                                m_PTZControlSettings.TourPreset2.Add(int.Parse(m_comboBoxEditPresetNo.Text), m_comboBoxEditNameOfPresetGroup.Text);
                            }
                            else
                            {
                                MessageBox.Show("Kayıt Mevcut !");
                                return;
                            }

                        }
                        break;
                    case 2:
                        {
                            if (!m_PTZControlSettings.TourPreset3.ContainsKey(int.Parse(m_comboBoxEditPresetNo.Text)))
                            {
                                m_PTZControlSettings.TourPreset3.Add(int.Parse(m_comboBoxEditPresetNo.Text), m_comboBoxEditNameOfPresetGroup.Text);
                            }
                            else
                            {
                                MessageBox.Show("Kayıt Mevcut !");
                                return;
                            }
                        }
                        break;
                    case 3:
                        {
                            if (!m_PTZControlSettings.TourPreset4.ContainsKey(int.Parse(m_comboBoxEditPresetNo.Text)))
                            {
                                m_PTZControlSettings.TourPreset4.Add(int.Parse(m_comboBoxEditPresetNo.Text), m_comboBoxEditNameOfPresetGroup.Text);
                            }
                            else
                            {
                                MessageBox.Show("Kayıt Mevcut !");
                                return;
                            }

                        }
                        break;
                    case 4:
                        {
                            if (!m_PTZControlSettings.TourPreset5.ContainsKey(int.Parse(m_comboBoxEditPresetNo.Text)))
                            {
                                m_PTZControlSettings.TourPreset5.Add(int.Parse(m_comboBoxEditPresetNo.Text), m_comboBoxEditNameOfPresetGroup.Text);
                            }
                            else
                            {
                                MessageBox.Show("Kayıt Mevcut !");
                                return;
                            }
                        }
                        break;
                    case 5:
                        {
                            if (!m_PTZControlSettings.TourPreset6.ContainsKey(int.Parse(m_comboBoxEditPresetNo.Text)))
                            {
                                m_PTZControlSettings.TourPreset6.Add(int.Parse(m_comboBoxEditPresetNo.Text), m_comboBoxEditNameOfPresetGroup.Text);
                            }
                            else
                            {
                                MessageBox.Show("Kayıt Mevcut !");
                                return;
                            }

                        }
                        break;
                    case 6:
                        {
                            if (!m_PTZControlSettings.TourPreset7.ContainsKey(int.Parse(m_comboBoxEditPresetNo.Text)))
                            {
                                m_PTZControlSettings.TourPreset7.Add(int.Parse(m_comboBoxEditPresetNo.Text), m_comboBoxEditNameOfPresetGroup.Text);
                            }
                            else
                            {
                                MessageBox.Show("Kayıt Mevcut !");
                                return;
                            }
                        }
                        break;
                    case 7:
                        {
                            if (!m_PTZControlSettings.TourPreset8.ContainsKey(int.Parse(m_comboBoxEditPresetNo.Text)))
                            {
                                m_PTZControlSettings.TourPreset8.Add(int.Parse(m_comboBoxEditPresetNo.Text), m_comboBoxEditNameOfPresetGroup.Text);
                            }
                            else
                            {
                                MessageBox.Show("Kayıt Mevcut !");
                                return;
                            }
                        }
                        break;
                    case 8:
                        {
                            if (!m_PTZControlSettings.TourPreset9.ContainsKey(int.Parse(m_comboBoxEditPresetNo.Text)))
                            {
                                m_PTZControlSettings.TourPreset9.Add(int.Parse(m_comboBoxEditPresetNo.Text), m_comboBoxEditNameOfPresetGroup.Text);
                            }
                            else
                            {
                                MessageBox.Show("Kayıt Mevcut !");
                                return;
                            }
                        }
                        break;
                    case 9:
                        {
                            if (!m_PTZControlSettings.TourPreset10.ContainsKey(int.Parse(m_comboBoxEditPresetNo.Text)))
                            {
                                m_PTZControlSettings.TourPreset10.Add(int.Parse(m_comboBoxEditPresetNo.Text), m_comboBoxEditNameOfPresetGroup.Text);
                            }
                            else
                            {
                                MessageBox.Show("Kayıt Mevcut !");
                                return;
                            }
                        }
                        break;

                }

                ListAllPreset(recordsPreset.Name);

                if (m_checkEditNewPreset.Checked)
                {
                    RecordPresets(presetOperation.Add);
                }
                m_PTZControlSettings.Serialize(m_PTZControlSettings);
            }
            catch
            {
                MessageBox.Show("Preset(ler) Kaydedilemedi !?");
            }
        }

        private void m_simpleButtonDeletePresetToGroup_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr;

                if (m_listViewPreset.SelectedIndices.Count > 0)
                {
                    dr = MessageBox.Show("Seçili Kayıt Silinecektir !?", "Preset v1.7", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {

                        switch (m_comboBoxEditNameOfPresetGroup.SelectedIndex)
                        {
                            case 0:
                                {
                                    m_PTZControlSettings.TourPreset1.Remove(int.Parse(m_listViewPreset.SelectedItems[0].Text));
                                }
                                break;
                            case 1:
                                {
                                    m_PTZControlSettings.TourPreset2.Remove(int.Parse(m_listViewPreset.SelectedItems[0].Text));
                                }
                                break;
                            case 2:
                                {
                                    m_PTZControlSettings.TourPreset3.Remove(int.Parse(m_listViewPreset.SelectedItems[0].Text));
                                }
                                break;
                            case 3:
                                {
                                    m_PTZControlSettings.TourPreset4.Remove(int.Parse(m_listViewPreset.SelectedItems[0].Text));

                                }
                                break;
                            case 4:
                                {
                                    m_PTZControlSettings.TourPreset5.Remove(int.Parse(m_listViewPreset.SelectedItems[0].Text));
                                }
                                break;
                            case 5:
                                {
                                    m_PTZControlSettings.TourPreset6.Remove(int.Parse(m_listViewPreset.SelectedItems[0].Text));
                                }
                                break;
                            case 6:
                                {
                                    m_PTZControlSettings.TourPreset7.Remove(int.Parse(m_listViewPreset.SelectedItems[0].Text));
                                }
                                break;
                            case 7:
                                {
                                    m_PTZControlSettings.TourPreset8.Remove(int.Parse(m_listViewPreset.SelectedItems[0].Text));
                                }
                                break;
                            case 8:
                                {
                                    m_PTZControlSettings.TourPreset9.Remove(int.Parse(m_listViewPreset.SelectedItems[0].Text));
                                }
                                break;
                            case 9:
                                {
                                    m_PTZControlSettings.TourPreset10.Remove(int.Parse(m_listViewPreset.SelectedItems[0].Text));
                                }
                                break;

                        }


                        if (m_checkEditNewPreset.Checked)
                        {
                            RecordPresets(presetOperation.Delete);
                        }

                        m_listViewPreset.SelectedItems[0].Remove();
                        m_PTZControlSettings.Serialize(m_PTZControlSettings);

                    }
                }
                else
                {
                    MessageBox.Show("Preset No Seçiniz !?");
                }
            }
            catch
            {
                MessageBox.Show("Kayıt(lar) Silinemedi !?");
            }
        }

        private void m_radioGroupZoomSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
      
                if (m_radioGroupZoomSpeed.SelectedIndex == 0)
                    m_PTZControlSettings.m_lensSpeed = D.LensSpeed.Low;
                else if (m_radioGroupZoomSpeed.SelectedIndex == 1)
                    m_PTZControlSettings.m_lensSpeed = D.LensSpeed.Medium;
                else if (m_radioGroupZoomSpeed.SelectedIndex == 2)
                    m_PTZControlSettings.m_lensSpeed = D.LensSpeed.High;
                else if (m_radioGroupZoomSpeed.SelectedIndex == 3)
                    m_PTZControlSettings.m_lensSpeed = D.LensSpeed.Turbo;

                m_PTZControlSettings.Serialize(m_PTZControlSettings);
        }
    }
}

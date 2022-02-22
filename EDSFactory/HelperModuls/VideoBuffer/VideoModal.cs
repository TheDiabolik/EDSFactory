using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.FFMPEG;
using AForge.Video;
using System.IO;
using DevExpress.XtraTab;


namespace EDSFactory
{
    public partial class VideoModal : DevExpress.XtraEditors.XtraForm, ISyncFileWatcher
    {
        private static VideoModal m_fhs;
        public MainForm m_mf; 
        Settings.Video m_settings;
        System.Timers.Timer m_timer;
        bool m_videoProcessSwitch = true, m_isCameraRunning;
        Dictionary<int, Camera> ahmet = new Dictionary<int, Camera>();

        Dictionary<string, int> m_cameras;

        public VideoModal()
        {
            InitializeComponent();
            MainForm.m_ahmet.AddWatcher(this);

            m_timer = new System.Timers.Timer(5000);
            m_timer.Elapsed += m_timer_Elapsed;

            m_settings = Settings.Video.Singleton();
            m_settings = m_settings.DeSerialize(m_settings);


            m_cameras = new Dictionary<string, int>();

            m_buttonEditImagesPath.Text = m_settings.m_imagePath;
            m_buttonEditImagesPath.Text = m_settings.m_imagePath;
            m_checkEditFirstCamera.Checked = m_settings.m_firstCamera;
            m_comboBoxEditFirstCameraType.Text = m_settings.m_firstCameraType;
            m_checkEditSecondCamera.Checked = m_settings.m_secondCamera;
            m_comboBoxEditSecondCameraType.Text = m_settings.m_secondCameraType;
            m_checkEditThirdCamera.Checked = m_settings.m_thirdCamera;
            m_comboBoxEditThirdCameraType.Text = m_settings.m_thirdCameraType;
            m_checkEditFourthCamera.Checked = m_settings.m_fourthCamera;
            m_comboBoxEditFourthCameraType.Text = m_settings.m_fourthCameraType;
            m_checkEditAutoStart.Checked = m_settings.m_autoStart;



            if (m_checkEditFirstCamera.Checked)
                m_cameras.Add(m_comboBoxEditFirstCameraType.Text,0);

            if (m_checkEditSecondCamera.Checked)
                m_cameras.Add( m_comboBoxEditSecondCameraType.Text,1);

            if (m_checkEditThirdCamera.Checked)
                m_cameras.Add( m_comboBoxEditThirdCameraType.Text,2);

            if (m_checkEditFourthCamera.Checked)
                m_cameras.Add( m_comboBoxEditFourthCameraType.Text,3);

            for (int i = 0; i < m_settings.Cameras.Count; i++)
            {
                switch(i)
                {
                    case 0 :
                        {
                            //giriş kamerası
                            m_comboBoxEditEntryCamStream.SelectedIndex = m_settings.Cameras[i].m_streamType;
                            m_textEditEntryCamURL.Text = m_settings.Cameras[i].m_cameraURL;
                            m_textEditEntryCamUserName.Text = m_settings.Cameras[i].m_userName;
                            m_textEditEntryCamPassword.Text = m_settings.Cameras[i].m_password;
                            m_spinEditEntryCamFPS.Value = Convert.ToInt32(m_settings.Cameras[i].m_beforeAndAfterSecond);
                            break;
                        }
                    case 1:
                        {
                            //çıkış kamerası
                            m_comboBoxEditExitCamStream.SelectedIndex = m_settings.Cameras[i].m_streamType;
                            m_textEditExitCamURL.Text = m_settings.Cameras[i].m_cameraURL;
                            m_textEditExitCamUserName.Text = m_settings.Cameras[i].m_userName;
                            m_textEditExitCamPassword.Text = m_settings.Cameras[i].m_password;
                            m_spinEditExitCamFPS.Value = Convert.ToInt32(m_settings.Cameras[i].m_beforeAndAfterSecond);
                            break;
                        }
                    case 2:
                        {
                            //giriş kamerası
                            m_comboBoxEditThirdCamStream.SelectedIndex = m_settings.Cameras[i].m_streamType;
                            m_textEditThirdCamURL.Text = m_settings.Cameras[i].m_cameraURL;
                            m_textEditThirdCamUserName.Text = m_settings.Cameras[i].m_userName;
                            m_textEditThirdCamPassword.Text = m_settings.Cameras[i].m_password;
                            m_spinEditThirdCamFPS.Value = Convert.ToInt32(m_settings.Cameras[i].m_beforeAndAfterSecond);
                            break;
                        }
                    case 3:
                        {
                            //giriş kamerası
                            m_comboBoxEditFourthCamStream.SelectedIndex = m_settings.Cameras[i].m_streamType;
                            m_textEditFourthCamURL.Text = m_settings.Cameras[i].m_cameraURL;
                            m_textEditFourthCamUserName.Text = m_settings.Cameras[i].m_userName;
                            m_textEditFourthCamPassword.Text = m_settings.Cameras[i].m_password;
                            m_spinEditFourthCamFPS.Value = Convert.ToInt32(m_settings.Cameras[i].m_beforeAndAfterSecond);
                            break;
                        }
                } 
            }


            if (m_settings.m_autoStart)
                m_timer.Start();
            else
                m_timer.Stop();
        }

        public static VideoModal Singleton(MainForm mf)
        {
            if (m_fhs == null)
                m_fhs = new VideoModal(mf);

            return m_fhs;
        }

        private VideoModal(MainForm mf)
            : this()
        {
            m_mf = mf;
        }

        void m_timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (m_videoProcessSwitch)
                VideoProcess();
        }

     
  
        private void VideoModal_Load(object sender, EventArgs e)
        {
            
        } 

        dynamic camPreview;
        private void m_simpleButtonEntryCamPreview_Click(object sender, EventArgs e)
        {
            if(m_xtraTabControl.SelectedTabPageIndex  == 1)
            {
                if (m_comboBoxEditEntryCamStream.SelectedIndex == 0)
                    camPreview = new MJPEGStream();
                else
                    camPreview = new JPEGStream();

                camPreview.Source = m_textEditEntryCamURL.Text;
                camPreview.Login = m_textEditEntryCamUserName.Text;
                camPreview.Password = m_textEditEntryCamPassword.Text;

                camPreview.NewFrame += new NewFrameEventHandler(ef_NewFrame);
              
                camPreview.Start();
            }
            else if (m_xtraTabControl.SelectedTabPageIndex == 2)
            {
                if (m_comboBoxEditExitCamStream.SelectedIndex == 0)
                    camPreview = new MJPEGStream();
                else
                    camPreview = new JPEGStream();

                camPreview.Source = m_textEditExitCamURL.Text;
                camPreview.Login = m_textEditExitCamUserName.Text;
                camPreview.Password = m_textEditExitCamPassword.Text;

                camPreview.NewFrame += new NewFrameEventHandler(ef_NewFrame);

                camPreview.Start();
            }
        }


        int counter = 0;

        void ef_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (counter < 50)
            {
                counter++;

                Bitmap FrameDate = (Bitmap)eventArgs.Frame.Clone();

                if (m_pictureEditEntryCamPreview.InvokeRequired)
                    m_pictureEditEntryCamPreview.Invoke((MethodInvoker)delegate
                    {
                        m_pictureEditEntryCamPreview.Image = FrameDate;
                    });
                else
                {
                    m_pictureEditEntryCamPreview.Image = FrameDate;
                }

            }
            else
            {
                counter = 0;

                if (camPreview.IsRunning)
                {
                    if (m_pictureEditEntryCamPreview.InvokeRequired)
                        m_pictureEditEntryCamPreview.Invoke((MethodInvoker)delegate
                        {
                            m_pictureEditEntryCamPreview.Image = Properties.Resources.test;
                        });
                    else
                    {
                        m_pictureEditEntryCamPreview.Image = Properties.Resources.test;
                    }

                    camPreview.Stop();

                }

                
                camPreview.NewFrame -= new NewFrameEventHandler(ef_NewFrame);
                camPreview.Dispose();
            }

        }

        private void m_simpleButtonSave_Click(object sender, EventArgs e)
        { 
            if (m_timer.Enabled)
            {
                m_timer.Stop(); 

                m_videoProcessSwitch  = true;
                m_startPopup.Caption = "Başlat";
            }
              
            m_settings.m_imagePath = m_buttonEditImagesPath.Text;
            m_settings.m_firstCamera = m_checkEditFirstCamera.Checked;
            m_settings.m_firstCameraType = m_comboBoxEditFirstCameraType.Text;
            m_settings.m_secondCamera = m_checkEditSecondCamera.Checked;
            m_settings.m_secondCameraType = m_comboBoxEditSecondCameraType.Text;
            m_settings.m_thirdCamera = m_checkEditThirdCamera.Checked;
            m_settings.m_thirdCameraType = m_comboBoxEditThirdCameraType.Text;
            m_settings.m_fourthCamera = m_checkEditFourthCamera.Checked;
            m_settings.m_fourthCameraType = m_comboBoxEditFourthCameraType.Text; 
            m_settings.m_autoStart = m_checkEditAutoStart.Checked;  

            m_settings.Cameras.Clear(); 

            for (int i = 0; i < m_xtraTabControl.TabPages.Count; i++)
            {
                switch(m_xtraTabControl.TabPages[i].Name)
                {
                    case "m_xtraTabPageFirstCamera":
                        {
                            //giriş kamerası 
                            CameraSettings entryCamera = new CameraSettings();
                            entryCamera.m_streamType = m_comboBoxEditEntryCamStream.SelectedIndex;
                            entryCamera.m_cameraURL = m_textEditEntryCamURL.Text;
                            entryCamera.m_userName = m_textEditEntryCamUserName.Text;
                            entryCamera.m_password = m_textEditEntryCamPassword.Text;
                            entryCamera.m_beforeAndAfterSecond = Convert.ToInt32(m_spinEditEntryCamFPS.Value);

                            m_settings.Cameras.Add(0, entryCamera);

                            break;
                        }
                    case "m_xtraTabPageSecondCamera":
                        {
                            //çıkış kamerası
                            CameraSettings exitCamera = new CameraSettings();
                            exitCamera.m_streamType = m_comboBoxEditExitCamStream.SelectedIndex;
                            exitCamera.m_cameraURL = m_textEditExitCamURL.Text;
                            exitCamera.m_userName = m_textEditExitCamUserName.Text;
                            exitCamera.m_password = m_textEditExitCamPassword.Text;
                            exitCamera.m_beforeAndAfterSecond = Convert.ToInt32(m_spinEditExitCamFPS.Value);

                            m_settings.Cameras.Add(1, exitCamera);

                            break;
                        }
                    case "m_xtraTabPageThirdCamera":
                        {
                            //çıkış kamerası
                            CameraSettings camera = new CameraSettings();
                            camera.m_streamType = m_comboBoxEditThirdCamStream.SelectedIndex;
                            camera.m_cameraURL = m_textEditThirdCamURL.Text;
                            camera.m_userName = m_textEditThirdCamUserName.Text;
                            camera.m_password = m_textEditThirdCamPassword.Text;
                            camera.m_beforeAndAfterSecond = Convert.ToInt32(m_spinEditThirdCamFPS.Value);

                            m_settings.Cameras.Add(2, camera);

                            break;
                        }
                    case "m_xtraTabPageFouthCamera":
                        {
                            //çıkış kamerası
                            CameraSettings camera = new CameraSettings();
                            camera.m_streamType = m_comboBoxEditFourthCamStream.SelectedIndex;
                            camera.m_cameraURL = m_textEditFourthCamURL.Text;
                            camera.m_userName = m_textEditFourthCamUserName.Text;
                            camera.m_password = m_textEditFourthCamPassword.Text;
                            camera.m_beforeAndAfterSecond = Convert.ToInt32(m_spinEditFourthCamFPS.Value);

                            m_settings.Cameras.Add(3, camera);

                            break;
                        }
                }
                 
            }  
 

            m_settings.Serilize(m_settings);
            m_settings = m_settings.DeSerialize(m_settings);  

            m_cameras.Clear();

            if (m_checkEditFirstCamera.Checked)
                m_cameras.Add(m_comboBoxEditFirstCameraType.Text, 0);

            if (m_checkEditSecondCamera.Checked)
                m_cameras.Add(m_comboBoxEditSecondCameraType.Text, 1);

            if (m_checkEditThirdCamera.Checked)
                m_cameras.Add(m_comboBoxEditThirdCameraType.Text, 2);

            if (m_checkEditFourthCamera.Checked)
                m_cameras.Add(m_comboBoxEditFourthCameraType.Text, 3);


            if (m_settings.m_autoStart)
                m_timer.Start();
            else
                m_timer.Stop();
        }

        int coun = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ahmet.Count; i++)
            { 
                ahmet[i].Trigged(DateTime.Now, coun++.ToString() + ".avi"); 
            } 
        }

        public void SyncFileCreated(string fileName)
        {
            if(m_isCameraRunning)
            {
                string imageType = ImageName.ImageType(fileName);
                string camType = imageType.Substring(0, 2);
                string withoutExtension = Path.GetFileNameWithoutExtension(fileName);
                string videoName = withoutExtension.Replace(imageType, imageType.Substring(0, 4) + "0");

                int channel;

                if ((imageType != "L1-C2") && (imageType != "L1-C0") && (imageType != "L2-C2") && (imageType != "L2-C0") && (imageType != "L3-C2") && (imageType != "L3-C0") &&
                    (imageType != "L4-C2") && (imageType != "L4-C0"))
                {
                    m_cameras.TryGetValue(camType, out channel);
                    ahmet[channel].Trigged(DateTime.Now, m_settings.m_imagePath + "\\" + videoName + ".avi");
                } 
            } 
        }

        private void VideoModal_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_fhs = null;

            StopCameras(ahmet.Values.ToList());
        }


   
     
     
        private void m_startPopup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            VideoProcess();
             
        }


        public void VideoProcess()
        {
            if (m_videoProcessSwitch)
            {
                ahmet = PrepareCameras(m_settings.Cameras);

                bool start = StartCameras(ahmet.Values.ToList());

                if (start)
                    m_isCameraRunning = true;
                else
                    m_isCameraRunning = false;
               

                m_videoProcessSwitch = false;
                m_startPopup.Caption = "Durdur";
            }
            else
            {

                bool stop =  StopCameras(ahmet.Values.ToList());

                if (stop)
                    m_isCameraRunning = true;
                else
                    m_isCameraRunning = true;

                m_startPopup.Caption = "Başlat";
                m_videoProcessSwitch = true;
            }
        }

        public Dictionary<int, Camera> PrepareCameras(SerializableDictionary<int, CameraSettings> lala)
        {
            Dictionary<int, Camera> channels = new Dictionary<int, Camera>();
            CameraCreator createCameraSource = new CameraCreator();


            m_settings = m_settings.DeSerialize(m_settings);

            foreach (var item in lala)
            {
                if (((m_settings.m_firstCamera) && (item.Key == 0))  || ((m_settings.m_secondCamera) && (item.Key == 1) ))
                { 
                    dynamic cameraSource;

                    if (item.Value.m_streamType == 0)
                        cameraSource = new MJPEGCameraBuilder();
                    else
                        cameraSource = new JPEGCameraBuilder();

                    createCameraSource.SetCameraBuilder(cameraSource);
                    createCameraSource.CreateCamera(item.Value.m_cameraURL, item.Value.m_userName, item.Value.m_password, item.Value.m_beforeAndAfterSecond);
                    Camera camera = createCameraSource.GetCamera();

                    channels.Add(item.Key, camera);
                }

            }

            return channels;
        }

        public bool StartCameras(List<Camera> lala)
        {
            bool startCameras = false;

            try
            {
                foreach (var item in lala)
                {
                    item.StartCamera();
                }


                startCameras = true;
                return startCameras;
            }
            catch
            {
                return startCameras; 
            } 
        }

        public bool StopCameras(List<Camera> lala)
        {
            bool stopCameras = false;

            try
            {
                foreach (var item in lala)
                {
                    item.StopCamera();
                }
                stopCameras = true;
                return stopCameras;
            }
            catch
            {
                return stopCameras;
            }
        }

        private void m_buttonEditImagesPath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (m_folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                m_buttonEditImagesPath.Text = m_folderBrowserDialog.SelectedPath;
        }
    }
}

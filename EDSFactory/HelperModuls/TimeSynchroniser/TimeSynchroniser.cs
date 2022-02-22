using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.API.Native;
//using DevExpress.XtraRichEdit.SpellChecker;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;

namespace EDSFactory
{
    public partial class TimeSynchroniser : DevExpress.XtraEditors.XtraForm, ITimeSyncToolWatcher, INotifyPropertyChanged
    {
        private static TimeSynchroniser m_timeSynchroniser;
        public MainForm m_mf;
        public Settings.TimeSync m_timeSync;
        //Stopwatch m_swConn; 
        FileSystemWatcher m_fsw;
        System.Timers.Timer m_timer;
        bool m_startSync = true;
        public enum Sync { Non, NTP, GPS }


        //BufferBlock<string> m_imagesNames ;
        //BufferBlock<string> m_notSyncImagesNames;

        //BlockingCollection<string> m_imagesNames;
        internal ConcurrentQueue<string> m_imagesNamesQueue;
        //GPS m_GPS;
         TimeTool m_timeTool;
         Thread m_moveFileSyncStatusFolder;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }
        private bool syncToolStatus;

       public bool SyncToolStatus
        {
            get { return syncToolStatus; }

           set
            {
               if(value != syncToolStatus)
               {
                   syncToolStatus = value;
                   //OnPropertyChanged("ImageFullPath");



                   if (syncToolStatus)
                   {
                       DisplayManager.RichTextBoxInvoke(m_richTextBoxSyncToolStatus, "Sunucuyla Haberleşildi...", Color.Black);

                       //m_swConn.Restart();

                       DisplayManager.LabelInvoke(m_labelControlComm, "Aktif");
                       DisplayManager.LabelInvoke(m_labelControlSyncTypeInfo, "Zaman Sunucusu");


                       m_timeSync = m_timeSync.DeSerialize(m_timeSync);

                       Thread denemeler = new Thread(new ParameterizedThreadStart(IsProcessWorking));
                       
                       denemeler.IsBackground = true;
                       denemeler.Start(m_timeSync.m_processPath);
                   }
                   else
                   {
                       DisplayManager.LabelInvoke(m_labelControlComm, "Pasif");
                       DisplayManager.LabelInvoke(m_labelControlSyncTypeInfo, "Bulunamadı!!!...");
                   }
                  
               }
            }

        }



        #region singleton
        public static TimeSynchroniser Singleton(MainForm mf)
        {
            if (m_timeSynchroniser == null)
                m_timeSynchroniser = new TimeSynchroniser(mf);

            return m_timeSynchroniser;
        }
        #endregion

        #region modal
        private TimeSynchroniser(MainForm mf)
            : this()
        {
            m_mf = mf;
        }
        #endregion

        public TimeSynchroniser()
        {
            InitializeComponent(); 

            m_timeTool = new TimeTool();

            m_timer = new System.Timers.Timer(5000);
            m_timer.Elapsed += m_timer_Elapsed;

            //m_GPS = GPS.Singleton();

            m_timeSync = Settings.TimeSync.Singleton();
            m_timeSync = m_timeSync.DeSerialize(m_timeSync);

            //m_swConn = new Stopwatch();  

            m_ipAddressControlTimerServerIp.Text = m_timeSync.m_timeServerIP;
            m_spinEditRefreshSystemTime.Value = m_timeSync.m_syncSecond;
            //m_textBoxXSyncPlateImagesPath.Text = m_timeSync.m_syncPath;
            m_checkEditAutoStart.Checked = m_timeSync.m_autoStart;
            m_buttonEditImagesPath.Text = m_timeSync.m_processPath;
            m_buttonEditPlateImagesPath.Text = m_timeSync.m_imagePath;

            //m_checkEditImagesPath.Checked = m_timeSync.m_openProcess;
            m_checkEditImagesPath.Checked = false;
            //m_buttonEditPlateImagesPath.Text = m_timeSync.m_imagePath;
            //m_imagesNames = new BufferBlock<string>();
            //m_notSyncImagesNames = new BufferBlock<string>();
            //m_imagesNames = new BlockingCollection<string>();
            m_imagesNamesQueue = new ConcurrentQueue<string>();

            #region filesystemwatcherevent
            m_fsw = new FileSystemWatcher();
            m_fsw.Created += m_fsw_Created;

            if (Directory.Exists(m_timeSync.m_imagePath))
            {
                m_fsw.Path = m_timeSync.m_imagePath;
                m_fsw.EnableRaisingEvents = true;
                m_fsw.NotifyFilter = NotifyFilters.FileName;
            }
            #endregion

            //bakalım nolcak
            //GC.KeepAlive(m_fsw);

        

            

            if (m_timeSync.m_autoStart)
                m_timer.Start();
            else
                m_timer.Stop();


            //ThreadPool.QueueUserWorkItem(InsertFileNameToTheDatabase);
            //ThreadPool.QueueUserWorkItem(DeleteNotSyncImages);


            List<string> imageNameList = FileOperation.Find(m_timeSync.m_imagePath);

            FileOperation.Move(m_timeSync.m_imagePath, imageNameList, m_timeSync.m_imagePath + "\\notsync", imageNameList);



            m_moveFileSyncStatusFolder = new Thread(new ParameterizedThreadStart(MoveFileSyncStatusFolder));
            m_moveFileSyncStatusFolder.IsBackground = true;
            m_moveFileSyncStatusFolder.Start();
        }


        private void TimeSynchroniser_FormClosing(object sender, FormClosingEventArgs e)
        { 
            m_timeSynchroniser = null;

            if (m_timer.Enabled)
                m_timer.Stop();

            m_timeTool.Stop();

            //if (m_saveSyncFileName.IsAlive)
            //    m_saveSyncFileName.Interrupt();

            RequestStop();

            RequestStopMoveFiles();

            RequestDeleteNotSyncImages();

           
        }

        void m_timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (m_startSync)
                StartSync();
           
        }

        int co = 0;
      
        void m_fsw_Created(object sender, FileSystemEventArgs e)
        {
            //co++;

            try
            {
                //m_imagesNames.Add(e.FullPath);
                m_imagesNamesQueue.Enqueue(e.FullPath);

                //if (SyncToolStatus)
                //{
                //    //MessageBox.Show("fsw1");

                //    //ThreadPool.QueueUserWorkItem(InsertFileNameToTheDatabase, e.Name);

                //     ThreadPool.UnsafeQueueUserWorkItem(InsertFileNameToTheDatabase, e.Name);
              
                //    //MessageBox.Show("fsw2");

                //    //m_imagesNamesQueue.Enqueue(e.Name);
                 
                //    //m_imagesNames.Post(e.Name);

                //    //m_imagesNames.Add(e.Name);
                 
                //}
                //else
                //{
                //    //MessageBox.Show("fsw3");
                //    //m_notSyncImagesNames.Post(e.FullPath);
                //    //ThreadPool.QueueUserWorkItem(DeleteNotSyncImages, e.FullPath);
                //    ThreadPool.UnsafeQueueUserWorkItem(DeleteNotSyncImages, e.FullPath);

                //}
            }
            catch (Exception ex)
            {
                Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "TimeSynchroniser");
            }
            finally
            {
                //Logging.WriteLog(DateTime.Now.ToString(), e.Name, e.Name, e.Name, e.Name);

                //co++;
            }
        }

        //public void DeleteNotSyncImages(object o)
        //{ 
        //    try
        //    { 
        //        //while (!_shouldStop3)
        //        {
        //            //string isim = m_notSyncImagesNames.Receive();
        //            string isim = (string) o;

        //            if(!string.IsNullOrEmpty(isim))
        //            { 
        //                bool isDeleted = false;
        //                //int count = 0;

        //                do
        //                {
        //                    isDeleted = FileOperation.DeleteFileReturnValue(Path.GetDirectoryName(isim), Path.GetFileName(isim));

        //                    //count++;

        //                }
        //                while ((!isDeleted));// && (count <= 3));  
        //            }

        //        } 
        //    }
        //    catch (ThreadInterruptedException ex)
        //    {
        //        Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "InsertFileNameToTheDatabase3");

        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "InsertFileNameToTheDatabase4");
        //    }
        //}

        internal readonly object m_lockProcess = new object();
   
        //public void InsertFileNameToTheDatabase(object o)
        //{ 
        //    try
        //    {
        //        //MessageBox.Show("InsertFileNameToTheDatabase");
        //        //while (!_shouldStop2)
        //        //lock (m_lockProcess)
        //        {
        //               string isim = (string) o;
        //               int value;
        //               //bool value;
        //               if (!string.IsNullOrEmpty(isim))
        //               {
        //                   do
        //                   {
        //                       Task<int> task = DatabaseOperation.NTP.Singleton().AsyncInsert(new List<string>(new string[] { isim }));
        //                       task.Wait();
        //                       value = task.Result; 

        //                   } 
        //                   while (value <= 0);


        //                     MainForm.m_ahmet.SyncFileCreated(isim);  

        //                   //string isim = m_imagesNames.Receive();

        //                     //MessageBox.Show("InsertFileNameToTheDatabase2");
        //                   //foreach (string item in m_imagesNames.GetConsumingEnumerable())
        //                   //{


        //                   //    //MainForm.m_ahmet.SyncFileCreated(item); 
        //                   //}
        //               }
        //        }
            
                 
        //    } 
        //    catch (ThreadInterruptedException ex)
        //    {
        //        Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "InsertFileNameToTheDatabase1");

        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "InsertFileNameToTheDatabase2");
        //    } 
        //}

        private readonly static object m_lockMoveFileSyncStatusFolder = new object();
        public void MoveFileSyncStatusFolder(object o)
        {
            while (!_shouldStop2)
            {
                lock(m_lockMoveFileSyncStatusFolder)
                {
                    string filePath = "";

                    while (m_imagesNamesQueue.TryDequeue(out filePath))
                    {
                        try
                        {
                            if (SyncToolStatus)
                            {
                                string syncPath = m_timeSync.m_imagePath + "\\" + "sync";

                                if (!Directory.Exists(syncPath))
                                {
                                    Directory.CreateDirectory(syncPath);
                                }

                                string destinationFilePath = syncPath + "\\" + Path.GetFileName(filePath);


                                bool isInUse = false;

                                do
                                {
                                    isInUse = FileOperation.IsFileLocked(filePath);

                                    if (isInUse)
                                        Thread.Sleep(400);

                                } while (isInUse);


                                if (!File.Exists(destinationFilePath))
                                {
                                    File.Move(filePath, destinationFilePath);

                                    MainForm.m_ahmet.SyncFileCreated(Path.GetFileName(destinationFilePath));
                                }
                                else
                                    Logging.WriteLog(DateTime.Now.ToString(), "dosya taşınmak istenen kaynakta var!", filePath, destinationFilePath, "TimeSynchronisersync");

                            }
                            else
                            {
                                string notSyncPath = m_timeSync.m_imagePath + "\\" + "notsync";

                                if (!Directory.Exists(notSyncPath))
                                {
                                    Directory.CreateDirectory(notSyncPath);
                                }

                                string destinationFilePath = notSyncPath + "\\" + Path.GetFileName(filePath);


                                bool isInUse = false;

                                do
                                {
                                    isInUse = FileOperation.IsFileLocked(filePath);

                                    if (isInUse)
                                        Thread.Sleep(400);

                                } while (isInUse);

                                if (!File.Exists(destinationFilePath))
                                    File.Move(filePath, destinationFilePath);
                                else
                                    Logging.WriteLog(DateTime.Now.ToString(), "dosya taşınmak istenen kaynakta var!", filePath, destinationFilePath, "TimeSynchronisernot");
                            }

                        }
                        catch (Exception ex)
                        {
                            Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "TimeSynchroniserfile");
                        }
                        //finally
                        //{
                        //    //Logging.WriteLog(DateTime.Now.ToString(), e.Name, e.Name, e.Name, e.Name);

                        //    //co++;
                        //}
                    }
                }
            


                //foreach (string item in m_imagesNames.GetConsumingEnumerable())
                //{

                //}
            } 
        }



        //bu ölesine yazıldı
        public void MoveNotSyncFile(string filePath)
        {
            string notSyncPath = m_timeSync.m_imagePath + "\\" + "notsync";

            if (!Directory.Exists(notSyncPath))
            {
                Directory.CreateDirectory(notSyncPath);
            }

            string destinationFilePath = notSyncPath + "\\" + Path.GetFileName(filePath);


            bool isInUse = false;

            do
            {
                isInUse = FileOperation.IsFileLocked(filePath);

                if (isInUse)
                    Thread.Sleep(400);

            } while (isInUse);

            if (!File.Exists(destinationFilePath))
                File.Move(filePath, destinationFilePath);
        }












         
        internal object m_lockClient = new object();
        public void StartSync()
        {
            if (m_startSync)
            {
                m_timeSync = m_timeSync.DeSerialize(m_timeSync); 

                if (string.IsNullOrEmpty(m_timeSync.m_processPath) || string.IsNullOrEmpty(m_timeSync.m_timeServerIP) || string.IsNullOrEmpty(m_buttonEditImagesPath.Text))
                {
                    DisplayManager.RichTextBoxInvoke(m_richTextBoxSyncToolStatus, "Ayarları Kontrol Ediniz!!!", Color.DarkRed); 
                    return;
                } 
  

                m_timeTool.Start();
              
                DisplayManager.RichTextBoxInvoke(m_richTextBoxSyncToolStatus, "Sunucuyla Haberleşiliyor...", Color.Black);
                m_startPopup.Caption = "Durdur";

                m_startSync = false;
            }
            else
            {
                m_timeTool.Stop();
                m_startSync = true;
                m_startPopup.Caption = "Başlat";
            }


        }

        private volatile bool _shouldStop;
        private volatile bool _shouldStop2;
        private volatile bool _shouldStop3;
        public void RequestStop()
        {
            _shouldStop = true; 
        }

        public void RequestStopMoveFiles()
        {
            _shouldStop2 = true; 
        }

        public void RequestDeleteNotSyncImages()
        {
            _shouldStop3 = true;
        }

        public void IsProcessWorking(object processPath)
        {
            while (!_shouldStop)
            {
                if (SyncToolStatus && m_timeSync.m_openProcess)
                {
                    bool isRunning;

                    FileVersionInfo fe = FileVersionInfo.GetVersionInfo(processPath.ToString());

                    if (fe.CompanyName == "Gritron")
                    {
                        isRunning = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(processPath.ToString()))
                         .FirstOrDefault(p => p.MainModule.FileName.StartsWith(Path.GetDirectoryName(processPath.ToString()))) != default(Process) ||
                          Process.GetProcessesByName("starter")
                         .FirstOrDefault(p => p.MainModule.FileName.StartsWith(Path.GetDirectoryName(processPath.ToString()) + "\\starter.exe")) != default(Process);

                        if (!isRunning)
                        {
                            Process.Start(Path.GetDirectoryName(processPath.ToString()) + "\\starter");
                        }
                    }

                    else
                    {
                        isRunning = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(processPath.ToString()))
                       .FirstOrDefault(p => p.MainModule.FileName.StartsWith(Path.GetDirectoryName(processPath.ToString()))) != default(Process);

                        if (!isRunning)
                        {
                            Process.Start(processPath.ToString());
                        }
                    } 
                }

                Thread.Sleep(10000);
            }
        }

        private void m_simpleButtonSave_Click(object sender, EventArgs e)
        {
            SimpleButton myButtonX = (SimpleButton)sender;

            if (m_timer.Enabled)
            {
                m_timer.Stop();

                m_timeTool.Stop(); 

                m_startSync = true;
                m_startPopup.Caption = "Başlat";
            }

            m_timeSync.m_autoStart = m_checkEditAutoStart.Checked;
            m_timeSync.m_timeServerIP = m_ipAddressControlTimerServerIp.Text;
            m_timeSync.m_syncSecond = Convert.ToInt32(m_spinEditRefreshSystemTime.Value);

            m_timeSync.m_openProcess = m_checkEditImagesPath.Checked;
            m_timeSync.m_imagePath = m_buttonEditPlateImagesPath.Text;
            //m_timeSync.m_deleteUnsynFile = m_checkBoxXDeleteUnsyncFile.Checked;

            m_timeSync.m_processPath = m_buttonEditImagesPath.Text;
       
            //if (File.Exists(Path.GetDirectoryName(m_buttonEditImagesPath.Text) + "\\lpr.ini"))
            //{
            //    IniFile ini = new IniFile(Path.GetDirectoryName(m_buttonEditImagesPath.Text) + "\\lpr.ini");

            //    string path = ini.IniReadValue("SETTINGS", "Path");
            //    string[] splitPath = path.Split('\\');

            //    path = "";

            //    for (int i = 0; i < splitPath.Length; i += 2)
            //        path += splitPath[i] + "\\";

            //    path = path.Substring(0, path.LastIndexOf("\\"));

            //    m_timeSync.m_imagePath = path;
            //}
            //else if (File.Exists(Path.GetDirectoryName(m_buttonEditImagesPath.Text) + "\\settings.ini"))
            //{
            //    IniFile ini = new IniFile(Path.GetDirectoryName(m_buttonEditImagesPath.Text) + "\\settings.ini");
            //    m_timeSync.m_imagePath = Path.GetDirectoryName(ini.IniReadValue("SaveFile", "format"));
            //}

            m_timeSync.Serilize(m_timeSync);
            m_timeSync = m_timeSync.DeSerialize(m_timeSync);

            if (Directory.Exists(m_timeSync.m_imagePath))
            {
                m_fsw.Path = m_timeSync.m_imagePath;
                m_fsw.EnableRaisingEvents = true;
            }

            if (m_timeSync.m_autoStart)
                m_timer.Start();
            else
                m_timer.Stop();

        } 

        private void m_buttonEditImagesPath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (m_openFileDialogProcessName.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                m_buttonEditImagesPath.Text = m_openFileDialogProcessName.FileName;
        }

        private void m_startPopup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            StartSync();  
        }


        public void SyncToolDate(DateTime SyncToolDate)
        {
         
            DisplayManager.LabelInvoke1(m_labelControlLastSyncDateTime,   string.Format("{0:d/MM/yyyy HH:mm:ss.fff tt}", SyncToolDate) );   
            //DisplayManager.LabelInvoke1(m_labelControlLastSyncDateTime, SyncToolDate.ToShortDateString() + " " + SyncToolDate.ToLongTimeString());   
        }

        public void SetSystemTime() 
        { 
        
        }

        public void SystemTimeUpdated(DateTime systemUpdatedTime) 
        {
            DisplayManager.LabelInvoke1(m_labelControlSystemDateInf, string.Format("{0:d/MM/yyyy HH:mm:ss.fff tt}", systemUpdatedTime)); 

            //DisplayManager.LabelInvoke1(m_labelControlSystemDateInf, systemUpdatedTime.ToShortDateString() + " " + systemUpdatedTime.ToLongTimeString()); 
        } 
     
        public void SyncStatus(bool status)
        {
            SyncToolStatus = status;
            
            //if(!SyncToolStatus)
            //    m_startSync = false;
        }

        private void m_richTextBoxSyncToolStatus_TextChanged(object sender, EventArgs e)
        {
            if (m_richTextBoxSyncToolStatus.Text.Length > 5000)
                m_richTextBoxSyncToolStatus.ResetText();

             m_richTextBoxSyncToolStatus.ScrollToCaret();
        }

        private void m_simpleButtonDeleteNTPRecord_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(UserMessages.DeleteQuestionMessage, UserMessages.MessageCaptionMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                Task<int> returnValueOfDelete = DatabaseOperation.NTP.Singleton().AsyncDelete();
                returnValueOfDelete.Wait();

                if (returnValueOfDelete.Result > 0)
                    MessageBox.Show(UserMessages.DeleteMessage, UserMessages.MessageCaptionMessage, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(UserMessages.DeleteErrorMessage, UserMessages.MessageCaptionMessage, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void TimeSynchroniser_Load(object sender, EventArgs e)
        {

        }

        private void m_buttonEditImagesPath_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void m_buttonEditPlateImagesPath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit myButton = (ButtonEdit)sender;

            if (myButton == m_buttonEditPlateImagesPath)
            {
                if (m_folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    m_buttonEditPlateImagesPath.Text = m_folderBrowserDialog.SelectedPath;
            }
        }

        private void m_checkEditImagesPath_CheckedChanged(object sender, EventArgs e)
        {
            
                m_buttonEditImagesPath.Enabled = m_checkEditImagesPath.Checked;
        }
    }
}

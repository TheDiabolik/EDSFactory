using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDSFactory 
{
    class NTPTimeTool : ITimeSyncStatus
    {
        //Stopwatch m_stopWatch;
        int m_syncTime;
        NTPClient m_client;
        //System.Timers.Timer m_timer;
        private volatile bool isInProcess;
        //Thread m_threadTimeSync; 
        System.Threading.Timer m_threadTimeSync; 
        private DateTime m_NTPTime;
        private bool m_isConn; 
        TimeSyncTool m_timeSyncTool;
 
      
        public string m_IP { get; set; }
         

        public NTPTimeTool( )
        {
            m_timeSyncTool = new TimeSyncTool();

            Settings.TimeSync m_settings = Settings.TimeSync.Singleton();
            m_settings = m_settings.DeSerialize(m_settings);

            //m_stopWatch = new Stopwatch();
            m_client = new NTPClient(m_settings.m_timeServerIP);
            m_syncTime = m_settings.m_syncSecond;
            m_IP = m_settings.m_timeServerIP;

            //m_timer = new System.Timers.Timer(4000);
            //m_timer.Elapsed += m_timer_Elapsed;

            //m_timer.Start(); 
        }

        public void Stop()
        {
            isInProcess = false;
            m_isConn = false;
        }

        public void Start()
        {

            m_timeSyncTool.AddWatcher(TimeSynchroniser.Singleton(MainForm.m_mf));
            isInProcess = true;

            m_client.TimeServer = m_IP;

            //m_threadTimeSync = new Thread(new ParameterizedThreadStart(SystemTimeSync));
            ////m_threadTimeSync.Priority = ThreadPriority.AboveNormal;
            //m_threadTimeSync.IsBackground = true;
            //m_threadTimeSync.Start();


            m_threadTimeSync = new System.Threading.Timer(SystemTimeSync, null, 0, m_syncTime * 1000);
             
        }

        private readonly static object m_lockProcess = new object();

        public void SystemTimeSync(object o)
        {
            Monitor.Enter(m_lockProcess);

            try
            {
                //while (isInProcess)
                //{   //m_stopWatch.Restart();

                m_isConn = m_client.Connect(false);

                SyncStatus(m_isConn);

                m_NTPTime = m_client.NTPTime();

                if (m_isConn)
                {
                    SyncToolDate(m_NTPTime);
                }
                else
                {
                    SyncStatus(false);

                    //SyncStopped.Invoke();
                }

                //m_stopWatch.Stop();

                //Thread.Sleep(m_syncTime * 1000); 

                //}
            }
            catch (ThreadAbortException ex)
            {
                //isInProcess = false;
                SyncStatus(false);
                Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "ThreadAbortException ntp");
            }
            catch (ThreadInterruptedException ex)
            {
                //isInProcess = false;
                SyncStatus(false);
                Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "ThreadInterruptedException ntp");
            }
            catch (SocketException ex)
            {
                //isInProcess = false;
                SyncStatus(false);
                Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "SocketException ntp");
            }
            catch (Exception ex)
            {
                //isInProcess = false;
                SyncStatus(false);
                Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "Exception ntp");
            }
            finally
            {
                Monitor.Exit(m_lockProcess);
            }

        }

        public void InterruptNTPSync()
        {
            if (m_threadTimeSync != null)
                m_threadTimeSync.Dispose();

            this.m_threadTimeSync =  new System.Threading.Timer(SystemTimeSync, null, 0, m_syncTime * 1000);

            //if (m_threadTimeSync != null && m_threadTimeSync.IsAlive)
            //    m_threadTimeSync.Interrupt();
        }


        void m_timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
        //    if (m_isConn && m_stopWatch.Elapsed.TotalSeconds > 10)
        //    {
        //        Logging.WriteLog(DateTime.Now.ToString(), "m_timer_Elapsed : Interrupt", "m_timer_Elapsed : Interrupt", "m_timer_Elapsed : Interrupt", "Interrupt");

        //        m_stopWatch.Restart();

        //        InterruptNTPSync();
                 
        //        Start();

        //        Logging.WriteLog(DateTime.Now.ToString(), "m_timer_Elapsed : Interrupt1", "m_timer_Elapsed : Interrupt1", "m_timer_Elapsed : Interrupt1", "Interrupt1");
        //    }
        }

        public void SyncToolDate(DateTime dt)
        {
            m_timeSyncTool. SyncToolDate(dt);
            SetSystemTime();
            
        }


        public void SetSystemTime( )//senkron saat sorununun nedeni
        {
            //if(DateTime.Now != m_NTPTime)
            {
                bool isSync = NTPClient.SetTime(m_NTPTime);

                if (isSync)
                    SystemTimeUpdated(DateTime.Now);
                else
                    SyncStatus(false);
            }

        }

        public void SystemTimeUpdated(DateTime dt)
        { 
            m_timeSyncTool.SystemTimeUpdated(dt); 
        }

        public void SyncStatus(bool status)
        {
            m_timeSyncTool.SyncStatus(status); 
        }
    }
}

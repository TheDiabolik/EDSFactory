
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDSFactory
{
    partial class SocketCommunication
    {
        #region variables
        public dynamic m_settings; 
        private readonly object m_lockDelete;
        public dynamic m_speedCorridor;
        internal ConcurrentQueue<string> m_toBeDeleted;
        internal MyList<string> m_toBeDeletedSyncFile; 
        internal string[] m_wantedImageType;
    
        internal ITriggeredViolation m_violation;

        #endregion

        #region constructor
        SocketCommunication()
        {
            m_lockDelete = new object();
            m_toBeDeleted = new ConcurrentQueue<string>();
            m_toBeDeletedSyncFile = new MyList<string>();
        }
         
        #endregion

        #region singletonpattern
        private static SocketCommunication m_do;

        public static SocketCommunication Singleton()
        {
            if (m_do == null)
                m_do = new SocketCommunication();

            return m_do;
        }
        #endregion


        public void Start()
        {
            m_settings = m_settings.DeSerialize(m_settings); 

            if (m_settings.m_workingType)
            {
                MainForm.m_ahmet.AddWatcher(SocketCommunication.Server.Singleton());
                SocketCommunication.Server.Singleton().StartServer();
            }

            else
            {
                MainForm.m_ahmet.AddWatcher(SocketCommunication.Client.Singleton());
                SocketCommunication.Client.Singleton().StartClient(m_settings.m_entryTagIP, "");
            } 
        }

        public void Stop()
        {
            if(m_settings != null)
            {
                m_settings = m_settings.DeSerialize(m_settings);

                if (m_settings.m_workingType)
                {
                    MainForm.m_ahmet.AddWatcher(SocketCommunication.Server.Singleton());
                    SocketCommunication.Server.Singleton().StopServer();
                }

                else
                {
                    MainForm.m_ahmet.AddWatcher(SocketCommunication.Client.Singleton());
                    SocketCommunication.Client.Singleton().StopClient(false);
                }


            }
            
        }


        private System.Threading.Timer STTimer;

        public void sda()
        {
            if(!kontrol)
            {
                kontrol = true;

                if(STTimer != null)
                    STTimer.Dispose();

                //this.STTimer = new System.Threading.Timer(DeleteInValidImages, null, 1000, System.Threading.Timeout.Infinite);
                this.STTimer = new System.Threading.Timer(DeleteInValidImages, null, 1000, 60000);
            }
           
        }


        bool kontrol = false;

        private readonly object m_Lock = new object();
        #region methods 
        public void DeleteInValidImages(object o)
        {
            try
            {
                string path;
                m_settings = m_settings.DeSerialize(m_settings);

                if (m_settings.m_workingType)
                    path = m_settings.m_entryTagPath + "\\" + "sync";  
                else
                    path = m_settings.m_imagePath + "\\" + "sync"; 

                //while (kontrol)
                {
                    //Thread.Sleep(60000);

                    string zre = ""; 

                    if (m_toBeDeleted.Count > 0)
                        DisplayManager.RichTextBoxInvoke(m_speedCorridor.richTextBox1, m_toBeDeleted.Count.ToString() + " Adet Silinecek Resim Bulundu...", Color.Red);


                    while (m_toBeDeleted.TryDequeue(out zre))
                    {
                        //Task taskDelete = FileOperation.FileDeleteAsync(path, zre);
                        //taskDelete.Wait();

                        bool value = FileOperation.DeleteFileReturnValue(path, zre);

                        if (!value)
                            m_toBeDeleted.Enqueue(zre);
                        //else
                        //{
                        //    Task<int> taskDeleteNTP = DatabaseOperation.NTP.Singleton().AsyncDelete(zre);
                        //} 
                    } 
                }
            }
            catch (ThreadInterruptedException ex)
            {
                kontrol = false;
                sda();
                Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "socket1"); 
               
            }
            catch (Exception ex)
            {
                kontrol = false;
                sda();
                Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "socket2"); 
            }
        }

        private readonly static object ef = new object();
        private static int StringToByteMsg(string str, byte[] buf)
        {
            //lock (ef)
            //{
                int textLen;

                textLen = Encoding.UTF8.GetBytes(str, 0, str.Length, buf, 4);
                byte[] bufLen = BitConverter.GetBytes(textLen);
                Array.Copy(bufLen, buf, 4);

                return textLen + 4;
            //}
        } 
       
        #endregion
          
        #region classinfo
        class ClientInfo
        {
            private Socket m_sock;
            private byte[] m_bufRecv, m_bufSend;
            private int m_indexRecv, m_indexSend;
            private int m_leftRecv, m_leftSend;
            private bool m_lengthFlagRecv;


            public ClientInfo()
            {
                m_bufRecv = new byte[1048576];
            }

            public Socket Sock
            {
                get { return m_sock; }
                set { m_sock = value; }
            }

            public byte[] BufRecv
            {
                get { return m_bufRecv; }
                set { m_bufRecv = value; }
            }

            public byte[] BufSend
            {
                get { return m_bufSend; }
                set { m_bufSend = value; }
            }
            public int IndexRecv
            {
                get { return m_indexRecv; }
                set { m_indexRecv = value; }
            }
            public int LeftRecv
            {
                get { return m_leftRecv; }
                set { m_leftRecv = value; }
            }
            public int LeftSend
            {
                get { return m_leftSend; }
                set { m_leftSend = value; }
            }

            public bool LengthFlagRecv
            {
                get { return m_lengthFlagRecv; }
                set { m_lengthFlagRecv = value; }
            }

            public int IndexSend
            {
                get { return m_indexSend; }
                set { m_indexSend = value; }
            }
        }
        #endregion
    }
}
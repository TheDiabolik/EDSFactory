
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
        public class Server : ISyncFileWatcher
        {
            #region variables

            private static Server m_do = null;
            internal Socket m_serverSock; 
               string m_protocolMsg, m_entryImageName = "", m_incomingMsg;
            double m_distance, m_speed;
            bool m_speedTolerance;
            int m_tolerancePercentage ;
            private readonly object m_lockServer;
            private readonly object m_lock;
             public enum Message { FindEntry, Waiting, NoImages, LastDate }
              Stopwatch m_stopWatch; 
            DateTime m_lastDate; 
             MyList<string> m_images;
             private MyList<ClientInfo> m_clients;
             private static readonly object m_padlock = new object();
            #endregion

            #region constructor
             Server()
             {
                 m_lockServer = new object();
                 m_lock = new object();
                 m_stopWatch = new Stopwatch();
                 m_images = new MyList<string>();
                 m_clients = new MyList<ClientInfo>();
             }

            #endregion

            #region singleton
            public static Server Singleton()
             {
                 lock (m_padlock)
                 {
                     if (m_do == null)
                         m_do = new Server();

                     return m_do;
                 }
            }
            #endregion 

            private readonly object sendSyncRoot = new object();
            private readonly object receiveSyncRoot = new object();
            #region filecreatedevent
            public void SyncFileCreated(string fileName)
            { 
                m_images.Add(fileName);  
             }
            #endregion 

          
            public void StartServer()
            {
                try
                { 
                    m_serverSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    m_serverSock.Bind(new IPEndPoint(IPAddress.Any, Convert.ToInt32(SocketCommunication.Singleton().m_settings.m_entryTagListenPort)));
                    m_serverSock.Listen(30);
                    m_serverSock.NoDelay = true;
                    m_serverSock.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
 
                    SocketCommunication.Singleton().sda();

                    DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Giriş Tagı Çıkış Tagının Bağlanması İçin Hazır...", Color.Black);

                    m_serverSock.BeginAccept(new AsyncCallback(ServerAcceptProc), null); 
 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), EDSType.CorridorSpeed); 
                }
            }
          
            public void StopServer()
            {
                if (m_serverSock != null || (m_serverSock != null && m_serverSock.Connected))
                {
                    foreach (ClientInfo ci in m_clients)
                    {
                        if (ci != null && ci.Sock.Connected)
                            SendMsgToClient(ci, "LISTENINGSTOP$"); 
                    }

                    m_serverSock.Close();
                    m_images.Clear();
    
                }
            }

            private void ServerAcceptProc(IAsyncResult iar)
            {
                try
                {
                    m_serverSock.BeginAccept(new AsyncCallback(ServerAcceptProc), null);

                    Socket clientSock;
                    clientSock = m_serverSock.EndAccept(iar);

                    ClientInfo ci = new ClientInfo();
                    ci.Sock = clientSock;
                    ci.Sock.NoDelay = true;
                    ci.Sock.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);

                    m_clients.Add(ci);

                    m_images = new MyList<string>();

                    //Task<List<string>> taskSelectNTP = DatabaseOperation.NTP.Singleton().AsycSelect();
                    //taskSelectNTP.Wait();

                    List<string> imageNameList = FileOperation.Find(SocketCommunication.Singleton().m_settings.m_entryTagPath + "\\" + "sync");

                    foreach (string item in imageNameList)
                    {
                        if (!m_images.Contains(item))
                            m_images.Add(item);
                    }

                    ci.LeftRecv = 4;
                    ci.IndexRecv = 0;
                    ci.LengthFlagRecv = true;

                    clientSock.BeginReceive(ci.BufRecv, ci.IndexRecv, ci.LeftRecv, SocketFlags.None, new AsyncCallback(ServerReceiveProc), ci);
                }
                catch (ObjectDisposedException)
                {
                    DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Giriş Tagı İsteklere Kapatıldı!!!", Color.DarkRed);

                }
            }

       
            private void ServerReceiveProc(IAsyncResult iar)
            {
                ClientInfo ci = (ClientInfo)iar.AsyncState;
                int n = 0;

                try
                {
                    n = ci.Sock.EndReceive(iar);

                    ci.IndexRecv += n;
                    ci.LeftRecv -= n;

                    if (ci.LeftRecv == 0)
                    {
                        if (ci.LengthFlagRecv)
                        {
                            ci.LeftRecv = BitConverter.ToInt32(ci.BufRecv, 0);
                            ci.IndexRecv = 0;
                            ci.LengthFlagRecv = false;
                        }
                        else
                        {
                            string msg = Encoding.UTF8.GetString(ci.BufRecv, 0, ci.IndexRecv);
                            m_protocolMsg = msg.Substring(0, msg.IndexOf("$"));
                            m_incomingMsg = msg.Substring(msg.IndexOf("$") + 1);

                            if (!m_stopWatch.IsRunning)
                            {
                                m_stopWatch.Reset();
                                m_stopWatch.Start();
                            }

                            #region connect
                            if (m_protocolMsg == "CONNECT")
                            {
                                m_speed = int.Parse(m_incomingMsg.Split('-')[0]);
                                m_distance = int.Parse(m_incomingMsg.Split('-')[1]);
                                m_speedTolerance = Convert.ToBoolean(m_incomingMsg.Split('-')[2]);
                                m_tolerancePercentage = int.Parse(m_incomingMsg.Split('-')[3]);

                                SendMsgToClient(ci, "CONNECTED$");
                                DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Giriş Tagı Bağlantı İsteğini Kabul Etti...", Color.Black);
                            }
                            #endregion connect
                            #region findentry
                            else if (m_protocolMsg == "FINDENTRY")
                            {
                                string exitPlate = ImageName.Plate(m_incomingMsg);
                                DateTime exitDate = ViolationsDate.StringDateToDateTime(ImageName.Day(m_incomingMsg), ImageName.Hour(m_incomingMsg));

                                string exitImageType = ImageName.ImageType(m_incomingMsg);

                                if (m_stopWatch.Elapsed.TotalMinutes > 1)
                                {
                                    m_stopWatch.Stop();
                                    FindOldestEntryDate(exitDate);
                                }

                                m_entryImageName = FindEntryImageName(exitPlate, exitDate, exitImageType);

                                if (string.IsNullOrEmpty(m_entryImageName) || !VerificateImageName.Name(m_entryImageName, false))
                                {
                                    m_entryImageName = "NOTFOUND$" + m_incomingMsg;
                                    DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Giriş Tagında İlgili Aracın Giriş Resmi Bulunamadı :" + m_incomingMsg, Color.DarkRed);
                                }
                                else
                                {
                                    m_entryImageName = "FOUND$" + m_entryImageName + "!" + m_incomingMsg;
                                }

                                SendMsgToClient(ci, m_entryImageName);
                            }
                            #endregion

                            #region sendimage
                            else if (m_protocolMsg == "SENDIMAGE")
                            {
                                string[] entryAndExitArray = m_incomingMsg.Split('!');

                                string entry = entryAndExitArray[0];
                                string exit = entryAndExitArray[1];


                                if (File.Exists(SocketCommunication.Singleton().m_settings.m_entryTagPath + "\\" + "sync" + "\\" + entry))
                                {
                                    byte[] imageInByte = File.ReadAllBytes(SocketCommunication.Singleton().m_settings.m_entryTagPath + "\\" + "sync" + "\\" + entry);

                                    byte[] byteDot = Encoding.UTF8.GetBytes("GETIMAGE$" + entry + "!" + exit + "+");
                                    byte[] buf = new byte[imageInByte.Length + byteDot.Length];

                                    Array.Copy(byteDot, buf, byteDot.Length);
                                    Array.Copy(imageInByte, 0, buf, byteDot.Length, imageInByte.Length);
                                    byte[] temp = new byte[buf.Length];
                                    Array.Copy(buf, temp, buf.Length);
                                    byte[] bufLen = BitConverter.GetBytes(buf.Length);
                                    Array.Resize(ref buf, (bufLen.Length + buf.Length));
                                    Array.Copy(bufLen, buf, bufLen.Length);
                                    Array.Copy(temp, 0, buf, bufLen.Length, temp.Length);

                                    SendMsgToClient(ci, buf);

                                    DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Çıkış Tagına İlgili Aracın Resmi Gönderildi :" + m_incomingMsg, Color.DarkGray);
                                }
                                else
                                {
                                    m_entryImageName = "NOTFOUND$" + exit;
                                    SendMsgToClient(ci, m_entryImageName);
                                    DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Giriş Tagında İlgili Aracın Giriş Resmi Bulunamadı :" + exit, Color.DarkRed);
                                }
                            }
                            #endregion
                            #region receivedimage
                            else if (m_protocolMsg == "RECEIVEDIMAGE")
                            {
                                DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Resim Kaydedildi Bilgisi Geldi...", Color.Black);
                                SendMsgToClient(ci, "SENDNEXT$");
                            }
                            #endregion
                            #region disconnect
                            else if (m_protocolMsg == "DISCONNECT")
                            { 
                                if (ci != null && ci.Sock.Connected)
                                {
                                    SendMsgToClient(ci, "DISCONNECTED$");

                                    DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Çıkış Tagı Bağlantıyı Bağlantıyı Kesti!!!", Color.DarkRed);
                                    ci.Sock.Shutdown(SocketShutdown.Both);
                                    ci.Sock.Close(); 
                                }
                                //ci.Sock.Dispose(); 
                            }
                            #endregion
                            #region sendingstop
                            else if (m_protocolMsg == "SENDINGSTOP")
                            {
                                m_serverSock.Close();
                                ci.Sock.Close();
                                DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Giriş Tagı Haberleşmeyi Durdurdu...", Color.DarkRed);
                            }
                            #endregion

                            #region noimageinexit
                            else if (m_protocolMsg == "NOIMAGEINEXITFOLDER")
                            {
                                m_lastDate = DateTime.Parse(m_incomingMsg);
                            }
                            #endregion

                            ci.LengthFlagRecv = true;
                            ci.IndexRecv = 0;
                            ci.LeftRecv = 4;
                        }
                    }

                    ci.Sock.BeginReceive(ci.BufRecv, ci.IndexRecv, ci.LeftRecv, SocketFlags.None, new AsyncCallback(ServerReceiveProc), ci);
                }
                catch (Exception e)
                {
                    DisposeClient(ci);
                    Logging.WriteLog(DateTime.Now.ToString(), e.Message.ToString(), e.StackTrace.ToString(), e.TargetSite.ToString(), EDSType.CorridorSpeed);
                }
            }

         

            private void ServerSendProc(IAsyncResult iar)
            {
                ClientInfo ci = (ClientInfo)iar.AsyncState;
                try
                {
                    int n = ci.Sock.EndSend(iar);

                    ci.IndexSend += n;
                    ci.LeftSend -= n;

                    //if (ci.LeftSend != 0)
                    //    ci.Sock.BeginSend(ci.BufSend, ci.IndexSend, ci.LeftSend, SocketFlags.None, new AsyncCallback(ServerSendProc), ci);
                    //}
                }
                catch (Exception e)
                {
                    DisposeClient(ci);
                    Logging.WriteLog(DateTime.Now.ToString(), e.Message.ToString(), e.StackTrace.ToString(), e.TargetSite.ToString(), EDSType.CorridorSpeed);
                    DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Exception oluştu: serversendproc " + e.Message, Color.DarkRed);
                }
            }

            #region methods
            private void DisposeClient(ClientInfo ci)
            {
                ci.Sock.Dispose(); 

                m_clients.Remove(ci); 
            }

            private void SendMsgToClient(ClientInfo ci, string msg)
            {
                try
                {
                    byte[] buf = new byte[5120];
                    int len;

                    len = StringToByteMsg(msg, buf);

                    ci.BufSend = buf;
                    ci.IndexSend = 0;
                    ci.LeftSend = len;

                    ci.Sock.BeginSend(ci.BufSend, ci.IndexSend, ci.LeftSend, SocketFlags.None, new AsyncCallback(ServerSendProc), ci);
                }
                catch (Exception e)
                {
                    DisposeClient(ci);

                    Logging.WriteLog(DateTime.Now.ToString(), e.Message.ToString(), e.StackTrace.ToString(), e.TargetSite.ToString(), EDSType.CorridorSpeed);
                    DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Exception oluştu: SendMsgToClient ", Color.DarkRed);
                }
            }

            private void SendMsgToClient(ClientInfo ci, byte[] buf)
            {
                try
                {
                    ci.BufSend = buf;
                    ci.IndexSend = 0;
                    ci.LeftSend = buf.Length;


                    ci.Sock.BeginSend(ci.BufSend, ci.IndexSend, ci.LeftSend, SocketFlags.None, new AsyncCallback(ServerSendProc), ci);
                }
                catch (Exception e)
                {
                    DisposeClient(ci);
                    Logging.WriteLog(DateTime.Now.ToString(), e.Message.ToString(), e.StackTrace.ToString(), e.TargetSite.ToString(), EDSType.CorridorSpeed);
                    DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Exception oluştu: SendMsgToClient ", Color.DarkRed);
                }
            }


            private object m_lockFindEntry = new object();
            public string FindEntryImageName(string plate, DateTime date, string imageType)
            {
                lock (m_lockFindEntry)
                {
                    double hour;

                    if (m_speedTolerance)
                    {
                        double speedTolerance = (m_speed / 100) * Convert.ToDouble(m_tolerancePercentage);
                        double speedWithTolerance = m_speed + speedTolerance + 1;

                        hour = ((m_distance / 1000) / speedWithTolerance);
                    }
                    else
                        hour = ((m_distance / 1000) / m_speed);

                    double minute = hour * 60;

                    DateTime exitDatesLimit = date.AddMinutes(-minute);
                    string exitImageName = ""; 

                    if(imageType == "L2-C0")
                    {
                        exitImageName = m_images.Find(x => (ImageName.Plate(x) == plate) && (ViolationsDate.StringDateToDateTime(ImageName.Day(x), ImageName.Hour(x)) > exitDatesLimit &&
                              ViolationsDate.StringDateToDateTime(ImageName.Day(x), ImageName.Hour(x)) < date) && ImageName.ImageType(x) =="L1-C0");
 
                    }
                    else
                    {
                        exitImageName = m_images.Find(x => (ImageName.Plate(x) == plate) && (ViolationsDate.StringDateToDateTime(ImageName.Day(x), ImageName.Hour(x)) > exitDatesLimit &&
                            ViolationsDate.StringDateToDateTime(ImageName.Day(x), ImageName.Hour(x)) < date) && ImageName.ImageType(x) != "L1-C0"); 
                    }

                        
                    return exitImageName;
                }
            }

            private object m_lockFindExit = new object();
            public DateTime FindExitDate(DateTime date)
            {
                lock (m_lockFindExit)
                {
                    double hour;

                    if (m_speedTolerance)
                    {
                        double speedTolerance = (m_speed / 100) * Convert.ToDouble(m_tolerancePercentage);
                        double speedWithTolerance = m_speed + speedTolerance;

                        hour = ((m_distance / 1000) / speedWithTolerance);
                    }
                    else
                        hour = ((m_distance / 1000) / m_speed);

                    double minute = hour * 60;

                    DateTime exitDatesLimit = date.AddMinutes(minute);

                    return exitDatesLimit;
                }
            }
            private object m_lockFindOldestEntryDate = new object();
            public void FindOldestEntryDate(DateTime date)
            {
                lock (m_lockFindOldestEntryDate)
                {
                    double hour;

                    if (m_speedTolerance)
                    {
                        double speedTolerance = (m_speed / 100) * Convert.ToDouble(m_tolerancePercentage);
                        double speedWithTolerance = m_speed + speedTolerance;

                        hour = ((m_distance / 1000) / speedWithTolerance);
                    }
                    else
                        hour = ((m_distance / 1000) / m_speed);

                    double minute = hour * 60;

                    DateTime entryDatesLimit = date.AddMinutes(-minute);  

                    List<string> orkinos = m_images.FindAll(x => ViolationsDate.StringDateToDateTime(ImageName.Day(x), ImageName.Hour(x)) < entryDatesLimit);

                   foreach (string item in orkinos)
                   { 
                       //Task<int> taskDeleteNTP = DatabaseOperation.NTP.Singleton().AsyncDelete(item);
                       //taskDeleteNTP.Wait();

                       //if(taskDeleteNTP.Result > 0)
                       //{
                           SocketCommunication.Singleton().m_toBeDeleted.Enqueue(item);
                           m_images.Remove(item); 
                       //}
                            
                   } 

                   //m_images.RemoveAll(x => ViolationsDate.StringDateToDateTime(ImageName.Day(x), ImageName.Hour(x)) < entryDatesLimit); 
                  
                }
            } 

            #endregion

        }
    }

    






}

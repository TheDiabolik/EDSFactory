using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
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
        public class Client : ISyncFileWatcher
        {
            #region variables
            private static Client m_do = null;
            internal Socket m_clientSock;  
            private readonly object m_lockClient;
            private readonly object m_lockQueue; 
            string incomingMsg, imageName,  exitImageName  ;
            private static BlockingCollection<string> m_imagesNames;
            private byte[] m_clientBufRecv, m_clientBufSend;
            private int m_clientIndexRecv, m_clientLeftRecv, m_clientIndexSend, m_clientLeftSend;
            private bool m_clientLengthFlagRecv;
              DateTime m_serverConnDate;
             //System.Threading.Timer m_threadTimer;
             // ManualResetEvent instances signal completion.
              //private static ManualResetEvent m_connectDone, m_sendDone, m_receiveDone;
             Stopwatch m_stopWatch;
             System.Timers.Timer m_timer;
             private static readonly object m_padlock = new object();
      
            #endregion 
             
            #region constructor
            Client()
            { 
                m_lockClient = new object();
                m_lockQueue = new object();
                m_clientBufRecv = new byte[1048576];
                m_clientBufSend = new byte[1048576]; 

                m_stopWatch = new Stopwatch();
                m_timer = new System.Timers.Timer(15000);
                m_timer.Elapsed += m_stopWatch_Elapsed; 
              }

            void m_stopWatch_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
            {
                if (m_clientSock != null)
                {
                    if (m_stopWatch.Elapsed.TotalSeconds > 5)
                    {
                        SendMsgToServer("AREYOUALIVE$");
                        DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Bağlantı Kontrol Ediliyor...!", Color.DarkRed);
                    }
                }
                else
                {
                    //m_clientSock.Dispose(); 
                    StartClient(SocketCommunication.Singleton().m_settings.m_entryTagIP, "stopwatch");
                }


                m_stopWatch.Restart();
            }
           
            #endregion

            #region singleton
            public static Client Singleton()
            {
                lock (m_padlock)
                {
                    if (m_do == null)
                        m_do = new Client();

                    return m_do;
                }
            }
            #endregion 

          
            #region filecreatedevent
            public void SyncFileCreated(string fileName)
            {
                m_imagesNames.Add(fileName);
            }
            #endregion

            public void StartClient(string ipAddress, string sekil)
            {
                if (m_timer.Enabled)
                    m_timer.Stop();


                m_timer.Start(); 
                SocketCommunication.Singleton().sda(); 

                //lock (m_lockClient)
                {
                     //Task<List<string>> taskSelectNTP =  DatabaseOperation.NTP.Singleton().AsycSelect();
                     //taskSelectNTP.Wait();
                    List<string> imageNameList = FileOperation.Find(SocketCommunication.Singleton().m_settings.m_imagePath + "\\" + "sync");
                    
                    m_imagesNames = new BlockingCollection<string>();

                    foreach (string item in imageNameList)
                    {
                        if (!m_imagesNames.Contains(item))
                            m_imagesNames.Add(item);
                    }
                }

                m_clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_clientSock.NoDelay = true;
                m_clientSock.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);

                m_clientSock.BeginConnect(new IPEndPoint(IPAddress.Parse(ipAddress), Convert.ToInt32(SocketCommunication.Singleton().m_settings.m_entryTagPort)), new AsyncCallback(ClientConnectProc), null); 

            }
        
            public void StopClient(bool keepAlive)
            {
                if ((m_clientSock != null) || (m_clientSock != null && m_clientSock.Connected))
                { 
                    if (!m_clientSock.Connected)
                    {
                        DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Sorgulama Durduruluyor...", Color.DarkRed);
                        //stopAsking = true;
                    }
                    
                    if (m_clientSock.Connected)
                    {
                        SendMsgToServer("DISCONNECT$"); 
                    }
                        
                }

                if (!keepAlive)
                    m_timer.Stop();
            }

            private void ClientConnectProc(IAsyncResult iar)
            {
                try
                {  
                    m_clientSock.EndConnect(iar);
                     

                    //alma
                    m_clientIndexRecv = 0;
                    m_clientLeftRecv = 4;
                    m_clientLengthFlagRecv = true; 
               
                    SendMsgToServer("CONNECT$" + SocketCommunication.Singleton().m_settings.m_speed + "-" + SocketCommunication.Singleton().m_settings.m_distance + "-" + SocketCommunication.Singleton().m_settings.m_applyTolerance + "-" + SocketCommunication.Singleton().m_settings.m_tolerancePercentage);

                    DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Giriş Tagına  Bağlanıldı...", Color.DarkOrange);

                    m_clientSock.BeginReceive(m_clientBufRecv, m_clientIndexRecv, m_clientLeftRecv,
                       SocketFlags.None, new AsyncCallback(ClientReceiveProc), null); 
                }
                catch (Exception e)
                {
                    DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "ClientSendProc Exception oluştu:" + e.Message, Color.DarkRed);
                    //MessageBox.Show(e.Message);
                } 
            }
             

            private void ClientReceiveProc(IAsyncResult iar)
            {
                try
                {
                    int n = 0;

                    n = m_clientSock.EndReceive(iar);

                    m_clientIndexRecv += n;
                    m_clientLeftRecv -= n;

                    if (m_clientLeftRecv == 0)
                    {
                        if (m_clientLengthFlagRecv)
                        {
                            m_clientLeftRecv = BitConverter.ToInt32(m_clientBufRecv, 0);
                            m_clientIndexRecv = 0;
                            m_clientLengthFlagRecv = false;
                        }
                        else
                        {
                            string msg = Encoding.UTF8.GetString(m_clientBufRecv, 0, m_clientIndexRecv);
                            incomingMsg = msg.Substring(0, msg.IndexOf("$"));
                            imageName = msg.Substring(msg.IndexOf("$") + 1);

                            if (!m_stopWatch.IsRunning)
                            {
                                m_stopWatch.Start();
                            }

                            m_stopWatch.Restart();

                            if (incomingMsg == "CONNECTED")
                            {
                                m_serverConnDate = DateTime.Now;

                                SendNextImageName();
                            }
                            else if (incomingMsg == "FOUND")
                            {
                                SendMsgToServer("SENDIMAGE$" + imageName); 
                            }

                            else if (incomingMsg == "NOTFOUND")
                            {
                                SocketCommunication.Singleton().m_toBeDeleted.Enqueue(imageName);

                                DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Silinecek Resimler Listesine Eklendi...", Color.DarkRed);

                                SendNextImageName();
                            }

                            else if (incomingMsg == "GETIMAGE")
                            {
                                string dwdw = imageName.Substring(0, imageName.IndexOf('+'));
                                string[] entryAndExitArray = dwdw.Split('!');

                                string entry = entryAndExitArray[0];
                                string exit = entryAndExitArray[1];

                                int finishCommand = Array.IndexOf(m_clientBufRecv, Convert.ToByte('+')) + 1;

                                byte[] image = new byte[m_clientBufRecv.Length - finishCommand];
                                Array.Copy(m_clientBufRecv, finishCommand, image, 0, image.Length);

                                using(MemoryStream ms = new MemoryStream(image))
                                {
                                //MemoryStream ms = new MemoryStream(image);
                                    Image returnImage = Image.FromStream(ms);
                                    returnImage.Save(SocketCommunication.Singleton().m_settings.m_imagePath + "\\" + "sync" + "\\" + entry);
                                    returnImage.Dispose();
                                    ms.Flush();
                                    
                                } 
                                DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Çıkış Tagı İlgili Aracın Giriş Resmini Kaydetti :" + entry, Color.DarkBlue);

                                SocketCommunication.Singleton().m_violation.Violation(new List<string>(new string[] { entry, exit })); 

                                SendMsgToServer("RECEIVEDIMAGE$" + entry); 
                            }
                            else if (incomingMsg == "SENDNEXT")
                            {
                                SendNextImageName();
                            }
                            else if (incomingMsg == "LISTENINGSTOP")
                            {
                                SendMsgToServer("SENDINGSTOP$");
                                DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Giriş Tagı Haberleşmeyi Durdurduğu İçin Sorgulama Durduruldu!!!", Color.DarkRed);
                            }
                            else if (incomingMsg == "DISCONNECTED")
                            {
                                m_clientSock.Shutdown(SocketShutdown.Both);
                                m_clientSock.Close();
                                m_clientSock = null;
                            }
                            else if (incomingMsg == "RECONNECT")
                            { 
                                StartClient(SocketCommunication.Singleton().m_settings.m_entryTagIP, "reconnect");
                            }

                            m_clientLengthFlagRecv = true;
                            m_clientIndexRecv = 0;
                            m_clientLeftRecv = 4;
                             
                        }
                    }

                    m_clientSock.BeginReceive(m_clientBufRecv, m_clientIndexRecv, m_clientLeftRecv, SocketFlags.None, new AsyncCallback(ClientReceiveProc), null);
                }
                catch (Exception e)
                {
                    Logging.WriteLog(DateTime.Now.ToString(), e.Message.ToString(), e.StackTrace.ToString(), e.TargetSite.ToString(), EDSType.CorridorSpeed);
                    //DisplayManager.SpeedCorridorConInfo(m_speedCorridor.richTextBox1, "Clientreceive Exception oluştu:" + e.Message, Color.DarkRed);

                    SocketCommunication.Singleton().m_toBeDeleted.Clear(); 

                    if (m_clientSock != null)
                    {
                        m_clientSock.Close();
                        m_clientSock = null;


                    }
                    return;
                } 
            }
       
            static object m_findExit = new object();
            public DateTime FindExitDate(DateTime date)
            {
                lock (m_findExit)
                {
                    double hour;

                    if (SocketCommunication.Singleton().m_settings.m_applyTolerance)
                    {
                        double speedTolerance = (Convert.ToDouble(SocketCommunication.Singleton().m_settings.m_speed) / 100) * Convert.ToDouble(SocketCommunication.Singleton().m_settings.m_tolerancePercentage);
                        double speedWithTolerance = Convert.ToDouble(SocketCommunication.Singleton().m_settings.m_speed) + speedTolerance;

                        hour = ((Convert.ToDouble(SocketCommunication.Singleton().m_settings.m_distance) / 1000) / speedWithTolerance);
                    }
                    else
                        hour = ((Convert.ToDouble(SocketCommunication.Singleton().m_settings.m_distance) / 1000) / Convert.ToDouble(SocketCommunication.Singleton().m_settings.m_speed));

                    double minute = hour * 60;

                    DateTime exitDatesLimit = date.AddMinutes(minute);

                    return exitDatesLimit;
                }
            }

            private readonly object sendSyncRoot = new object();
            private readonly object receiveSyncRoot = new object();


            private void ClientSendProc(IAsyncResult iar)
            {
                try
                {
                    int n = m_clientSock.EndSend(iar);

                    m_clientIndexSend += n;
                    m_clientLeftSend -= n;

                    //if (m_clientLeftSend != 0)
                    //    m_clientSock.BeginSend(m_clientBufSend, m_clientIndexSend, m_clientLeftSend, SocketFlags.None, new AsyncCallback(ClientSendProc), null);
                    //else
       

                }
                catch (Exception e)
                {
                    Logging.WriteLog(DateTime.Now.ToString(), e.Message.ToString(), e.StackTrace.ToString(), e.TargetSite.ToString(), EDSType.CorridorSpeed);
                    DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "ClientSendProc Exception oluştu:" + e.Message, Color.DarkRed);
                    //MessageBox.Show("Exception oluştu:" + e.Message);
                } 
            }

            #region methods
            private void SendMsgToServer(string msg)
            {
                try
                {
                    int len;

                    len = StringToByteMsg(msg, m_clientBufSend);

                    m_clientIndexSend = 0;
                    m_clientLeftSend = len;

                    m_clientSock.BeginSend(m_clientBufSend, m_clientIndexSend, m_clientLeftSend, SocketFlags.None, new AsyncCallback(ClientSendProc), null);
                }
                catch (Exception e)
                {

                    Logging.WriteLog(DateTime.Now.ToString(), e.Message.ToString(), e.StackTrace.ToString(), e.TargetSite.ToString(), EDSType.CorridorSpeed);
                    DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "SendMsgToServer Exception oluştu:" + e.Message, Color.DarkRed);

                    SocketCommunication.Singleton().m_toBeDeleted.Clear();
                    if (m_clientSock != null)
                    {
                        m_clientSock.Close();
                        m_clientSock = null;


                    }
                    return;
                } 
            } 
         
            public void SendNextImageName()
            {
                foreach (string item in m_imagesNames.GetConsumingEnumerable())
                {
                    int dete;
                    int count = 0;

                    //do
                    //{
                    //    Task<int> taskDeleteNTP = DatabaseOperation.NTP.Singleton().AsyncDelete(item);
                    //    taskDeleteNTP.Wait();
                    //    dete = taskDeleteNTP.Result;

                    //    if(dete <= 0)
                    //        Thread.Sleep(400);

                    //    count++;

                    //} while ((dete <= 0) && (count <= 3)); 


                    bool isWantedType = CheckImageNameOrList.IsWantedType(item, SocketCommunication.Singleton().m_wantedImageType);

                    if (isWantedType && VerificateImageName.Name(item, false))
                    {
                        exitImageName = item;
                        string plate = ImageName.Plate(exitImageName);
                        DateTime re = ViolationsDate.StringDateToDateTime(ImageName.Day(exitImageName), ImageName.Hour(exitImageName));

                        DateTime maxViolationDate = ViolationsDate.FindDate(re, SocketCommunication.Singleton().m_settings.m_protectViolationTime, 0, 0, Enums.Date.Add);
                        DateTime minViolationDate = ViolationsDate.FindDate(re, SocketCommunication.Singleton().m_settings.m_protectViolationTime, 0, 0, Enums.Date.Subtract);
                      
                        //List<DateTime> violationDateInDatabase = new List<DateTime>();
                        Task<List<DateTime>> checkDatabaseViolation;

                        if (SocketCommunication.Singleton().m_wantedImageType.Contains("L2-C0"))
                        {
                            checkDatabaseViolation = DatabaseOperation.CorridorSpeed.Singleton().AsycSelect(plate, minViolationDate.ToShortDateString(), maxViolationDate.ToShortDateString()); 
                        }
                        else
                        {
                            checkDatabaseViolation = DatabaseOperation.CorridorSpeed.Singleton().AsycSelect(plate, minViolationDate.ToShortDateString(), maxViolationDate.ToShortDateString()); 
                        }

                        checkDatabaseViolation.Wait();

                        List<DateTime> hasViolation = checkDatabaseViolation.Result.FindAll(x => x < maxViolationDate && x > minViolationDate);


                        if (hasViolation.Count > 0)//o zaman kayıtlı ihlal var ?
                        {
                            SocketCommunication.Singleton().m_toBeDeleted.Enqueue(exitImageName);
                        }
                        else
                        {
                            DisplayManager.RichTextBoxInvoke(SocketCommunication.Singleton().m_speedCorridor.richTextBox1, "Soruluyor : " + item, Color.DarkRed);
                            SendMsgToServer("FINDENTRY$" + item);
                            break;
                        }
                    }
                    else if (isWantedType && !VerificateImageName.Name(item, false))
                    {
                        SocketCommunication.Singleton().m_toBeDeleted.Enqueue(item);
                    }
                }
            }
 
            #endregion

        }
    }
}

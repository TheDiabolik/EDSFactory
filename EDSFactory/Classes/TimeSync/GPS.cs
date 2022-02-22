using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDSFactory
{
    class GPS
    {
        #region variable
        private static GPS m_do;
        public SerialPort m_serialPort;
        public MethodInvoker m_startPort;
        public MethodInvoker m_stopPort;
        GPSData gpsData;

        private bool isConn;

        public bool IsConn
        {
            get { return isConn; }
            set
            {
                bool conn = isConn;
                isConn = value;

                if (Status != null)
                {
                    Status.Invoke(conn, value);
                }
            }
        }




        private object m_lock = new object();

        //public SerialPort SerialPort
        //{
        //    get { return m_serialPort; }
        //    set { m_serialPort = value; }
        //}
        #endregion

        #region struct
        public struct GPSData
        {
            public DateTime m_dateTime; 

            public double m_latitude;
            public double m_longitude;
            public double m_speed;
        };
        #endregion 

        #region singleton
        //public static GPS Singleton(SerialPort serialPort)
        //{
        //    if (m_do == null)
        //        m_do = new GPS(serialPort);

        //    return m_do;
        //}

        public static GPS Singleton()
        {
            if (m_do == null)
                m_do = new GPS();

            return m_do;
        }
        #endregion

        #region constructor
        //public GPS(SerialPort serialPort)
        //{
        //    m_serialPort = serialPort;
        //    m_serialPort.DataReceived += m_serialPort_DataReceived;

        //    DataReceived += GPS_DataReceived;
        //    LocalTimeUpdated += GPS_LocalTimeUpdated;
        //}

        public GPS()
        {
            m_serialPort = null;

            m_serialPort = new SerialPort();
            m_serialPort.RtsEnable = true;
            //m_serialPort.ReadTimeout = 1000;
            //m_serialPort.WriteBufferSize = 128;
            m_serialPort.DtrEnable = true;
            m_serialPort.Handshake = Handshake.None;
            m_startPort = new MethodInvoker( StartListening);
            m_stopPort = new MethodInvoker( StopListening);
            Status += Status;
          
        }

        void GPS_LocalTimeUpdated(DateTime dt)
        {
            NTPClient.SetTime(dt);
        }

        void GPS_DataReceived(GPS.GPSData received)
        {
            LocalTimeUpdated.Invoke(received.m_dateTime);
        }


        #endregion


        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.EndsWith(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }


        StringBuilder sb = new StringBuilder(); 
        #region event
        void m_serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //SerialPort mySerialPort = (SerialPort)sender;  
                //sb.Append(mySerialPort.ReadExisting());  

                //string s = getBetween(sb.ToString(), "$GPRMC", "\r\n"); 

                string s = m_serialPort.ReadLine(); 
                 
                if (s.StartsWith("$GPRMC"))//if (!string.IsNullOrEmpty(s))
                {
                    sb.Clear();
                    //gps verileri parse ediliyor
                    string[] splitArray = s.Split(',');

                    //enlem ve boylam bilgileri
                    string lat = splitArray[3];
                    string log = splitArray[5];

                    //tarih bilgileri
                    string date = splitArray[9];
                    string day = date.Substring(0, 2);
                    string month = date.Substring(2, 2);
                    string year = string.Format("20{0}", date.Substring(4, 2));

                    //saat bilgileri
                    string time = splitArray[1];
                    string hour = time.Substring(0, 2);
                    string minute = time.Substring(2, 2);
                    string second = time.Substring(4, 2);

                    //hız bilgisi
                    string speed = splitArray[7];

                    //struct değişkenleri set ediliyor
                    gpsData.m_dateTime = UTCToLocalTime(new DateTime(int.Parse(year), int.Parse(month), int.Parse(day), int.Parse(hour), int.Parse(minute), int.Parse(second), DateTimeKind.Utc));

                    if (!string.IsNullOrEmpty(lat))
                        gpsData.m_latitude = ConvertLocation(lat);

                    if (!string.IsNullOrEmpty(log))
                        gpsData.m_longitude = ConvertLocation(log);

                    if (!string.IsNullOrEmpty(speed))
                        gpsData.m_speed = CalculateSpeed(speed);
                    
                    
                    IsConn = true;
                    
                    //struct değişkenleri için event tetikleniyor
                    DataReceived.Invoke(gpsData);
                }  
            }
            catch (Exception)
            {
                IsConn = false;
                //throw new GPSException("GPS Verileri Yorumlanamadı! \n Hata Mesajı : " + ex.ToString());
            }
        }
        #endregion
    
        #region methods
        public void StartListening()
        {
            try
            {

                if (m_serialPort.IsOpen)
                {

                    m_serialPort.DataReceived -= m_serialPort_DataReceived;

                    DataReceived -= GPS_DataReceived;
                    LocalTimeUpdated -= GPS_LocalTimeUpdated;
                    m_serialPort.Close();

                }

                else
                {

                    m_serialPort.DataReceived += m_serialPort_DataReceived;

                    DataReceived += GPS_DataReceived;
                    LocalTimeUpdated += GPS_LocalTimeUpdated;

                    m_serialPort.Open();
                }
            }
            catch (Exception e)
            {
                IsConn = false;
                throw new GPSException("GPS Bağlantısı Başlatılamadı! \n Hata Mesajı : " + e.ToString());
            } 
        } 

        public void StopListening()
        {
            try
            {
                if (m_serialPort.IsOpen)
                {
                    m_serialPort.Close();
                    sb.Clear();
                }

                IsConn = false;
            }
            catch (GPSException e)
            {
                throw new GPSException("GPS Bağlantısı Sonlandırılamadı! \n Hata Mesajı : " + e.ToString());
            }
        }

        public double ConvertLocation(string latorlong)
        {
            double x1 = Convert.ToDouble(latorlong.Substring(0, 2));
            double x2 = Convert.ToDouble(latorlong.Substring(2));

            return x1 + x2 / 600000; 
        }

        public double CalculateSpeed(string speed)
        { 
            return Convert.ToDouble(speed.Replace('.',',')) * double.Parse("1,852"); 
        }

        public DateTime UTCToLocalTime(DateTime dt)
        {
          return  dt.ToLocalTime();
        }
        #endregion

        #region raise event
        public delegate void ConnectionStatus(bool oldConn, bool newConn);
        public event ConnectionStatus Status;

        public delegate void GPSDataReceived(GPSData received);
        public event GPSDataReceived DataReceived;

        public delegate void SystemTimeUpdated(DateTime dt);
        public event SystemTimeUpdated LocalTimeUpdated;
       

        public class GPSException : ApplicationException
        {
            public GPSException(string message)
                : base(message)
            {
            }
        }

        #endregion
    }
}

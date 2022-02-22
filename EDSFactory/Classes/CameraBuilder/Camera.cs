using AForge.Video;
using AForge.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks; 
using System.Collections.Concurrent;
using System.Windows.Forms;
using AForge.Video.VFW; 


namespace EDSFactory 
{
    public class Camera : ICamera
    {
        public delegate void ToBeWriteFrame(List<byte[]> fra, string videoName, int beforeAndAfterSecond);
        public static event ToBeWriteFrame Status;

         private List<KeyValuePair<DateTime, byte[]>> m_buffer;

         public List<KeyValuePair<DateTime, byte[]>> Buffer
        {
            get { return m_buffer; }
            set { m_buffer = value; }
        }

        public int m_beforeAndAfterSecond { get; set; }
        public string m_cameraURL { get; set; }
        public string m_userName { get; set; }
        public string m_password { get; set; }
        public dynamic m_stream { get; set; } 

        public Camera()
        { 
            Camera.Status -= Camera_Status;   
            Camera.Status += Camera_Status;   
        }
        private static readonly object lao = new object(); 
        static void Camera_Status(List<byte[]> fra, string videoName, int beforeAndAfterSecond)
        {
            lock (lao)
            { 
                VideoFileWriter writer = new VideoFileWriter();
                double os = (Convert.ToDouble(fra.Count) / (Convert.ToDouble(beforeAndAfterSecond) * 2));
                int frameCount = Convert.ToInt32(Math.Round(os));

                if (frameCount <= 0)
                    frameCount = 1; 

                writer.Open(videoName, 640, 480, frameCount);
                foreach (var item1 in fra)
                {
                    writer.WriteVideoFrame(BitmapOperation.ByteToBitmap(item1));
                }
               
                writer.Close();
                writer.Dispose();
            } 
        }
         
        public void CameraStream()
        {
            this.m_stream = m_stream;
        }
         
        public void SetCameraSource(string entryCamCameraURL)
        {
            this.m_cameraURL = entryCamCameraURL;
        } 

        public void SetCameraUserName(string entryCamUserName)
        {
            this.m_userName = entryCamUserName;
        }

        public void SetCameraPassword(string entryCamPassword)
        {
            this.m_password = entryCamPassword;
        } 

        public void SetBeforeAndAfterTime(int beforeAndAfterSecond)
        {
            this.m_beforeAndAfterSecond = beforeAndAfterSecond;
        }

        public void SetBuffer(List<KeyValuePair<DateTime, byte[]>> buffer)
        {
            this.Buffer = buffer;
        } 

        public void StartCamera()
        {
            m_stream.Source = m_cameraURL;
            m_stream.Login = m_userName;
            m_stream.Password = m_password; 

            m_stream.NewFrame += new NewFrameEventHandler(m_cameraStream_NewFrame);
            m_stream.Start(); 
        }

        void m_cameraStream_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (Buffer.Count > ((m_beforeAndAfterSecond * 2 * 25)))
                Buffer.RemoveAt(0);

            Bitmap Frame = eventArgs.Frame;
            byte[] freame = BitmapOperation.BitmapToByte(Frame);
            m_buffer.Add(new KeyValuePair<DateTime, byte[]>(DateTime.Now, freame)); 

            GC.Collect();
        }

     
        public  void Trigged(DateTime dt, string videoName)
        { 
            Thread myThre = new Thread(new ParameterizedThreadStart(SaveVideo));
            myThre.IsBackground = true;
            myThre.Start(videoName); 
        }
        
   
        private void SaveVideo(object videoName)
        {
            //string name = videoName.ToString();
            //string violationDay = StringFormatOperation.Date(ImageName.Day(name));
            //string violationHour = StringFormatOperation.Hour(ImageName.Hour(name));

            //DateTime triggeredDate = DateTime.Parse(violationDay + " " + violationHour);
            DateTime triggeredDate = DateTime.Now;
            DateTime minBufDate = triggeredDate.Subtract(new TimeSpan(0, 0, m_beforeAndAfterSecond));
            DateTime maxBufDate = triggeredDate.AddSeconds(m_beforeAndAfterSecond);

            Thread.Sleep(m_beforeAndAfterSecond * 1000);

            List<KeyValuePair<DateTime, byte[]>> ahmet1 = Buffer.ToList();
            List<byte[]> framesBeforAndAfter = ahmet1.Where(x => (minBufDate <= x.Key) && (x.Key <= maxBufDate)).Select(f => f.Value).ToList();

            Status.Invoke(framesBeforAndAfter, videoName.ToString(), m_beforeAndAfterSecond);
        }

        public void StopCamera()
        {
            m_buffer.Clear();
            m_stream.SignalToStop();
            m_stream.NewFrame -= new NewFrameEventHandler(m_cameraStream_NewFrame);
            GC.Collect();
        }
   
    }
}

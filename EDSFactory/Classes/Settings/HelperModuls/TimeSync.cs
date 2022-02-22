using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    public partial class Settings
    {
        [Serializable]
        public class TimeSync
        {
            public bool m_GPSActive { get; set; }
            public string m_portName { get; set; }
            public int m_baudRate { get; set; }
            public string m_dataBits { get; set; }
            public Parity m_parity { get; set; }
            public StopBits m_stopBits { get; set; }

            public bool m_autoStart { get; set; }
            public string m_syncPath { get; set; }
            public string m_timeServerIP { get; set; }
            public int m_syncSecond { get; set; }
            public bool m_deleteUnsynFile { get; set; }

            public bool m_openProcess { get; set; }
            public string m_processPath { get; set; }
            public string m_imagePath { get; set; }

            private static TimeSync mhss = new TimeSync();

            public static TimeSync Singleton()
            {
                return mhss;
            }

            public TimeSync()
            {
                //m_syncPrograms = new MyList<string>();
                //m_timeSync = Serialization.TimeSynchronizerSerilization.Singleton();
            }

            public void Serilize(TimeSync m_ses)
            {
                //lock (m_ses)
                Serialization.SerializeClass.Serialize(SerializationPaths.TimeSycn, m_ses);
            }
            public TimeSync DeSerialize(TimeSync m_ses)
            {
                CheckSerializationFile();
                return Serialization.SerializeClass.DeSerialize(SerializationPaths.TimeSycn, m_ses);
            }

            public void CheckSerializationFile()
            {
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(SerializationPaths.TimeSycn)))
                        Directory.CreateDirectory(Path.GetDirectoryName(SerializationPaths.TimeSycn));

                    //xmlserilization dosyasını kontrol ediyoruz
                    if (!File.Exists(SerializationPaths.TimeSycn))
                    {
                        TimeSync.Singleton().m_timeServerIP = "10.5.1.10";
                        TimeSync.Singleton().m_syncSecond  = 2;
                        TimeSync.Singleton().m_processPath = ""; 

                        TimeSync.Singleton().Serilize(TimeSync.Singleton());
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.CheckSerilizationFileExceptionMessage, ex);
                }
            } 

        }
    }
}

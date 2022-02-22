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
        public  class PTZControlSettings
        {
            #region properties
            public string m_portName { get; set; }
            public int m_baudRate { get; set; }
            public string m_dataBits { get; set; }
            public Parity m_parity { get; set; }
            public StopBits m_stopBits { get; set; }
            public bool m_autoStart { get; set; }
            public bool m_presetChoice { get; set; }
            public bool m_comPortStatus { get; set; }
            public int m_presetChangingTime { get; set; }
            public int m_totalPresetNumber { get; set; }
            public D.LensSpeed m_lensSpeed { get; set; }
            public int m_waitingTime { get; set; }
            public bool m_waitingTimeUnit { get; set; }

            //preset ayarları
            private SerializableDictionary<int, string> m_tourPreset1;
            private SerializableDictionary<int, string> m_tourPreset2;
            private SerializableDictionary<int, string> m_tourPreset3;
            private SerializableDictionary<int, string> m_tourPreset4;
            private SerializableDictionary<int, string> m_tourPreset5;
            private SerializableDictionary<int, string> m_tourPreset6;
            private SerializableDictionary<int, string> m_tourPreset7;
            private SerializableDictionary<int, string> m_tourPreset8;
            private SerializableDictionary<int, string> m_tourPreset9;
            private SerializableDictionary<int, string> m_tourPreset10;

            public SerializableDictionary<int, string> TourPreset1
            {
                get { return m_tourPreset1; }
                set { m_tourPreset1 = value; }
            }

            public SerializableDictionary<int, string> TourPreset2
            {
                get { return m_tourPreset2; }
                set { m_tourPreset2 = value; }
            }

            public SerializableDictionary<int, string> TourPreset3
            {
                get { return m_tourPreset3; }
                set { m_tourPreset3 = value; }
            }

            public SerializableDictionary<int, string> TourPreset4
            {
                get { return m_tourPreset4; }
                set { m_tourPreset4 = value; }
            }

            public SerializableDictionary<int, string> TourPreset5
            {
                get { return m_tourPreset5; }
                set { m_tourPreset5 = value; }
            }

            public SerializableDictionary<int, string> TourPreset6
            {
                get { return m_tourPreset6; }
                set { m_tourPreset6 = value; }
            }

            public SerializableDictionary<int, string> TourPreset7
            {
                get { return m_tourPreset7; }
                set { m_tourPreset7 = value; }
            }

            public SerializableDictionary<int, string> TourPreset8
            {
                get { return m_tourPreset8; }
                set { m_tourPreset8 = value; }
            }

            public SerializableDictionary<int, string> TourPreset9
            {
                get { return m_tourPreset9; }
                set { m_tourPreset9 = value; }
            }


            public SerializableDictionary<int, string> TourPreset10
            {
                get { return m_tourPreset10; }
                set { m_tourPreset10 = value; }
            }

            private static PTZControlSettings m_PTZControlSettings;
            //private static Serialization.PTZControlSettingsSerialization m_PTZcss;
            #endregion

            #region singleton


            public static PTZControlSettings Singleton()
            {
                if (m_PTZControlSettings == null)
                {
                    m_PTZControlSettings = new PTZControlSettings();

                }

                return m_PTZControlSettings;
            }
            #endregion


            private PTZControlSettings()
            {
                this.TourPreset1 = new SerializableDictionary<int, string>();
                this.TourPreset2 = new SerializableDictionary<int, string>();
                this.TourPreset3 = new SerializableDictionary<int, string>();
                this.TourPreset4 = new SerializableDictionary<int, string>();
                this.TourPreset5 = new SerializableDictionary<int, string>();
                this.TourPreset6 = new SerializableDictionary<int, string>();
                this.TourPreset7 = new SerializableDictionary<int, string>();
                this.TourPreset8 = new SerializableDictionary<int, string>();
                this.TourPreset9 = new SerializableDictionary<int, string>();
                this.TourPreset10 = new SerializableDictionary<int, string>();

                //m_PTZcss = Serialization.PTZControlSettingsSerialization.Singleton();
            }


            public void Serialize(PTZControlSettings PTZControlSettings)
            {
                Serialization.SerializeClass.Serialize(SerializationPaths.PTZController,PTZControlSettings);

 
            }
            public PTZControlSettings DeSerialize(PTZControlSettings PTZControlSettings)
            {
                CheckSerializationFile();
                return Serialization.SerializeClass.DeSerialize(SerializationPaths.PTZController, PTZControlSettings);
            }


            public void CheckSerializationFile()
            {
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(SerializationPaths.PTZController)))
                        Directory.CreateDirectory(Path.GetDirectoryName(SerializationPaths.PTZController));

                    //xmlserilization dosyasını kontrol ediyoruz
                    if (!File.Exists(SerializationPaths.PTZController))
                    {
                        //PTZControlSettings.Singleton().m_imagePath = "";
                        //PTZControlSettings.Singleton().m_violationImagesPath = "";
                        //PTZControlSettings.Singleton().m_thumbNailImagesPath = "";
                        //PTZControlSettings.Singleton().m_deleteImages = false;
                        //PTZControlSettings.Singleton().m_protectViolationTime = 2;

                        PTZControlSettings.Singleton().Serialize(PTZControlSettings.Singleton());
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

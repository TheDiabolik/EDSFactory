using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EDSFactory 
{
    
    public partial class Settings
    {
        [Serializable]
        public class StandingSettings : AreaViolation
        {
            public int m_scanImageTime { get; set; }
            public bool m_videoMode { get; set; }

            private static StandingSettings m_ss = new StandingSettings();
            //internal Serialization.SingleEntrySettingsSerialization m_sess = new Serialization.SingleEntrySettingsSerialization();
            public static StandingSettings Singleton()
            {
                return m_ss;
            }

            public StandingSettings()
            {
            }

            public   void Serilize(dynamic m_ses)
            {
                Serialization.SerializeClass.Serialize(SerializationPaths.Standing, m_ses);

            }
            public   dynamic DeSerialize(dynamic m_ses)
            {
                CheckSerializationFile();
                return Serialization.SerializeClass.DeSerialize(SerializationPaths.Standing, m_ses);
            }


            public void CheckSerializationFile()
            {
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(SerializationPaths.Standing)))
                        Directory.CreateDirectory(Path.GetDirectoryName(SerializationPaths.Standing));

                    //xmlserilization dosyasını kontrol ediyoruz
                    if (!File.Exists(SerializationPaths.Standing))
                    {
                        StandingSettings.Singleton().m_imagePath = "";
                        StandingSettings.Singleton().m_violationImagesPath = "";
                        StandingSettings.Singleton().m_thumbNailImagesPath = "";
                        StandingSettings.Singleton().m_deleteImages = false;
                        StandingSettings.Singleton().m_protectViolationTime = 2;

                        StandingSettings.Singleton().Serilize(StandingSettings.Singleton());
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

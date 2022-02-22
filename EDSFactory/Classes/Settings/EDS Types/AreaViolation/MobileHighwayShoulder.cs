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
        public class MobileHighwayShoulderSettings : AreaViolation
        {
            public int m_scanImageTime { get; set; }

            private static MobileHighwayShoulderSettings mhss = new MobileHighwayShoulderSettings();

            public static MobileHighwayShoulderSettings Singleton()
            {
                return mhss;
            }

            public MobileHighwayShoulderSettings()
            {
            }

            public void Serilize(MobileHighwayShoulderSettings m_ses)
            {
                Serialization.SerializeClass.Serialize(SerializationPaths.MobileHighwayShoulder, m_ses);
            }
            public MobileHighwayShoulderSettings DeSerialize(MobileHighwayShoulderSettings m_ses)
            {
                CheckSerializationFile();
                return Serialization.SerializeClass.DeSerialize(SerializationPaths.MobileHighwayShoulder, m_ses);
            }

            public void CheckSerializationFile()
            {
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(SerializationPaths.MobileHighwayShoulder)))
                        Directory.CreateDirectory(Path.GetDirectoryName(SerializationPaths.MobileHighwayShoulder));

                    //xmlserilization dosyasını kontrol ediyoruz
                    if (!File.Exists(SerializationPaths.MobileHighwayShoulder))
                    {
                        MobileHighwayShoulderSettings.Singleton().m_imagePath = "";
                        MobileHighwayShoulderSettings.Singleton().m_violationImagesPath = "";
                        MobileHighwayShoulderSettings.Singleton().m_thumbNailImagesPath = "";
                        MobileHighwayShoulderSettings.Singleton().m_deleteImages = false;
                        MobileHighwayShoulderSettings.Singleton().m_protectViolationTime = 2;

                        MobileHighwayShoulderSettings.Singleton().Serilize(MobileHighwayShoulderSettings.Singleton());
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

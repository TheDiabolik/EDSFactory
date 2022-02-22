using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EDSFactory 
{
    
    public partial class Settings
    {
        public class FixedHighwayShoulderSettings : SpeedViolation
        {
            public int m_scanImageTime { get; set; }
            public bool m_videoMode { get; set; }
         
            private static FixedHighwayShoulderSettings m_fhss = new FixedHighwayShoulderSettings();

            public static FixedHighwayShoulderSettings Singleton()
            {
                return m_fhss;
            }

            public FixedHighwayShoulderSettings()
            {
            }
            public void Serilize(FixedHighwayShoulderSettings eaevs)
            {
                Serialization.SerializeClass.Serialize(SerializationPaths.FixedHighwayShoulder, eaevs);
            }
            public FixedHighwayShoulderSettings DeSerialize(FixedHighwayShoulderSettings eaevs)
            {
                CheckSerializationFile();
                return Serialization.SerializeClass.DeSerialize(SerializationPaths.FixedHighwayShoulder, eaevs);
            }

            public void CheckSerializationFile()
            {
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(SerializationPaths.FixedHighwayShoulder)))
                        Directory.CreateDirectory(Path.GetDirectoryName(SerializationPaths.FixedHighwayShoulder));

                    //xmlserilization dosyasını kontrol ediyoruz
                    if (!File.Exists(SerializationPaths.FixedHighwayShoulder))
                    {
                        FixedHighwayShoulderSettings.Singleton().m_imagePath = "";
                        FixedHighwayShoulderSettings.Singleton().m_violationImagesPath = "";
                        FixedHighwayShoulderSettings.Singleton().m_thumbNailImagesPath = "";
                        FixedHighwayShoulderSettings.Singleton().m_deleteImages = false;
                        FixedHighwayShoulderSettings.Singleton().m_protectViolationTime = 2;

                        FixedHighwayShoulderSettings.Singleton().Serilize(FixedHighwayShoulderSettings.Singleton());
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

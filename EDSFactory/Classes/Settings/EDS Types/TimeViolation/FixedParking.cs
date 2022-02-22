using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EDSFactory 
{
    
    public partial class Settings
    {
        public class FixedParkingSettings : TimeViolation
        {
            public int m_scanImageTime { get; set; }
            public bool m_videoMode { get; set; }


            private static FixedParkingSettings m_fps = new FixedParkingSettings();
            public static FixedParkingSettings Singleton()
            {
                return m_fps;
            }

            public FixedParkingSettings()
            {
            }

            public   void Serilize(FixedParkingSettings eaevs)
            {
                Serialization.SerializeClass.Serialize(SerializationPaths.FixedParking, eaevs);
            }
            public   FixedParkingSettings DeSerialize(FixedParkingSettings eaevs)
            {
                CheckSerializationFile();
                return Serialization.SerializeClass.DeSerialize(SerializationPaths.FixedParking, eaevs);
            }


            public void CheckSerializationFile()
            {
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(SerializationPaths.FixedParking)))
                        Directory.CreateDirectory(Path.GetDirectoryName(SerializationPaths.FixedParking));

                    //xmlserilization dosyasını kontrol ediyoruz
                    if (!File.Exists(SerializationPaths.FixedParking))
                    {
                        FixedParkingSettings.Singleton().m_imagePath = "";
                        FixedParkingSettings.Singleton().m_violationImagesPath = "";
                        FixedParkingSettings.Singleton().m_thumbNailImagesPath = "";
                        FixedParkingSettings.Singleton().m_deleteImages = false;
                        FixedParkingSettings.Singleton().m_protectViolationTime = 2;

                        FixedParkingSettings.Singleton().Serilize(FixedParkingSettings.Singleton());
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EDSFactory 
{
    
    public partial class Settings
    {
        public class MobileParkingSettings : TimeViolation
        {
            public int m_scanImageTime { get; set; }

            private static MobileParkingSettings m_mps = new MobileParkingSettings();
            public static MobileParkingSettings Singleton()
            {
                return m_mps;
            }


            public MobileParkingSettings()
            {
            }
            public   void Serilize(MobileParkingSettings eaevs)
            {
                Serialization.SerializeClass.Serialize(SerializationPaths.MobileParking, eaevs);
            }

            public   MobileParkingSettings DeSerialize(MobileParkingSettings eaevs)
            {
                CheckSerializationFile();
                return Serialization.SerializeClass.DeSerialize(SerializationPaths.MobileParking, eaevs);
            }

            public void CheckSerializationFile()
            {
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(SerializationPaths.MobileParking)))
                        Directory.CreateDirectory(Path.GetDirectoryName(SerializationPaths.MobileParking));

                    //xmlserilization dosyasını kontrol ediyoruz
                    if (!File.Exists(SerializationPaths.MobileParking))
                    {
                        MobileParkingSettings.Singleton().m_imagePath = "";
                        MobileParkingSettings.Singleton().m_violationImagesPath = "";
                        MobileParkingSettings.Singleton().m_thumbNailImagesPath = "";
                        MobileParkingSettings.Singleton().m_deleteImages = false;
                        MobileParkingSettings.Singleton().m_protectViolationTime = 2;

                        MobileParkingSettings.Singleton().Serilize(MobileParkingSettings.Singleton());
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

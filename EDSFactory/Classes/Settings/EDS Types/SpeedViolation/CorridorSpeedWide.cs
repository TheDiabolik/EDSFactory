using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EDSFactory 
{
   
    public partial class Settings
    {
        public class CorridorSpeedWideSettings : SpeedViolation
        {
            public string m_entryTagPath { get; set; }
            public string m_entryTagIP { get; set; }
            public string m_entryTagPort { get; set; }
            public bool m_workingType { get; set; }
            public string m_entryTagListenPort { get; set; }
            public bool m_showLaneInWideImage { get; set; }
             

            private static CorridorSpeedWideSettings m_fhss = new CorridorSpeedWideSettings();

            public static CorridorSpeedWideSettings Singleton()
            {
                return m_fhss;
            }

            public CorridorSpeedWideSettings()
            {
            }
            public   void Serilize(CorridorSpeedWideSettings eaevs)
            {
                Serialization.SerializeClass.Serialize(SerializationPaths.SpeedCorridorWide, eaevs);
            }
            public   CorridorSpeedWideSettings DeSerialize(CorridorSpeedWideSettings eaevs)
            {
                CheckSerializationFile();
                return Serialization.SerializeClass.DeSerialize(SerializationPaths.SpeedCorridorWide, eaevs);
            }


            public void CheckSerializationFile()
            {
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(SerializationPaths.SpeedCorridorWide)))
                        Directory.CreateDirectory(Path.GetDirectoryName(SerializationPaths.SpeedCorridorWide));

                    //xmlserilization dosyasını kontrol ediyoruz
                    if (!File.Exists(SerializationPaths.SpeedCorridorWide))
                    {
                        CorridorSpeedWideSettings.Singleton().m_imagePath = "";
                        CorridorSpeedWideSettings.Singleton().m_violationImagesPath = "";
                        CorridorSpeedWideSettings.Singleton().m_thumbNailImagesPath = "";
                        CorridorSpeedWideSettings.Singleton().m_deleteImages = false;
                        CorridorSpeedWideSettings.Singleton().m_protectViolationTime = 2;

                        CorridorSpeedWideSettings.Singleton().Serilize(CorridorSpeedWideSettings.Singleton());
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

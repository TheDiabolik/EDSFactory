using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    public partial class Settings
    {
        public class CorridorSpeedSettings : SpeedViolation
        {
            public string m_entryTagPath { get; set; }
            public string m_entryTagIP { get; set; }
            public string m_entryTagPort { get; set; }
            public string m_entryTagListenPort { get; set; }
            public bool m_workingType { get; set; }  


            private static CorridorSpeedSettings m_fhss = new CorridorSpeedSettings();

            public static CorridorSpeedSettings Singleton()
            {
                return m_fhss;
            }

            public CorridorSpeedSettings()
            {
                
            }
            public void Serilize(CorridorSpeedSettings eaevs)
            {
                Serialization.SerializeClass.Serialize(SerializationPaths.SpeedCorridor, eaevs);
            }
            public CorridorSpeedSettings DeSerialize(CorridorSpeedSettings eaevs)
            {
                CheckSerializationFile();
                return Serialization.SerializeClass.DeSerialize(SerializationPaths.SpeedCorridor, eaevs);
            } 

            public void CheckSerializationFile( )
            {
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(SerializationPaths.SpeedCorridor)))
                        Directory.CreateDirectory(Path.GetDirectoryName(SerializationPaths.SpeedCorridor));

                    //xmlserilization dosyasını kontrol ediyoruz
                    if (!File.Exists(SerializationPaths.SpeedCorridor))
                    {  
                         CorridorSpeedSettings.Singleton().m_imagePath = "";
                         CorridorSpeedSettings.Singleton().m_violationImagesPath = "";
                         CorridorSpeedSettings.Singleton().m_thumbNailImagesPath = "";
                         CorridorSpeedSettings.Singleton().m_deleteImages = false; 
                         CorridorSpeedSettings.Singleton().m_protectViolationTime = 2;

                          CorridorSpeedSettings.Singleton().Serilize(CorridorSpeedSettings.Singleton());
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

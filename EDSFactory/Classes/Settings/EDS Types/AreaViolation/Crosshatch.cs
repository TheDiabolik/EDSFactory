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
        [Serializable]
        public class CrosshatchSettings : AreaViolation
        {
            public int m_scanImageTime { get; set; }

            private static CrosshatchSettings mhss = new CrosshatchSettings();

            public static CrosshatchSettings Singleton()
            {
                return mhss;
            }

            public CrosshatchSettings()
            {
            }

            public void Serilize(CrosshatchSettings m_ses)
            {
                Serialization.SerializeClass.Serialize(SerializationPaths.Crosshatch, m_ses);
            }

            public CrosshatchSettings DeSerialize(CrosshatchSettings m_ses)
            {
                CheckSerializationFile();
                return Serialization.SerializeClass.DeSerialize(SerializationPaths.Crosshatch, m_ses);
            }

            public void CheckSerializationFile()
            {
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(SerializationPaths.Crosshatch)))
                        Directory.CreateDirectory(Path.GetDirectoryName(SerializationPaths.Crosshatch));

                    //xmlserilization dosyasını kontrol ediyoruz
                    if (!File.Exists(SerializationPaths.Crosshatch))
                    {
                        CrosshatchSettings.Singleton().m_imagePath = "";
                        CrosshatchSettings.Singleton().m_violationImagesPath = "";
                        CrosshatchSettings.Singleton().m_thumbNailImagesPath = "";
                        CrosshatchSettings.Singleton().m_deleteImages = false;
                        CrosshatchSettings.Singleton().m_protectViolationTime = 2;

                        CrosshatchSettings.Singleton().Serilize(CrosshatchSettings.Singleton());
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

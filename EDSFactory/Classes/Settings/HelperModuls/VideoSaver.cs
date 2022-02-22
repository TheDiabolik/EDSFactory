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
        public class Video
        {
            //private List<CameraSettings> m_cameras = new List<CameraSettings>();  


            private SerializableDictionary<int, CameraSettings> m_cameras  ;  

            public string m_imagePath { get; set; }

            public bool m_firstCamera { get; set; }
            public bool m_secondCamera { get; set; }
            public bool m_thirdCamera { get; set; }
            public bool m_fourthCamera { get; set; }

            public bool m_autoStart { get; set; }


            public string m_firstCameraType { get; set; }
            public string m_secondCameraType { get; set; }
            public string m_thirdCameraType { get; set; }
            public string m_fourthCameraType { get; set; }



            public SerializableDictionary<int, CameraSettings> Cameras
            {
                get { return m_cameras; }
                set { m_cameras = value; }
            }


             private static Video m_fhss = new Video();

            public static Video Singleton()
            {
                return m_fhss;
            }

            public Video()
            {
                m_cameras = new SerializableDictionary<int, CameraSettings>(); 
            }

            public void Serilize(Video eaevs)
            {
                Serialization.SerializeClass.Serialize(SerializationPaths.Video, eaevs);
            }
            public Video DeSerialize(Video eaevs)
            {
                CheckSerializationFile();
                return Serialization.SerializeClass.DeSerialize(SerializationPaths.Video, eaevs);
            }

            public void CheckSerializationFile()
            {
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(SerializationPaths.Video)))
                        Directory.CreateDirectory(Path.GetDirectoryName(SerializationPaths.Video));

                    //xmlserilization dosyasını kontrol ediyoruz
                    if (!File.Exists(SerializationPaths.Video))
                    {
                        //Video.Singleton().Cameras = new List<Camera>();
                             
                        //CorridorSpeedSettings.Singleton().m_imagePath = "";
                        //CorridorSpeedSettings.Singleton().m_violationImagesPath = "";
                        //CorridorSpeedSettings.Singleton().m_thumbNailImagesPath = "";
                        //CorridorSpeedSettings.Singleton().m_deleteImages = false;
                        //CorridorSpeedSettings.Singleton().m_protectViolationTime = 2;

                        Video.Singleton().Serilize(Video.Singleton());
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

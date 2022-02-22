using AForge.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    public abstract class CameraBuilder
    {
        protected Camera m_camera;

        public void CameraCreate()
        {
            m_camera = new Camera();
        }

        public Camera GetCamera()
        {
            return m_camera;
        }


        public abstract void CreateCameraStream();

        public abstract void CreateCameraSource(string entryCamCameraURL);

        public abstract void CreateCameraUserName(string entryCamUserName);

        public abstract void CreateCameraPassword(string entryCamPassword);

        public abstract void CreateBeforeAndAfterTime(int beforeAndAfterSecond);

        public abstract void CreateBuffer();


        //public abstract void StartCamera(string cameraURL, string userName, string password, int beforeAndAfterSecond, int cameraFPS);

        //public abstract void Trigged(string videoName);

      

        //public abstract void StopCamera();
    }
}

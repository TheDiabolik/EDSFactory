using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    public class CameraCreator
    {
        private CameraBuilder m_cameraBuilder;
     

        public void SetCameraBuilder(CameraBuilder cameraBuilder)
        {
            this.m_cameraBuilder = cameraBuilder;
        }

        public Camera GetCamera()
        { 

            return m_cameraBuilder.GetCamera();
        }

        public void CreateCamera(string cameraURL, string userName, string password, int beforeAndAfterSecond)
        {
            m_cameraBuilder.CameraCreate();
            m_cameraBuilder.CreateCameraStream();
            m_cameraBuilder.CreateCameraSource(cameraURL);
            m_cameraBuilder.CreateCameraUserName(userName);
            m_cameraBuilder.CreateCameraPassword(password);
            m_cameraBuilder.CreateBeforeAndAfterTime(beforeAndAfterSecond);
            m_cameraBuilder.CreateBuffer(); 
        }

    }
}

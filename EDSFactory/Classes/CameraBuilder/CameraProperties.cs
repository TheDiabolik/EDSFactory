using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    public class CameraSettings : ICamera
    {
        public int m_streamType { get; set; }
        public int m_beforeAndAfterSecond { get; set; }
        public string m_cameraURL { get; set; }
        public string m_userName { get; set; }
        public string m_password { get; set; } 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    public interface ICamera
    {
       int m_beforeAndAfterSecond { get; set; }
       string m_cameraURL { get; set; }
       string m_userName { get; set; }
       string m_password { get; set; }
    }
}

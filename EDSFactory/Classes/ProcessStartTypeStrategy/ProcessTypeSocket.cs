using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    public class ProcessTypeSocket : IProcessStartType
    {
        public void Start()
        { 
            SocketCommunication.Singleton().Start(); 
        }

        public void Stop()
        {
            SocketCommunication.Singleton().Stop();  
        }
    }
}

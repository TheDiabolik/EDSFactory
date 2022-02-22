using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    public class ProcessTypeTimer : IProcessStartType
    {
        public void Start()
        {
            Mediator.Singleton().m_timer.Start(); 
        }

        public void Stop()
        {
            Mediator.Singleton().m_timer.Stop();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    public class ProcessTypeStarter
    {
        IProcessStartType processType;

        public ProcessTypeStarter( IProcessStartType processType)
        {
            this.processType = processType;
        }

        public void Start()
        {
            processType.Start();
        }

        public void Stop()
        {
            processType.Stop();
        }
    }
}

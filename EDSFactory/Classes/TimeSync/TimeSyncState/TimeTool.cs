using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    class TimeTool
    {
        private ITimeSyncStatus timeSyncStatus; 

        public TimeTool()
        {
            timeSyncStatus = new NTPTimeTool();
         
        }

        public void TimeSyncToolChange(ITimeSyncStatus timeSyncStatus)
        {
            this.timeSyncStatus = timeSyncStatus;
        }

        public void Start()
        {
            timeSyncStatus.Start();
        }

        public void Stop()
        {
            timeSyncStatus.Stop();
        }

    }
}

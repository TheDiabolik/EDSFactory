using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    interface ITimeSyncStatus
    {
        void Start();
        void Stop();
        void SyncToolDate(DateTime SyncToolDate);
        void SetSystemTime();
        void SystemTimeUpdated(DateTime SystemUpdatedTime);
        void SyncStatus(bool status);
        //void SyncStopped();
    }
}

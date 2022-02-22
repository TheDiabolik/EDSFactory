using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    interface ITimeSyncToolWatcher
    {
        void SyncToolDate(DateTime SyncToolDate);
        void SetSystemTime();
        void SystemTimeUpdated(DateTime SystemUpdatedTime);
        void SyncStatus(bool status);
    }
}

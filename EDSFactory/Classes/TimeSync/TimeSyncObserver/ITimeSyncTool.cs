using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    interface ITimeSyncTool
    {
        void InformSyncToolDate();
        void InformSetSystemTime();
        void InformSystemTimeUpdated();
        void InformSyncStatus();
      

        void AddWatcher(ITimeSyncToolWatcher syncToolWatcher);
    }
}

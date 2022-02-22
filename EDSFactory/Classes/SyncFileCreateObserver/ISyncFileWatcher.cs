using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    public interface ISyncFileWatcher
    {
        void SyncFileCreated(string syncFileName);
    }
}

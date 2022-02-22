using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    public  interface ISyncFileCreate
    {
        void InformWatcher();

        void AddWatcher(ISyncFileWatcher watcher);
    }
}

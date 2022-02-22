using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory
{
    public interface IDataSourceWatcher
    { 
        void InformWatcher();

        void AddWatcher(IDataSourceChanged watcher);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    interface IWorkingInformer
    {
        void InformWatcher();

        void AddWatcher(IWorkingWatcher workingWatcher);
    }
}

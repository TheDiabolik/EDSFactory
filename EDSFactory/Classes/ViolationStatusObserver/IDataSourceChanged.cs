using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    public interface IDataSourceChanged
    {
        void SyncFileCreated(List<string> values);
    }
}

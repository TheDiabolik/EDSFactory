using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    public interface IDatabase
    {
        int Insert(List<string> values);
        int Delete();
    }
}

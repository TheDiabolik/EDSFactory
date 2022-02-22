using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    class TriggeredViolation
    { 
        public ITriggeredViolation Type(string EDSType)
        {
            if (EDSType == "CorridorSpeedWide")
                return CorridorSpeedWideViolation.Singleton(); 
            else if (EDSType == "CorridorSpeed")
                return CorridorSpeedViolation.Singleton(); 
            else 
                return null;
        }
    }
}

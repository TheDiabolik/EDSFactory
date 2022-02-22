using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    class TimeViolation
    {
        private static TimeViolation m_do; 

        public static TimeViolation Singleton()
        {
            if (m_do == null)
                m_do = new TimeViolation();

            return m_do;
        } 

        public IScanForViolation Type(string EDSType)
        {
            if (EDSType == "FixedParking")
                return FixedParkingViolation.Singleton(); 
            else if (EDSType == "MobileParking")
                return MobileParkingViolation.Singleton();
            else if (EDSType == "FixedHighwayShoulder")
                return FixedHighwayShoulderViolation.Singleton();
            else if (EDSType == "MobileHighwayShoulder")
                return MobileHighwayShoulderViolation.Singleton();
            else if (EDSType == "Standing")
                return StandingViolation.Singleton();
            else if (EDSType == "Crosshatch")
                return CrosshatchViolation.Singleton();
            else
                return null;
        }

     
        
    }
}

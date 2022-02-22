using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    public class Enums
    {
        public enum ControlScanningType
        {
            Load, Click
        }

        public enum WorkPlan
        {
            In, Out
        }

        public enum Validate
        {
            Valid, InValid
        }

        public enum TypeAndRange
        {
            Wanted, NotWanted
        }

        public enum Date
        {
            Add, Subtract
        }

        public enum Violation
        {
            Violation, NotViolation
        }

        public enum FindDate
        {
            Enterance, Exit, EntranceAndExit
        }

        public enum IsAlwaysWorkingViolation
        {
            Yes, No, Nan
        }

        public enum IsWorking
        {
            Yes, No
        }
    }
}

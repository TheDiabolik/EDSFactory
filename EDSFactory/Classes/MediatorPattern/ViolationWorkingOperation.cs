using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    public abstract class ViolationWorkingOperation
    {
        protected Mediator mediator;

        public ViolationWorkingOperation(Mediator mediator)
        {
            this.mediator = mediator;
        }

        public abstract void CheckViolationForAlwaysWorking();
        public abstract void StartWorking();
        public abstract void StopWorking();
    }
}

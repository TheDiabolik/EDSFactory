using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    public class WorkingInformer : IWorkingInformer
    {
        public List<IWorkingWatcher> workingInformer = new List<IWorkingWatcher>();
        public bool workingStatus = false;
        public string EDSType;


        public void InformWatcher()
        {
            foreach (var item in workingInformer)
            {
                item.WorkingStatus(workingStatus, EDSType);
            }
        }

        public void AddWatcher(IWorkingWatcher watcher)
        {
            if(!workingInformer.Contains(watcher))
                workingInformer.Add(watcher);
        }

        public void WorkingStatusChanged(bool workingStatus, string EDSType)
        {
            this.workingStatus = workingStatus;
            this.EDSType = EDSType;

            InformWatcher();

        }
    }



}

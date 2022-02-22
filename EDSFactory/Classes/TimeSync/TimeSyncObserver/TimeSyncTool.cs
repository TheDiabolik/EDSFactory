using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    class TimeSyncTool : ITimeSyncTool
    {
        private DateTime m_syncToolTime;
        private DateTime m_systemUpdatedTime;
        private bool m_status;

        public List<ITimeSyncToolWatcher> syncToolInformer = new List<ITimeSyncToolWatcher>(); 

        public void InformSyncToolDate()
        {
            foreach (var item in syncToolInformer)
            {
                item.SyncToolDate(m_syncToolTime);
            }
        }

        public void InformSetSystemTime()
        {
            foreach (var item in syncToolInformer)
            {
                item.SetSystemTime();
            }
        }

        public void InformSystemTimeUpdated()
        {
            foreach (var item in syncToolInformer)
            {
                item.SystemTimeUpdated(m_systemUpdatedTime);
            }
        }

        public void InformSyncStatus()
        {
            foreach (var item in syncToolInformer)
            {
                item.SyncStatus(m_status);
            }
        }

    

        public void AddWatcher(ITimeSyncToolWatcher watcher)
        {
            if(!syncToolInformer.Contains(watcher)) 
                syncToolInformer.Add(watcher);
        }

        public void SyncToolDate(DateTime dt)
        {
            this.m_syncToolTime = dt;

            InformSyncToolDate();
        }


        public void SetSystemTime()
        {

        }


        public void SystemTimeUpdated(DateTime dt)
        {

            this.m_systemUpdatedTime = dt;

            InformSystemTimeUpdated();
        }

        public void SyncStatus(bool status)
        {
            this.m_status = status;

            InformSyncStatus();
        }

       

    }
}

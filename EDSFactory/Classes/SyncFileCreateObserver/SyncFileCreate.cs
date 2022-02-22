using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    public class SyncFileCreate : ISyncFileCreate
    {
        private List<ISyncFileWatcher> m_syncFileWatcher = new List<ISyncFileWatcher>();
        private string m_fileCreatedName;

        public void InformWatcher()
        {
            foreach (ISyncFileWatcher watcher in m_syncFileWatcher)
            {
                watcher.SyncFileCreated(m_fileCreatedName);
            }
        }

        public void AddWatcher(ISyncFileWatcher watcher)
        {
            m_syncFileWatcher.Add(watcher); 
        }

        public void SyncFileCreated(string fileCreatedName)
        {
            this.m_fileCreatedName = fileCreatedName;
            InformWatcher(); 
        }
    }
}

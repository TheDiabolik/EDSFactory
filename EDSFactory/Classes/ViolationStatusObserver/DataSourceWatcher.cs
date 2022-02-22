using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    public class DataSourceWatcher : IDataSourceWatcher
    {
        private List<IDataSourceChanged> m_dataSourceWatcher = new List<IDataSourceChanged>();
        private List<string> m_values;

        public void InformWatcher()
        {
            foreach (IDataSourceChanged watcher in m_dataSourceWatcher)
            {
                watcher.SyncFileCreated(m_values);
            }
        }

        public void AddWatcher(IDataSourceChanged watcher)
        {
            m_dataSourceWatcher.Add(watcher);
        }

        public void SyncFileCreated(List<string> values)
        {
            this.m_values = values;
            InformWatcher();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory 
{
    public partial class Settings
    {
        public abstract class CommanSettings
        {
            public string m_imagePath { get; set; }
            public string m_violationImagesPath { get; set; }
            public string m_thumbNailImagesPath { get; set; }
            public bool m_deleteImages { get; set; }
            public int m_protectViolationTime { get; set; }

        
        }

        public abstract class TimeViolation : CommanSettings
        {
            public int m_minViolationTimeMinute { get; set; }
            public int m_minViolationTimeHour { get; set; }
            public int m_minViolationTimeSecond { get; set; }
            public int m_maxViolationTimeSecond { get; set; }
            public int m_maxViolationTimeMinute { get; set; }
            public int m_maxViolationTimeHour { get; set; }
        }

        public  abstract class SpeedViolation : CommanSettings
        {
            public int m_distance { get; set; }
            public int m_speed { get; set; }
            public bool m_applyTolerance { get; set; }
            public int m_tolerancePercentage { get; set; }

            //public abstract void Serilize(dynamic m_ses);
            //public abstract dynamic DeSerialize(dynamic m_ses);
        }

        public abstract class AreaViolation : CommanSettings
        {

         

            //internal Serialization.SingleEntrySettingsSerialization m_sess = new Serialization.SingleEntrySettingsSerialization();
        }
    }
}

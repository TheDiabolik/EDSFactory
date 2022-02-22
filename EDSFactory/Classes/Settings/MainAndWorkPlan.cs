using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EDSFactory 
{
   
    public partial class Settings
    {
        [Serializable]
        public class WorkPlanAndMainSettings  
        {
            private static WorkPlanAndMainSettings m_wpams;
            //private static Serialization.WorkPlanAndMainSettingsSerialization m_wpamss;

            public static WorkPlanAndMainSettings Singleton()
            {
                if (m_wpams == null)
                    m_wpams = new WorkPlanAndMainSettings();

                return m_wpams;
            }

            public WorkPlanAndMainSettings()
            {
                //m_wpamss = Serialization.WorkPlanAndMainSettingsSerialization.Singleton();
            }

            private List<string> m_fixedParkingWorkingPlan = new List<string>();
            private List<string> m_mobileParkingWorkingPlan = new List<string>();
            private List<string> m_fixedHighwayShoulderWorkingPlan = new List<string>();
            private List<string> m_mobileHighwayShoulderWorkingPlan = new List<string>();
            private List<string> m_noVehiclesWorkingPlan = new List<string>();
            private List<string> m_standingWorkingPlan = new List<string>();
            private List<string> m_crosshatchWorkingPlan = new List<string>();


            private List<string> m_corridorSpeedWorkingPlan = new List<string>();
            private List<string> m_corridorSpeedWideWorkingPlan = new List<string>();


            private List<string> m_wrongWayWorkingPlan = new List<string>();

            private HashSet<int> m_startAutoHelperModulsIndex = new HashSet<int>();
            public bool m_deleteLog { get; set; }
            public int m_deleteLogPeriod { get; set; }
            public bool m_startAuto { get; set; }
            public string m_startAutoProgramName { get; set; }
            public bool m_alwaysSearching { get; set; }
            public string m_alwaysSearchingProgramName { get; set; }

            public List<string> WrongWayWorkingPlan
            {
                get { return m_wrongWayWorkingPlan; }
                set { m_wrongWayWorkingPlan = value; }
            }

            public List<string> SpeedCorridorWorkingPlan
            {
                get { return m_corridorSpeedWorkingPlan; }
                set { m_corridorSpeedWorkingPlan = value; }
            }

            public List<string> SpeedCorridorWideWorkingPlan
            {
                get { return m_corridorSpeedWideWorkingPlan; }
                set { m_corridorSpeedWideWorkingPlan = value; }
            }

            public HashSet<int> StartAutoHelperModulsIndex
            {
                get { return m_startAutoHelperModulsIndex; }
                set { m_startAutoHelperModulsIndex = value; }
            }

            public List<string> StandingWorkingPlan
            {
                get { return m_standingWorkingPlan; }
                set { m_standingWorkingPlan = value; }
            }


            public List<string> CrosshatchWorkingPlan
            {
                get { return m_crosshatchWorkingPlan; }
                set { m_crosshatchWorkingPlan = value; }
            }

            public List<string> NoVehiclesWorkingPlan
            {
                get { return m_noVehiclesWorkingPlan; }
                set { m_noVehiclesWorkingPlan = value; }
            }

            public List<string> FixedHighwayShoulderWorkingPlan
            {
                get { return m_fixedHighwayShoulderWorkingPlan; }
                set { m_fixedHighwayShoulderWorkingPlan = value; }
            }

            public List<string> MobileHighwayShoulderWorkingPlan
            {
                get { return m_mobileHighwayShoulderWorkingPlan; }
                set { m_mobileHighwayShoulderWorkingPlan = value; }
            }

            public List<string> FixedParkingWorkingPlan
            {
                get { return m_fixedParkingWorkingPlan; }
                set { m_fixedParkingWorkingPlan = value; }
            }

            public List<string> MobileParkingWorkingPlan
            {
                get { return m_mobileParkingWorkingPlan; }
                set { m_mobileParkingWorkingPlan = value; }
            }

            public void Serialize(Settings.WorkPlanAndMainSettings wpams)
            {
                Serialization.SerializeClass.Serialize(SerializationPaths.Settings, wpams);
            }
            public Settings.WorkPlanAndMainSettings DeSerialize(Settings.WorkPlanAndMainSettings wpams)
            {
                CheckSerializationFile();
                return Serialization.SerializeClass.DeSerialize(SerializationPaths.Settings, wpams);
            }


            public void CheckSerializationFile()
            {
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(SerializationPaths.Settings)))
                        Directory.CreateDirectory(Path.GetDirectoryName(SerializationPaths.Settings));

                    //xmlserilization dosyasını kontrol ediyoruz
                    if (!File.Exists(SerializationPaths.Settings))
                    {
                        //WorkPlanAndMainSettings.Singleton().m_imagePath = "";
                        //WorkPlanAndMainSettings.Singleton().m_violationImagesPath = "";
                        //WorkPlanAndMainSettings.Singleton().m_thumbNailImagesPath = "";
                        //WorkPlanAndMainSettings.Singleton().m_deleteImages = false;
                        //WorkPlanAndMainSettings.Singleton().m_protectViolationTime = 2;

                        //WorkPlanAndMainSettings.Singleton().Serialize(WorkPlanAndMainSettings.Singleton());
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.CheckSerilizationFileExceptionMessage, ex);
                }
            } 
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace EDSFactory 
{
    public class Mediator
    {
        private CorridorSpeedWorkingOperation m_corridor;
        private CorridorSpeedWideWorkingOperation m_corridorWide;
        private FixedParkingWorkingOperation m_fixedParking;
        private MobileParkingWorkingOperation m_mobileParking;
        private MobileHighwayShoulderWorkingOperation m_mobileHighwayShoulder;
        private FixedHighwayShoulderWorkingOperation m_fixedHighwayShoulder;
        private StandingWorkingOperation m_standing;
        private CrosshatchWorkingOperation m_crosshatch; 

        public ProcessTypeStarter processStartType;
         
        //public StreamChooser formatSeçici;
     
        public string m_workingProgramName { get; set; } 

        private static Mediator m_do;
         
        public System.Timers.Timer m_timer;
        public System.Timers.Timer m_alwaysSearchingTimer;
        internal AbortableBackgroundWorker bw;


        public Mediator()
        {
            m_timer = new System.Timers.Timer();
            m_alwaysSearchingTimer = new System.Timers.Timer();
            bw = new AbortableBackgroundWorker();

            m_timer.Elapsed += m_timer_Elapsed;
            m_alwaysSearchingTimer.Elapsed += m_alwaysSearchingTimer_Elapsed;
            bw.DoWork += bw_DoWork;

            m_alwaysSearchingTimer.Interval = 10000;
        }


        public static Mediator Singleton()
        {
            if (m_do == null)
                m_do = new Mediator();

            return m_do;
        }

        public void AddCrosshatch(CrosshatchWorkingOperation crosshatch)
        {
            this.m_crosshatch = crosshatch;
        }

        public void AddCorridorSpeed(CorridorSpeedWorkingOperation corridor)
        {
            this.m_corridor = corridor;
        }

        public void AddCorridorSpeedWide(CorridorSpeedWideWorkingOperation corridorWide)
        {
            this.m_corridorWide = corridorWide;
        }

        public void AddFixedParking(FixedParkingWorkingOperation fixedParking)
        {
            this.m_fixedParking = fixedParking; 
        }

        public void AddMobileParking(MobileParkingWorkingOperation mobileParking)
        {
            this.m_mobileParking = mobileParking;

        }

        public void AddMobileHighwayShoulder(MobileHighwayShoulderWorkingOperation mobileHighwayShoulder)
        {
            this.m_mobileHighwayShoulder = mobileHighwayShoulder;

        }

        public void AddFixedHighwayShoulder(FixedHighwayShoulderWorkingOperation fixedHighwayShoulder)
        {
            this.m_fixedHighwayShoulder = fixedHighwayShoulder;

        } 

        public void AddStanding(StandingWorkingOperation standing)
        {
            this.m_standing = standing;
        }


        public Enums.IsAlwaysWorkingViolation IsAlwaysWorkingViolationType(string EDSType)
        {
            string alwaysScanProgramName = GetAlwaysSearchingProgramName();

            if (!string.IsNullOrEmpty(alwaysScanProgramName) && (EDSType == alwaysScanProgramName))
                return Enums.IsAlwaysWorkingViolation.Yes;
            else if (string.IsNullOrEmpty(alwaysScanProgramName))
                return Enums.IsAlwaysWorkingViolation.Nan;
            else
            {
             
                return Enums.IsAlwaysWorkingViolation.No;
            } 
        } 


        #region timer control metot 
        public void StartProcess()
        {
            if (!string.IsNullOrEmpty(m_workingProgramName))
            {
                MainForm.m_workingProcessInformer.WorkingStatusChanged(true, m_workingProgramName);
                processStartType.Start();
            }
        }

        public void StopProcess()
        {
            if (!string.IsNullOrEmpty(m_workingProgramName))
            {
                MainForm.m_workingProcessInformer.WorkingStatusChanged(false, m_workingProgramName);
                processStartType.Stop();
            }
        }
        #endregion


        internal void StartAlwaysScanProgram()
        {
            if ((!string.IsNullOrEmpty(m_workingProgramName)))
            { 
                m_alwaysSearchingTimer.Start();
            }
        }

        public string GetAlwaysSearchingProgramName()
        {
            Settings.WorkPlanAndMainSettings workPlan = Settings.WorkPlanAndMainSettings.Singleton();
            workPlan = workPlan.DeSerialize(workPlan);

            string alwaysScan = "";

            if (workPlan.m_alwaysSearching)
            {
                switch (workPlan.m_alwaysSearchingProgramName)
                {
                    case "Emniyet Şeridi EDS - Sabit":
                        {
                            alwaysScan = "FixedHighwayShoulder";
                            break;
                        }

                    case "Emniyet Şeridi EDS - Mobil":
                        {
                            alwaysScan = "MobileHighwayShoulder";
                            break;
                        }

                    case "Park EDS - Sabit":
                        {
                            alwaysScan = "FixedParking";
                            break;
                        }

                    case "Park EDS - Mobil":
                        {
                            alwaysScan = "MobileParking";
                            break;
                        }

                    case "Duraklama EDS":
                        {
                            alwaysScan = "Standing";
                            break;
                        }

                    case "Taşıt Giremez EDS":
                        {
                            alwaysScan = "NoVehicles";
                            break;
                        }

                    case "Hız Koridor EDS - Dar":
                        {
                            alwaysScan = "CorridorSpeed";
                            break;
                        }
                    case "Hız Koridor EDS - Geniş":
                        {
                            alwaysScan = "CorridorSpeedWide";
                            break;
                        }
                    case "Ters Yön EDS":
                        {
                            alwaysScan = "WrongWay";
                            break;
                        }
                    case "Ofset Tarama EDS":
                        {
                            alwaysScan = "Crosshatch";
                            break;
                        }
                }
            }
            return alwaysScan;

        }

        internal void m_timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (!bw.IsBusy)
                    bw.RunWorkerAsync(m_workingProgramName);
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionMessages.UserMessageExceptionMessage, ex);
            }
        } 

        internal void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            string workingProgramName = e.Argument.ToString();

            TimeViolation timeViolation = TimeViolation.Singleton();
            IScanForViolation scanViolation = timeViolation.Type(workingProgramName);

            scanViolation.Violation();
        }

        internal void m_alwaysSearchingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                 m_alwaysSearchingTimer.Stop();

                StartProcess();
            }
            catch (Exception)
            {
                //throw new Exception(ExceptionMessages.UserMessageExceptionMessage, ex);
            }
        }

        public Enums.IsWorking TimerSwitch()
        {
            if (!m_timer.Enabled)
            {
                return Enums.IsWorking.Yes;
            }
            else
            {
                return Enums.IsWorking.No;
            }
        }
         

        internal void StopScan()
        {
            //if ((MainForm.m_timer.Enabled) || (MainForm.m_alwaysSearchingTimer.Enabled))
            //{
                StopProcess();
                    m_alwaysSearchingTimer.Stop();
            //}

        }

    }
}

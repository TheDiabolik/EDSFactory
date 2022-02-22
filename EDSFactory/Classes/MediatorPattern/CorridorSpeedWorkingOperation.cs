using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDSFactory 
{
    public class CorridorSpeedWorkingOperation : ViolationWorkingOperation
    {
        TriggeredViolation triggered;

        public CorridorSpeedWorkingOperation(Mediator mediator)
            : base(mediator)
        {
            mediator.AddCorridorSpeed(this);
            mediator.processStartType = new ProcessTypeStarter(new ProcessTypeSocket());
            triggered = new TriggeredViolation();
        }
        public override void CheckViolationForAlwaysWorking()
        {
            Enums.IsAlwaysWorkingViolation alwaysWorking = mediator.IsAlwaysWorkingViolationType(EDSType.CorridorSpeed);

            if (alwaysWorking == Enums.IsAlwaysWorkingViolation.Yes)
            {
                PrepareProperties();

                mediator.m_workingProgramName = EDSType.CorridorSpeed;

                mediator.m_alwaysSearchingTimer.Start();
            }

        }
        public override void StartWorking( )
        {
            Enums.IsAlwaysWorkingViolation alwaysWorking = mediator.IsAlwaysWorkingViolationType(EDSType.CorridorSpeed);

            if (alwaysWorking == Enums.IsAlwaysWorkingViolation.No)
            {
                MessageBox.Show("Sürekli Tarama Modu Aktif Olan EDS Tipinden Başka EDS Tipinde Tarama Yapamazsınız!");
                return;
            }
            else if (alwaysWorking == Enums.IsAlwaysWorkingViolation.Yes)
            {
                PrepareProperties();

                mediator.m_workingProgramName = EDSType.CorridorSpeed;

                if (mediator.m_alwaysSearchingTimer.Enabled)
                    mediator.m_alwaysSearchingTimer.Stop();

                if (!MainForm.m_workingProcessInformer.workingStatus)
                    mediator.StartProcess();
                else
                {
                    mediator.StopProcess();

                    if (!mediator.m_alwaysSearchingTimer.Enabled)
                        mediator.m_alwaysSearchingTimer.Start();
                }   
            }
            else if (alwaysWorking == Enums.IsAlwaysWorkingViolation.Nan)
            {
                if (MainForm.m_workingProcessInformer.workingStatus && mediator.m_workingProgramName != EDSType.CorridorSpeed)
                {
                    MessageBox.Show("Aktif Tarama Yapan EDS Mevcut!");
                    return;
                }

                PrepareProperties();

                mediator.m_workingProgramName = EDSType.CorridorSpeed;

                if (!MainForm.m_workingProcessInformer.workingStatus)
                    mediator.StartProcess();
                else
                    mediator.StopProcess(); 

            }
        }

        public override void StopWorking()
        {
            Settings.CorridorSpeedSettings m_settings = Settings.CorridorSpeedSettings.Singleton();
            SocketCommunication.Singleton().m_settings = m_settings;
            mediator.StopScan();
        }

        private void PrepareProperties()
        {
            SocketCommunication.Singleton().m_speedCorridor = CorridorSpeed.Singleton(MainForm.m_mf);
            Settings.CorridorSpeedSettings m_settings = Settings.CorridorSpeedSettings.Singleton();

            m_settings = m_settings.DeSerialize(m_settings);

            SocketCommunication.Singleton().m_settings = m_settings;
         
            SocketCommunication.Singleton().m_wantedImageType = new string[] { "L2-C1", "L2-C2", "L2-C3", "L2-C4" };

            ITriggeredViolation violation = triggered.Type(EDSType.CorridorSpeed);

            SocketCommunication.Singleton().m_violation = violation;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDSFactory 
{
    public class CorridorSpeedWideWorkingOperation : ViolationWorkingOperation
    {
        TriggeredViolation triggered;

        public CorridorSpeedWideWorkingOperation(Mediator mediator)
            : base(mediator)
        {
            mediator.AddCorridorSpeedWide(this);
            mediator.processStartType = new ProcessTypeStarter(new ProcessTypeSocket());
            triggered = new TriggeredViolation();
        }
        public override void CheckViolationForAlwaysWorking()
        {
            Enums.IsAlwaysWorkingViolation alwaysWorking = mediator.IsAlwaysWorkingViolationType(EDSType.CorridorSpeedWide);

            if (alwaysWorking == Enums.IsAlwaysWorkingViolation.Yes)
            {
                PrepareProperties();

                mediator.m_workingProgramName = EDSType.CorridorSpeedWide;

                mediator.m_alwaysSearchingTimer.Start();
            } 
        }
        public override void StartWorking( )
        {
            Enums.IsAlwaysWorkingViolation alwaysWorking = mediator.IsAlwaysWorkingViolationType(EDSType.CorridorSpeedWide);

            if (alwaysWorking == Enums.IsAlwaysWorkingViolation.No)
            {
                MessageBox.Show("Sürekli Tarama Modu Aktif Olan EDS Tipinden Başka EDS Tipinde Tarama Yapamazsınız!");
                return;
            }
            else if (alwaysWorking == Enums.IsAlwaysWorkingViolation.Yes)
            {
                PrepareProperties();

                mediator.m_workingProgramName = EDSType.CorridorSpeedWide;

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
                if (MainForm.m_workingProcessInformer.workingStatus && mediator.m_workingProgramName != EDSType.CorridorSpeedWide)
                {
                    MessageBox.Show("Aktif Tarama Yapan EDS Mevcut!");
                    return;
                }

                PrepareProperties();
            
                mediator.m_workingProgramName = EDSType.CorridorSpeedWide;

                if(!MainForm.m_workingProcessInformer.workingStatus)
                    mediator.StartProcess();
                else
                    mediator.StopProcess(); 
            }
        }

        public override void StopWorking()
        {
            Settings.CorridorSpeedWideSettings m_settings = Settings.CorridorSpeedWideSettings.Singleton();
            SocketCommunication.Singleton().m_settings = m_settings;
            mediator.StopScan();
        }


        private void PrepareProperties()
        {
            SocketCommunication.Singleton().m_speedCorridor = CorridorSpeedWide.Singleton(MainForm.m_mf);
            Settings.CorridorSpeedWideSettings m_settings = Settings.CorridorSpeedWideSettings.Singleton();

            m_settings = m_settings.DeSerialize(m_settings);

            SocketCommunication.Singleton().m_settings = m_settings;
          
            SocketCommunication.Singleton().m_wantedImageType = new string[] { "L2-C0", "L2-C1", "L2-C2", "L2-C3", "L2-C4" }; 

            ITriggeredViolation violation = triggered.Type(EDSType.CorridorSpeedWide);

            SocketCommunication.Singleton().m_violation = violation;

        }

    }
}

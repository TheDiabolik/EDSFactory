using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDSFactory 
{
    public class FixedHighwayShoulderWorkingOperation : ViolationWorkingOperation
    {
        //IStreamType resimFormatıNesnesi, resimFormatıNesnesi1;

        public FixedHighwayShoulderWorkingOperation(Mediator mediator)
            : base(mediator)
        {
            mediator.AddFixedHighwayShoulder(this);

            mediator.processStartType = new ProcessTypeStarter(new ProcessTypeTimer());
            //mediator.formatSeçici = new StreamChooser();
        
        }

        public override void CheckViolationForAlwaysWorking()
        {
            Enums.IsAlwaysWorkingViolation alwaysWorking = mediator.IsAlwaysWorkingViolationType(EDSType.FixedHighwayShoulder);

            if (alwaysWorking == Enums.IsAlwaysWorkingViolation.Yes)
            {
                mediator.m_workingProgramName = EDSType.FixedHighwayShoulder;  
                mediator.m_alwaysSearchingTimer.Start();  
            }
        }

        public override void StartWorking()
        {
            Enums.IsAlwaysWorkingViolation alwaysWorking = mediator.IsAlwaysWorkingViolationType(EDSType.FixedHighwayShoulder);

            if (alwaysWorking == Enums.IsAlwaysWorkingViolation.No)
            {
                MessageBox.Show("Sürekli Tarama Modu Aktif Olan EDS Tipinden Başka EDS Tipinde Tarama Yapamazsınız!");
                  return;
            }
            else if (alwaysWorking == Enums.IsAlwaysWorkingViolation.Yes)
            {
                
                mediator.m_workingProgramName = EDSType.FixedHighwayShoulder;

                if (mediator.m_alwaysSearchingTimer.Enabled)
                    mediator.m_alwaysSearchingTimer.Stop();

              Enums.IsWorking isWorking = mediator.TimerSwitch();

                if (isWorking == Enums.IsWorking.Yes) 
                    mediator.StartProcess( );
                else
                {
                    mediator.StopProcess();

                    if (!mediator.m_alwaysSearchingTimer.Enabled)
                        mediator.m_alwaysSearchingTimer.Start();
                } 
            }

            else if (alwaysWorking == Enums.IsAlwaysWorkingViolation.Nan)
            {
              

                if (MainForm.m_workingProcessInformer.workingStatus && mediator.m_workingProgramName != EDSType.FixedHighwayShoulder)
                {
                    MessageBox.Show("Aktif Tarama Yapan EDS Mevcut!");
                    return;
                } 

                mediator.m_workingProgramName = EDSType.FixedHighwayShoulder;

                Enums.IsWorking isWorking = mediator.TimerSwitch();

                if (isWorking == Enums.IsWorking.Yes)
                    mediator.StartProcess();
                else
                    mediator.StopProcess();  
            } 
        }

        public override void StopWorking()
        {
            mediator.StopScan();
        }

       
    }
}

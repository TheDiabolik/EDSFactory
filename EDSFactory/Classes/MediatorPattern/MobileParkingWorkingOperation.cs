using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDSFactory 
{
    public class MobileParkingWorkingOperation : ViolationWorkingOperation
    {
        public MobileParkingWorkingOperation(Mediator mediator)
            : base(mediator)
        {
            mediator.AddMobileParking(this);

            mediator.processStartType = new ProcessTypeStarter(new ProcessTypeTimer());
        }
        public override void CheckViolationForAlwaysWorking()
        {
            Enums.IsAlwaysWorkingViolation alwaysWorking = mediator.IsAlwaysWorkingViolationType(EDSType.MobileParking);

            if (alwaysWorking == Enums.IsAlwaysWorkingViolation.Yes)
            {
                mediator.m_workingProgramName = EDSType.MobileParking;


                mediator.m_alwaysSearchingTimer.Start();
            }
        }
        public override void StartWorking( )
        {
            Enums.IsAlwaysWorkingViolation alwaysWorking = mediator.IsAlwaysWorkingViolationType(EDSType.MobileParking);

            if (alwaysWorking == Enums.IsAlwaysWorkingViolation.No)
            {
                MessageBox.Show("Sürekli Tarama Modu Aktif Olan EDS Tipinden Başka EDS Tipinde Tarama Yapamazsınız!");
                return;
            }
            else if (alwaysWorking == Enums.IsAlwaysWorkingViolation.Yes)
            {
                mediator.m_workingProgramName = EDSType.MobileParking;


                if (mediator.m_alwaysSearchingTimer.Enabled)
                    mediator.m_alwaysSearchingTimer.Stop();


                Enums.IsWorking isWorking = mediator.TimerSwitch();

                if (isWorking == Enums.IsWorking.Yes)
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
                if (mediator.m_timer.Enabled && mediator.m_workingProgramName != EDSType.MobileParking)
                {
                    MessageBox.Show("Aktif Tarama Yapan EDS Mevcut!");
                    return;
                }

                mediator.m_workingProgramName = EDSType.MobileParking;

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

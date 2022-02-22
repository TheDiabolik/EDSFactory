using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDSFactory 
{
    public partial class SettingsModal : DevExpress.XtraEditors.XtraForm
    {
        private static SettingsModal m_settingsModal;
        public MainForm m_mf;
        Settings.WorkPlanAndMainSettings m_wpams;

        public SettingsModal()
        {
            InitializeComponent();

            m_wpams = Settings.WorkPlanAndMainSettings.Singleton();
            m_wpams = m_wpams.DeSerialize(m_wpams);

            m_checkEditDeleteLog.Checked = m_wpams.m_deleteLog;
            m_checkEditAutoStart.Checked = m_wpams.m_startAuto;
            m_checkEditAlwaysSearching.Checked = m_wpams.m_alwaysSearching;

            if (m_wpams.m_deleteLog)
                m_comboBoxEditDeleteLogPeriod.SelectedIndex = m_wpams.m_deleteLogPeriod;
              
            if (m_wpams.m_startAuto)
                m_comboBoxEditAutoStartProgram.SelectedItem = m_wpams.m_startAutoProgramName;
              
            if (m_wpams.m_alwaysSearching)
                m_comboBoxEditAlwaysSearching.SelectedItem = m_wpams.m_alwaysSearchingProgramName;

            foreach (int index in m_wpams.StartAutoHelperModulsIndex)
                m_checkedListBoxControlAutoStartHelperModuls.SetItemChecked(index, true);

        }

         public static SettingsModal Singleton(MainForm mf)
        {
            if (m_settingsModal == null)
                m_settingsModal = new SettingsModal(mf);

            return m_settingsModal;
        }

         private SettingsModal(MainForm mf)
            : this()
        {
            m_mf = mf;
        }

         private void SettingsModal_FormClosing(object sender, FormClosingEventArgs e)
         {
             m_settingsModal = null;

             MainForm.m_mediator.StartAlwaysScanProgram();
         }

         private void m_checkBoxEdits_CheckedChanged(object sender, EventArgs e)
         {
             CheckEdit myCheckBox = (CheckEdit)sender;

             if (myCheckBox == m_checkEditDeleteLog)
             {
                 if (m_checkEditDeleteLog.Checked)
                     m_comboBoxEditDeleteLogPeriod.Enabled = true;
                 else
                     m_comboBoxEditDeleteLogPeriod.Enabled = false;
             }
             else if (myCheckBox == m_checkEditAutoStart)
             {
                 if (m_checkEditAutoStart.Checked)
                     m_comboBoxEditAutoStartProgram.Enabled = true;
                 else
                     m_comboBoxEditAutoStartProgram.Enabled = false;
             }
             else if (myCheckBox == m_checkEditAlwaysSearching)
             {
                 if (m_checkEditAlwaysSearching.Checked)
                     m_comboBoxEditAlwaysSearching.Enabled = true;
                 else
                     m_comboBoxEditAlwaysSearching.Enabled = false;
             }

         }

         private void m_treeViewEDSType_SelectionChanged(object sender, EventArgs e)
         {

         }

         private void m_treeViewEDSType_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
         {
             workPlanControl1.Clean();

             TreeListNode node = m_treeViewEDSType.FocusedNode;

             if (node.Level == 1)
             {
                 TreeListNode parentNode = node.ParentNode;
                 string workPlanName = parentNode.GetDisplayText(0) + " " + node.GetDisplayText(0);

                 switch (workPlanName)
                 {
                     case "Emniyet Şeridi Elektronik Denetleme Sistemi Sabit":
                         {
                             workPlanControl1.ShowDateToWorkPlan(m_wpams.FixedHighwayShoulderWorkingPlan);
                             break;
                         }
                     case "Emniyet Şeridi Elektronik Denetleme Sistemi Mobil":
                         {
                             workPlanControl1.ShowDateToWorkPlan(m_wpams.MobileHighwayShoulderWorkingPlan);
                             break;
                         }
                     case "Park İhlal Elektronik Denetleme Sistemi Sabit":
                         {
                             workPlanControl1.ShowDateToWorkPlan(m_wpams.FixedParkingWorkingPlan);
                             break;
                         }
                     case "Park İhlal Elektronik Denetleme Sistemi Mobil":
                         {
                             workPlanControl1.ShowDateToWorkPlan(m_wpams.MobileParkingWorkingPlan);
                             break;
                         }
                     case "Koridor Hız Elektronik Denetleme Sistemi Dar":
                         {
                             workPlanControl1.ShowDateToWorkPlan(m_wpams.SpeedCorridorWorkingPlan);
                             break;
                         }
                     case "Koridor Hız Elektronik Denetleme Sistemi Geniş":
                         {
                             workPlanControl1.ShowDateToWorkPlan(m_wpams.SpeedCorridorWideWorkingPlan);
                             break;
                         }
                      
                 }
             } 
             else
             {
                   string workPlanName =  node.GetDisplayText(0);

                   switch (workPlanName)
                   {
                       case "Duraklama Elektronik Denetleme Sistemi":
                           {
                               workPlanControl1.ShowDateToWorkPlan(m_wpams.StandingWorkingPlan);
                               break;
                           }
                       case "Ofset Tarama Elektronik Denetleme Sistemi":
                           {
                               workPlanControl1.ShowDateToWorkPlan(m_wpams.CrosshatchWorkingPlan);
                               break;
                           }
                   }

             }


         }

         private void m_simpleButtons_Click(object sender, EventArgs e)
         {
             for (int i = 0; i < m_checkedListBoxControlAutoStartHelperModuls.Items.Count; i++)
             {
                 CheckState cs = m_checkedListBoxControlAutoStartHelperModuls.GetItemCheckState(i);

                 if (cs == CheckState.Checked)
                     m_wpams.StartAutoHelperModulsIndex.Add(i);
                 else if (cs == CheckState.Unchecked)
                     m_wpams.StartAutoHelperModulsIndex.Remove(i);
             }

             m_wpams.m_deleteLog = m_checkEditDeleteLog.Checked;

             if (m_checkEditDeleteLog.Checked)
                 m_wpams.m_deleteLogPeriod = m_comboBoxEditDeleteLogPeriod.SelectedIndex;

             m_wpams.m_startAuto = m_checkEditAutoStart.Checked;

             if (m_checkEditAutoStart.Checked)
                 m_wpams.m_startAutoProgramName = m_comboBoxEditAutoStartProgram.SelectedItem.ToString();

             m_wpams.m_alwaysSearching = m_checkEditAlwaysSearching.Checked;

             if (m_checkEditAlwaysSearching.Checked)
                 m_wpams.m_alwaysSearchingProgramName = m_comboBoxEditAlwaysSearching.SelectedItem.ToString();

             TreeListNode node = m_treeViewEDSType.FocusedNode;

             if (node.Level == 1)
             {
                 TreeListNode parentNode = node.ParentNode;
                 string workPlanName = parentNode.GetDisplayText(0) + " " + node.GetDisplayText(0);

                 switch (workPlanName)
                 {
                     case "Emniyet Şeridi Elektronik Denetleme Sistemi Sabit":
                         {
                             m_wpams.FixedHighwayShoulderWorkingPlan.Clear();
                             m_wpams.Serialize(m_wpams);

                             m_wpams.FixedHighwayShoulderWorkingPlan.AddRange(workPlanControl1.WorkingDates);
                             break;
                         }
                     case "Emniyet Şeridi Elektronik Denetleme Sistemi Mobil":
                         {
                             m_wpams.MobileHighwayShoulderWorkingPlan.Clear();
                             m_wpams.Serialize(m_wpams);

                             m_wpams.MobileHighwayShoulderWorkingPlan.AddRange(workPlanControl1.WorkingDates);
                             break;
                         }
                     case "Park İhlal Elektronik Denetleme Sistemi Sabit":
                         {
                             m_wpams.FixedParkingWorkingPlan.Clear();
                             m_wpams.Serialize(m_wpams);

                             m_wpams.FixedParkingWorkingPlan.AddRange(workPlanControl1.WorkingDates);
                             break;
                         }
                     case "Park İhlal Elektronik Denetleme Sistemi Mobil":
                         {
                             m_wpams.MobileParkingWorkingPlan.Clear();
                             m_wpams.Serialize(m_wpams);

                             m_wpams.MobileParkingWorkingPlan.AddRange(workPlanControl1.WorkingDates);
                             break;
                         }
                     case "Koridor Hız Elektronik Denetleme Sistemi Dar":
                         {
                             m_wpams.SpeedCorridorWorkingPlan.Clear();
                             m_wpams.Serialize(m_wpams);

                             m_wpams.SpeedCorridorWorkingPlan.AddRange(workPlanControl1.WorkingDates); 
                             break;
                         }
                     case "Koridor Hız Elektronik Denetleme Sistemi Geniş":
                         {
                             m_wpams.SpeedCorridorWideWorkingPlan.Clear();
                             m_wpams.Serialize(m_wpams);

                             m_wpams.SpeedCorridorWideWorkingPlan.AddRange(workPlanControl1.WorkingDates);
                             break;
                         }
                     

                 } 
             }
             else
             {
                 string workPlanName = node.GetDisplayText(0);

                 switch(workPlanName)
                 {
                     case "Duraklama Elektronik Denetleme Sistemi":
                         {
                             m_wpams.StandingWorkingPlan.Clear();
                             m_wpams.Serialize(m_wpams);

                             m_wpams.StandingWorkingPlan.AddRange(workPlanControl1.WorkingDates);
                             break;
                         }

                     case "Ofset Tarama Elektronik Denetleme Sistemi":
                         {
                             m_wpams.CrosshatchWorkingPlan.Clear();
                             m_wpams.Serialize(m_wpams);

                             m_wpams.CrosshatchWorkingPlan.AddRange(workPlanControl1.WorkingDates);
                             break;
                         }
                 } 

             }

             m_wpams.Serialize(m_wpams);

             SimpleButton button = (SimpleButton)sender;

             if (button == m_simpleButtonApply)
                 this.Close();
         }

         private void SettingsModal_Load(object sender, EventArgs e)
         {
             MainForm.m_mediator.StopScan();
         }
    }
}

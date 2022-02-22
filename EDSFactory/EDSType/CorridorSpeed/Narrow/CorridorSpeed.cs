using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDSFactory 
{
    public partial class CorridorSpeed : DevExpress.XtraEditors.XtraForm, IWorkingWatcher, IDataSourceChanged
    {
        private static CorridorSpeed m_corridor;
        public MainForm m_mf;

        CorridorSpeedWorkingOperation m_workingOperation;

        public CorridorSpeed()
        {
            InitializeComponent();

            m_workingOperation = new CorridorSpeedWorkingOperation(MainForm.m_mediator);

            ReSizer.FormToBeResize(this);
        }

         public static CorridorSpeed Singleton(MainForm mf)
        {
            if (m_corridor == null)
                m_corridor = new CorridorSpeed(mf);

            return m_corridor;
        }

         private CorridorSpeed(MainForm mf)
            : this()
        {
            m_mf = mf;
        }

         private void CorridorSpeed_FormClosing(object sender, FormClosingEventArgs e)
         {
             m_corridor = null;
             m_workingOperation.StopWorking(); 
         }

         private void m_SettingsPopup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
         {
             CorridorSpeedSettingsModal corridorSpeedSettingsModal = CorridorSpeedSettingsModal.Singleton(this);
             corridorSpeedSettingsModal.Owner = this;
             corridorSpeedSettingsModal.ShowDialog();
         }

         private void CorridorSpeed_Resize(object sender, EventArgs e)
         {
             ReSizer.ResizeAndLocateControl(this.ClientSize.Width, this.ClientSize.Height);
         }

          

         private void m_tabPageSocketInfo_Resize(object sender, EventArgs e)
         {
             richTextBox1.Size = new Size(m_tabPageSocketInfo.ClientSize.Width - 3, m_tabPageSocketInfo.ClientSize.Height - 3);
         }

         private void m_tabPageViolation_Paint(object sender, PaintEventArgs e)
         {
             gridControl1.Size = new Size(m_tabPageViolation.ClientSize.Width - 3, m_tabPageViolation.ClientSize.Height - 3);
         }

         private void CorridorSpeed_Load(object sender, EventArgs e)
         {
             m_toolStripStatusLabelUserInfo.Caption = UserMessages.ProgramStatusMessage;
             MainForm.m_workingProcessInformer.AddWatcher(this);



             gridControl1.DataSource = DatabaseOperation.CorridorSpeed.Singleton().FillDataSet();
             gridView1.PopulateColumns();

             gridView1.Columns[0].Caption = "ID";
             gridView1.Columns[1].Caption = "Plaka";
             gridView1.Columns[2].Caption = "Giriş Tarihi";
             gridView1.Columns[3].Caption = "Giriş Saati";
             gridView1.Columns[4].Caption = "Çıkış Tarihi";
             gridView1.Columns[5].Caption = "Çıkış Saati";
             gridView1.Columns[6].Caption = "Hız Limiti(km/sa)";
             gridView1.Columns[7].Caption = "Tolerans(%)";
             gridView1.Columns[8].Caption = "Hız(km/sa)"; 
             gridView1.Columns[9].Caption = "Giriş Resim İsmi"; 
             gridView1.Columns[10].Caption = "Çıkış Resim İsmi"; 
             gridView1.Columns[11].Caption = "Küçük Resim Yolu";

             gridView1.Columns[0].Width = 50;
             gridView1.Columns[1].Width = 90;
             gridView1.Columns[2].Width = 85;
             gridView1.Columns[3].Width = 85;
             gridView1.Columns[4].Width = 85;
             gridView1.Columns[5].Width = 85;
             gridView1.Columns[6].Width = 85;
             gridView1.Columns[7].Width = 85;
             gridView1.Columns[8].Width = 85;
             gridView1.Columns[9].Width = 300;
             gridView1.Columns[10].Width = 300;
             gridView1.Columns[11].Width = 350;

             gridView1.Columns[0].FieldName = "ID";
             gridView1.Columns[1].FieldName = "Plaka";
             gridView1.Columns[2].FieldName = "Giriş Tarihi";
             gridView1.Columns[3].FieldName = "Giriş Saati";
             gridView1.Columns[4].FieldName = "Çıkış Tarihi";
             gridView1.Columns[5].FieldName = "Çıkış Saati";
             gridView1.Columns[6].FieldName = "Hız Limiti(km/sa)";
             gridView1.Columns[7].FieldName = "Tolerans(%)";
             gridView1.Columns[8].FieldName = "Hız(km/sa)";
             gridView1.Columns[9].FieldName = "Giriş Resim İsmi";
             gridView1.Columns[10].FieldName = "Çıkış Resim İsmi";
             gridView1.Columns[11].FieldName = "Küçük Resim Yolu";

             m_workingOperation.CheckViolationForAlwaysWorking();
         }

         private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
         {
             try
             {
                 if (e.FocusedRowHandle >= 0)
                 {

                     ımageSlider1.Images.Clear();

                     string path = gridView1.GetDataRow(e.FocusedRowHandle)[11].ToString();
                     string entryNarrowThumbImagePath = path + "\\" + gridView1.GetDataRow(e.FocusedRowHandle)[9].ToString();
                     string exitNarrowThumbImagePath = path + "\\" + gridView1.GetDataRow(e.FocusedRowHandle)[10].ToString(); 

                     if (File.Exists(entryNarrowThumbImagePath))
                     {
                         using (FileStream myStream = new FileStream(entryNarrowThumbImagePath, FileMode.Open))
                         {
                             ımageSlider1.Images.Add(Image.FromStream(myStream));
                         }
                     }
                     else
                         ımageSlider1.Images.Add(Properties.Resources.noimage);

                     if (File.Exists(exitNarrowThumbImagePath))
                     {
                         using (FileStream myStream = new FileStream(exitNarrowThumbImagePath, FileMode.Open))
                         {
                             ımageSlider1.Images.Add(Image.FromStream(myStream));
                         }
                     }
                     else
                         ımageSlider1.Images.Add(Properties.Resources.noimage);
                 }
             }
             catch (Exception ex)
             {
                 Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), EDSType.CorridorSpeed);
                 //   DisplayManager.ShowExceptionMessage(ex.Message.ToString(), 4000, m_toolStripStatusLabelExceptionMessage, this);
             } 
         }

         private void richTextBox1_TextChanged(object sender, EventArgs e)
         {
             if (richTextBox1.Text.Length > 5000)
                 richTextBox1.ResetText();

             richTextBox1.ScrollToCaret();
         }

         private void m_StartPopup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
         {
             m_workingOperation.StartWorking(); 
         }


         public void WorkingStatus(bool workingStatus, string EDSType)
         { 
             if (EDSType == "CorridorSpeed")
             {
                 if (workingStatus)
                 {
                     m_toolStripStatusLabelUserInfo.Caption = UserMessages.StartCommunaticationMessage;
                     m_StartPopup.Caption = "Durdur";
                 }
                 else
                 {
                     m_toolStripStatusLabelUserInfo.Caption = UserMessages.ProgramStatusMessage;
                     m_StartPopup.Caption = "Başlat";
                 }
             }
         }

         public void SyncFileCreated(List<string> fileName)
         {
             if (gridControl1.InvokeRequired)
                 gridControl1.Invoke((MethodInvoker)delegate
                 {
                     gridView1.AddNewRow();

                     gridView1.SetFocusedRowCellValue(gridView1.Columns["Plaka"].FieldName, fileName[0].ToString());
                     gridView1.SetFocusedRowCellValue(gridView1.Columns["Giriş Tarihi"].FieldName, fileName[1].ToString());
                     gridView1.SetFocusedRowCellValue(gridView1.Columns["Giriş Saati"].FieldName, fileName[2].ToString());
                     gridView1.SetFocusedRowCellValue(gridView1.Columns["Çıkış Tarihi"].FieldName, fileName[3].ToString());
                     gridView1.SetFocusedRowCellValue(gridView1.Columns["Çıkış Saati"].FieldName, fileName[4].ToString());
                     gridView1.SetFocusedRowCellValue(gridView1.Columns["Hız Limiti(km/sa)"].FieldName, fileName[5].ToString());  
                     gridView1.SetFocusedRowCellValue(gridView1.Columns["Tolerans(%)"].FieldName, fileName[6].ToString());
                     gridView1.SetFocusedRowCellValue(gridView1.Columns["Hız(km/sa)"].FieldName, fileName[7].ToString());
                     gridView1.SetFocusedRowCellValue(gridView1.Columns["Giriş Resim İsmi"].FieldName, fileName[8].ToString());
                     gridView1.SetFocusedRowCellValue(gridView1.Columns["Çıkış Resim İsmi"].FieldName, fileName[9].ToString());
                     gridView1.SetFocusedRowCellValue(gridView1.Columns["Küçük Resim Yolu"].FieldName, fileName[10].ToString()); 

                     gridView1.FocusedRowHandle = (gridView1.RowCount - 1);
                 });
             else
             {
                 gridView1.AddNewRow();
                 gridView1.SetFocusedRowCellValue(gridView1.Columns["Plaka"].FieldName, fileName[0].ToString());
                 gridView1.SetFocusedRowCellValue(gridView1.Columns["Giriş Tarihi"].FieldName, fileName[1].ToString());
                 gridView1.SetFocusedRowCellValue(gridView1.Columns["Giriş Saati"].FieldName, fileName[2].ToString());
                 gridView1.SetFocusedRowCellValue(gridView1.Columns["Çıkış Tarihi"].FieldName, fileName[3].ToString());
                 gridView1.SetFocusedRowCellValue(gridView1.Columns["Çıkış Saati"].FieldName, fileName[4].ToString());
                 gridView1.SetFocusedRowCellValue(gridView1.Columns["Hız Limiti(km/sa)"].FieldName, fileName[5].ToString());
                 gridView1.SetFocusedRowCellValue(gridView1.Columns["Tolerans(%)"].FieldName, fileName[6].ToString());
                 gridView1.SetFocusedRowCellValue(gridView1.Columns["Hız(km/sa)"].FieldName, fileName[7].ToString());
                 gridView1.SetFocusedRowCellValue(gridView1.Columns["Giriş Resim İsmi"].FieldName, fileName[8].ToString());
                 gridView1.SetFocusedRowCellValue(gridView1.Columns["Çıkış Resim İsmi"].FieldName, fileName[9].ToString());
                 gridView1.SetFocusedRowCellValue(gridView1.Columns["Küçük Resim Yolu"].FieldName, fileName[10].ToString()); 
                 gridView1.FocusedRowHandle = (gridView1.RowCount - 1);
             }


         }
    }
}

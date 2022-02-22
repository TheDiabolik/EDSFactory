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
    public partial class Offset : DevExpress.XtraEditors.XtraForm, IWorkingWatcher, IDataSourceChanged
    {
        private static Offset m_mhsm;
        public MainForm m_mf;

        CrosshatchWorkingOperation m_workingOperation;

        public Offset()
        {
            InitializeComponent();

            m_workingOperation = new CrosshatchWorkingOperation(MainForm.m_mediator);

            ReSizer.FormToBeResize(this);
        }

           public static Offset Singleton(MainForm mf)
        {
            if (m_mhsm == null)
                m_mhsm = new Offset(mf);

            return m_mhsm;
        }

           private Offset(MainForm mf)
            : this()
        {
            m_mf = mf;
        }

           private void Offset_FormClosing(object sender, FormClosingEventArgs e)
           {
               m_mhsm = null;
               m_workingOperation.StopWorking(); 
           }

           private void m_SettingsPopup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
           {
               OffsetSettingsModal mobileHighwayShoulderSettingsModeless = OffsetSettingsModal.Singleton(this);
               mobileHighwayShoulderSettingsModeless.Owner = this;
               mobileHighwayShoulderSettingsModeless.ShowDialog();
           }

           private void Offset_Resize(object sender, EventArgs e)
           {
               ReSizer.ResizeAndLocateControl(this.ClientSize.Width, this.ClientSize.Height);
           }

           public void WorkingStatus(bool workingStatus, string EDSType)
           {
               if (EDSType == "Crosshatch")
               {
                   if (workingStatus)
                   {
                       m_toolStripStatusLabelUserInfo.Caption = UserMessages.FileSearchingMessage;
                       m_StartPopup.Caption = "Durdur";
                   }
                   else
                   {
                       m_toolStripStatusLabelUserInfo.Caption = UserMessages.ProgramStatusMessage;
                       m_StartPopup.Caption = "Başlat";
                   }
               }
           }

           private void Offset_Load(object sender, EventArgs e)
           {
               MainForm.m_workingProcessInformer.AddWatcher(this);
               m_toolStripStatusLabelUserInfo.Caption = UserMessages.ProgramStatusMessage;


               gridControl1.DataSource = DatabaseOperation.Crosshatch.Singleton().FillDataSet();  
               gridView1.PopulateColumns();

               gridView1.Columns[0].Caption = "ID";
               gridView1.Columns[1].Caption = "Plaka";
               gridView1.Columns[2].Caption = "Tarih";
               gridView1.Columns[3].Caption = "Saat";
               gridView1.Columns[4].Caption = "Dar Açı Resim İsmi";
               gridView1.Columns[5].Caption = "Geniş Açı Resim İsmi";
               gridView1.Columns[6].Caption = "Küçük Resim Yolu";

               gridView1.Columns[0].Width = 50;
               gridView1.Columns[1].Width = 90;
               gridView1.Columns[2].Width = 85;
               gridView1.Columns[3].Width = 85;
               gridView1.Columns[4].Width = 300;
               gridView1.Columns[5].Width = 300;
               gridView1.Columns[6].Width = 350;

               gridView1.Columns[0].FieldName = "ID";
               gridView1.Columns[1].FieldName = "Plaka";
               gridView1.Columns[2].FieldName = "Tarih";
               gridView1.Columns[3].FieldName = "Saat";
               gridView1.Columns[4].FieldName = "Dar Açı Resim İsmi";
               gridView1.Columns[5].FieldName = "Geniş Açı Resim İsmi";
               gridView1.Columns[6].FieldName = "Küçük Resim Yolu";

               m_workingOperation.CheckViolationForAlwaysWorking();
           }

           private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
           {
               try
               {
                   if (e.FocusedRowHandle >= 0)
                   { 
                       ımageSlider1.Images.Clear();

                       string path = gridView1.GetDataRow(e.FocusedRowHandle)[6].ToString();
                       string entryNarrowThumbImagePath = path + "\\" + gridView1.GetDataRow(e.FocusedRowHandle)[4].ToString();
                       string exitNarrowThumbImagePath = path + "\\" + gridView1.GetDataRow(e.FocusedRowHandle)[5].ToString();

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
                   Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), EDSType.MobileHighwayShoulder);
                   //   DisplayManager.ShowExceptionMessage(ex.Message.ToString(), 4000, m_toolStripStatusLabelExceptionMessage, this);
               }
           }

           private void m_StartPopup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
           {
               m_workingOperation.StartWorking();
           }

           public void SyncFileCreated(List<string> fileName)
           {
               if (gridControl1.InvokeRequired)
                   gridControl1.Invoke((MethodInvoker)delegate
                   {
                       gridView1.AddNewRow();

                       gridView1.SetFocusedRowCellValue(gridView1.Columns["Plaka"].FieldName, fileName[0].ToString());
                       gridView1.SetFocusedRowCellValue(gridView1.Columns["Tarih"].FieldName, fileName[1].ToString());
                       gridView1.SetFocusedRowCellValue(gridView1.Columns["Saat"].FieldName, fileName[2].ToString());
                       gridView1.SetFocusedRowCellValue(gridView1.Columns["Dar Açı Resim İsmi"].FieldName, fileName[3].ToString());
                       gridView1.SetFocusedRowCellValue(gridView1.Columns["Geniş Açı Resim İsmi"].FieldName, fileName[4].ToString());
                       gridView1.SetFocusedRowCellValue(gridView1.Columns["Küçük Resim Yolu"].FieldName, fileName[5].ToString());

                       gridView1.FocusedRowHandle = (gridView1.RowCount - 1);
                   });
               else
               {
                   gridView1.AddNewRow();
                   gridView1.SetFocusedRowCellValue(gridView1.Columns["Plaka"].FieldName, fileName[0].ToString());
                   gridView1.SetFocusedRowCellValue(gridView1.Columns["Tarih"].FieldName, fileName[1].ToString());
                   gridView1.SetFocusedRowCellValue(gridView1.Columns["Saat"].FieldName, fileName[2].ToString());
                   gridView1.SetFocusedRowCellValue(gridView1.Columns["Dar Açı Resim İsmi"].FieldName, fileName[3].ToString());
                   gridView1.SetFocusedRowCellValue(gridView1.Columns["Geniş Açı Resim İsmi"].FieldName, fileName[4].ToString());
                   gridView1.SetFocusedRowCellValue(gridView1.Columns["Küçük Resim Yolu"].FieldName, fileName[5].ToString());
                   gridView1.FocusedRowHandle = (gridView1.RowCount - 1);
               }


           }
    }
}

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
    public partial class MobileParking : DevExpress.XtraEditors.XtraForm, IWorkingWatcher, IDataSourceChanged
    {
        private static MobileParking m_mobileParking;
        public MainForm m_mf;
        MobileParkingWorkingOperation m_workingOperation; 

        public MobileParking()
        {
            InitializeComponent();

            m_workingOperation = new MobileParkingWorkingOperation(MainForm.m_mediator);

            ReSizer.FormToBeResize(this);
        }

        public static MobileParking Singleton(MainForm mf)
        {
            if (m_mobileParking == null)
                m_mobileParking = new MobileParking(mf);

            return m_mobileParking;
        }

        private MobileParking(MainForm mf)
            : this()
        {
            m_mf = mf;
        }

        private void m_SettingsPopup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MobileParkingSettingsModal mobileParkingSettingsModal = MobileParkingSettingsModal.Singleton(this);
            mobileParkingSettingsModal.Owner = this;
            mobileParkingSettingsModal.ShowDialog();
        }

        private void MobileParking_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_mobileParking = null;
            m_workingOperation.StopWorking(); 
        }

        private void MobileParking_Resize(object sender, EventArgs e)
        {
            ReSizer.ResizeAndLocateControl(this.ClientSize.Width, this.ClientSize.Height);
        }

        private void MobileParking_Load(object sender, EventArgs e)
        {
            MainForm.m_workingProcessInformer.AddWatcher(this);
            m_toolStripStatusLabelUserInfo.Caption = UserMessages.ProgramStatusMessage;


            gridControl1.DataSource = DatabaseOperation.MobileParking.Singleton().FillDataSet();   
            gridView1.PopulateColumns();

            gridView1.Columns[0].Caption = "ID";
            gridView1.Columns[1].Caption = "Plaka";
            gridView1.Columns[2].Caption = "Giriş Tarihi";
            gridView1.Columns[3].Caption = "Giriş Saati";
            gridView1.Columns[4].Caption = "Çıkış Tarihi";
            gridView1.Columns[5].Caption = "Çıkış Saati";
            gridView1.Columns[6].Caption = "Giriş Dar Açı Resim İsmi";
            gridView1.Columns[7].Caption = "Giriş Geniş Açı Resim İsmi";
            gridView1.Columns[8].Caption = "Çıkış Dar Açı Resim İsmi";
            gridView1.Columns[9].Caption = "Çıkış Geniş Açı Resim İsmi";
            gridView1.Columns[10].Caption = "Küçük Resim Yolu";

            gridView1.Columns[0].Width = 50;
            gridView1.Columns[1].Width = 90;
            gridView1.Columns[2].Width = 85;
            gridView1.Columns[3].Width = 85;
            gridView1.Columns[4].Width = 85;
            gridView1.Columns[5].Width = 85;
            gridView1.Columns[6].Width = 300;
            gridView1.Columns[7].Width = 300;
            gridView1.Columns[8].Width = 300;
            gridView1.Columns[9].Width = 300;
            gridView1.Columns[10].Width = 350;

            gridView1.Columns[0].FieldName = "ID";
            gridView1.Columns[1].FieldName = "Plaka";
            gridView1.Columns[2].FieldName = "Giriş Tarihi";
            gridView1.Columns[3].FieldName = "Giriş Saati";
            gridView1.Columns[4].FieldName = "Çıkış Tarihi";
            gridView1.Columns[5].FieldName = "Çıkış Saati";
            gridView1.Columns[6].FieldName = "Giriş Dar Açı Resim İsmi";
            gridView1.Columns[7].FieldName = "Giriş Geniş Açı Resim İsmi";
            gridView1.Columns[8].FieldName = "Çıkış Dar Açı Resim İsmi";
            gridView1.Columns[9].FieldName = "Çıkış Geniş Açı Resim İsmi";
            gridView1.Columns[10].FieldName = "Küçük Resim Yolu";

            m_workingOperation.CheckViolationForAlwaysWorking();
        }

        private void m_StartPopup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_workingOperation.StartWorking();
        }

        public void WorkingStatus(bool workingStatus, string EDSType)
        {
            if (EDSType == "MobileParking")
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {

                    ımageSlider1.Images.Clear();

                    string path = "", entryNarrowThumbImagePath = "", entryWideThumbImagePath = "", exitNarrowThumbImagePath = "", exitWideThumbImagePath = "";


                    path = gridView1.GetDataRow(e.FocusedRowHandle)[10].ToString();
                    entryNarrowThumbImagePath = path + "\\" + gridView1.GetDataRow(e.FocusedRowHandle)[6].ToString();
                    entryWideThumbImagePath = path + "\\" + gridView1.GetDataRow(e.FocusedRowHandle)[7].ToString();
                    exitNarrowThumbImagePath = path + "\\" + gridView1.GetDataRow(e.FocusedRowHandle)[8].ToString();
                    exitWideThumbImagePath = path + "\\" + gridView1.GetDataRow(e.FocusedRowHandle)[9].ToString(); 

                    if (!string.IsNullOrEmpty(entryNarrowThumbImagePath) && File.Exists(entryNarrowThumbImagePath))
                    {
                        using (FileStream myStream = new FileStream(entryNarrowThumbImagePath, FileMode.Open))
                        {
                            ımageSlider1.Images.Add(Image.FromStream(myStream));
                        }
                    }
                    else
                        ımageSlider1.Images.Add(Properties.Resources.noimage);

                    if (!string.IsNullOrEmpty(entryWideThumbImagePath) && File.Exists(entryWideThumbImagePath))
                    {
                        using (FileStream myStream = new FileStream(entryWideThumbImagePath, FileMode.Open))
                        {
                            ımageSlider1.Images.Add(Image.FromStream(myStream));
                        }
                    }
                    else
                        ımageSlider1.Images.Add(Properties.Resources.noimage);

                    if (!string.IsNullOrEmpty(exitNarrowThumbImagePath) && File.Exists(exitNarrowThumbImagePath))
                    {
                        using (FileStream myStream = new FileStream(exitNarrowThumbImagePath, FileMode.Open))
                        {
                            ımageSlider1.Images.Add(Image.FromStream(myStream));
                        }
                    }
                    else
                        ımageSlider1.Images.Add(Properties.Resources.noimage);

                    if (!string.IsNullOrEmpty(exitWideThumbImagePath) && File.Exists(exitWideThumbImagePath))
                    {
                        using (FileStream myStream = new FileStream(exitWideThumbImagePath, FileMode.Open))
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
                Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), EDSType.FixedParking);
                //DisplayManager.ShowExceptionMessage(ex.Message.ToString(), 4000, m_toolStripStatusLabelExceptionMessage, this);
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
                    gridView1.SetFocusedRowCellValue(gridView1.Columns["Giriş Dar Açı Resim İsmi"].FieldName, fileName[5].ToString());
                    gridView1.SetFocusedRowCellValue(gridView1.Columns["Giriş Geniş Açı Resim İsmi"].FieldName, fileName[6].ToString());
                    gridView1.SetFocusedRowCellValue(gridView1.Columns["Çıkış Dar Açı Resim İsmi"].FieldName, fileName[7].ToString());
                    gridView1.SetFocusedRowCellValue(gridView1.Columns["Çıkış Geniş Açı Resim İsmi"].FieldName, fileName[8].ToString());
                    gridView1.SetFocusedRowCellValue(gridView1.Columns["Küçük Resim Yolu"].FieldName, fileName[9].ToString());

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
                gridView1.SetFocusedRowCellValue(gridView1.Columns["Giriş Dar Açı Resim İsmi"].FieldName, fileName[5].ToString());
                gridView1.SetFocusedRowCellValue(gridView1.Columns["Giriş Geniş Açı Resim İsmi"].FieldName, fileName[6].ToString());
                gridView1.SetFocusedRowCellValue(gridView1.Columns["Çıkış Dar Açı Resim İsmi"].FieldName, fileName[7].ToString());
                gridView1.SetFocusedRowCellValue(gridView1.Columns["Çıkış Geniş Açı Resim İsmi"].FieldName, fileName[8].ToString());
                gridView1.SetFocusedRowCellValue(gridView1.Columns["Küçük Resim Yolu"].FieldName, fileName[9].ToString());
                gridView1.FocusedRowHandle = (gridView1.RowCount - 1);
            }


        }
    }
}

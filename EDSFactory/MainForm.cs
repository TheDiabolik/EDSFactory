using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EDSFactory
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        internal static MainForm m_mf; 
         internal static Mediator m_mediator;  
        internal static WorkingInformer m_workingProcessInformer; 
        internal static SyncFileCreate m_ahmet;

        public MainForm()
        {
            InitializeComponent();
            m_mf = this;

            m_ahmet = new SyncFileCreate();
             m_mediator = Mediator.Singleton();
            m_workingProcessInformer = new WorkingInformer();

            DisplayManager.OpeningForm();
            DisplayManager.AutoStartHelperModuls();
        }
 
        private void m_tileItemPTZCameraControl_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            PTZCameraControl settingsModal = PTZCameraControl.Singleton(this);
            //settingsModal.MdiParent = this;
            settingsModal.Show();
            settingsModal.BringToFront();
        }

        private void m_tileItemTimeSync_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            TimeSynchroniser timeSynchroniser = TimeSynchroniser.Singleton(this);
            //newReport.Owner = this;
            timeSynchroniser.Show();
            timeSynchroniser.BringToFront();
        }
 

        private void m_tileItemParking_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            m_popupMenuParking.ShowPopup(Control.MousePosition);
        }

        private void m_tileItemHighwayShoulder_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            m_popupMenuHighwayShoulder.ShowPopup(Control.MousePosition);
        }

        private void m_barButtonItemHSFixed_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FixedHighwayShoulder fixedHighwayShoulder = FixedHighwayShoulder.Singleton(this);
            //fixedHighwayShoulder.MdiParent = this;
            fixedHighwayShoulder.Show();
            fixedHighwayShoulder.BringToFront();
        }

        private void m_barButtonItemHSMobile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MobileHighwayShoulder mobileHighwayShoulder = MobileHighwayShoulder.Singleton(this);
            //mobileHighwayShoulder.MdiParent = this;
            mobileHighwayShoulder.Show();
            mobileHighwayShoulder.BringToFront();
        }

        private void m_barButtonItemFixed_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FixedParking fixedParking = FixedParking.Singleton(this);
            //fixedParking.MdiParent = this;
            fixedParking.Show();
            fixedParking.BringToFront();
        }

        private void m_barButtonItemMobile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MobileParking mobileParking = MobileParking.Singleton(this);
            //mobileParking.MdiParent = this;
            mobileParking.Show();
            mobileParking.BringToFront();
        }

        private void m_tileItemSettings_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            SettingsModal settingsModal = SettingsModal.Singleton(this);
            settingsModal.Owner = this;
            settingsModal.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void m_tileItemCorridorSpeed_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            m_popupMenuCorridorSpeed.ShowPopup(Control.MousePosition);
        }

        private void m_barButtonItemNarrowAngle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CorridorSpeed speedCorridor = CorridorSpeed.Singleton(this);
            //speedCorridor.MdiParent = this;
            speedCorridor.Show();
            speedCorridor.BringToFront();
        }

        private void m_barButtonItemWideAngle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CorridorSpeedWide speedCorridor = CorridorSpeedWide.Singleton(this);
            //speedCorridor.MdiParent = this;
            speedCorridor.Show();
            speedCorridor.BringToFront();
        }

        private void m_tileItemStanding_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Standing standing = Standing.Singleton(this);
            //speedCorridor.MdiParent = this;
            standing.Show();
            standing.BringToFront();
        }

        private void m_tileItemAbout_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            About about = new About();
            about.ShowDialog(); 
        }

        private void tileControl1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            ReSizer.ResizeAndLocateControl(this.ClientSize.Width, this.ClientSize.Height);
        }

        private void m_tileItemOffset_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Offset standing = Offset.Singleton(this);
            //speedCorridor.MdiParent = this;
            standing.Show();
            standing.BringToFront();
        }

        private void m_tileItemVideoBuffer_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            VideoModal standing = VideoModal.Singleton(this);
            //speedCorridor.MdiParent = this;
            standing.Show();
            standing.BringToFront();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}

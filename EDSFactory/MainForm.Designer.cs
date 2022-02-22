namespace EDSFactory
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.TileItemElement tileItemElement1 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement2 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement3 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement4 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement5 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement6 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement7 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement8 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement9 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement10 = new DevExpress.XtraEditors.TileItemElement();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.formAssistant1 = new DevExpress.XtraBars.FormAssistant();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.tileControl1 = new DevExpress.XtraEditors.TileControl();
            this.tileGroup1 = new DevExpress.XtraEditors.TileGroup();
            this.m_tileItemParking = new DevExpress.XtraEditors.TileItem();
            this.m_tileItemCorridorSpeed = new DevExpress.XtraEditors.TileItem();
            this.m_tileItemHighwayShoulder = new DevExpress.XtraEditors.TileItem();
            this.m_tileItemStanding = new DevExpress.XtraEditors.TileItem();
            this.m_tileItemOffset = new DevExpress.XtraEditors.TileItem();
            this.m_tileItemAbout = new DevExpress.XtraEditors.TileItem();
            this.tileGroup2 = new DevExpress.XtraEditors.TileGroup();
            this.m_tileItemPTZCameraControl = new DevExpress.XtraEditors.TileItem();
            this.m_tileItemTimeSync = new DevExpress.XtraEditors.TileItem();
            this.m_tileItemSettings = new DevExpress.XtraEditors.TileItem();
            this.m_tileItemVideoBuffer = new DevExpress.XtraEditors.TileItem();
            this.m_popupMenuHighwayShoulder = new DevExpress.XtraBars.PopupMenu(this.components);
            this.m_barButtonItemHSFixed = new DevExpress.XtraBars.BarButtonItem();
            this.m_barButtonItemHSMobile = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.m_barButtonItemFixed = new DevExpress.XtraBars.BarButtonItem();
            this.m_barButtonItemMobile = new DevExpress.XtraBars.BarButtonItem();
            this.m_barButtonItemNarrowAngle = new DevExpress.XtraBars.BarButtonItem();
            this.m_barButtonItemWideAngle = new DevExpress.XtraBars.BarButtonItem();
            this.m_barSubItemFixed = new DevExpress.XtraBars.BarSubItem();
            this.m_barSubItemMobile = new DevExpress.XtraBars.BarSubItem();
            this.m_popupMenuParking = new DevExpress.XtraBars.PopupMenu(this.components);
            this.m_popupMenuCorridorSpeed = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.m_popupMenuHighwayShoulder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_popupMenuParking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_popupMenuCorridorSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Blueprint";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 389);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = null;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(816, 20);
            // 
            // tileControl1
            // 
            this.tileControl1.AllowDrag = false;
            this.tileControl1.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.tileControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileControl1.DragSize = new System.Drawing.Size(0, 0);
            this.tileControl1.EnableItemDoubleClickEvent = false;
            this.tileControl1.Groups.Add(this.tileGroup1);
            this.tileControl1.Groups.Add(this.tileGroup2);
            this.tileControl1.Location = new System.Drawing.Point(0, 0);
            this.tileControl1.MaxId = 20;
            this.tileControl1.Name = "tileControl1";
            this.tileControl1.Size = new System.Drawing.Size(884, 415);
            this.tileControl1.TabIndex = 0;
            this.tileControl1.Text = "Duraklama EDS";
            this.tileControl1.Click += new System.EventHandler(this.tileControl1_Click);
            // 
            // tileGroup1
            // 
            this.tileGroup1.Items.Add(this.m_tileItemParking);
            this.tileGroup1.Items.Add(this.m_tileItemCorridorSpeed);
            this.tileGroup1.Items.Add(this.m_tileItemHighwayShoulder);
            this.tileGroup1.Items.Add(this.m_tileItemStanding);
            this.tileGroup1.Items.Add(this.m_tileItemOffset);
            this.tileGroup1.Items.Add(this.m_tileItemAbout);
            this.tileGroup1.Name = "tileGroup1";
            this.tileGroup1.Text = "EDS";
            // 
            // m_tileItemParking
            // 
            this.m_tileItemParking.AllowAnimation = false;
            this.m_tileItemParking.AllowHtmlText = DevExpress.Utils.DefaultBoolean.False;
            this.m_tileItemParking.AppearanceItem.Normal.BackColor = System.Drawing.Color.DodgerBlue;
            this.m_tileItemParking.AppearanceItem.Normal.BorderColor = System.Drawing.Color.Black;
            this.m_tileItemParking.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.m_tileItemParking.AppearanceItem.Normal.Options.UseBackColor = true;
            this.m_tileItemParking.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.m_tileItemParking.AppearanceItem.Normal.Options.UseFont = true;
            tileItemElement1.Image = global::EDSFactory.Properties.Resources.duraklama;
            tileItemElement1.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopLeft;
            tileItemElement1.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileItemElement1.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement1.Text = "Park EDS";
            tileItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomCenter;
            this.m_tileItemParking.Elements.Add(tileItemElement1);
            this.m_tileItemParking.EnableItemDoubleClickEvent = DevExpress.Utils.DefaultBoolean.False;
            this.m_tileItemParking.Id = 0;
            this.m_tileItemParking.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.m_tileItemParking.Name = "m_tileItemParking";
            this.m_tileItemParking.TextShowMode = DevExpress.XtraEditors.TileItemContentShowMode.Always;
            this.m_tileItemParking.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.m_tileItemParking_ItemClick);
            // 
            // m_tileItemCorridorSpeed
            // 
            this.m_tileItemCorridorSpeed.AllowAnimation = false;
            this.m_tileItemCorridorSpeed.AllowHtmlText = DevExpress.Utils.DefaultBoolean.False;
            this.m_tileItemCorridorSpeed.AppearanceItem.Normal.BackColor = System.Drawing.Color.Silver;
            this.m_tileItemCorridorSpeed.AppearanceItem.Normal.BorderColor = System.Drawing.Color.Black;
            this.m_tileItemCorridorSpeed.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.m_tileItemCorridorSpeed.AppearanceItem.Normal.Options.UseBackColor = true;
            this.m_tileItemCorridorSpeed.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.m_tileItemCorridorSpeed.AppearanceItem.Normal.Options.UseFont = true;
            tileItemElement2.Image = global::EDSFactory.Properties.Resources.koridor;
            tileItemElement2.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopLeft;
            tileItemElement2.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileItemElement2.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement2.Text = "Koridor Hız EDS";
            tileItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            this.m_tileItemCorridorSpeed.Elements.Add(tileItemElement2);
            this.m_tileItemCorridorSpeed.EnableItemDoubleClickEvent = DevExpress.Utils.DefaultBoolean.False;
            this.m_tileItemCorridorSpeed.Id = 1;
            this.m_tileItemCorridorSpeed.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.m_tileItemCorridorSpeed.Name = "m_tileItemCorridorSpeed";
            this.m_tileItemCorridorSpeed.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.m_tileItemCorridorSpeed_ItemClick);
            // 
            // m_tileItemHighwayShoulder
            // 
            this.m_tileItemHighwayShoulder.AllowAnimation = false;
            this.m_tileItemHighwayShoulder.AllowHtmlText = DevExpress.Utils.DefaultBoolean.False;
            this.m_tileItemHighwayShoulder.AppearanceItem.Normal.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_tileItemHighwayShoulder.AppearanceItem.Normal.BorderColor = System.Drawing.Color.Black;
            this.m_tileItemHighwayShoulder.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.m_tileItemHighwayShoulder.AppearanceItem.Normal.Options.UseBackColor = true;
            this.m_tileItemHighwayShoulder.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.m_tileItemHighwayShoulder.AppearanceItem.Normal.Options.UseFont = true;
            tileItemElement3.Image = global::EDSFactory.Properties.Resources.emniyet;
            tileItemElement3.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileItemElement3.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileItemElement3.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement3.Text = "Emniyet Şeridi EDS";
            this.m_tileItemHighwayShoulder.Elements.Add(tileItemElement3);
            this.m_tileItemHighwayShoulder.EnableItemDoubleClickEvent = DevExpress.Utils.DefaultBoolean.False;
            this.m_tileItemHighwayShoulder.Id = 2;
            this.m_tileItemHighwayShoulder.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.m_tileItemHighwayShoulder.Name = "m_tileItemHighwayShoulder";
            this.m_tileItemHighwayShoulder.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.m_tileItemHighwayShoulder_ItemClick);
            // 
            // m_tileItemStanding
            // 
            this.m_tileItemStanding.AllowAnimation = false;
            this.m_tileItemStanding.AllowHtmlText = DevExpress.Utils.DefaultBoolean.False;
            this.m_tileItemStanding.AppearanceItem.Normal.BackColor = System.Drawing.Color.SeaGreen;
            this.m_tileItemStanding.AppearanceItem.Normal.BorderColor = System.Drawing.Color.Black;
            this.m_tileItemStanding.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.m_tileItemStanding.AppearanceItem.Normal.Options.UseBackColor = true;
            this.m_tileItemStanding.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.m_tileItemStanding.AppearanceItem.Normal.Options.UseFont = true;
            tileItemElement4.Image = global::EDSFactory.Properties.Resources.park1;
            tileItemElement4.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileItemElement4.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement4.Text = "Duraklama EDS";
            tileItemElement4.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            this.m_tileItemStanding.Elements.Add(tileItemElement4);
            this.m_tileItemStanding.EnableItemDoubleClickEvent = DevExpress.Utils.DefaultBoolean.False;
            this.m_tileItemStanding.Id = 13;
            this.m_tileItemStanding.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.m_tileItemStanding.Name = "m_tileItemStanding";
            this.m_tileItemStanding.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.m_tileItemStanding_ItemClick);
            // 
            // m_tileItemOffset
            // 
            this.m_tileItemOffset.AllowAnimation = false;
            this.m_tileItemOffset.AllowHtmlText = DevExpress.Utils.DefaultBoolean.False;
            this.m_tileItemOffset.AppearanceItem.Normal.BackColor = System.Drawing.Color.RosyBrown;
            this.m_tileItemOffset.AppearanceItem.Normal.BorderColor = System.Drawing.Color.Black;
            this.m_tileItemOffset.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.m_tileItemOffset.AppearanceItem.Normal.Options.UseBackColor = true;
            this.m_tileItemOffset.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.m_tileItemOffset.AppearanceItem.Normal.Options.UseFont = true;
            tileItemElement5.Image = global::EDSFactory.Properties.Resources.PixelKit_road_cone_icon;
            tileItemElement5.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileItemElement5.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement5.Text = "Ofset Tarama EDS";
            tileItemElement5.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            this.m_tileItemOffset.Elements.Add(tileItemElement5);
            this.m_tileItemOffset.EnableItemDoubleClickEvent = DevExpress.Utils.DefaultBoolean.False;
            this.m_tileItemOffset.Id = 15;
            this.m_tileItemOffset.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.m_tileItemOffset.Name = "m_tileItemOffset";
            this.m_tileItemOffset.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.m_tileItemOffset_ItemClick);
            // 
            // m_tileItemAbout
            // 
            this.m_tileItemAbout.AllowAnimation = false;
            this.m_tileItemAbout.AllowHtmlText = DevExpress.Utils.DefaultBoolean.False;
            this.m_tileItemAbout.AppearanceItem.Normal.BorderColor = System.Drawing.Color.Black;
            this.m_tileItemAbout.AppearanceItem.Normal.Options.UseBorderColor = true;
            tileItemElement6.Image = global::EDSFactory.Properties.Resources.isbak_beyaz;
            tileItemElement6.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement6.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileItemElement6.Text = "";
            this.m_tileItemAbout.Elements.Add(tileItemElement6);
            this.m_tileItemAbout.EnableItemDoubleClickEvent = DevExpress.Utils.DefaultBoolean.False;
            this.m_tileItemAbout.Id = 16;
            this.m_tileItemAbout.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.m_tileItemAbout.Name = "m_tileItemAbout";
            this.m_tileItemAbout.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.m_tileItemAbout_ItemClick);
            // 
            // tileGroup2
            // 
            this.tileGroup2.Items.Add(this.m_tileItemPTZCameraControl);
            this.tileGroup2.Items.Add(this.m_tileItemTimeSync);
            this.tileGroup2.Items.Add(this.m_tileItemSettings);
            this.tileGroup2.Items.Add(this.m_tileItemVideoBuffer);
            this.tileGroup2.Name = "tileGroup2";
            this.tileGroup2.Text = "Yardımcı Modüller";
            // 
            // m_tileItemPTZCameraControl
            // 
            this.m_tileItemPTZCameraControl.AllowAnimation = false;
            this.m_tileItemPTZCameraControl.AllowHtmlText = DevExpress.Utils.DefaultBoolean.False;
            this.m_tileItemPTZCameraControl.AppearanceItem.Normal.BackColor = System.Drawing.SystemColors.ControlDark;
            this.m_tileItemPTZCameraControl.AppearanceItem.Normal.BorderColor = System.Drawing.Color.Black;
            this.m_tileItemPTZCameraControl.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.m_tileItemPTZCameraControl.AppearanceItem.Normal.Options.UseBackColor = true;
            this.m_tileItemPTZCameraControl.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.m_tileItemPTZCameraControl.AppearanceItem.Normal.Options.UseFont = true;
            tileItemElement7.Image = global::EDSFactory.Properties.Resources.Joystick;
            tileItemElement7.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileItemElement7.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileItemElement7.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement7.Text = "Kamera Kontrol";
            this.m_tileItemPTZCameraControl.Elements.Add(tileItemElement7);
            this.m_tileItemPTZCameraControl.EnableItemDoubleClickEvent = DevExpress.Utils.DefaultBoolean.False;
            this.m_tileItemPTZCameraControl.Id = 5;
            this.m_tileItemPTZCameraControl.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.m_tileItemPTZCameraControl.Name = "m_tileItemPTZCameraControl";
            this.m_tileItemPTZCameraControl.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.m_tileItemPTZCameraControl_ItemClick);
            // 
            // m_tileItemTimeSync
            // 
            this.m_tileItemTimeSync.AllowAnimation = false;
            this.m_tileItemTimeSync.AllowHtmlText = DevExpress.Utils.DefaultBoolean.False;
            this.m_tileItemTimeSync.AppearanceItem.Normal.BackColor = System.Drawing.Color.Tan;
            this.m_tileItemTimeSync.AppearanceItem.Normal.BorderColor = System.Drawing.Color.Black;
            this.m_tileItemTimeSync.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.m_tileItemTimeSync.AppearanceItem.Normal.Options.UseBackColor = true;
            this.m_tileItemTimeSync.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.m_tileItemTimeSync.AppearanceItem.Normal.Options.UseFont = true;
            tileItemElement8.Image = global::EDSFactory.Properties.Resources.Refresh;
            tileItemElement8.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileItemElement8.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileItemElement8.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement8.Text = "Zaman Eşitleyici";
            tileItemElement8.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            this.m_tileItemTimeSync.Elements.Add(tileItemElement8);
            this.m_tileItemTimeSync.EnableItemDoubleClickEvent = DevExpress.Utils.DefaultBoolean.False;
            this.m_tileItemTimeSync.Id = 6;
            this.m_tileItemTimeSync.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.m_tileItemTimeSync.Name = "m_tileItemTimeSync";
            this.m_tileItemTimeSync.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.m_tileItemTimeSync_ItemClick);
            // 
            // m_tileItemSettings
            // 
            this.m_tileItemSettings.AllowAnimation = false;
            this.m_tileItemSettings.AllowHtmlText = DevExpress.Utils.DefaultBoolean.False;
            this.m_tileItemSettings.AppearanceItem.Normal.BackColor = System.Drawing.Color.DarkCyan;
            this.m_tileItemSettings.AppearanceItem.Normal.BorderColor = System.Drawing.Color.Black;
            this.m_tileItemSettings.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.m_tileItemSettings.AppearanceItem.Normal.Options.UseBackColor = true;
            this.m_tileItemSettings.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.m_tileItemSettings.AppearanceItem.Normal.Options.UseFont = true;
            tileItemElement9.Image = global::EDSFactory.Properties.Resources.Setting1;
            tileItemElement9.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopCenter;
            tileItemElement9.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileItemElement9.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Top;
            tileItemElement9.Text = "Ayarlar";
            tileItemElement9.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomCenter;
            this.m_tileItemSettings.Elements.Add(tileItemElement9);
            this.m_tileItemSettings.EnableItemDoubleClickEvent = DevExpress.Utils.DefaultBoolean.False;
            this.m_tileItemSettings.Id = 11;
            this.m_tileItemSettings.Name = "m_tileItemSettings";
            this.m_tileItemSettings.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.m_tileItemSettings_ItemClick);
            // 
            // m_tileItemVideoBuffer
            // 
            this.m_tileItemVideoBuffer.AppearanceItem.Normal.BackColor = System.Drawing.SystemColors.HotTrack;
            this.m_tileItemVideoBuffer.AppearanceItem.Normal.BorderColor = System.Drawing.Color.Black;
            this.m_tileItemVideoBuffer.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.m_tileItemVideoBuffer.AppearanceItem.Normal.Options.UseBackColor = true;
            this.m_tileItemVideoBuffer.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.m_tileItemVideoBuffer.AppearanceItem.Normal.Options.UseFont = true;
            this.m_tileItemVideoBuffer.AppearanceItem.Normal.Options.UseTextOptions = true;
            this.m_tileItemVideoBuffer.AppearanceItem.Normal.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.m_tileItemVideoBuffer.AppearanceItem.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            tileItemElement10.Image = global::EDSFactory.Properties.Resources.videobuffer;
            tileItemElement10.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopCenter;
            tileItemElement10.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Top;
            tileItemElement10.Text = "Video Oluşturucu";
            tileItemElement10.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopCenter;
            tileItemElement10.TextLocation = new System.Drawing.Point(10, 0);
            this.m_tileItemVideoBuffer.Elements.Add(tileItemElement10);
            this.m_tileItemVideoBuffer.EnableItemDoubleClickEvent = DevExpress.Utils.DefaultBoolean.False;
            this.m_tileItemVideoBuffer.Id = 19;
            this.m_tileItemVideoBuffer.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
            this.m_tileItemVideoBuffer.Name = "m_tileItemVideoBuffer";
            this.m_tileItemVideoBuffer.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.m_tileItemVideoBuffer_ItemClick);
            // 
            // m_popupMenuHighwayShoulder
            // 
            this.m_popupMenuHighwayShoulder.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.m_barButtonItemHSFixed),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_barButtonItemHSMobile)});
            this.m_popupMenuHighwayShoulder.Manager = this.barManager1;
            this.m_popupMenuHighwayShoulder.Name = "m_popupMenuHighwayShoulder";
            // 
            // m_barButtonItemHSFixed
            // 
            this.m_barButtonItemHSFixed.Caption = "Sabit";
            this.m_barButtonItemHSFixed.CategoryGuid = new System.Guid("24ed1d19-e6f0-40ea-b3b0-96a9424ca7d0");
            this.m_barButtonItemHSFixed.Glyph = global::EDSFactory.Properties.Resources._fixed;
            this.m_barButtonItemHSFixed.Id = 7;
            this.m_barButtonItemHSFixed.Name = "m_barButtonItemHSFixed";
            this.m_barButtonItemHSFixed.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.m_barButtonItemHSFixed_ItemClick);
            // 
            // m_barButtonItemHSMobile
            // 
            this.m_barButtonItemHSMobile.Caption = "Mobil";
            this.m_barButtonItemHSMobile.CategoryGuid = new System.Guid("24ed1d19-e6f0-40ea-b3b0-96a9424ca7d0");
            this.m_barButtonItemHSMobile.Glyph = global::EDSFactory.Properties.Resources.mobile;
            this.m_barButtonItemHSMobile.Id = 8;
            this.m_barButtonItemHSMobile.Name = "m_barButtonItemHSMobile";
            this.m_barButtonItemHSMobile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.m_barButtonItemHSMobile_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.Categories.AddRange(new DevExpress.XtraBars.BarManagerCategory[] {
            new DevExpress.XtraBars.BarManagerCategory("Parking", new System.Guid("9d98dccb-b7b5-4a7e-8a3d-2f0e79765e75")),
            new DevExpress.XtraBars.BarManagerCategory("HighwayShoulder", new System.Guid("24ed1d19-e6f0-40ea-b3b0-96a9424ca7d0")),
            new DevExpress.XtraBars.BarManagerCategory("CorridorSpeed", new System.Guid("ee7bf2c7-e18e-4141-a012-793442ab7112"))});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.m_barButtonItemFixed,
            this.m_barButtonItemMobile,
            this.m_barButtonItemHSFixed,
            this.m_barButtonItemHSMobile,
            this.m_barButtonItemNarrowAngle,
            this.m_barButtonItemWideAngle});
            this.barManager1.MaxItemId = 11;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(884, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 415);
            this.barDockControlBottom.Size = new System.Drawing.Size(884, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 415);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(884, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 415);
            // 
            // m_barButtonItemFixed
            // 
            this.m_barButtonItemFixed.Caption = "Sabit";
            this.m_barButtonItemFixed.CategoryGuid = new System.Guid("9d98dccb-b7b5-4a7e-8a3d-2f0e79765e75");
            this.m_barButtonItemFixed.Glyph = global::EDSFactory.Properties.Resources._fixed;
            this.m_barButtonItemFixed.Id = 5;
            this.m_barButtonItemFixed.Name = "m_barButtonItemFixed";
            this.m_barButtonItemFixed.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.m_barButtonItemFixed_ItemClick);
            // 
            // m_barButtonItemMobile
            // 
            this.m_barButtonItemMobile.Caption = "Mobil";
            this.m_barButtonItemMobile.CategoryGuid = new System.Guid("9d98dccb-b7b5-4a7e-8a3d-2f0e79765e75");
            this.m_barButtonItemMobile.Glyph = global::EDSFactory.Properties.Resources.mobile;
            this.m_barButtonItemMobile.Id = 6;
            this.m_barButtonItemMobile.Name = "m_barButtonItemMobile";
            this.m_barButtonItemMobile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.m_barButtonItemMobile_ItemClick);
            // 
            // m_barButtonItemNarrowAngle
            // 
            this.m_barButtonItemNarrowAngle.Caption = "Dar Açılı";
            this.m_barButtonItemNarrowAngle.CategoryGuid = new System.Guid("ee7bf2c7-e18e-4141-a012-793442ab7112");
            this.m_barButtonItemNarrowAngle.Glyph = global::EDSFactory.Properties.Resources.narrow;
            this.m_barButtonItemNarrowAngle.Id = 9;
            this.m_barButtonItemNarrowAngle.Name = "m_barButtonItemNarrowAngle";
            this.m_barButtonItemNarrowAngle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.m_barButtonItemNarrowAngle_ItemClick);
            // 
            // m_barButtonItemWideAngle
            // 
            this.m_barButtonItemWideAngle.Caption = "Geniş Açılı";
            this.m_barButtonItemWideAngle.CategoryGuid = new System.Guid("ee7bf2c7-e18e-4141-a012-793442ab7112");
            this.m_barButtonItemWideAngle.Glyph = global::EDSFactory.Properties.Resources.wide;
            this.m_barButtonItemWideAngle.Id = 10;
            this.m_barButtonItemWideAngle.Name = "m_barButtonItemWideAngle";
            this.m_barButtonItemWideAngle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.m_barButtonItemWideAngle_ItemClick);
            // 
            // m_barSubItemFixed
            // 
            this.m_barSubItemFixed.Caption = "Sabit";
            this.m_barSubItemFixed.CategoryGuid = new System.Guid("728a8a89-a08b-44be-9154-edc0190af1ff");
            this.m_barSubItemFixed.Glyph = global::EDSFactory.Properties.Resources._fixed;
            this.m_barSubItemFixed.Id = 0;
            this.m_barSubItemFixed.ItemAppearance.Normal.Options.UseImage = true;
            this.m_barSubItemFixed.Name = "m_barSubItemFixed";
            this.m_barSubItemFixed.ShowMenuCaption = true;
            // 
            // m_barSubItemMobile
            // 
            this.m_barSubItemMobile.Caption = "Mobil";
            this.m_barSubItemMobile.CategoryGuid = new System.Guid("728a8a89-a08b-44be-9154-edc0190af1ff");
            this.m_barSubItemMobile.Glyph = global::EDSFactory.Properties.Resources.mobile;
            this.m_barSubItemMobile.Id = 1;
            this.m_barSubItemMobile.Name = "m_barSubItemMobile";
            // 
            // m_popupMenuParking
            // 
            this.m_popupMenuParking.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.m_barButtonItemFixed),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_barButtonItemMobile)});
            this.m_popupMenuParking.Manager = this.barManager1;
            this.m_popupMenuParking.Name = "m_popupMenuParking";
            // 
            // m_popupMenuCorridorSpeed
            // 
            this.m_popupMenuCorridorSpeed.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.m_barButtonItemNarrowAngle),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_barButtonItemWideAngle)});
            this.m_popupMenuCorridorSpeed.Manager = this.barManager1;
            this.m_popupMenuCorridorSpeed.Name = "m_popupMenuCorridorSpeed";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 415);
            this.Controls.Add(this.tileControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EDS Fabrikası";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.m_popupMenuHighwayShoulder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_popupMenuParking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_popupMenuCorridorSpeed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.FormAssistant formAssistant1;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraEditors.TileControl tileControl1;
        private DevExpress.XtraEditors.TileGroup tileGroup1;
        private DevExpress.XtraEditors.TileItem m_tileItemParking;
        private DevExpress.XtraEditors.TileItem m_tileItemCorridorSpeed;
        private DevExpress.XtraEditors.TileItem m_tileItemHighwayShoulder;
        private DevExpress.XtraEditors.TileGroup tileGroup2;
        private DevExpress.XtraEditors.TileItem m_tileItemPTZCameraControl;
        private DevExpress.XtraEditors.TileItem m_tileItemTimeSync;
        private DevExpress.XtraEditors.TileItem m_tileItemSettings;
        private DevExpress.XtraBars.PopupMenu m_popupMenuHighwayShoulder;
        private DevExpress.XtraBars.BarSubItem m_barSubItemMobile;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarSubItem m_barSubItemFixed;
        private DevExpress.XtraBars.PopupMenu m_popupMenuParking;
        private DevExpress.XtraBars.BarButtonItem m_barButtonItemFixed;
        private DevExpress.XtraBars.BarButtonItem m_barButtonItemMobile;
        private DevExpress.XtraBars.BarButtonItem m_barButtonItemHSFixed;
        private DevExpress.XtraBars.BarButtonItem m_barButtonItemHSMobile;
        private DevExpress.XtraBars.BarButtonItem m_barButtonItemNarrowAngle;
        private DevExpress.XtraBars.BarButtonItem m_barButtonItemWideAngle;
        private DevExpress.XtraBars.PopupMenu m_popupMenuCorridorSpeed;
        private DevExpress.XtraEditors.TileItem m_tileItemStanding;
        private DevExpress.XtraEditors.TileItem m_tileItemOffset;
        private DevExpress.XtraEditors.TileItem m_tileItemAbout;
        private DevExpress.XtraEditors.TileItem m_tileItemVideoBuffer;

    }
}


using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDSFactory 
{
    class DisplayManager
    {
        public static void OpeningForm()
        {
            Settings.WorkPlanAndMainSettings wpams = Settings.WorkPlanAndMainSettings.Singleton();
            wpams = wpams.DeSerialize(wpams);

            bool autoStartProgram = wpams.m_startAuto;
            string autoStartProgramName = wpams.m_startAutoProgramName; 

            if (autoStartProgram)
            {
                switch (autoStartProgramName)
                {
                    case "Emniyet Şeridi EDS - Sabit":
                        {
                            FixedHighwayShoulder fixedHighwayShoulder = FixedHighwayShoulder.Singleton(MainForm.m_mf);
                            //fixedHighwayShoulder.MdiParent = mf;
                            fixedHighwayShoulder.Show();
                            fixedHighwayShoulder.BringToFront();

                            break;
                        }
                    case "Emniyet Şeridi EDS - Mobil":
                        {
                            MobileHighwayShoulder mobileHighwayShoulder = MobileHighwayShoulder.Singleton(MainForm.m_mf);
                            //mobileHighwayShoulder.MdiParent = mf;
                            mobileHighwayShoulder.Show();
                            mobileHighwayShoulder.BringToFront();

                            break;
                        }
                    case "Park EDS - Sabit":
                        {

                            FixedParking fixedParking = FixedParking.Singleton(MainForm.m_mf);
                            //fixedParking.MdiParent = mf;
                            fixedParking.Show();
                            fixedParking.BringToFront();


                            break;
                        }
                    case "Park EDS - Mobil":
                        {
                            MobileParking mobileParking = MobileParking.Singleton(MainForm.m_mf);
                            //mobileParking.MdiParent = mf;
                            mobileParking.Show();
                            mobileParking.BringToFront();

                            break;
                        } 
                    case "Hız Koridor EDS - Dar":
                        {
                            CorridorSpeed speedCorridor = CorridorSpeed.Singleton(MainForm.m_mf);
                            //speedCorridor.MdiParent = this;
                            speedCorridor.Show();
                            speedCorridor.BringToFront();

                            break;
                        }
                    case "Hız Koridor EDS - Geniş":
                        {
                            CorridorSpeedWide speedCorridor = CorridorSpeedWide.Singleton(MainForm.m_mf);
                            //speedCorridor.MdiParent = this;
                            speedCorridor.Show();
                            speedCorridor.BringToFront();

                            break;
                        }
                    case "Duraklama EDS":
                        {
                            Standing standing = Standing.Singleton(MainForm.m_mf);
                            ////standing.MdiParent = mf;
                            standing.Show();
                            standing.BringToFront();

                            break;
                        }
                    case "Ofset Tarama EDS":
                        {
                            Offset standing = Offset.Singleton(MainForm.m_mf);
                            ////standing.MdiParent = mf;
                            standing.Show();
                            standing.BringToFront();

                            break;
                        }
                }

                //MainForm.m_mf.SendToBack();

                //MainForm.m_mf.WindowState = FormWindowState.Minimized;
            }
        }

        public static void AutoStartHelperModuls()
        {
            Settings.WorkPlanAndMainSettings wpams = Settings.WorkPlanAndMainSettings.Singleton();
            wpams = wpams.DeSerialize(wpams);

           HashSet<int> startAutoHelperModulsIndex = wpams.StartAutoHelperModulsIndex; 

            foreach (int item in startAutoHelperModulsIndex)
            {
                switch (item)
                {
                    case 0:
                        {
                            PTZCameraControl PTZController = PTZCameraControl.Singleton(MainForm.m_mf);
                            //PTZController.MdiParent = mf;
                            PTZController.Show();
                            PTZController.BringToFront();

                            break;
                        }
                    case 1:
                        {
                            TimeSynchroniser timeSynchroniser = TimeSynchroniser.Singleton(MainForm.m_mf);
                            //PTZController.MdiParent = mf;
                            timeSynchroniser.Show();
                            timeSynchroniser.BringToFront();

                            break;
                        }
                    case 2:
                        {
                            VideoModal videoModal = VideoModal.Singleton(MainForm.m_mf);
                            //PTZController.MdiParent = mf;
                            videoModal.Show();
                            videoModal.BringToFront();

                            break;
                        }
                }
            }
        }


        public static void RichTextBoxInvoke(RichEditControl richTextBox, string infoText, Color selectionColor)
        {
            try
            {

                if (!richTextBox.IsHandleCreated || richTextBox.IsDisposed)
                    return;

                if (richTextBox.InvokeRequired)
                    richTextBox.Invoke((MethodInvoker)delegate
                    {

                        //richTextBox.Document.SelectionColor = selectionColor;
                        richTextBox.Document.AppendText(infoText + "\n" + "\n");

                    });
                else
                {
                    //richTextBox.Document.SelectionColor = selectionColor;
                    richTextBox.Document.AppendText(infoText + "\n" + "\n");
                }
            }
            catch
            {
            }
        }

        public static void LabelInvoke(LabelControl label, string text)
        {  
            if (label.InvokeRequired)
                label.Invoke((MethodInvoker)delegate
                {
                    label.Text = text;
                });
            else
            {
                label.Text = text;
            } 
        }

        public static void LabelInvoke1(LabelControl label, string text)
        {
            if (label.InvokeRequired)
                label.Invoke((MethodInvoker)delegate
                {
                    label.Text = text;
                });
            else
            {
                label.Text = text;
            }
        }

        public static void GridViewInvoke( GridView gridView, string text)
        {

            //gridView.

            //if (gridView.InvokeRequired)
            //    gridView.Invoke((MethodInvoker)delegate
            //    {
            //        //label.Text = text;
            //    });
            //else
            //{
            //    //label.Text = text;
            //}
        }
    }
}

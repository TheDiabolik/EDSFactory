using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace EDSFactory
{
    class ImageSize
    {
        public static void ReSizeImageInList(string imagePath, List<string> imageNamesToResize, string thumbNailNamePath, List<string> thumbNailNamesName, Size size)
        {
            try
            {
                ImageList imagesToResize = new ImageList();

                imagesToResize.TransparentColor = Color.Transparent;
                imagesToResize.ColorDepth = ColorDepth.Depth32Bit;
                imagesToResize.ImageSize = size;


                for (int i = 0; i < imageNamesToResize.Count; i++)
                {
                    bool isInUse = false;

                    do
                    {
                        isInUse = FileOperation.IsFileLocked(imagePath + "\\" + imageNamesToResize[i]);

                        if (isInUse)
                            Thread.Sleep(400);

                    } while (isInUse);

                    using (FileStream myStream = new FileStream(imagePath + "\\" + imageNamesToResize[i], FileMode.Open))
                    {
                        imagesToResize.Images.Add(Image.FromStream(myStream));
                    }
                }

                for (int i = 0; i < imagesToResize.Images.Count; i++)
                {
                    Image temp = imagesToResize.Images[i];
                    string name = thumbNailNamePath + "\\" + thumbNailNamesName[i];
                    temp.Save(name);
                    temp.Dispose();
                }

                imagesToResize.Dispose();
            }
            catch (Exception ex)
            {
                Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "void ReSizeImageInList");
                throw new Exception(ExceptionMessages.ReSizeImageExceptionMessage, ex);
            }
        }

        public static Task ReSizeImageInListAsync(string imagePath, List<string> imageNamesToResize, string thumbNailNamePath, List<string> thumbNailNamesName, Size size)
        {
            return Task.Factory.StartNew(() =>
                    {
                        try
                        {

                            ImageList imagesToResize = new ImageList();

                            imagesToResize.TransparentColor = Color.Transparent;
                            imagesToResize.ColorDepth = ColorDepth.Depth32Bit;
                            imagesToResize.ImageSize = size;
                           

                            for (int i = 0; i < imageNamesToResize.Count; i++)
                            {
                                using (FileStream myStream = new FileStream(imagePath + "\\" + imageNamesToResize[i], FileMode.Open))
                                {
                                    imagesToResize.Images.Add(Image.FromStream(myStream));
                                }
                            }

                            for (int i = 0; i < imagesToResize.Images.Count; i++)
                            {
                                Image temp = imagesToResize.Images[i];
                                string name = thumbNailNamePath + "\\" + thumbNailNamesName[i];
                                temp.Save(name);
                                temp.Dispose();
                            }

                            imagesToResize.Dispose();
                        }
                        catch (Exception ex)
                        {
                            Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "Task ReSizeImageInListAsync");
                            throw new Exception(ExceptionMessages.ReSizeImageExceptionMessage, ex);
                        }
                    });
        }

        private static Image ReSizeImage(Image ImgToReSize, Size size)
        {
            try
            {
                int sourceWidth = ImgToReSize.Width;
                int sourceHeight = ImgToReSize.Height;

                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)size.Width / (float)sourceWidth);
                nPercentH = ((float)size.Height / (float)sourceHeight);

                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;

                int destWidth = (int)(sourceWidth * nPercent);
                int destHeight = (int)(sourceHeight * nPercent);

                Bitmap b = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage((Image)b);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(ImgToReSize, 0, 0, destWidth, destHeight);
                g.Dispose();

                return (Image)b;
            }
            catch (Exception ex)
            {
                Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.TargetSite.ToString(), "Image ReSizeImage(Image ImgToReSize, Size size)");
                throw new Exception(ExceptionMessages.ReSizeImageExceptionMessage, ex);
            }
        }














        //private static void ReSizeImage(string imagePath, string imageName, string thumbNailNamePath, string thumbnailName, Size size)
        //{
        //    //try
        //    //{
        //    int sourceWidth = orgimg.Width;
        //    int sourceHeight = orgimg.Height;

        //    float nPercent = 0;
        //    float nPercentW = 0;
        //    float nPercentH = 0;

        //    nPercentW = ((float)size.Width / (float)sourceWidth);
        //    nPercentH = ((float)size.Height / (float)sourceHeight);

        //    if (nPercentH < nPercentW)
        //        nPercent = nPercentH;
        //    else
        //        nPercent = nPercentW;

        //    int destWidth = (int)(sourceWidth * nPercent);
        //    int destHeight = (int)(sourceHeight * nPercent);

        //    Bitmap b = new Bitmap(destWidth, destHeight);
        //    Graphics g = Graphics.FromImage((Image)b);
        //    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

        //    g.DrawImage(orgimg, 0, 0, destWidth, destHeight);
        //    g.Dispose();

        //    b.Save(thumbNailNamePath + "\\" + thumbnailName, ImageFormat.Jpeg);
        //    b.Dispose();

        //    orgimg.Dispose();
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    throw new Exception(ExceptionMessages.ReSizeImageExceptionMessage, ex);
        //    //}
        //}
    }
}

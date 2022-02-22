using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSFactory
{
    class BitmapOperation
    {
        public static byte[] BitmapToByte(Bitmap img)
        { lock(img)
            {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
            }
        }

        public static Bitmap ByteToBitmap(byte[] img)
        {
            lock(img)
            {
                Bitmap bmp;

                using (var ms = new MemoryStream(img))
                {
                    bmp = new Bitmap(ms);
                }

                return bmp;
            }
            
        }
    }
}

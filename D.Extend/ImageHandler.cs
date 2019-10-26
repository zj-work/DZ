using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Extend
{
    /// <summary>
    /// 图像处理类
    /// </summary>
    public class ImageHandler
    {
        /// <summary>
        /// 矩阵转换为Bitmap对象
        /// </summary>
        /// <param name="pixelMatrix">像素矩阵</param>
        /// <returns></returns>
        public Bitmap Matrix2Bitmap(byte[,] pixelMatrix)
        {
            if (pixelMatrix == null) { throw new Exception("像素矩阵不能为空"); }
            //创建Bitmap对象以及对应的数据对象
            int height = pixelMatrix.GetLength(0);
            int width = pixelMatrix.GetLength(1);
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            //声明变量
            int stride = bmpData.Stride;
            int offset = stride - width;
            IntPtr iPtr = bmpData.Scan0;
            int scanBytes = stride * height;
            byte[] pixelValues = new byte[scanBytes];
            int posScan = 0;

            //将像素矩阵中的值复制到数据对象中
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    pixelValues[posScan++] = pixelMatrix[i, j];
                }
                posScan += offset;
            }
            System.Runtime.InteropServices.Marshal.Copy(pixelValues, 0, iPtr, scanBytes);
            bmp.UnlockBits(bmpData);

            // 生成位图的索引表，从而生成灰度图  
            ColorPalette tempPalette;
            using (Bitmap tempBmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
            {
                tempPalette = tempBmp.Palette;
            }
            for (int i = 0; i < 256; i++)
            {
                tempPalette.Entries[i] = Color.FromArgb(i, i, i);
            }
            bmp.Palette = tempPalette;

            return bmp;
        }

        /// <summary>
        /// 矩阵转换为Bitmap对象
        /// </summary>
        /// <param name="pixelMatrix">像素矩阵</param>
        /// <returns></returns>
        public Bitmap Matrix2Bitmap(byte[,] rMatrix, byte[,] gMatrix, byte[,] bMatrix)
        {
            if (rMatrix == null || gMatrix == null || bMatrix == null) { throw new Exception("像素矩阵不能为空"); }
            //创建Bitmap对象以及对应的数据对象
            int height = rMatrix.GetLength(0);
            int width = rMatrix.GetLength(1);
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            //声明变量
            int stride = bmpData.Stride;
            int offset = stride - width * 3;
            IntPtr iPtr = bmpData.Scan0;
            int scanBytes = stride * height;
            byte[] pixelValues = new byte[scanBytes];
            int posScan = 0;

            //将像素矩阵中的值复制到数据对象中
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    pixelValues[posScan] = rMatrix[i, j];
                    pixelValues[posScan + 1] = gMatrix[i, j];
                    pixelValues[posScan + 2] = bMatrix[i, j];
                    posScan += 3;
                }
                posScan += offset;
            }
            System.Runtime.InteropServices.Marshal.Copy(pixelValues, 0, iPtr, scanBytes);
            bmp.UnlockBits(bmpData);

            return bmp;
        }

    }
}

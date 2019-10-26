using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Extend
{
    /// <summary>
    /// 数据类型转换
    /// </summary>
    public class ConvertUtils
    {
        public static byte Int2Byte(int num)
        {
            return BitConverter.GetBytes(num).LastOrDefault();
        }

        public static byte[] Int2Byte(int[] array)
        {
            return Array.ConvertAll<int, byte>(array, delegate (int item) { return Int2Byte(item); });
        }

        public static byte[,] Int2Byte(int[,] array)
        {
            int height = array.GetLength(0);
            int width = array.GetLength(1);
            byte[,] res = new byte[height, width];
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    res[i, j] = Int2Byte(array[i, j]);
                }
            }
            return res;
        }
    }
}

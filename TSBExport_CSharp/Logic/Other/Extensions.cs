using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;

namespace TSBExport_CSharp.Other
{
    public static class Extensions
    {
        public static long Next(this Random rand, long min, long max)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return Math.Abs(longRand % (max - min)) + min;
        }

        public static object[,] ToExcelArray(this DataTable data, int interval=0, Action<int,int> callback=null)
        {
            var rows = data.Rows.Count;
            var cols = data.Columns.Count;

            var result = new object[rows, cols];
            for (int y = 0; y < rows; ++y)
            {
                for (int x = 0; x < cols; ++x)
                {
                    result[y, x] = data.Rows[y][x];
                }

                if (interval > 0)
                {
                    callback?.Invoke(y,rows);
                    Thread.Sleep(20);
                }
            }

            return result;
        }

        public static T[,] ToExcelArray<T>(this IEnumerable<T> source)
        {
            var array = source.ToArray();

            var result = new T[1, array.Length];
            for (var i = 0; i < array.Length; ++i)
            {
                result[0, i] = array[i];
            }

            return result;
        }
    }
}
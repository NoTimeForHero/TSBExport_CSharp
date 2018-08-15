using System;

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
    }
}
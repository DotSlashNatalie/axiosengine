using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axios.Engine
{
    // Implemenation of an XORShift
    // http://en.wikipedia.org/wiki/Xorshift
    // http://stackoverflow.com/questions/6275593/how-to-write-you-own-random-number-algorithm
    public class AxiosRandom
    {
        private static int x;
        private static int y;
        private static int z;
        private static int w;
        private static int t = 0;

        public void init(int x, int y, int z, int w)
        {
            AxiosRandom.x = x;
            AxiosRandom.y = y;
            AxiosRandom.z = z;
            AxiosRandom.w = w;
        }

        public static void init()
        {
            AxiosRandom.x = generateVector();
            AxiosRandom.y = generateVector();
            AxiosRandom.z = generateVector();
            AxiosRandom.w = generateVector();
        }

        public static int next()
        {
            t = x ^ (x << 11);
            x = y; y = z; z = w;
            return w = w ^ (w >> 19) ^ (t ^ (t >> 8));
        }

        private static int generateVector()
        {
            int[] x = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] val = new int[9];
            for (int i = 0; i < 9; i++)
                val[i] = x[GameServices.GetService<Random>().Next(x.Count() - 1)];
            return int.Parse(String.Join("", val));
        }


    }
}

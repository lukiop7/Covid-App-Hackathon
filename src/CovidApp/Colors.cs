using ChartJs.Blazor.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace CovidApp
{
    public static class Colors
    {
        public static string[] RandomColors(int quantity)
        {
            Random r = new Random();

            string[] s = new string[quantity];

            for(int i = 0; i < quantity; i++)
            s[i] = ColorUtil.ColorHexString((byte)r.Next(40, 250), (byte)r.Next(40, 250), (byte)r.Next(40, 250));

            return s;
        }
        public static string[] GradientColors(int quantity, Vector3 rgbInts)
        {
            string[] s = new string[quantity];

            float length = rgbInts.Length();
            Vector3 normal = Vector3.Normalize(rgbInts);
            Vector3 tmp;


            for (int i = 0; i < quantity; i++)
            {
                tmp = normal * length * ((quantity + 2) / (float)(i + 1));
                s[i] = ColorUtil.ColorHexString((byte)tmp.X, (byte)tmp.Y, (byte)tmp.Z);
            }

            return s;
        }
        public static string[] GradientColors(int quantity, int r, int g, int b)
        {
            string[] s = new string[quantity];

            Vector3 rgbInts = new Vector3(r, g, b);

            float length = rgbInts.Length();
            Vector3 normal = Vector3.Normalize(rgbInts);
            Vector3 tmp;


            for (int i = 0; i < quantity; i++)
            {
                tmp = rgbInts * (((quantity - i) / (float)(quantity + 2)));
                s[i] = ColorUtil.ColorHexString((byte)tmp.X, (byte)tmp.Y, (byte)tmp.Z);
                Console.WriteLine(s[i]);
            }

            return s;
        }
    }
}

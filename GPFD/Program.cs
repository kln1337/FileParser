using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Formatting input file
/// Distance    Del
/// 100     \t  300
/// 200     \t  200
/// 300     \t  100
/// </summary>
namespace GPFD
{
    class Program
    {
        static double[] _get_data(string aPath, int aPosition, string aSplitLine)
        {
            if (aPath != null)
            {
                try
                {
                    string __buff;
                    StreamReader __file = new StreamReader(aPath);
                    List<double> __out = new List<double>();
                    double __temp;

                    do
                    {
                        __buff = __file.ReadLine();

                        if (__buff != null)
                        {
                            __temp = _find_number(ref __buff, aPosition, aSplitLine);
                            if (!Double.IsNaN(__temp)) __out.Add(__temp);
                            else throw new Exception("Format input file is wrong");
                        }

                    } while (__buff != null);

                    return __out.ToArray();
                }
                catch (Exception __e) { Console.WriteLine(__e.Message); return null; }
            }
            return null;
        }

        static double _find_number(ref string aString, int aPosition, string aSplit)
        {
            if (aString != null)
            {
                string[] __buff = aString.Split(aSplit);

                if (__buff == null || aPosition >= __buff.Length) return double.NaN;
                else
                {
                    if (double.TryParse(__buff[aPosition], out double __out) == false) return double.NaN;
                    else return __out;
                }
            }
            return double.NaN;
        }

        public static void CalculateAB(double[] aY, double[] aX, out double A, out double B)
        {
            A = B = double.NaN;

            if (aY != null && aX != null && aY.Length == aX.Length)
            {
                double __sum_x, __sum_y, __sum_x2, __sum_xy;
                __sum_x = __sum_y = __sum_x2 = __sum_xy = 0;

                for (int __i = 0; __i < aX.Length; __i++)
                {
                    __sum_x += aX[__i];
                    __sum_y += aY[__i];
                    __sum_x2 += aX[__i] * aX[__i];
                    __sum_xy += aX[__i] * aY[__i];
                }

                B = (__sum_x * __sum_xy - __sum_x2 * __sum_y) / (__sum_x * __sum_x - aX.Length * __sum_x2);
                A = (__sum_y - B * aX.Length) / __sum_x;
            }
        }
        public static void GetData(out double[] aDistance, out double[] aDelta, string aPath)
        {
            aDelta = aDistance = null;

            if (aPath != null)
            {
                aDistance = _get_data(aPath, 0, "\t");
                aDelta  = _get_data(aPath, 1, "\t");
            }
        }

        static void Main(string[] aArgs)
        {
            if (aArgs != null && aArgs.Length > 0)
            {
                GetData(out double[] __distance, out double[] __delta, aArgs[0]);

                if (__distance != null && __delta != null)
                {
                    foreach (var __i in __distance) Console.WriteLine(__i);
                    foreach (var __i in __delta) Console.WriteLine(__i);
                }

                CalculateAB(__delta, __distance, out double A, out double B);
                Console.WriteLine("A:{0}; B:{1}", A, B);
            }
            else Console.WriteLine("Input params is empty");
        }
    }
}

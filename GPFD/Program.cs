using System;
using System.IO;
using System.Collections.Generic;

namespace GPFD
{
    class Program
    {
        static List<double> GetData(string aPath, int aPosition, string aSplitLine)
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
                            if (__temp != double.NaN) __out.Add(__temp);
                        }

                    } while (__buff != null);

                    return __out;
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
                    double.TryParse(__buff[aPosition], out double __out);
                    return __out;
                }
            }
            return double.NaN;
        }

        static void Main(string[] aArgs)
        {
            if (aArgs != null && aArgs.Length > 0) GetData(aArgs[0], 1, "\t");
        }
    }
}

using System;

namespace MeTestPrj
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "";
            ChangeStr(str);

            Console.WriteLine(str);

        }

        static void ChangeStr(string str)
        {
            str += "change";
        }
    }
}

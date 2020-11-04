using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "dsajkdbajkdasbksf";
            Test test = new Test() { Name = str };
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(test.GetHashCode());
            }
        }
        
    }
}

using System;
using System.Linq;
using System.Collections.Generic;

namespace _02.WordCruncher
{
    class Program
    {
        public static Dictionary<int, List<string>> dict = new Dictionary<int, List<string>>();
        public static int attempt = 0;
        public static string word = "";
        static void Main(string[] args)
        {
            //fi, fil, ra, rat, i, o, n, on, iltra, in, tr, tra, tion, filt, filtra
            //infiltration

            //al, la, ab, lab, bala, bal, a, b, l, or, por, tok, to, k 
            //alabalaportokala

            //a, b, ab, ba
            //aba

            List<string> input = Console.ReadLine()
            .Split(", ")
            .Distinct()
            .OrderBy(s => s)
            .ToList();

            string target = Console.ReadLine();
            word = target;
            dict[0] = new List<string>();

            FindMatch(input, target);

            SortedSet<string> set = new SortedSet<string>();

            foreach (var item in dict.Values)
            {
                if (item.Count > 0)
                {
                    string str = "";
                    foreach (var a in item)
                    {
                        str += a + " ";
                    }
                    set.Add(str);
                    //Console.WriteLine(string.Join(" ", item));
                }
            }
            foreach (var item in set)
            {
                string b = GenerateWord(item);
                if (item.Length > 0 && word == b)
                {
                    Console.WriteLine(string.Join(" ", item));
                }
            }
        }

        private static string GenerateWord(string item)
        {
            string str = "";
            foreach (var ch in item)
            {
                if (ch != ' ')
                {
                    str += ch;
                }
            }

            return str;
        }

        private static void FindMatch(List<string> input, string target)
        {
            if (target == "")
            {
                attempt += 1;
                var result = new List<string>(dict[attempt - 1]);
                dict[attempt] = result;
                return;
            }

            for (int i = 0; i < input.Count; i++)
            {
                if (target.StartsWith(input[i]))
                {
                    string item = input[i];
                    dict[attempt].Add(item);
                    target = target.Substring(item.Length);
                    //input.RemoveAt(i);

                    FindMatch(input, target);

                    //input.Insert(i, item);
                    dict[attempt].Remove(item);
                    target = item + target;
                }
            }
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;

namespace _02.WordCruncher
{
    class Program
    {
        public static Dictionary<int, List<string>> dict = new Dictionary<int, List<string>>();
        public static int attempt = 0;
        static void Main(string[] args)
        {
            List<string> input = Console.ReadLine()
                .Split(new char[] {' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Distinct()
                .OrderBy(s => s)
                .ToList();

            string target = Console.ReadLine();

            FindMatch(input, target);

            foreach (var item in dict.Values)
            {
                if (item.Count > 0)
                {
                    Console.WriteLine(string.Join(" ", item));
                }
            }
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
            if (input.Count == 0)
            {
                return;
            }

            for (int i = 0; i < input.Count; i++)
            {
                if (target.StartsWith(input[i]))
                {
                    if (!dict.ContainsKey(attempt))
                    {
                        dict[attempt] = new List<string>();
                    }
                    dict[attempt].Add(input[i]);

                    target = target.Substring(input[i].Length);
                    string item = input[i];
                    input.RemoveAt(i);

                    FindMatch(input, target);

                    input.Insert(i, item);
                    dict[attempt].Remove(item);
                    target = item + target;
                }
            }
        }
    }
}

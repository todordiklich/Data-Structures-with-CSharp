using System;
using System.Linq;
using System.Collections.Generic;

namespace _02.WordCruncher
{
    class Program
    {
         public static List<Node> permutations = new List<Node>();
        public static SortedSet<string> results = new SortedSet<string>();
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(", ");
            var target = Console.ReadLine();

            permutations = GeneratePermutations(input.OrderBy(s => s).ToList(), target);

            foreach (var path in GetAllPaths())
            {
                var result = string.Join(' ', path);
                if (!results.Contains(result))
                {
                    results.Add(result);
                }
            }

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        private static IEnumerable<IEnumerable<string>> GetAllPaths()
        {
            List<string> way = new List<string>();

            foreach (var key in VisitPath(permutations, new List<string>()))
            {
                if (key == null)
                {
                    yield return way;
                    way = new List<string>();
                }
                else
                {
                    way.Add(key);
                }
            }
        }

        private static IEnumerable<string> VisitPath(List<Node> permutations, List<string> path)
        {
            if (permutations == null)
            {
                foreach (var pathItem in path)
                {
                    yield return pathItem;
                }
                yield return null;
            }
            else
            {
                foreach (var node in permutations)
                {
                    path.Add(node.Key);
                    foreach (var item in VisitPath(node.Value, path))
                    {
                        yield return item;
                    }

                    path.RemoveAt(path.Count - 1);
                }
            }
        }

        private static List<Node> GeneratePermutations(List<string> input, string target)
        {
            if (string.IsNullOrEmpty(target) || input.Count == 0)
            {
                return null;
            }

            List<Node> returnValues = null;

            for (int i = 0; i < input.Count; i++)
            {
                var key = input[i];

                if (target.StartsWith(key))
                {
                    var node = new Node()
                    {
                        Key = key,
                        Value = GeneratePermutations(input.Where((s, index) => index != i).ToList(), target.Substring(key.Length))
                    };

                    if (node.Value == null && node.Key != target)
                    {
                        continue;
                    }

                    if (returnValues == null)
                    {
                        returnValues = new List<Node>();
                    }

                    returnValues.Add(node);
                }
            }

            return returnValues;
        }
    }

    internal class Node
    {
        public string Key { get; set; }
        public List<Node> Value { get; set; }
    }
}

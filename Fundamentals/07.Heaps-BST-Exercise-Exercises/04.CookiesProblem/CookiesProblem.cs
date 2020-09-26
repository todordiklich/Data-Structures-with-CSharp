using System;
using Wintellect.PowerCollections;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int k, int[] cookies)
        {
            var bag = new OrderedBag<int>();

            foreach (var cookie in cookies)
            {
                bag.Add(cookie);
            }

            int steps = 0;
            int minSweetness = bag.GetFirst();

            while (minSweetness < k && bag.Count > 1)
            {
                int firstSweet = bag.RemoveFirst();
                int secondSweet = bag.RemoveFirst();

                int sweetness = firstSweet + (2 * secondSweet);
                bag.Add(sweetness);

                minSweetness = bag.GetFirst();
                steps++;
            }

            return minSweetness < k ? -1 : steps;
        }
    }
}

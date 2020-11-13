namespace _01.Microsystem
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Microsystems : IMicrosystem
    {
        private List<Computer> computers = new List<Computer>();
        private Dictionary<int, Computer> byNumber = new Dictionary<int, Computer>();
        private Dictionary<string, List<Computer>> byBrand = new Dictionary<string, List<Computer>>();
        private Dictionary<string, List<Computer>> byColor = new Dictionary<string, List<Computer>>();

        public void CreateComputer(Computer computer)
        {
            CheckIfNumberExist(computer.Number);

            //Add in computers
            computers.Add(computer);

            //Add in byNumber
            byNumber[computer.Number] = computer;

            //Add in byBrand
            if (!byBrand.ContainsKey(computer.Brand.ToString()))
            {
                byBrand[computer.Brand.ToString()] = new List<Computer>();
            }
            byBrand[computer.Brand.ToString()].Add(computer);

            //Add in byColor
            if (!byColor.ContainsKey(computer.Color.ToString()))
            {
                byColor[computer.Color.ToString()] = new List<Computer>();
            }
            byColor[computer.Color.ToString()].Add(computer);
        }

        public bool Contains(int number)
        {
            return byNumber.ContainsKey(number);
        }

        public int Count()
        {
            return computers.Count;
        }

        public Computer GetComputer(int number)
        {
            CheckIfNumberDontExist(number);

            return byNumber[number];
        }

        public void Remove(int number)
        {
            CheckIfNumberDontExist(number);

            Computer computerToRemove = byNumber[number];

            //Remove in computers
            computers.Remove(computerToRemove);

            //Remove in byNumber
            byNumber.Remove(number);

            //Remove in byBrand
            byBrand[computerToRemove.Brand.ToString()].Remove(computerToRemove);
            if (byBrand[computerToRemove.Brand.ToString()].Count == 0)
            {
                byBrand.Remove(computerToRemove.Brand.ToString());
            }

            //Remove in byColor
            byColor[computerToRemove.Color.ToString()].Remove(computerToRemove);
            if (byColor[computerToRemove.Color.ToString()].Count == 0)
            {
                byColor.Remove(computerToRemove.Color.ToString());
            }
        }

        public void RemoveWithBrand(Brand brand)
        {
            if (!byBrand.ContainsKey(brand.ToString()))
            {
                throw new ArgumentException();
            }

            List<Computer> computersToremove = new List<Computer>(byBrand[brand.ToString()]);

            //It can be optimized
            foreach (var computer in computersToremove)
            {
                Remove(computer.Number);
            }
        }

        public void UpgradeRam(int ram, int number)
        {
            CheckIfNumberDontExist(number);

            Computer computer = byNumber[number];
            if (computer.RAM < ram)
            {
                computer.RAM = ram;
            }
        }

        public IEnumerable<Computer> GetAllFromBrand(Brand brand)
        {
            if (!byBrand.ContainsKey(brand.ToString()))
            {
                return new List<Computer>();
            }

            return byBrand[brand.ToString()].OrderByDescending(c => c.Price);
        }

        public IEnumerable<Computer> GetAllWithScreenSize(double screenSize)
        {
            return computers.Where(c => c.ScreenSize == screenSize).OrderByDescending(c => c.Number);
        }

        public IEnumerable<Computer> GetAllWithColor(string color)
        {

            if (!byColor.ContainsKey(color))
            {
                return new List<Computer>();
            }

            return byColor[color].OrderByDescending(c => c.Price);
        }

        public IEnumerable<Computer> GetInRangePrice(double minPrice, double maxPrice)
        {
            return computers.Where(c => minPrice <= c.Price && c.Price <= maxPrice)
                .OrderByDescending(c => c.Price);
        }

        private void CheckIfNumberExist(int number)
        {
            if (byNumber.ContainsKey(number))
            {
                throw new ArgumentException();
            }
        }

        private void CheckIfNumberDontExist(int number)
        {
            if (!byNumber.ContainsKey(number))
            {
                throw new ArgumentException();
            }
        }
    }
}

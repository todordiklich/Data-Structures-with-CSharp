using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Test
    {
        public string Name { get; set; }
        public override bool Equals(object other)
        {
            Test element = (Test)other;
            bool equals = this.Name == element.Name;
            return equals;
        }

        public override int GetHashCode()
        {
            return this.CombineHashCodes(this.Name.GetHashCode(), this.Name.GetHashCode());
        }

        private int CombineHashCodes(int h1, int h2)
        {
            return ((h1 << 5) + h1) ^ h2;
        }
    }
}

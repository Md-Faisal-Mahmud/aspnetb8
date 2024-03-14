using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEvents
{
    public class Person : IComparable<Person>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public int CompareTo(Person? other)
        {
            if (Name == other.Name)
            {
                if (Age == other.Age)
                    return 0;
                else if (Age > other.Age)
                    return 1;
                else
                    return -1;
            }
            else
                return Name.CompareTo(other.Name);
        }
    }
}

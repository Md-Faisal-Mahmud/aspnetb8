using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEvents
{
    public class PersonCompare : IComparer<Person>
    {
        public int Compare(Person? x, Person? y)
        {
            if (x.Name == y.Name)
            {
                if (x.Age == y.Age)
                    return 0;
                else if (x.Age > y.Age)
                    return 1;
                else
                    return -1;
            }
            else
                return x.Name.CompareTo(y.Name);
        }
    }
}

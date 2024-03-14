using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class Class2 : IClass
    {
        public void Print(object o)
        {
            PrintSomething(o);
        }

        public void PrintSomething(object o)
        {
            Console.WriteLine(o);
        }
    }
}

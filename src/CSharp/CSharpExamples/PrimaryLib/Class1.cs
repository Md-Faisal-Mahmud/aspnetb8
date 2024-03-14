using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class Class1
    {
        private IClass _class;

        public Class1(IClass @class)
        {
            _class = @class;
        }

        public void DoSomething()
        {
            Console.WriteLine("Doing Something");
            int x = 10;
            _class.Print(x);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLib
{
    public class Chart : IPlugin
    {
        public Chart(string name)
        {

        }

        private void Start()
        {
            Console.WriteLine("Running Chart");
        }
    }
}

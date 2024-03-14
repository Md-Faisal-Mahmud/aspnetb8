using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLib
{
    public class Report : IPlugin
    {
        private string _name;

        public Report(string name)
        {
            _name = name;
        }

        private void Start()
        {
            Console.WriteLine($"Running Report: {_name}");
        }
    }
}

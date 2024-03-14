using FactoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern
{
	public class Printer
	{
		public string Model { get; set; }
		public int PagePrintCapacity { get; set; }

		public static Printer Create(string model, int capacity)
		{
			return new Printer { Model = model, PagePrintCapacity = capacity };
		}
	}
}

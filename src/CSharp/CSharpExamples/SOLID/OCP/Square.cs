using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.OCP
{
	public class Square : Shape
	{
		private readonly double _side;
		public Square(double side)
		{
			_side = side;
		}
		public override double CalculateArea()
		{
			return _side * _side;
		}
	}
}

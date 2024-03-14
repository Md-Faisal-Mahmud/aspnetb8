using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.OCP
{
	public class Geometry
	{
		public double CalculateCircleArea(double radius)
		{
			return Math.PI * radius * radius;
		}

		public virtual double CalcualteSquareArea(double side)
		{
			return side * side;
		}

		public double CalculateRectangleArea(double width, double height)
		{
			return width * height;
		}
	}
}

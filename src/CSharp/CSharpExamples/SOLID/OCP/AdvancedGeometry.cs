using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.OCP
{
	public class AdvancedGeometry : Geometry
	{
		public double CalculateTriangleArea(double baseLength, double height)
		{
			throw new NotImplementedException();
		}

		public override double CalcualteSquareArea(double side)
		{
			throw new NotImplementedException();
		}
	}
}

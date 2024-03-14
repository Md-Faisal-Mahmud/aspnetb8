using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern
{
	public static class CarFactory
	{
		public static Car Create(string model)
		{
			Car car = null;
			if(model == "toyota")
			{
				car = new Toyota();
				car.model = "Corolla";
				car.color = "white";
				car.engineHorsePower = 200;
				car.tireType = "medium";
			}
			else if(model == "BMW")
			{

			}

			return car;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavorCompositionOverInheritance
{
	public class BMW
	{
		public string Color { get; set; }
		private Car _car;

		public BMW(string model, double speed, string color)		
		{
			_car = new Car { Model = model, Speed = speed };
			Color = color;
		}

		public void Start()
		{
			_car.UpdateSpeed(0);
		}

		public void Move(double speed)
		{
			_car.UpdateSpeed(speed);
		}

		public void Stop()
		{
			_car.UpdateSpeed(0);
		}

	}
}

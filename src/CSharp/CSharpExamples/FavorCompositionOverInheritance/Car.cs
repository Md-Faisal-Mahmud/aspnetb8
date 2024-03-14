using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavorCompositionOverInheritance
{
	public class Car
	{
		public string Model { get; set; }
		public double Speed { get; set; }

		public void UpdateSpeed(double speed)
		{
			if(speed >0)
				Speed = speed;
		}
	}
}

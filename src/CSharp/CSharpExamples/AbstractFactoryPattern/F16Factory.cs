using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryPattern
{
	public class F16Factory : FighterFactory
	{
		public override Fighter CreateFighter()
		{
			return new F16();
		}

		public override Weapon CreateWeapon()
		{
			return new F16Weapon();
		}
	}
}

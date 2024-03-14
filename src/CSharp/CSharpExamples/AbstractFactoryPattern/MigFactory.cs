using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryPattern
{
	public class MigFactory : FighterFactory
	{
		public override Fighter CreateFighter()
		{
			return new Mig29();
		}

		public override Weapon CreateWeapon()
		{
			return new Mig29Weapon();
		}
	}
}

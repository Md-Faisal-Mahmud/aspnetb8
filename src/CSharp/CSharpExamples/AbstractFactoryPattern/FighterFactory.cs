using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryPattern
{
	public abstract class FighterFactory
	{
		public abstract Fighter CreateFighter();
		public abstract Weapon CreateWeapon();
	}
}

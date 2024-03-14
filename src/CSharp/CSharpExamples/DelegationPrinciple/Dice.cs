using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegationPrinciple
{
	public class Dice
	{
		private readonly Random _random;

		public Dice()
		{
			_random = new Random(DateTime.Now.Second);
		}
		public int Roll()
		{
			return _random.Next(1, 6);
		}
	}
}

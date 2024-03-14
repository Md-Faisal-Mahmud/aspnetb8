using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegationPrinciple
{
	public class LudoBoard
	{
		private readonly Dice _dice;

		public LudoBoard()
		{
			_dice = new Dice();
		}
		public int RollDice()
		{
			return _dice.Roll();
		}
	}
}

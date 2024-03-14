using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
	public class GiftCardBuilder
	{
		private GiftCard _giftCard;

		public GiftCardBuilder()
		{
			_giftCard = new GiftCard();
		}

		public void AddColor(Color color)
		{
			if (color == Color.Black)
				_giftCard.color = "#000000";
			else if (color == Color.White)
				_giftCard.color = "#FFFFFF";
		}

		public void AddGlitter(string type)
		{

		}

		public void AddNote(string note)
		{

		}

		public void AddSticker(string type)
		{

		}

		public GiftCard GetCard()
		{
			return _giftCard;
		}
	}
}

using GeekShopping.CardAPI.Data.ValueObjects;

namespace GeekShopping.CardAPI.Model
{
	public class Card
	{
		public CardHeader CardHeader { get; set; }

		public IEnumerable<CardDetail> CardDetail { get; set; }
	}
}

namespace GeekShopping.CardAPI.Data.ValueObjects
{
	public class CardVO
	{
		public CardHeaderVO CardHeaderVO { get; set; }

		public IEnumerable<CardDetailsVO> CardDetailsVO { get; set; }
	}
}

using GeekShopping.CardAPI.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CardAPI.Data.ValueObjects
{
	public class CardDetailsVO
	{
		public class CardDetail
		{
			public long Id { get; set; }
			public long CardHeaderId { get; set; }

			public CardHeaderVO CardHeaderVO { get; set; }

			public long ProductId { get; set; }

			public ProductVO Product { get; set; }

			public int Count { get; set; }
		}
	}
}

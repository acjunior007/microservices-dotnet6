using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CardAPI.Data.ValueObjects
{
	public class CardHeaderVO
	{
		public long Id { get; set; }

		public string UserId { get; set; }

		public string CouponCode { get; set; }
	}
}

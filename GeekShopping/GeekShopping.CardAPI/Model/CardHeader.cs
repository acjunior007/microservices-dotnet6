using GeekShopping.CardAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CardAPI.Model
{
	[Table("CardHeader")]
	public class CardHeader : BaseEntity
	{
		[Column("UserId")]
		public string UserId { get; set; }

		[Column("CouponCode")]
		public string CouponCode { get; set; }
	}
}

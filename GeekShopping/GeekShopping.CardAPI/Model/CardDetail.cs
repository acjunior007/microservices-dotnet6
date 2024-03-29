﻿using GeekShopping.CardAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CardAPI.Model
{
	[Table("CardDetail")]
	public class CardDetail : BaseEntity
	{
		public long CardHeaderId { get; set; }

		[ForeignKey("CardHeaderId")]
		public virtual CardHeader CardHeader { get; set; }

		public long ProductId { get; set; }
		[ForeignKey("ProductId")]
		public virtual Product Product { get; set; }

		[Column("Count")]
		public int Count { get; set; }
	}
}

using GeekShopping.CardAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CardAPI.Model
{
	public class Product : BaseEntity
	{
		[Column("Name")]
		[Required]
		[StringLength(150)]
		public string Name { get; set; } = string.Empty;

		[Column("Price")]
		[Required]
		[Range(1, 10000)]
		public decimal Price { get; set; }

		[Column("Description")]
		[StringLength(500)]
		public string Description { get; set; } = string.Empty;

		[Column("CategoryName")]
		[StringLength(50)]
		public string CategoryName { get; set; } = string.Empty;

		[Column("CategoryDescription")]
		[StringLength(100)]
		public string CategoryDescription { get; set; } = string.Empty;

		[Column("ImageUrl")]
		[StringLength(250)]
		public string ImageUrl { get; set; }
	}
}

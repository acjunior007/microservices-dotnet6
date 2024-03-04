using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CardAPI.Model.Context
{
	public class MySQLContext : DbContext
	{
		public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

		public DbSet<Product> Product { get; set; }
	}
}

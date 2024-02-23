using Microsoft.EntityFrameworkCore;

namespace GeekShoppingProductAPI.Models.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                Name = "Name 2",
                Price = new decimal(19.9),
                Description = "Description 2",
                ImageUrl = "https://github.com/leandrocgsi/erudio-microservices-dotnet6/blob/main/ShoppingImages/00_no_image.jpg?raw=true",
                CategoryName = "Category Name 2",
                CategoryDescription = "Category Description 2",
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 3,
                Name = "Name 3",
                Price = new decimal(25),
                Description = "Description 3",
                ImageUrl = "https://github.com/leandrocgsi/erudio-microservices-dotnet6/blob/main/ShoppingImages/00_no_image.jpg?raw=true",
                CategoryName = "Category Name 3",
                CategoryDescription = "Category Description 3",
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 4,
                Name = "Name 4",
                Price = new decimal(5),
                Description = "Description 4",
                ImageUrl = "https://github.com/leandrocgsi/erudio-microservices-dotnet6/blob/main/ShoppingImages/00_no_image.jpg?raw=true",
                CategoryName = "Category Name 4",
                CategoryDescription = "Category Description 4",
            });
        }
    }
}

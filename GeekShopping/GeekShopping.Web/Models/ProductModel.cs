﻿namespace GeekShopping.Web.Models
{
    public class ProductModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }

        public string ImageUrl { get; set; }
    }
}

using System;

namespace Nike.Application.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }
        public DateTime CreateDate { get; set; }

        public int ProductCategoryId { get; set; }
    }
}
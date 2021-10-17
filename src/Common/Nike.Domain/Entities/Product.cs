using Nike.Domain.Common;

namespace Nike.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

    }
}

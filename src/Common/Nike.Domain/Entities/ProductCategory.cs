using Nike.Domain.Common;
using System.Collections.Generic;

namespace Nike.Domain.Entities
{
    public class ProductCategory : AuditableEntity
    {
        public ProductCategory()
        {
            Products = new List<Product>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Product> Products { get; set; }

    }
}

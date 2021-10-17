using System.Threading;
using System.Threading.Tasks;
using Nike.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Nike.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<City> Cities { get; set; }

        DbSet<District> Districts { get; set; }

        DbSet<Village> Villages { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }
        DbSet<Product> Products { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

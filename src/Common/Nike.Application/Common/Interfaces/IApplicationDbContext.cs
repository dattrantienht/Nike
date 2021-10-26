using System.Threading;
using System.Threading.Tasks;
using Nike.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Nike.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ProductCategory> ProductCategories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<TeamMember> TeamMembers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

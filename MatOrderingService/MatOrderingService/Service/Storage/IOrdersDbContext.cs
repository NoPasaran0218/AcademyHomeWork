using MatOrderingService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace MatOrderingService.Service.Storage
{
    public interface IOrdersDbContext
    {
        DbSet<Order> Orders { get; set; }
        DbSet<Product> Products { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancelationToken = default(CancellationToken));

        DatabaseFacade Database { get; }
    }
}
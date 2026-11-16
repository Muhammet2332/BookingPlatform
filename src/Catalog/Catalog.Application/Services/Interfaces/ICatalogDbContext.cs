using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Services.Interfaces
{
    public interface ICatalogDbContext
    {
        DbSet<Room> Rooms { get; }
        DbSet<RoomType> RoomTypes { get; }

        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}

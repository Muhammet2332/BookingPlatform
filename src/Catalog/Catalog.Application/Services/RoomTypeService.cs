using Catalog.Application.DTOs.Room_Types;
using Catalog.Application.Services.Interfaces;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly ICatalogDbContext _context;

        public RoomTypeService(ICatalogDbContext context)
        {
            _context = context;
        }

        public async Task<RoomTypeDto> CreateAsync(
            CreateRoomTypeDto dto,
            CancellationToken cancellationToken = default)
        {
            var nameExists = await _context.RoomTypes
                .AnyAsync(
                    x => x.Name == dto.Name,
                    cancellationToken);

            if (nameExists)
                throw new InvalidOperationException(
                    "Room type with this name already exists.");

            var roomType = new RoomType
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.RoomTypes.Add(roomType);

            await _context.SaveChangesAsync(cancellationToken);

            return new RoomTypeDto(
                roomType.Id,
                roomType.Name,
                roomType.Description);
        }

        public async Task<RoomTypeDto?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var roomType = await _context.RoomTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);

            return roomType is null
                ? null
                : new RoomTypeDto(
                    roomType.Id,
                    roomType.Name,
                    roomType.Description);
        }

        public async Task<IReadOnlyList<RoomTypeDto>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.RoomTypes
                .AsNoTracking()
                .Select(x => new RoomTypeDto(
                    x.Id,
                    x.Name,
                    x.Description))
                .ToListAsync(cancellationToken);
        }
    }
}

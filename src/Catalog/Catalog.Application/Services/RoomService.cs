using Catalog.Application.DTOs.Rooms;
using Catalog.Application.Services.Interfaces;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly ICatalogDbContext _context;

        public RoomService(ICatalogDbContext context)
        {
            _context = context;
        }

        public async Task<RoomDto> CreateAsync(
            CreateRoomDto dto,
            CancellationToken cancellationToken = default)
        {
            var roomType = await _context.RoomTypes
                .FirstOrDefaultAsync(
                    x => x.Id == dto.RoomTypeId,
                    cancellationToken);

            if (roomType is null)
                throw new KeyNotFoundException("Room type not found.");

            var roomExists = await _context.Rooms
                .AnyAsync(
                    x => x.Number == dto.Number,
                    cancellationToken);

            if (roomExists)
                throw new InvalidOperationException(
                    "Room with this number already exists.");

            var room = new Room
            {
                Number = dto.Number,
                RoomTypeId = dto.RoomTypeId,
                BasePricePerNight = dto.BasePricePerNight,
                Description = dto.Description
            };

            _context.Rooms.Add(room);

            await _context.SaveChangesAsync(cancellationToken);

            return new RoomDto(
                room.Id,
                room.Number,
                room.RoomTypeId,
                roomType.Name,
                room.BasePricePerNight,
                room.Description,
                room.Status.ToString()
            );
        }

        public async Task<RoomDto?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var room = await _context.Rooms
                .Include(x => x.RoomType)
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);

            if (room is null)
                return null;

            return new RoomDto(
                room.Id,
                room.Number,
                room.RoomTypeId,
                room.RoomType!.Name,
                room.BasePricePerNight,
                room.Description,
                room.Status.ToString()
            );
        }
    }
}

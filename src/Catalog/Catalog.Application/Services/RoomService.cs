
using Catalog.Application.DTOs.Rooms;
using Catalog.Application.Services.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Services;

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
                x => x.Id == dto.RoomTypeId && x.IsActive,
                cancellationToken);

        if (roomType is null)
            throw new KeyNotFoundException(
                "Active room type not found.");

        var number = dto.Number.Trim();

        var exists = await _context.Rooms
            .AnyAsync(x => x.Number == number, cancellationToken);

        if (exists)
            throw new InvalidOperationException(
                "Room with this number already exists.");

        var room = new Room
        {
            Number = number,
            RoomTypeId = dto.RoomTypeId,
            BasePricePerNight = dto.BasePricePerNight,
            Description = dto.Description.Trim()
        };

        _context.Rooms.Add(room);

        await _context.SaveChangesAsync(cancellationToken);

        room.RoomType = roomType;

        return MapToDto(room);
    }

    public async Task<RoomDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var room = await _context.Rooms
            .AsNoTracking()
            .Include(x => x.RoomType)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

        return room is null ? null : MapToDto(room);
    }

    public async Task<IReadOnlyList<RoomDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Rooms
            .AsNoTracking()
            .Include(x => x.RoomType)
            .Select(x => new RoomDto(
                x.Id,
                x.Number,
                x.RoomTypeId,
                x.RoomType!.Name,
                x.BasePricePerNight,
                x.Description,
                x.Status.ToString()))
            .ToListAsync(cancellationToken);
    }

    public async Task<RoomDto?> UpdateAsync(
        int id,
        UpdateRoomDto dto,
        CancellationToken cancellationToken = default)
    {
        var room = await _context.Rooms
            .Include(x => x.RoomType)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

        if (room is null)
            return null;

        var roomType = await _context.RoomTypes
            .FirstOrDefaultAsync(
                x => x.Id == dto.RoomTypeId && x.IsActive,
                cancellationToken);

        if (roomType is null)
            throw new KeyNotFoundException(
                "Active room type not found.");

        var number = dto.Number.Trim();

        var numberExists = await _context.Rooms
            .AnyAsync(
                x => x.Id != id && x.Number == number,
                cancellationToken);

        if (numberExists)
            throw new InvalidOperationException(
                "Room with this number already exists.");

        room.Number = number;
        room.RoomTypeId = dto.RoomTypeId;
        room.RoomType = roomType;
        room.BasePricePerNight = dto.BasePricePerNight;
        room.Description = dto.Description.Trim();
        room.Status = dto.Status;

        await _context.SaveChangesAsync(cancellationToken);

        return MapToDto(room);
    }

    public async Task<bool> DeactivateAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var room = await _context.Rooms
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

        if (room is null)
            return false;

        room.Status = RoomStatus.Inactive;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static RoomDto MapToDto(Room room)
    {
        return new RoomDto(
            room.Id,
            room.Number,
            room.RoomTypeId,
            room.RoomType!.Name,
            room.BasePricePerNight,
            room.Description,
            room.Status.ToString());
    }
}

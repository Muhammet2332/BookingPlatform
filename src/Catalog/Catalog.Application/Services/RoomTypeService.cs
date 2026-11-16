
using Catalog.Application.DTOs.Room_Types;
using Catalog.Application.Services.Interfaces;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Services;

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
        var name = dto.Name.Trim();

        var exists = await _context.RoomTypes
            .AnyAsync(x => x.Name == name, cancellationToken);

        if (exists)
            throw new InvalidOperationException(
                "Room type with this name already exists.");

        var roomType = new RoomType
        {
            Name = name,
            Description = dto.Description.Trim()
        };

        _context.RoomTypes.Add(roomType);
        await _context.SaveChangesAsync(cancellationToken);

        return MapToDto(roomType);
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

        return roomType is null ? null : MapToDto(roomType);
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

    public async Task<RoomTypeDto?> UpdateAsync(
        int id,
        UpdateRoomTypeDto dto,
        CancellationToken cancellationToken = default)
    {
        var roomType = await _context.RoomTypes
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

        if (roomType is null)
            return null;

        var name = dto.Name.Trim();

        var nameExists = await _context.RoomTypes
            .AnyAsync(
                x => x.Id != id && x.Name == name,
                cancellationToken);

        if (nameExists)
            throw new InvalidOperationException(
                "Room type with this name already exists.");

        roomType.Name = name;
        roomType.Description = dto.Description.Trim();

        await _context.SaveChangesAsync(cancellationToken);

        return MapToDto(roomType);
    }

    public async Task<bool> DeactivateAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var roomType = await _context.RoomTypes
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

        if (roomType is null)
            return false;

        roomType.IsActive = false;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static RoomTypeDto MapToDto(RoomType roomType)
    {
        return new RoomTypeDto(
            roomType.Id,
            roomType.Name,
            roomType.Description);
    }
}

using Catalog.Application.DTOs.Room_Types;
using Catalog.Application.DTOs.RoomTypes;

namespace Catalog.Application.Services.Interfaces
{
    public interface IRoomTypeService
    {
        Task<RoomTypeDto> CreateAsync(
            CreateRoomTypeDto dto,
            CancellationToken cancellationToken = default);

        Task<RoomTypeDto?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<RoomTypeDto>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<RoomTypeDto?> UpdateAsync(
            int id,
            UpdateRoomTypeDto dto,
            CancellationToken cancellationToken = default);

        Task<bool> DeactivateAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}

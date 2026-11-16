using Catalog.Application.DTOs.Rooms;

namespace Catalog.Application.Services.Interfaces
{
    public interface IRoomService
    {
        Task<RoomDto> CreateAsync(
            CreateRoomDto dto,
            CancellationToken cancellationToken = default);

        Task<RoomDto?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<RoomDto>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<RoomDto?> UpdateAsync(
            int id,
            UpdateRoomDto dto,
            CancellationToken cancellationToken = default);

        Task<bool> DeactivateAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}

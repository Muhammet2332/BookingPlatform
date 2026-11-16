using Catalog.Domain.Enums;

namespace Catalog.Application.DTOs.Rooms;

public record UpdateRoomDto(
    string Number,
    int RoomTypeId,
    decimal BasePricePerNight,
    string Description,
    RoomStatus Status
);
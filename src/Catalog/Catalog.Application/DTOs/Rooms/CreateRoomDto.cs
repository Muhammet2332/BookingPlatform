namespace Catalog.Application.DTOs.Rooms
{
    public record CreateRoomDto(
    string Number,
    int RoomTypeId,
    decimal BasePricePerNight,
    string Description
);
}

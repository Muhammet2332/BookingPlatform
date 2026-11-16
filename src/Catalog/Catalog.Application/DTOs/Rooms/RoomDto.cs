namespace Catalog.Application.DTOs.Rooms
{
    public record RoomDto(
    int Id,
    string Number,
    int RoomTypeId,
    string RoomTypeName,
    decimal BasePricePerNight,
    string Description,
    string Status   
);
}

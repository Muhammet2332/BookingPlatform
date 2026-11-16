namespace Catalog.Api.Contracts.Rooms
{
    public record CreateRoomRequest(
    string Number,
    int RoomTypeId,
    decimal BasePricePerNight,
    string Description
);
}

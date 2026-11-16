using Catalog.Application.DTOs.Rooms;
using Catalog.Application.Services.Interfaces;

namespace Catalog.Api.Endpoints
{
    public static class RoomEndpoints
    {
        public static void MapRoomEndpoints(
            this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/rooms")
                .WithTags("Rooms");

            group.MapGet("/{id:int}", async (
                int id,
                IRoomService roomService,
                CancellationToken cancellationToken) =>
            {
                var room = await roomService.GetByIdAsync(
                    id,
                    cancellationToken);

                return room is null
                    ? Results.NotFound()
                    : Results.Ok(room);
            });

            group.MapPost("/", async (
                CreateRoomDto dto,
                IRoomService roomService,
                CancellationToken cancellationToken) =>
            {
                var room = await roomService.CreateAsync(
                    dto,
                    cancellationToken);

                return Results.Created(
                    $"/api/rooms/{room.Id}",
                    room);
            });

        }
    }
}

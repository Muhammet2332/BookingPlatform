using Catalog.Application.DTOs.Room_Types;
using Catalog.Application.Services.Interfaces;

namespace Catalog.Api.Endpoints
{
    public static class RoomTypeEndpoints
    {
        public static void MapRoomTypeEndpoints(
            this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/room-types")
                .WithTags("Room Types");

            group.MapPost("/", async (
                CreateRoomTypeDto dto,
                IRoomTypeService service,
                CancellationToken cancellationToken) =>
            {
                var result = await service.CreateAsync(
                    dto,
                    cancellationToken);

                return Results.Created(
                    $"/api/room-types/{result.Id}",
                    result);
            });

            group.MapGet("/{id:int}", async (
                int id,
                IRoomTypeService service,
                CancellationToken cancellationToken) =>
            {
                var result = await service.GetByIdAsync(
                    id,
                    cancellationToken);

                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });

            group.MapGet("/", async (
                IRoomTypeService service,
                CancellationToken cancellationToken) =>
            {
                var result = await service.GetAllAsync(
                    cancellationToken);

                return Results.Ok(result);
            });
        }
    }
}

using Catalog.Application.DTOs.Room_Types;
using Catalog.Application.Services.Interfaces;

namespace Catalog.Api.Endpoints;

public static class RoomTypeEndpoints
{
    public static void MapRoomTypeEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/room-types")
            .WithTags("Room Types");

        // GET /api/room-types
        group.MapGet("/", async (
            IRoomTypeService service,
            CancellationToken ct) =>
        {
            var result = await service.GetAllAsync(ct);
            return Results.Ok(result);
        });

        // GET /api/room-types/{id}
        group.MapGet("/{id:int}", async (
            int id,
            IRoomTypeService service,
            CancellationToken ct) =>
        {
            var result = await service.GetByIdAsync(id, ct);

            return result is null
                ? Results.NotFound()
                : Results.Ok(result);
        });

        // POST /api/room-types
        group.MapPost("/", async (
            CreateRoomTypeDto dto,
            IRoomTypeService service,
            CancellationToken ct) =>
        {
            try
            {
                var result = await service.CreateAsync(dto, ct);

                return Results.Created(
                    $"/api/room-types/{result.Id}",
                    result);
            }
            catch (InvalidOperationException ex)
            {
                return Results.Conflict(new { message = ex.Message });
            }
        });

        // PUT /api/room-types/{id}
        group.MapPut("/{id:int}", async (
            int id,
            UpdateRoomTypeDto dto,
            IRoomTypeService service,
            CancellationToken ct) =>
        {
            try
            {
                var result = await service.UpdateAsync(id, dto, ct);

                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Results.Conflict(new { message = ex.Message });
            }
        });

        // DELETE /api/room-types/{id}
        group.MapDelete("/{id:int}", async (
            int id,
            IRoomTypeService service,
            CancellationToken ct) =>
        {
            var deleted = await service.DeactivateAsync(id, ct);

            return deleted
                ? Results.NoContent()
                : Results.NotFound();
        });
    }
}
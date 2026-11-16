using Catalog.Application.DTOs.Rooms;
using Catalog.Application.Services.Interfaces;

namespace Catalog.Api.Endpoints;

public static class RoomEndpoints
{
    public static void MapRoomEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/rooms")
            .WithTags("Rooms");

        // GET /api/rooms
        group.MapGet("/", async (
            IRoomService service,
            CancellationToken ct) =>
        {
            var result = await service.GetAllAsync(ct);
            return Results.Ok(result);
        });

        // GET /api/rooms/{id}
        group.MapGet("/{id:int}", async (
            int id,
            IRoomService service,
            CancellationToken ct) =>
        {
            var result = await service.GetByIdAsync(id, ct);

            return result is null
                ? Results.NotFound()
                : Results.Ok(result);
        });

        // POST /api/rooms
        group.MapPost("/", async (
            CreateRoomDto dto,
            IRoomService service,
            CancellationToken ct) =>
        {
            try
            {
                var result = await service.CreateAsync(dto, ct);

                return Results.Created(
                    $"/api/rooms/{result.Id}",
                    result);
            }
            catch (InvalidOperationException ex)
            {
                return Results.Conflict(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        });

        // PUT /api/rooms/{id}
        group.MapPut("/{id:int}", async (
            int id,
            UpdateRoomDto dto,
            IRoomService service,
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
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        });

        // DELETE /api/rooms/{id}
        group.MapDelete("/{id:int}", async (
            int id,
            IRoomService service,
            CancellationToken ct) =>
        {
            var deleted = await service.DeactivateAsync(id, ct);

            return deleted
                ? Results.NoContent()
                : Results.NotFound();
        });
    }
}
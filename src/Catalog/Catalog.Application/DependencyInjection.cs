using Catalog.Application.Services.Interfaces;
using Catalog.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IRoomService, RoomService>();

        services.AddScoped<IRoomTypeService, RoomTypeService>();

        return services;
    }
}
using Catalog.Api.Endpoints;
using Catalog.Application;
using Catalog.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(
        new JsonStringEnumConverter()
    );
});

builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("CatalogDatabase")!
);

builder.Services.AddApplication();

var app = builder.Build();

app.MapRoomEndpoints();
app.MapRoomTypeEndpoints();

app.Run();


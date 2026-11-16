using Catalog.Api.Endpoints;
using Catalog.Application;
using Catalog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("CatalogDatabase")!
);

builder.Services.AddApplication();

var app = builder.Build();

app.MapRoomEndpoints();
app.MapRoomTypeEndpoints();

app.Run();


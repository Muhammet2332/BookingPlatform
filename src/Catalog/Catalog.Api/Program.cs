using Catalog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("CatalogDatabase")!
);

var app = builder.Build();

app.Run();


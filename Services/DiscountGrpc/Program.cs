using DiscountGrpc.Data;
using DiscountGrpc.Repositories;
using DiscountGrpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddScoped<IDiscountRepository, DiscountRepository>();

/*** Database connection ***/
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PSQL")));

builder.Services.AddGrpc();

var app = builder.Build();

/*** Init develop database data ***/
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetService<DataContext>()!;
    DataInitializer.Init(context);
}

app.MapGrpcService<GreeterService>();

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. " +
        "To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
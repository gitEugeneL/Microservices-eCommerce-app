using BasketApi.Endpoints;
using BasketApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*** Redis connection ***/
builder.Services.AddStackExchangeRedisCache(options => 
    options.Configuration = builder.Configuration.GetConnectionString("Redis"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

/*** Add Endpoints ***/
app.MapBasketEndpoints();

app.UseHttpsRedirection();

app.Run();

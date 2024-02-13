using BasketApi.Endpoints;
using BasketApi.Repositories;
using BasketApi.Services;
using DiscountGrpc;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddScoped<IDiscountClientService, DiscountClientService>()
    .AddScoped<IBasketRepository, BasketRepository>();

/*** Register gRPC client ***/
builder.Services.AddGrpcClient<DiscountProto.DiscountProtoClient>(option =>
    option.Address = new Uri(builder.Configuration.GetConnectionString("DiscountGrpc")!));

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

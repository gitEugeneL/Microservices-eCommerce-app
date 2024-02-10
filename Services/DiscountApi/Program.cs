using DiscountApi.Data;
using DiscountApi.Endpoints;
using DiscountApi.Models.Dto;
using DiscountApi.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddScoped<IDiscountRepository, DiscountRepository>()
    .AddScoped<IValidator<DiscountRequestDto>, DiscountRequestValidator>();

/*** Database connection ***/
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PSQL")));

var app = builder.Build();

/*** Init develop database data ***/
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetService<DataContext>()!;
    DataInitializer.Init(context);
}

app.UseSwagger();
app.UseSwaggerUI();

/*** Add Endpoints ***/
app.MapDiscountEndpoints();

app.UseHttpsRedirection();

app.Run();

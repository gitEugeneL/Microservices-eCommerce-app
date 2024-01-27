using DiscountApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*** Swagger configuration ***/
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description =
            " Standard JWT Bearer Authorization with refresh token. Example: Bearer {your token} ",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

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

app.UseHttpsRedirection();

app.Run();

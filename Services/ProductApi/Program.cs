using Asp.Versioning;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProductApi;
using ProductApi.Data;
using ProductApi.Endpoints;
using ProductApi.Models.DTO.Products;
using ProductApi.Repositories;
using ProductApi.Repositories.Interfaces;
using ProductApi.Services;
using ProductApi.Services.Interfaces;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddScoped<ICategoryService, CategoryService>()
    .AddScoped<ICategoryRepository, CategoryRepository>()
    .AddScoped<IProductService, ProductService>()
    .AddScoped<IProductRepository, ProductRepository>()
    .AddScoped<IValidator<ProductRequestDto>, ProductRequestValidator>()
    .AddScoped<IValidator<ProductUpdateDto>, ProductUpdateValidator>();

/*** Api versioning connection ***/
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

/*** Swagger connection ***/
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
    options.UseNpgsql(builder.Configuration.GetConnectionString(("PSQL")))
);

/*** Global exception handler ***/
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

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
app.MapCategoryEndpoints();
app.MapProductEndpoints();

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.Run();
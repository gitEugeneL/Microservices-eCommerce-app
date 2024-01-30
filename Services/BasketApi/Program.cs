var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*** Redis connection ***/
builder.Services.AddStackExchangeRedisCache(options => 
    options.Configuration = builder.Configuration.GetConnectionString("Redis"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.Run();

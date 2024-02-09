using MongoDB.Driver;
using MongoDB.Entities;
using SearchApi.Endpoints;
using SearchApi.Entities;
using SearchApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ISearchRepository, SearchRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*** Database configure ***/
await DB.InitAsync(builder.Configuration.GetConnectionString("MongoDB")!, 
    MongoClientSettings.FromConnectionString(builder.Configuration.GetConnectionString("MongoConnection")));
await DB.Index<Item>()
    .Key(x => x.AuctionId, KeyType.Text)
    .Key(x => x.CategoryId, KeyType.Text)
    .Key(x => x.SellerId, KeyType.Text)
    .Key(x => x.WinnerId, KeyType.Text)
    .CreateAsync();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

/*** Add Endpoints ***/
app.MapSearchEndpoints();

app.UseHttpsRedirection();

app.Run();

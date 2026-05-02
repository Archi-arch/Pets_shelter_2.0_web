using Pet_shelter_learning.Data;
using Pet_shelter_learning.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();
builder.AddSpeciesDb();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPetsEndpoints();

app.MigrateDb();

app.Run();

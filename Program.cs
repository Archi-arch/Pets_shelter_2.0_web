using Pet_shelter_learning.dtos;
using Pet_shelter_learning.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPetsEndpoints();

app.Run();


using Microsoft.EntityFrameworkCore;
using Pet_shelter_learning.Data;
using Pet_shelter_learning.dtos;
using Pet_shelter_learning.Models;

namespace Pet_shelter_learning.Endpoints;

public static class SpeciesEndpoints
{
    public static void MapSpeciesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/species");

        // get
        group.MapGet("/", async (Pet_shelter_context dbContext) => 
            await dbContext.Species
                .Select(s => new SpeciesDto(s.Id, s.Name))
                .AsNoTracking()
                .ToListAsync());

        // 2. post
        group.MapPost("/", async (CreateSpeciesDto newSpecies, Pet_shelter_context dbContext) =>
        {
            Species species = new() { Name = newSpecies.Name };
            
            dbContext.Species.Add(species);
            await dbContext.SaveChangesAsync();

            return Results.Created($"/species/{species.Id}", new SpeciesDto(species.Id, species.Name));
        });

        // 3. put)
        group.MapPut("/{id}", async (int id, UpdateSpeciesDto updatedSpecies, Pet_shelter_context dbContext) =>
        {
            var existingSpecies = await dbContext.Species.FindAsync(id);

            if (existingSpecies is null) return Results.NotFound();

            existingSpecies.Name = updatedSpecies.Name;
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // delete
        group.MapDelete("/{id}", async (int id, Pet_shelter_context dbContext) =>
        {
            await dbContext.Species
                     .Where(s => s.Id == id)
                     .ExecuteDeleteAsync();

            return Results.NoContent();
        });
    }
}


using Microsoft.EntityFrameworkCore;
using Pet_shelter_learning.Data;
using Pet_shelter_learning.dtos;
using Pet_shelter_learning.Models;



namespace Pet_shelter_learning.Endpoints;

public static class Endpoints
{

    const string GetPetEndpointName = "GetPet";
   
    public static void MapPetsEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("/pets");

        // get pets
        group.MapGet("/", async (Pet_shelter_context dbContext) => await dbContext.Pets
        .Include(Pet => Pet.Species)
        .Select(Pet => new PetsSummarydto(
            Pet.Id,
            Pet.Species!.Name,
            Pet.Age,
            Pet.Nickname!,
            Pet.Description!,
            Pet.Personality!,
            Pet.Donation,
            Pet.AdmisisonDate
        ))
        .AsNoTracking()
        .ToListAsync());
        // get pet
        group.MapGet("/{id}", async (int id, Pet_shelter_context dbContext) =>
        {
            var pet = await dbContext.Pets.FindAsync(id);

            return pet is null ? Results.NotFound() : Results.Ok(
                new PetsDetailsDto(
                pet.Id,
                pet.SpeciesId,
                pet.Age,
                pet.Nickname!,
                pet.Description!,
                pet.Personality!,
                pet.Donation,
                pet.AdmisisonDate
            ));
        }).WithName(GetPetEndpointName);


        // post
        group.MapPost("/", async (CreatePetsDto newPet, Pet_shelter_context dbContext) =>
        {

            Pet pet = new()
            {
                SpeciesId = newPet.SpeciesId,
                Age = newPet.Age,
                Nickname = newPet.Nickname,
                Description = newPet.Description,
                Personality = newPet.Personality,
                Donation = 0.00M,
                AdmisisonDate = DateOnly.FromDateTime(DateTime.Now)
            };

            // Pets_dto pet = new(
            //     pets.Count + 1,
            //     newPet.Species,
            //     newPet.Age,
            //     newPet.Nickname,
            //     newPet.Description,
            //     newPet.Personality,
            //     0.00M,
            //     DateOnly.FromDateTime(DateTime.Now)
            //);

            dbContext.Pets.Add(pet);
            await dbContext.SaveChangesAsync();


            PetsDetailsDto Pets_dto = new(
                pet.Id,
                pet.SpeciesId,
                pet.Age,
                pet.Nickname,
                pet.Description,
                pet.Personality,
                pet.Donation,
                pet.AdmisisonDate

            );

            return Results.CreatedAtRoute(GetPetEndpointName, new { id = Pets_dto.Id }, Pets_dto);
        });

        //put

        group.MapPut("/{id}", async (
            int id, 
            UpdatePetDto updatedPet, 
            Pet_shelter_context dbContext) =>
        {
            var existingPet = await dbContext.Pets.FindAsync(id);

            if (existingPet is null)
            {
                return Results.NotFound();
            }

                existingPet.SpeciesId = updatedPet.SpeciesId;
                existingPet.Age = updatedPet.Age;
                existingPet.Nickname = updatedPet.Nickname;
                existingPet.Description = updatedPet.Description;
                existingPet.Personality = updatedPet.Personality;
                existingPet.Donation = existingPet.Donation;
                existingPet.AdmisisonDate = existingPet.AdmisisonDate;

                await dbContext.SaveChangesAsync();
            

            return Results.NoContent();
        });

        // delete

        group.MapDelete("/{id}", async (int id, Pet_shelter_context dbContext) =>
        {
            await dbContext.Pets
                            .Where(pet => pet.Id == id)
                            .ExecuteDeleteAsync();
            return Results.NoContent();
        });
    }
}

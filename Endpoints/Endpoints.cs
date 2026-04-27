
using Pet_shelter_learning.dtos;



namespace Pet_shelter_learning.Endpoints;

public static class Endpoints
{

    const string GetPetEndpointName = "GetPet";
    private static readonly List<Pets_dto> pets = [


        new (
            1,
            "cat",
            1.5,
            "Bobi",
            "very shy and fat cat",
            "shy",
            0.00M,
            new DateOnly(2026, 3, 12)
        ),
        new (
            2,
            "dog",
            2.0,
            "Barsik",
            "brown",
            "pos",
            12.45M,
            new DateOnly(2025, 4, 26)
        ),
        new (
            3,
            "jaba",
            0.5,
            "Ropa",
            "",
            "green",
            5.15M,
            new DateOnly(2026, 1, 1)
        )
        ];

    public static void MapPetsEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("/pets");

        // get pets
        group.MapGet("/", () => pets);
        // get pet
        group.MapGet("/{id}", (int id) =>
        {
            var pet = pets.Find(pets => pets.Id == id);

            return pet is null ? Results.NotFound() : Results.Ok(pet);
        }).WithName(GetPetEndpointName);


        // post
        group.MapPost("/", (CreatePetsDto newPet) =>
        {
            Pets_dto pet = new(
                pets.Count + 1,
                newPet.Species,
                newPet.Age,
                newPet.Nickname,
                newPet.Description,
                newPet.Personality,
                0.00M,
                DateOnly.FromDateTime(DateTime.Now)
            );

            pets.Add(pet);

            return Results.CreatedAtRoute(GetPetEndpointName, new { id = pet.Id }, pet);
        });

        //put

        group.MapPut("/{id}", (int id, UpdatePetDto updatedPet) =>
        {
            var index = pets.FindIndex(pet => pet.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }
            
            var existingPet = pets[index];



            pets[index] = new Pets_dto(
                id,
                updatedPet.Species,
                updatedPet.Age,
                updatedPet.Nickname,
                updatedPet.Description,
                updatedPet.Personality,
                existingPet.Donation,
                existingPet.AdmisisonDate
            );

            return Results.NoContent();
        });

        // delete

        group.MapDelete("/{id}", (int id) =>
        {
            pets.RemoveAll(pet => pet.Id == id);

            return Results.NoContent();
        });
    }
}

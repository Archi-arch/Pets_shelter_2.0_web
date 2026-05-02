using System;
using Microsoft.EntityFrameworkCore;
using Pet_shelter_learning.Models;

namespace Pet_shelter_learning.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<Pet_shelter_context>();

        dbContext.Database.Migrate();
    }

    public static void AddSpeciesDb(this WebApplicationBuilder builder)
    {
        var connString = builder.Configuration.GetConnectionString("Pet_Shelter");

        builder.Services.AddSqlite<Pet_shelter_context>(
            connString,
            optionsAction: options => options.UseSeeding((context, _) =>
            {
                if (!context.Set<Species>().Any())
                {
                    context.Set<Species>().AddRange(
                        new Species { Name = "Cat" },
                        new Species { Name = "Dog" }
                    );

                    context.SaveChanges();
                }
            })
        );
    }
}

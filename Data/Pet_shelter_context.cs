

using Microsoft.EntityFrameworkCore;
using Pet_shelter_learning.Models;

namespace Pet_shelter_learning.Data;

public class Pet_shelter_context(DbContextOptions<Pet_shelter_context> options) : DbContext(options)
{
    public DbSet<Pet>  Pets => Set<Pet>();

    public DbSet<Species>  Species => Set<Species>();
}

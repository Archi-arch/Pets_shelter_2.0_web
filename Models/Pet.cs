
namespace Pet_shelter_learning.Models;

public class Pet
{
    public int Id { get; set; }

    public Species? Species { get; set; }

    public int SpeciesId { get; set; }

    public double Age { get; set; }

    public string? Nickname { get; set; }


    public string? Description { get; set; }


    public string? Personality { get; set; }

    public decimal Donation { get; set; }

    public DateOnly AdmisisonDate { get; set; }
}

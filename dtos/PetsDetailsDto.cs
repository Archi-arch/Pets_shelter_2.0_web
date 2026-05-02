namespace Pet_shelter_learning.dtos;

public record PetsDetailsDto(
    int Id,
    int SpeciesId,
    double Age,
    string Nickname,
    string Description,
    string Personality,
    decimal Donation,
    DateOnly AdmisisonDate
    
);

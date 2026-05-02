namespace Pet_shelter_learning.dtos;

public record PetsSummarydto(
    int Id,
    string Species,
    double Age,
    string Nickname,
    string Description,
    string Personality,
    decimal Donation,
    DateOnly AdmisisonDate
    
);


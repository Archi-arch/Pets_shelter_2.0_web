namespace Pet_shelter_learning.dtos;

public record SpeciesDto(int Id, string Name);
public record CreateSpeciesDto(string Name); 
public record UpdateSpeciesDto(string Name);
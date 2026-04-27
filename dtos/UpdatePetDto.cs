using System.ComponentModel.DataAnnotations;

namespace Pet_shelter_learning.dtos;

public record UpdatePetDto(
    [Required][StringLength(20)]string Species,
    [Required][Range(0,30)]double Age,
    [Required][StringLength(30)]string Nickname,
    [Required][StringLength(200)]string Description,
    [Required][StringLength(100)]string Personality

);

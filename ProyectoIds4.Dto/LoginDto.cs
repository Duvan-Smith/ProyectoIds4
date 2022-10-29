using System.ComponentModel.DataAnnotations;

namespace ProyectoIds4.Dto;

public class LoginDto
{
    [Required(ErrorMessage = "El email es obligatorio")]
    public string User { get; set; } = default!;

    [Required(ErrorMessage = "El campo password es obligatorio")]
    public string Password { get; set; } = default!;
}
using ProyectoIds4.Dto;

namespace SeguridadToken.Presentacion.Login;

public interface ILoginService
{
    Task<UserDto> ActionLogin(LoginDto loginDto);
    Task<UserDto> UserValidate(UserDto userDto);
}

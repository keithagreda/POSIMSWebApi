using Domain.ApiResponse;
using POSIMSWebApi.Authentication.Dtos;

namespace POSIMSWebApi.Authentication.Interface
{
    public interface IUserAuthServices
    {
        Task<ApiResponse<UserLoginDto>> LoginAccount(LoginUserDto login);
        Task<string> RegisterUser(RegisterUserDto register);
    }
}
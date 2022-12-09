using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;

public interface IAuthService
{
    Task<UserLoginResponse> Login(UserLogin user);
    Task<UserLoginResponse> Register(UserRegister user);
    
}
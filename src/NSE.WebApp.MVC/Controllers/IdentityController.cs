using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Controllers;

public class IdentityController : MainController
{
    private readonly IAuthService _authService;


    public IdentityController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    [Route("nova-conta")]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    [Route("nova-conta")]
    public async Task<IActionResult> Register(UserRegister userRegister)
    {
        if (!ModelState.IsValid) return View(userRegister);
        
        var response = await _authService.Register(userRegister);

        if (ResponseHasErrors(response.ResponseResult))
            return View(userRegister);
        
        await RealizarLogin(response);
        
        return RedirectToAction("Index", "Home");
    }
    
    [HttpGet]
    [Route("login")]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(UserLogin userLogin)
    {
        if (!ModelState.IsValid) return View(userLogin);
        
        var response = await _authService.Login(userLogin);

        if (ResponseHasErrors(response.ResponseResult))
            return View(userLogin);
        
        await RealizarLogin(response);
        
        return RedirectToAction("Index", "Home");
    }
    
    [HttpGet]
    [Route("sair")]
    public async Task<IActionResult> Logout()
    {
        return RedirectToAction("Index", "Home");
    }
    
    private async Task RealizarLogin(UserLoginResponse response)
    {
        var token = ObterTokenFormatado(response.AccessToken);
        
        var claims = new List<Claim>
        {
            new Claim("JWT", response.AccessToken),
        };
        
        claims.AddRange(token.Claims);
        
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
            IsPersistent = true
        };
        
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }
    
    private static JwtSecurityToken ObterTokenFormatado(string jwtToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.ReadJwtToken(jwtToken);
        return token;
    }

    
}
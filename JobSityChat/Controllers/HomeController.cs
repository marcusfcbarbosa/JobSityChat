using JobSity.Core.Communications;
using JobSityChat.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JobSityChat.Controllers
{

    public class HomeController : MainController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthService _authenticationService;
        public HomeController(ILogger<HomeController> logger, IAuthService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLogin userLogin, string returnUrl = null)
        {

            if (!ModelState.IsValid) return View(userLogin);
            var result = await _authenticationService.Login(userLogin);
            if (!ResponseHasErrors(result.ResponseResult))
            {
                await LogIn(result);
                if (string.IsNullOrEmpty(returnUrl))
                    return RedirectToAction(actionName: "Chat", controllerName: "Home");
                return LocalRedirect(returnUrl);
            }
            return View(userLogin);
        }

        [HttpGet]
        [Route("register")]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid) return View(userRegister);
            var result = await _authenticationService.Register(userRegister);
            if (ResponseHasErrors(result.ResponseResult)) return View(userRegister);
            await LogIn(result);
            return RedirectToAction(actionName: "Chat", controllerName: "Home");
        }

        [HttpGet]
        [Route("chat")]
        public async Task<IActionResult> Chat()
        {
            return View();
        }
        private async Task LogIn(UserAnswersLogin userAnswersLogin)
        {
            var token = GetFormattedToken(userAnswersLogin.AccessToken);
            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", userAnswersLogin.AccessToken));
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
        private static JwtSecurityToken GetFormattedToken(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
        }

    }
}

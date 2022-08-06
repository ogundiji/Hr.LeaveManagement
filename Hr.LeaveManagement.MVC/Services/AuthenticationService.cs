using Hr.LeaveManagement.MVC.Contracts;
using Hr.LeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.MVC.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private JwtSecurityTokenHandler _tokenHandler;
        public AuthenticationService(IClient client,ILocalStorageService localStorage,IHttpContextAccessor httpContextAccessor):base(client,localStorage)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._tokenHandler = new JwtSecurityTokenHandler();
        }
        public async Task<bool> Authenticate(string email, string password)
        {
            try
            {
                AuthRequest authenticationRequest = new() { Email = email, Password = password };
                var authenticationResponse = await _client.LoginAsync(authenticationRequest);

                if (authenticationResponse.Token != string.Empty)
                {
                    //Get Claims from token and Build auth user object
                    var tokenContent = _tokenHandler.ReadJwtToken(authenticationResponse.Token);
                    var claims = ParseClaims(tokenContent);
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                    var login = _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                    _localStorage.setStorageValue("token", authenticationResponse.Token);

                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task Logout()
        {
            _localStorage.clearStorage(new List<string> { "token" });
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<bool> Register(string firstName, string lastName, string userName, string email, string password)
        {

            RegistrationRequest registrationRequest = new() { Email=email,Firstname=firstName, Lastname=lastName,Username=userName, Password=password};
            var response = await _client.RegisterAsync(registrationRequest);

            if (!string.IsNullOrEmpty(response.UserId))
            {
                return true;
            }
            return false;
        }

        private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }
    }
}

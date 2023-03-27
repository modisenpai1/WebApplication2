using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication2.Domain.DTOs;
using WebApplication2.Domain.Models;

namespace WebApplication2.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User _user;

        public AuthManager(UserManager<User> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration= configuration;
        }
        public async Task<bool> AuthenticateUser(UserLoginDto userLoginDto)
        {
            _user = await _userManager.FindByNameAsync(userLoginDto.email);
            return(_user != null && await _userManager.CheckPasswordAsync(_user,userLoginDto.password));
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions=GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSetting = _configuration.GetSection("Jwt");
            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSetting.GetSection("LifeTime").Value));
            var token = new JwtSecurityToken(
                issuer: jwtSetting.GetSection("Issuer").Value,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
                );
            return token;
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,_user.UserName),
                new Claim(ClaimTypes.NameIdentifier,_user.Id),
                new Claim(ClaimTypes.Country,_user.CountryId.ToString()),
                new Claim("City",_user.CityId.ToString())
            };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;

        }

        private SigningCredentials GetSigningCredentials()
        {
            var key= _configuration.GetSection("Jwt").GetSection("Key").Value;
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret,SecurityAlgorithms.HmacSha256);
        }
    }
}

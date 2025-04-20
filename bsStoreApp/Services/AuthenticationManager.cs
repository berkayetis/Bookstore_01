using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationManager : IAuthenticationService
    {
        readonly ILoggerService _logger;
        readonly IMapper _mapper;
        readonly UserManager<User> _manager;
        readonly IConfiguration _configuration;
        User? _user;
        public AuthenticationManager(ILoggerService logger, IMapper mapper, UserManager<User> manager, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _manager = manager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistirationDto userForRegistirationDto)
        {
            User user = _mapper.Map<User>(userForRegistirationDto);

            IdentityResult result = await _manager.CreateAsync(user, userForRegistirationDto.Password);
            if(result.Succeeded)
            {
                await _manager.AddToRolesAsync(user, userForRegistirationDto.Roles);
            }

            return result;
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthenticationDto)
        {
            _user = await _manager.FindByNameAsync(userForAuthenticationDto.UserName);
            if(_user != null && await _manager.CheckPasswordAsync(_user, userForAuthenticationDto.Password))
            {
                return true;
            }
            else
            {
                _logger.LogWarning($"{nameof(ValidateUser)} : Authentication failed. Wrong username of password.");
                return false;
            }
        }
        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            var refreshToken = GenerateRefreshToken();
            _user.RefreshToken = refreshToken;

            if(populateExp)
            {
                _user.RefreshTokenExpireTime = DateTime.UtcNow.AddDays(7);
            }

            await _manager.UpdateAsync(_user);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            var tokenDto = new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return tokenDto;
        }

        string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        //Expired (süresi geçmiş) bir JWT access token'ı validate etmeden
        //decode etmek ve içinden ClaimsPrincipal (kullanıcı bilgileri) almak.
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["secretKey"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtToken || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        SigningCredentials GetSigningCredentials()
        {
            var jwtsSettings = _configuration.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtsSettings["secretKey"]);
            var symmetricSecurityKey = new SymmetricSecurityKey(secretKey);

            return new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        }
        async Task<List<Claim>> GetClaims()
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };

            var roles = await _manager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtsSettings = _configuration.GetSection("JwtSettings");
            Console.WriteLine(jwtsSettings["validIssuer"]);
            return new JwtSecurityToken(
                issuer: jwtsSettings["validIssuer"],
                audience: jwtsSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtsSettings["expires"])),
                signingCredentials: signingCredentials
            );
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var claimPrenciples = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var user = await _manager.FindByNameAsync(claimPrenciples.Identity.Name);

            if (user == null ||
                user.RefreshToken != tokenDto.RefreshToken || 
                user.RefreshTokenExpireTime <= DateTime.Now)
            {
                throw new RefreshTokenBadRequestException();
            }

            _user = user;

            return await CreateToken(false);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OrdersAPI.Constants;
using OrdersAPI.Entities;
using OrdersAPI.Exceptions;
using OrdersAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrdersAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(UserManager<User> userManager, IMapper mapper, AuthenticationSettings authenticationSettings)
        {
            _userManager = userManager;
            _mapper = mapper;
            _authenticationSettings = authenticationSettings;
        }

        public async Task<string> GenerateJWT(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
            {
                throw new BadRequestException("Invalid email or password");
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordCorrect)
            {
                throw new BadRequestException("Invalid email or password");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email),
                new("DateOfBirth", user.DateOfBirth.ToString("yyyy-MM-dd"))
            };

            foreach (var role in roles)
            {
                claims.Add(new(ClaimTypes.Role, role));
            }

            var expirationDate = DateTime.UtcNow.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                null,
                expirationDate,
                signature);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public async Task Register(RegisterDto dto)
        {
            var emailExists = await _userManager.FindByEmailAsync(dto.Email);
            if (emailExists != null)
            {
                throw new ValidationException("Email already exists");
            }

            var user = _mapper.Map<User>(dto);

            await _userManager.CreateAsync(user, dto.Password);
            await _userManager.AddToRoleAsync(user, Roles.User);
        }
    }
}
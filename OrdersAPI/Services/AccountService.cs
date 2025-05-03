using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OrdersAPI.Constants;
using OrdersAPI.Entities;
using OrdersAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace OrdersAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AccountService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task Register(RegisterUserDto dto)
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
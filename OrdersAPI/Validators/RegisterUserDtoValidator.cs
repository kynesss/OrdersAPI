using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OrdersAPI.Entities;
using OrdersAPI.Models;

namespace OrdersAPI.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(UserManager<User> userManager)
        {
            RuleFor(x => x.Email)
                 .NotEmpty()
                 .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one digit.")
                .Matches(@"[\!\?\*\.\@\#\$\%\^]").WithMessage("Password must contain at least one special character (! ? * . @ # $ % ^).");

            RuleFor(x => x.ConfirmedPassword)
                 .Equal(x => x.Password)
                 .WithMessage("Confirmed password must match the password.");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .MinimumLength(9);

            RuleFor(x => x.DateOfBirth)
                .NotEmpty();
        }
    }
}
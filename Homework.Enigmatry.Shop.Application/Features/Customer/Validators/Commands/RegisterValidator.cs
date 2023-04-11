﻿using FluentValidation;
using Homework.Enigmatry.Shop.Application.Features.Customer.Requests.Commands;

namespace Homework.Enigmatry.Shop.Application.Features.Customer.Validators.Commands
{
    public class RegisterValidator:AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(request => request.Username).NotEmpty()
                                                            .MinimumLength(8).WithMessage("Your username length must be at least 8.");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Your password cannot be empty")
                .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
        }
    }
}

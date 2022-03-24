﻿using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Dto.Users;
using FluentValidation;

namespace ContactlessOrder.Api.Validators
{
    public class UserValidation : AbstractValidator<UserDto>
    {
        public UserValidation(IUserService userService)
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Username)
              .MustAsync(async (x, c, f) => await userService.GetUser(c) == null)
              .When(u => !string.IsNullOrWhiteSpace(u.Username))
              .WithMessage("User already exists");

            RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull();
        }
    }
}

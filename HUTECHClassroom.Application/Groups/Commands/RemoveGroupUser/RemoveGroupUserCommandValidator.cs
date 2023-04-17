﻿using FluentValidation;

namespace HUTECHClassroom.Application.Groups.Commands.RemoveGroupUser
{
    public class RemoveGroupUserCommandValidator : AbstractValidator<RemoveGroupUserCommand>
    {
        public RemoveGroupUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.UserName).NotEmpty().NotNull();
        }
    }
}
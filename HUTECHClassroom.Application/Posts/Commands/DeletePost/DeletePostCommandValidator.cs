using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Posts.DTOs;

namespace HUTECHClassroom.Application.Posts.Commands.DeletePost;

public sealed class DeletePostCommandValidator : DeleteCommandValidator<DeletePostCommand, PostDTO>
{
}

using HUTECHClassroom.Domain.Constants.Services;
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HUTECHClassroom.Application.Account.Commands.AddAvatar;

public record AddAvatarCommand(IFormFile File) : IRequest<Unit>;
public class AddAvatarCommandHandler : IRequestHandler<AddAvatarCommand, Unit>
{
    private readonly IPhotoAccessor _photoAccessor;

    public AddAvatarCommandHandler(IPhotoAccessor photoAccessor)
    {
        _photoAccessor = photoAccessor;
    }
    public async Task<Unit> Handle(AddAvatarCommand request, CancellationToken cancellationToken)
    {
        await _photoAccessor.AddPhoto(request.File, ServiceConstants.ROOT_IMAGE_FOLDER + "/" + ServiceConstants.AVATAR_FOLDER).ConfigureAwait(false);
        return Unit.Value;
    }
}

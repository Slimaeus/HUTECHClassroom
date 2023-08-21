using HUTECHClassroom.Domain.Constants.Services;
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Account.Commands.AddAvatar;

public record AddAvatarCommand(IFormFile File) : IRequest<Unit>;
public class AddAvatarCommandHandler : IRequestHandler<AddAvatarCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPhotoAccessor _photoAccessor;
    private readonly IUserAccessor _userAccessor;
    private readonly IRepository<ApplicationUser> _userRepository;

    public AddAvatarCommandHandler(IUnitOfWork unitOfWork, IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
    {
        _unitOfWork = unitOfWork;
        _photoAccessor = photoAccessor;
        _userAccessor = userAccessor;
        _userRepository = unitOfWork.Repository<ApplicationUser>();
    }
    public async Task<Unit> Handle(AddAvatarCommand request, CancellationToken cancellationToken)
    {
        var result = await _photoAccessor.AddPhoto(request.File, ServiceConstants.ROOT_IMAGE_FOLDER + "/" + ServiceConstants.AVATAR_FOLDER).ConfigureAwait(false);
        var query = _userRepository
            .SingleResultQuery()
            .Include(i => i.Include(x => x.Faculty))
            .Include(i => i.Include(x => x.ApplicationUserRoles).ThenInclude(x => x.Role))
            .AndFilter(x => x.Id == _userAccessor.Id);

        var user = await _userRepository
            .SingleOrDefaultAsync(query, cancellationToken);

        if (user == null) throw new UnauthorizedAccessException(nameof(ApplicationUser));

        if (user.AvatarUrl is not null)
        {
            // TODO: Delete previous avatar
        }

        user.AvatarUrl = result.Url;

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        return Unit.Value;
    }
}

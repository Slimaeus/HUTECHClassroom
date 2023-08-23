using HUTECHClassroom.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Account.Commands.RemoveAvatar;

public record RemoveAvatarCommand() : IRequest<Unit>;
public class RemoveAvatarCommandHandler : IRequestHandler<RemoveAvatarCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<ApplicationUser> _userRepository;
    private readonly IRepository<Photo> _photoRepository;
    private readonly IUserAccessor _userAccessor;
    private readonly IPhotoAccessor _photoAccessor;

    public RemoveAvatarCommandHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor, IPhotoAccessor photoAccessor)
    {
        _unitOfWork = unitOfWork;
        _userRepository = unitOfWork.Repository<ApplicationUser>();
        _photoRepository = unitOfWork.Repository<Photo>();
        _userAccessor = userAccessor;
        _photoAccessor = photoAccessor;
    }
    public async Task<Unit> Handle(RemoveAvatarCommand request, CancellationToken cancellationToken)
    {
        var query = _userRepository
            .SingleResultQuery()
            .Include(i => i.Include(x => x.Avatar))
            .AndFilter(x => x.Id == _userAccessor.Id);

        var user = await _userRepository
            .SingleOrDefaultAsync(query, cancellationToken);

        if (user == null) throw new UnauthorizedAccessException(typeof(ApplicationUser).Name);

        if (user.Avatar is not null)
        {
            await _photoAccessor.DeletePhoto(user.Avatar.PublicId).ConfigureAwait(false);
            _photoRepository.Remove(user.Avatar);
            user.Avatar = null;
            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);
        }


        return Unit.Value;
    }
}

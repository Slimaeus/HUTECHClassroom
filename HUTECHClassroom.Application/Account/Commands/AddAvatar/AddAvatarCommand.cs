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
    private readonly IRepository<Photo> _photoRepository;

    public AddAvatarCommandHandler(IUnitOfWork unitOfWork, IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
    {
        _unitOfWork = unitOfWork;
        _photoAccessor = photoAccessor;
        _userAccessor = userAccessor;
        _userRepository = unitOfWork.Repository<ApplicationUser>();
        _photoRepository = unitOfWork.Repository<Photo>();
    }
    public async Task<Unit> Handle(AddAvatarCommand request, CancellationToken cancellationToken)
    {
        var query = _userRepository
            .SingleResultQuery()
            .Include(i => i.Include(x => x.Avatar))
            .AndFilter(x => x.Id == _userAccessor.Id);

        var user = await _userRepository
            .SingleOrDefaultAsync(query, cancellationToken)
            ?? throw new UnauthorizedAccessException(typeof(ApplicationUser).Name);

        var result = await _photoAccessor
            .AddPhoto(request.File, $"{ServiceConstants.ROOT_IMAGE_FOLDER}/{ServiceConstants.AVATAR_FOLDER}/{user.Id}")
            .ConfigureAwait(false);

        if (user.Avatar is { })
        {
            await _photoAccessor
                .DeletePhoto(user.Avatar.PublicId)
                .ConfigureAwait(false);
            _photoRepository
                .Remove(user.Avatar);
        }

        if (!result.IsSuccess)
        {
            throw new InvalidOperationException(result.Errors.FirstOrDefault());
        }

        var avatar = await _photoRepository
            .AddAsync(new Photo
            {
                PublicId = result.Data.PublicId,
                Url = result.Data.Url,
                Title = user.Id.ToString()
            }, cancellationToken);

        user.Avatar = avatar;

        await _unitOfWork
            .SaveChangesAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Unit.Value;
    }
}

using EntityFrameworkCore.Repository.Collections;
using HUTECHClassroom.Application.Scores.DTOs;
using HUTECHClassroom.Domain.Constants.Services;
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HUTECHClassroom.Application.Scores.Commands.GetStudentResultsWithPaginationByImage;

public sealed record GetStudentResultsWithPaginationByImageQuery(IFormFile File) : IRequest<IPagedList<StudentResultDTO>>;
public sealed class Handler : IRequestHandler<GetStudentResultsWithPaginationByImageQuery, IPagedList<StudentResultDTO>>
{
    private readonly IUserAccessor _userAccessor;
    private readonly IPhotoAccessor _photoAccessor;

    public Handler(IUserAccessor userAccessor, IPhotoAccessor photoAccessor)
    {
        _userAccessor = userAccessor;
        _photoAccessor = photoAccessor;
    }
    public async Task<IPagedList<StudentResultDTO>> Handle(GetStudentResultsWithPaginationByImageQuery request, CancellationToken cancellationToken)
    {
        var result = await _photoAccessor
            .AddPhoto(request.File, $"{ServiceConstants.ROOT_IMAGE_FOLDER}/{ServiceConstants.AVATAR_FOLDER}/{_userAccessor.Id}")
            .ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            throw new InvalidOperationException(result.Errors.FirstOrDefault());
        }

        throw new NotImplementedException();
    }
}

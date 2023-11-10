using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Collections;
using EntityFrameworkCore.Repository.Extensions;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Scores.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomResultsWithPagination;

public sealed record GetClassroomResultsWithPaginationQuery(Guid ClassroomId, int? ScoreTypeId, PaginationParams Params) : IRequest<IPagedList<StudentResultDTO>>;
public sealed class Handler : IRequestHandler<GetClassroomResultsWithPaginationQuery, IPagedList<StudentResultDTO>>
{
    private readonly IRepository<StudentResult> _classroomRepository;
    private readonly IMapper _mapper;

    public Handler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _classroomRepository = unitOfWork.Repository<StudentResult>();
        _mapper = mapper;
    }
    public async Task<IPagedList<StudentResultDTO>> Handle(GetClassroomResultsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = (IMultipleResultQuery<StudentResult>)_classroomRepository
            .MultipleResultQuery()
            .AndFilter(x => x.ClassroomId == request.ClassroomId);

        if (request.ScoreTypeId is { })
            query = (IMultipleResultQuery<StudentResult>)query.AndFilter(x => x.ScoreTypeId == request.ScoreTypeId);

        query = (IMultipleResultQuery<StudentResult>)query.OrderBy(x => x.OrdinalNumber);

        if (string.IsNullOrEmpty(request.Params.SearchString) is false)
        {
            var searchString = request.Params.SearchString.ToLowerInvariant();
            query = (IMultipleResultQuery<StudentResult>)query
                .AndFilter(x => x.Student != null && (x.Student.FirstName.Contains(searchString) || x.Student.LastName.Contains(searchString)));
        }

        var pagedListQueryable = _classroomRepository
            .ToQueryable(query);

        var projectedPagedList = pagedListQueryable
            .ProjectTo<StudentResultDTO>(_mapper.ConfigurationProvider)
            .AsSplitQuery();

        var asyncPagedList = projectedPagedList.ToListAsync(cancellationToken);

        var pagedList = await asyncPagedList
            .Then<List<StudentResultDTO>, IList<StudentResultDTO>>(result => result, cancellationToken)
            .ToPagedListAsync(query.Paging.PageIndex,
                              query.Paging.PageSize,
                              query.Paging.TotalCount,
                              cancellationToken);

        return pagedList;
    }
}

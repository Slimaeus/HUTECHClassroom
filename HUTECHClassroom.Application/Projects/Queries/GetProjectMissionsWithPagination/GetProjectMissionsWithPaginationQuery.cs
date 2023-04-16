using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Collections;
using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Projects.DTOs;
using HUTECHClassroom.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Projects.Queries.GetProjectMissionsWithPagination
{
    public record GetProjectMissionsWithPaginationQuery(Guid Id, PaginationParams Params) : GetWithPaginationQuery<ProjectMissionDTO>(Params);
    public class GetProjectMissionsWithPaginationQueryHandler : IRequestHandler<GetProjectMissionsWithPaginationQuery, IPagedList<ProjectMissionDTO>>
    {
        private readonly IRepository<Mission> _repository;
        private readonly IMapper _mapper;

        public GetProjectMissionsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = unitOfWork.Repository<Mission>();
            _mapper = mapper;
        }
        public async Task<IPagedList<ProjectMissionDTO>> Handle(GetProjectMissionsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var query = (IMultipleResultQuery<Mission>)_repository
                .MultipleResultQuery()
                .Page(request.Params.PageNumber, request.Params.PageSize)
                .AndFilter(x => x.ProjectId == request.Id)
                .OrderBy(x => x.CreateDate);

            var pagedList = await _repository
                .ToQueryable(query)
                .ProjectTo<ProjectMissionDTO>(_mapper.ConfigurationProvider)
                .AsSplitQuery()
                .ToListAsync(cancellationToken)
                .Then<List<ProjectMissionDTO>, IList<ProjectMissionDTO>>(result => result, cancellationToken)
                .ToPagedListAsync(query.Paging.PageIndex, query.Paging.PageSize, query.Paging.TotalCount, cancellationToken);

            if (pagedList.Count <= 0) throw new NotFoundException(nameof(Mission), "Id");

            return pagedList;
        }
    }
}

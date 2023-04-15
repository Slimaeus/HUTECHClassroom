using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Collections;
using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Missions.Queries.GetMissionUsersWithPagination
{
    public record GetMissionUsersWithPaginationQuery(Guid Id, PaginationParams Params) : GetWithPaginationQuery<MemberDTO>(Params);
    public class GetMissionUsersWithPaginationQueryHandler : IRequestHandler<GetMissionUsersWithPaginationQuery, IPagedList<MemberDTO>>
    {
        private readonly IRepository<ApplicationUser> _repository;
        private readonly IMapper _mapper;

        public GetMissionUsersWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = unitOfWork.Repository<ApplicationUser>();
            _mapper = mapper;
        }
        public async Task<IPagedList<MemberDTO>> Handle(GetMissionUsersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var query = (IMultipleResultQuery<ApplicationUser>)_repository
                .MultipleResultQuery()
                .Page(request.Params.PageNumber, request.Params.PageSize)
                .AndFilter(x => x.MissionUsers.Any(x => x.MissionId == request.Id))
                .OrderBy(x => x.UserName);
            
            var pagedList = await _repository
                .ToQueryable(query)
                .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                .AsSplitQuery()
                .ToListAsync(cancellationToken)
                .Then<List<MemberDTO>, IList<MemberDTO>>(result => result, cancellationToken)
                .ToPagedListAsync(query.Paging.PageIndex, query.Paging.PageSize, query.Paging.TotalCount, cancellationToken);

            if (pagedList.Count <= 0) throw new NotFoundException(nameof(ApplicationUser), "Id");

            return pagedList;
        }
    }
}

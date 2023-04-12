using AutoMapper;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Collections;
using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Missions.Queries
{
    public record GetMissionsWithPaginationQuery(int PageNumber = 1, int PageSize = 10) : IRequest<IPagedList<Mission>>;
    public class GetMissionsWithPaginationQueryHandler : IRequestHandler<GetMissionsWithPaginationQuery, IPagedList<Mission>>
    {
        private readonly IRepository<Mission> _repository;
        private readonly IMapper _mapper;

        public GetMissionsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = unitOfWork.Repository<Mission>();
            _mapper = mapper;
        }
        public async Task<IPagedList<Mission>> Handle(GetMissionsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var query = (IMultipleResultQuery<Mission>)_repository
                .MultipleResultQuery()
                .Page(request.PageNumber, request.PageSize)
                .OrderBy(x => x.CreateDate);

            var pagedList = await _repository
                .ToQueryable(query)
                .AsSplitQuery()
                .ToListAsync(cancellationToken)
                .Then<List<Mission>, IList<Mission>>(result => result, cancellationToken)
                .ToPagedListAsync(query.Paging.PageIndex, query.Paging.PageSize, query.Paging.TotalCount, cancellationToken);
            
            if (pagedList.Count <= 0) throw new NotFoundException("Mission", "Id");

            return pagedList;
        }
    }

}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Collections;
using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Common.Requests
{
    public record GetWithPaginationQuery<TDTO>(
            int PageNumber = 1,
            int PageSize = 10,
            string SearchString = null
        ) : IRequest<IPagedList<TDTO>>
            where TDTO : class;
    public abstract class GetWithPaginationQueryHandler<TEntity, TQuery, TDTO> : IRequestHandler<TQuery, IPagedList<TDTO>>
        where TEntity : BaseEntity
        where TQuery : GetWithPaginationQuery<TDTO>
        where TDTO : class
    {   
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public GetWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = unitOfWork.Repository<TEntity>();
            _mapper = mapper;
        }
        public async Task<IPagedList<TDTO>> Handle(TQuery request, CancellationToken cancellationToken)
        {
            var query = (IMultipleResultQuery<TEntity>)_repository
                .MultipleResultQuery()
                .Page(request.PageNumber, request.PageSize)
                .AndFilter(FilterPredicate())
                .OrderBy(x => x.CreateDate);

            if (!string.IsNullOrEmpty(request.SearchString))
                query = (IMultipleResultQuery<TEntity>)query.AndFilter(SearchStringPredicate(request.SearchString));

            var pagedList = await _repository
                .ToQueryable(query)
                .ProjectTo<TDTO>(_mapper.ConfigurationProvider)
                .AsSplitQuery()
                .ToListAsync(cancellationToken)
                .Then<List<TDTO>, IList<TDTO>>(result => result, cancellationToken)
                .ToPagedListAsync(query.Paging.PageIndex, query.Paging.PageSize, query.Paging.TotalCount, cancellationToken);

            if (pagedList.Count <= 0) throw new NotFoundException(nameof(TEntity), "Id");

            return pagedList;
        }
        public virtual Expression<Func<TEntity, bool>> SearchStringPredicate(string searchString) => x => true;
        public virtual Expression<Func<TEntity, bool>> FilterPredicate() => x => true;
    }
}

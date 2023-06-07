using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Collections;
using EntityFrameworkCore.Repository.Extensions;
using HUTECHClassroom.Application.Common.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Common.Requests;

public record GetWithPaginationQuery<TDTO, TPaginationParams>(
        TPaginationParams Params = default
    ) : IRequest<IPagedList<TDTO>>
        where TDTO : class
        where TPaginationParams : PaginationParams;
public abstract class GetWithPaginationQueryHandler<TKey, TEntity, TQuery, TDTO, TPaginationParams> : IRequestHandler<TQuery, IPagedList<TDTO>>
    where TEntity : class
    where TQuery : GetWithPaginationQuery<TDTO, TPaginationParams>
    where TDTO : class
    where TPaginationParams : PaginationParams
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
            .Page(request.Params.PageNumber ?? 1, request.Params.PageSize ?? 10)
            .AndFilter(FilterPredicate(request));

        query = SortingQuery(query, request);

        query = (IMultipleResultQuery<TEntity>)Order(query);

        if (string.IsNullOrEmpty(request.Params.SearchString) is false)
        {
            query = (IMultipleResultQuery<TEntity>)query
                .AndFilter(SearchStringPredicate(request.Params.SearchString));
        }

        var pagedList = await _repository
            .ToQueryable(query)
            .ProjectTo<TDTO>(_mapper.ConfigurationProvider, GetMappingParameters())
            .AsSplitQuery()
            .ToListAsync(cancellationToken)
            .Then<List<TDTO>, IList<TDTO>>(result => result, cancellationToken)
            .ToPagedListAsync(query.Paging.PageIndex,
                              query.Paging.PageSize,
                              query.Paging.TotalCount,
                              cancellationToken);

        return pagedList;
    }
    protected virtual IMappingParams GetMappingParameters() => default;
    protected virtual Expression<Func<TEntity, bool>> SearchStringPredicate(string searchString)
        => x => true;
    protected virtual Expression<Func<TEntity, bool>> FilterPredicate(TQuery query)
        => x => true;
    protected virtual IQuery<TEntity> Order(IMultipleResultQuery<TEntity> query) => query;
    protected virtual IMultipleResultQuery<TEntity> SortingQuery(IMultipleResultQuery<TEntity> query, TQuery request) => query;
}
public abstract class GetWithPaginationQueryHandler<TEntity, TQuery, TDTO, TPaginationParams> : GetWithPaginationQueryHandler<Guid, TEntity, TQuery, TDTO, TPaginationParams>
    where TEntity : class
    where TQuery : GetWithPaginationQuery<TDTO, TPaginationParams>
    where TDTO : class
    where TPaginationParams : PaginationParams
{
    protected GetWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
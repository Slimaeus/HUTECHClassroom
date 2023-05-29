using AutoMapper.QueryableExtensions;
using HUTECHClassroom.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Common.Requests;

public record GetQuery<TDTO> : IRequest<TDTO> where TDTO : class;
public abstract class GetQueryHandler<TKey, TEntity, TQuery, TDTO> : IRequestHandler<TQuery, TDTO>
    where TEntity : class
    where TQuery : GetQuery<TDTO>
    where TDTO : class
{
    private readonly IRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public GetQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = unitOfWork.Repository<TEntity>();
        _mapper = mapper;
    }
    public async Task<TDTO> Handle(TQuery request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .AndFilter(FilterPredicate(request));

        var entity = await _repository
            .ToQueryable(query)
            .AsSplitQuery()
            .ProjectTo<TDTO>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(cancellationToken);

        return entity ?? throw new NotFoundException(typeof(TEntity).Name, GetNotFoundKey(request));
    }
    public virtual object GetNotFoundKey(TQuery query) => string.Empty;
    public virtual Expression<Func<TEntity, bool>> FilterPredicate(TQuery query)
        => x => true;
}
public abstract class GetQueryHandler<TEntity, TQuery, TDTO> : GetQueryHandler<Guid, TEntity, TQuery, TDTO>
    where TEntity : class
    where TQuery : GetQuery<TDTO>
    where TDTO : class
{
    protected GetQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Common.Requests;

public record GetQuery<TDTO> : IRequest<TDTO> where TDTO : class;
public abstract class GetQueryHandler<TEntity, TQuery, TDTO> : IRequestHandler<TQuery, TDTO>
    where TEntity : class, IEntity
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

        return entity ?? throw new NotFoundException(nameof(TEntity), GetNotFoundKey(request));
    }
    public virtual object GetNotFoundKey(TQuery query) => string.Empty;
    public virtual Expression<Func<TEntity, bool>> FilterPredicate(TQuery query)
        => x => true;
}

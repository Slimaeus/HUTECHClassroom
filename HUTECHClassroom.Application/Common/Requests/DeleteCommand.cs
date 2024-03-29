﻿using AutoMapper.QueryableExtensions;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Common.Requests;

public record DeleteCommand<TKey, TDTO>(TKey Id) : IRequest<TDTO> where TKey : notnull where TDTO : class;
public abstract class DeleteCommandHandler<TKey, TEntity, TCommand, TDTO> : IRequestHandler<TCommand, TDTO>
    where TKey : notnull
    where TEntity : class, IEntity<TKey>
    where TCommand : DeleteCommand<TKey, TDTO>
    where TDTO : class
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public DeleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<TEntity>();
        _mapper = mapper;
    }
    public async Task<TDTO> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var query = _repository.SingleResultQuery()
            .AndFilter(m => m.Id.Equals(request.Id));

        var dto = await _repository
            .ToQueryable(query)
            .ProjectTo<TDTO>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException(typeof(TEntity).Name, request.Id);

        await _repository
            .RemoveAsync(x => x.Id.Equals(request.Id), cancellationToken)
            .ConfigureAwait(false);

        return dto;
    }
}
public record DeleteCommand<TDTO>(Guid Id) : DeleteCommand<Guid, TDTO>(Id) where TDTO : class;
public abstract class DeleteCommandHandler<TEntity, TCommand, TDTO> : DeleteCommandHandler<Guid, TEntity, TCommand, TDTO>
    where TEntity : class, IEntity<Guid>
    where TCommand : DeleteCommand<TDTO>
    where TDTO : class
{
    protected DeleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
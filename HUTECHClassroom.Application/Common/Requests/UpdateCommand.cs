﻿using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Common.Requests;

public record UpdateCommand<TKey>(TKey Id) : IRequest<Unit>
    where TKey : notnull;
public abstract class UpdateCommandHandler<TKey, TEntity, TCommand> : IRequestHandler<TCommand, Unit>
    where TKey : notnull
    where TEntity : class, IEntity<TKey>
    where TCommand : UpdateCommand<TKey>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public UpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<TEntity>();
        _mapper = mapper;
    }
    public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .AndFilter(m => m.Id.Equals(request.Id));

        var entity = await _repository
            .FirstOrDefaultAsync(query, cancellationToken)
            ?? throw new NotFoundException(typeof(TEntity).Name, request.Id);

        _mapper.Map(request, entity);

        await _unitOfWork
            .SaveChangesAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Unit.Value;
    }
}
public record UpdateCommand(Guid Id) : UpdateCommand<Guid>(Id);
public abstract class UpdateCommandHandler<TEntity, TCommand> : UpdateCommandHandler<Guid, TEntity, TCommand>
    where TEntity : class, IEntity<Guid>
    where TCommand : UpdateCommand<Guid>
{
    protected UpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}

using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Application.Common.Requests;

public record DeleteRangeCommand<TKey>(IList<TKey> Ids) : IRequest<Unit>;
public class DeleteRangeCommandHandler<TKey, TEntity, TCommand> : IRequestHandler<TCommand, Unit>
    where TEntity : class, IEntity<TKey>
    where TCommand : DeleteRangeCommand<TKey>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public DeleteRangeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<TEntity>();
        _mapper = mapper;
    }
    public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .MultipleResultQuery()
            .AndFilter(m => request.Ids.Contains(m.Id));

        var entities = _repository
            .ToQueryable(query);

        _repository.RemoveRange(entities);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        return Unit.Value;
    }
}
public record DeleteRangeCommand(IList<Guid> Ids) : DeleteRangeCommand<Guid>(Ids);

public class DeleteRangeCommandHandler<TEntity, TCommand> : DeleteRangeCommandHandler<Guid, TEntity, TCommand>
    where TEntity : class, IEntity<Guid>
    where TCommand : DeleteRangeCommand
{
    public DeleteRangeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
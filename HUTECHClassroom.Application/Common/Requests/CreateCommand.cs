using EntityFrameworkCore.Repository.Extensions;
using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Application.Common.Requests;

public record CreateCommand<TKey> : IRequest<TKey>;
public abstract class CreateCommandHandler<TKey, TEntity, TCommand> : IRequestHandler<TCommand, TKey>
    where TEntity : class, IEntity<TKey>
    where TCommand : CreateCommand<TKey>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public CreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<TEntity>();
        _mapper = mapper;
    }
    public async Task<TKey> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TEntity>(request);

        await ValidateAdditionalField(request, entity);

        await _repository.AddAsync(entity, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(entity);

        return entity.Id;
    }

    protected virtual Task ValidateAdditionalField(TCommand request, TEntity entity) => Task.CompletedTask;
}
public record CreateCommand : CreateCommand<Guid>;

public abstract class CreateCommandHandler<TEntity, TCommand> : CreateCommandHandler<Guid, TEntity, TCommand>
    where TEntity : class, IEntity<Guid>
    where TCommand : CreateCommand<Guid>
{
    protected CreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
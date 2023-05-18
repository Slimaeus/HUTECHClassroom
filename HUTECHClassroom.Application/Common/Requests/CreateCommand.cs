using EntityFrameworkCore.Repository.Extensions;
using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Application.Common.Requests;

public record CreateCommand<TDTO> : IRequest<TDTO> where TDTO : class;
public abstract class CreateCommandHandler<TKey, TEntity, TCommand, TDTO> : IRequestHandler<TCommand, TDTO>
    where TEntity : class, IEntity<TKey>
    where TCommand : CreateCommand<TDTO>
    where TDTO : class
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
    public async Task<TDTO> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TEntity>(request);

        await ValidateAdditionalField(request, entity);

        await _repository.AddAsync(entity, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);

        var result = _mapper.Map<TDTO>(entity);

        _repository.RemoveTracking(entity);

        return result;
    }

    protected virtual Task ValidateAdditionalField(TCommand request, TEntity entity) => Task.CompletedTask;
}

public abstract class CreateCommandHandler<TEntity, TCommand, TDTO> : CreateCommandHandler<Guid, TEntity, TCommand, TDTO>
    where TEntity : class, IEntity<Guid>
    where TCommand : CreateCommand<TDTO>
    where TDTO : class
{
    protected CreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
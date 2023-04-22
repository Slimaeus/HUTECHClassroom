using AutoMapper;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Domain.Common;
using MediatR;

namespace HUTECHClassroom.Application.Common.Requests;

public record DeleteRangeCommand(IList<Guid> Ids) : IRequest<Unit>;
public class DeleteRangeCommandHandler<TEntity, TCommand> : IRequestHandler<TCommand, Unit>
    where TEntity : class, IEntity
    where TCommand : DeleteRangeCommand
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

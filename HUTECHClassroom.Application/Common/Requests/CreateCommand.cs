using AutoMapper;
using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Domain.Common;
using MediatR;

namespace HUTECHClassroom.Application.Common.Requests;

public record CreateCommand<TDTO> : IRequest<TDTO> where TDTO : class;
public abstract class CreateCommandHandler<TEntity, TCommand, TDTO> : IRequestHandler<TCommand, TDTO>
    where TEntity : class, IEntity
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

using AutoMapper;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HUTECHClassroom.Application.Common.Requests
{
    public record DeleteCommand<TDTO>([Required] Guid Id) : IRequest<TDTO> where TDTO : class;
    public abstract class DeleteCommandHandler<TEntity, TCommand, TDTO> : IRequestHandler<TCommand, TDTO>
        where TEntity : BaseEntity
        where TCommand : DeleteCommand<TDTO>
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
                .AndFilter(m => m.Id == request.Id);

            var entity = await _repository.FirstOrDefaultAsync(query, cancellationToken) ?? throw new NotFoundException(nameof(TEntity), request.Id);

            _repository.Remove(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            return _mapper.Map<TDTO>(entity);
        }
    }
}

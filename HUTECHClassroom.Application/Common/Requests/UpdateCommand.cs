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
    public record UpdateCommand<TDTO>([Required] Guid Id) : IRequest<TDTO> where TDTO : class;
    public abstract class UpdateCommandHandler<TEntity, TCommand, TDTO> : IRequestHandler<TCommand, TDTO>
        where TEntity : BaseEntity
        where TCommand : UpdateCommand<TDTO>
        where TDTO : class
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
        public async Task<TDTO> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var query = _repository.SingleResultQuery()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
                .AndFilter(m => m.Id == request.Id);

            var entity = await _repository.FirstOrDefaultAsync(query, cancellationToken) ?? throw new NotFoundException(nameof(TEntity), request.Id);

            _mapper.Map(request, entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            
            return _mapper.Map<TDTO>(entity);
        }
    }
}

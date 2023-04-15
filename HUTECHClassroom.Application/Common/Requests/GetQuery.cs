using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Common.Requests
{
    public record GetQuery<TDTO>(Guid Id) : IRequest<TDTO> where TDTO : class;
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
            if (request.Id == Guid.Empty) throw new NotFoundException(nameof(TEntity), request.Id); // !!!Change to Validation Exception

            var query = _repository
                .SingleResultQuery()
                .AndFilter(m => m.Id == request.Id);

            var entity = await _repository
                .ToQueryable(query)
                .AsSplitQuery()
                .ProjectTo<TDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            return entity ?? throw new NotFoundException(nameof(TEntity), request.Id);
        }
    }
}

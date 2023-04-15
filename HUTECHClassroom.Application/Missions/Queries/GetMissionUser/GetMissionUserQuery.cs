using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Missions.Queries.GetMissionUser
{
    public record GetMissionUserQuery(Guid Id, string UserName) : IRequest<MemberDTO>;
    public class GetMissionUserQueryHandler : IRequestHandler<GetMissionUserQuery, MemberDTO>
    {
        private readonly IRepository<ApplicationUser> _repository;
        private readonly IMapper _mapper;

        public GetMissionUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = unitOfWork.Repository<ApplicationUser>();
            _mapper = mapper;
        }
        public async Task<MemberDTO> Handle(GetMissionUserQuery request, CancellationToken cancellationToken)
        {
            var query = (ISingleResultQuery<ApplicationUser>)_repository
                .SingleResultQuery()
                .AndFilter(x => x.MissionUsers.Any(x => x.MissionId == request.Id && x.User.UserName == request.UserName));

            var member = await _repository
                .ToQueryable(query)
                .AsSplitQuery()
                .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            return member ?? throw new NotFoundException(nameof(ApplicationUser), request.Id);
        }
    }
}
using AutoMapper;
using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Application.Missions.Commands.RemoveMissionUser;
using HUTECHClassroom.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Missions.Commands.RemoveMissionUser
{
    public record RemoveMissionUserCommand(Guid Id, string UserName) : IRequest<Unit>;
    public class RemoveMissionUserCommandHandler : IRequestHandler<RemoveMissionUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Mission> _repository;

        public RemoveMissionUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = unitOfWork.Repository<Mission>();
        }
        public async Task<Unit> Handle(RemoveMissionUserCommand request, CancellationToken cancellationToken)
        {
            var query = _repository
                .SingleResultQuery()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
                .Include(i => i.Include(x => x.MissionUsers).ThenInclude(x => x.User))
                .AndFilter(x => x.Id == request.Id);

            var mission = await _repository
                .SingleOrDefaultAsync(query, cancellationToken);

            if (mission == null) throw new NotFoundException(nameof(Mission), request.Id);

            var user = mission.MissionUsers.SingleOrDefault(x => x.User.UserName == request.UserName);

            if (user == null) throw new NotFoundException(nameof(user), request.UserName);

            mission.MissionUsers.Remove(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            _repository.RemoveTracking(mission);

            return Unit.Value;
        }
    }
}

using AutoMapper;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Application.Missions.Commands.AddMissionUser
{
    public record AddMissionUserCommand(Guid Id, string UserName) : IRequest<Unit>;
    public class AddMissionUserCommandHandler : IRequestHandler<AddMissionUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IRepository<Mission> _repository;

        public AddMissionUserCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _repository = unitOfWork.Repository<Mission>();
        }
        public async Task<Unit> Handle(AddMissionUserCommand request, CancellationToken cancellationToken)
        {
            var query = _repository
                .SingleResultQuery()
                .UseQueryTrackingBehavior(Microsoft.EntityFrameworkCore.QueryTrackingBehavior.TrackAll)
                .AndFilter(x => x.Id == request.Id);

            var mission = await _repository
                .SingleOrDefaultAsync(query, cancellationToken);

            if (mission == null) throw new NotFoundException(nameof(Mission), request.Id);

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null) throw new NotFoundException(nameof(user), request.UserName);

            mission.MissionUsers.Add(new MissionUser { User = user });

            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}

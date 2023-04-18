using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Classrooms.Commands.AddClassroomUser;

public record AddClassroomUserCommand(Guid Id, string UserName) : IRequest<Unit>;
public class AddClassroomUserCommandHandler : IRequestHandler<AddClassroomUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Classroom> _repository;
    private readonly IRepository<ApplicationUser> _userRepository;

    public AddClassroomUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<Classroom>();
        _userRepository = _unitOfWork.Repository<ApplicationUser>();
    }
    public async Task<Unit> Handle(AddClassroomUserCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.ClassroomUsers).ThenInclude(x => x.User))
            .AndFilter(x => x.Id == request.Id);

        var mission = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        if (mission == null) throw new NotFoundException(nameof(Classroom), request.Id);

        if (mission.ClassroomUsers.Any(x => x.User.UserName == request.UserName)) throw new InvalidOperationException($"{request.UserName} already exists");

        var userQuery = _userRepository
            .SingleResultQuery()
            .AndFilter(x => x.UserName == request.UserName);

        var user = await _userRepository
            .SingleOrDefaultAsync(userQuery, cancellationToken);

        if (user == null) throw new NotFoundException(nameof(ApplicationUser), request.UserName);

        mission.ClassroomUsers.Add(new ClassroomUser { User = user });

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(mission);

        return Unit.Value;
    }
}

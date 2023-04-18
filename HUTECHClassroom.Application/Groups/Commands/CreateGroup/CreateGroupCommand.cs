using AutoMapper;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Groups.DTOs;
using HUTECHClassroom.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Application.Groups.Commands.CreateGroup
{
    public record CreateGroupCommand : CreateCommand<GroupDTO>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LeaderName { get; set; }
    }
    public class CreateGroupCommandHandler : CreateCommandHandler<Group, CreateGroupCommand, GroupDTO>
    {
        private readonly IRepository<ApplicationUser> _userRepository;

        public CreateGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _userRepository = unitOfWork.Repository<ApplicationUser>();
        }
        protected override async Task ValidateAdditionalField(CreateGroupCommand request, Group entity)
        {
            var query = _userRepository
                .SingleResultQuery()
                .AndFilter(x => x.UserName == request.LeaderName);

            var leader = await _userRepository.SingleOrDefaultAsync(query);

            if (leader == null) throw new NotFoundException(nameof(ApplicationUser), request.LeaderName);

            entity.Leader = leader;
        }
    }
}

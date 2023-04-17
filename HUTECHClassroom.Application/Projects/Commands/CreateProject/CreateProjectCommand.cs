using AutoMapper;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Projects.DTOs;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Projects.Commands.CreateProject
{
    public record CreateProjectCommand : CreateCommand<ProjectDTO>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid GroupId { get; set; }
    }
    public class CreateProjectCommandHandler : CreateCommandHandler<Project, CreateProjectCommand, ProjectDTO>
    {
        private IRepository<Group> _groupRepository;

        public CreateProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _groupRepository = unitOfWork.Repository<Group>();
        }
        protected override async Task ValidateAdditionalField(CreateProjectCommand request, Project entity)
        {
            var query = _groupRepository
                .SingleResultQuery()
                .AndFilter(x => x.Id == request.GroupId);

            var group = await _groupRepository
                .SingleOrDefaultAsync(query);

            if (group == null) throw new NotFoundException(nameof(Group), request.GroupId);

            entity.Group = group;
        }
    }
}

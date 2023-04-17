using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Groups.DTOs;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Groups.Commands.DeleteGroup
{
    public record DeleteGroupCommand(Guid Id) : DeleteCommand<GroupDTO>(Id);
    public class DeleteGroupCommandHandler : DeleteCommandHandler<Group, DeleteGroupCommand, GroupDTO>
    {
        public DeleteGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}

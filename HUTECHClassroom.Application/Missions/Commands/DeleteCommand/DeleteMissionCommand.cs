using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Missions.Commands.DeleteCommand
{
    public record DeleteMissionCommand(Guid Id) : DeleteCommand<MissionDTO>(Id);
    public class DeleteMissionCommandHandler : DeleteCommandHandler<Mission, DeleteMissionCommand, MissionDTO>
    {
        public DeleteMissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}

using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Missions.Commands
{
    public record UpdateMissionCommand(Guid Id) : UpdateCommand<MissionDTO>(Id)
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
    }
    public class UpdateMissionCommandHandler : UpdateCommandHandler<Mission, UpdateMissionCommand, MissionDTO>
    {
        public UpdateMissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}

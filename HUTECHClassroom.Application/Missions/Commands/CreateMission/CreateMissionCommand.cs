using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace HUTECHClassroom.Application.Missions.Commands.CreateMission
{
    public record CreateMissionCommand : CreateCommand<MissionDTO>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; } = false;
    }
    public class CreateMissionCommandHandler : CreateCommandHandler<Mission, CreateMissionCommand, MissionDTO>
    {
        public CreateMissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}

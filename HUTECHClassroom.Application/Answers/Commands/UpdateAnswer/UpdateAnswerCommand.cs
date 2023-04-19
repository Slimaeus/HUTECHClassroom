using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Answers.Commands.UpdateAnswer;

public record UpdateAnswerCommand(Guid Id) : UpdateCommand(Id)
{
    public string Description { get; set; }
    public string Link { get; set; }
    public float Score { get; set; }
}
public class UpdateAnswerCommandHandler : UpdateCommandHandler<Answer, UpdateAnswerCommand>
{
    public UpdateAnswerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}

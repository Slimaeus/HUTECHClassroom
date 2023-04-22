using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Answers.Commands.DeleteRangeAnswer;

public record DeleteRangeAnswerCommand(IList<Guid> Ids) : DeleteRangeCommand(Ids);
public class DeleteRangeAnswerCommandHandler : DeleteRangeCommandHandler<Answer, DeleteRangeAnswerCommand>
{
    public DeleteRangeAnswerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}

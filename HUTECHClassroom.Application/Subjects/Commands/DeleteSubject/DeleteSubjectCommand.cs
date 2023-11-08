using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Subjects.DTOs;

namespace HUTECHClassroom.Application.Subjects.Commands.DeleteSubject;

public record DeleteSubjectCommand(Guid Id) : DeleteCommand<SubjectDTO>(Id);
public sealed class DeleteSubjectCommandHandler : DeleteCommandHandler<Subject, DeleteSubjectCommand, SubjectDTO>
{
    public DeleteSubjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}

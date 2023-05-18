using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Subjects.DTOs;

namespace HUTECHClassroom.Application.Subjects.Commands.DeleteSubject;

public record DeleteSubjectCommand(string Id) : DeleteCommand<string, SubjectDTO>(Id);
public class DeleteSubjectCommandHandler : DeleteCommandHandler<string, Subject, DeleteSubjectCommand, SubjectDTO>
{
    public DeleteSubjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}

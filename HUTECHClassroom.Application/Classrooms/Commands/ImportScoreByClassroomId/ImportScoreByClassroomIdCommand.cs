using EntityFrameworkCore.Repository.Collections;
using EntityFrameworkCore.Repository.Extensions;
using HUTECHClassroom.Application.Scores.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HUTECHClassroom.Application.Classrooms.Commands.ImportScoreByClassroomId;

public sealed record ImportScoreByClassroomIdCommand(Guid ClassroomId, IFormFile File) : IRequest<IPagedList<StudentResultWithOrdinalDTO>>;
public sealed class Hanlder : IRequestHandler<ImportScoreByClassroomIdCommand, IPagedList<StudentResultWithOrdinalDTO>>
{
    private readonly IExcelServie _excelServie;

    public Hanlder(IExcelServie excelServie)
    {
        _excelServie = excelServie;
    }
    public async Task<IPagedList<StudentResultWithOrdinalDTO>> Handle(ImportScoreByClassroomIdCommand request, CancellationToken cancellationToken)
    {
        var studentResults = _excelServie.ReadExcelFileWithColumnNames<StudentResultWithOrdinalDTO>(request.File.OpenReadStream(), null);

        var pagedList = studentResults.ToPagedList(1, studentResults.Count, studentResults.Count);

        return pagedList;
    }
}

using HUTECHClassroom.Application.Classrooms.Commands.UpdateClassroomScores;
using HUTECHClassroom.Application.Scores.Commands.AddStudentResult;
using HUTECHClassroom.Application.Scores.DTOs;
using HUTECHClassroom.Application.Scores.Queries.GetStudentScoresFromFile;
using HUTECHClassroom.Application.ScoreTypes.Queries.GetScoreType;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[Authorize(Policy = NeedRolePolicy)]
[ApiVersion("1.0")]
public sealed class ScoresController : BaseApiController
{
    [HttpPut]
    public async Task<IActionResult> UpdateScore(UpdateClassroomScoresCommand request)
    {
        await Mediator.Send(request).ConfigureAwait(false);
        return NoContent();
    }

    [HttpPost("ScanResult")]
    public async Task<ActionResult<IEnumerable<StudentResultDTO>>> ScanResult(IFormFile file)
    {
        return Ok(await Mediator.Send(new GetStudentScoresFromFileQuery(file)));
    }

    [HttpPost("add-result")]
    public async Task<IActionResult> AddResutl(AddStudentResultCommand request)
    {
        await Mediator.Send(request);
        return NoContent();
    }

    [HttpPost("ScanMultipleResult")]
    public async Task<ActionResult<IEnumerable<StudentResultDTO[]>>> ScanMultipleResult(List<IFormFile> files)
    {
        var tasks = files.Select(f => Mediator.Send(new GetStudentScoresFromFileQuery(f)));
        var result = await Task.WhenAll(tasks);
        return Ok(result);
    }

    [HttpPost("ScanMultipleFileResult")]
    public async Task<ActionResult<IEnumerable<StudentResultDTO>>> ScanMultipleFileResult(List<IFormFile> files)
    {
        var tasks = files.Select(f => Mediator.Send(new GetStudentScoresFromFileQuery(f)));
        var lists = await Task.WhenAll(tasks);
        var result = lists[0]
            .Concat(lists[1])
            .GroupBy(x => x.Student?.UserName ?? x.StudentId)
            .Select(g => g.First())
            .OrderBy(x => x.OrdinalNumber)
            .ToList();

        return Ok(result);
    }

    [HttpPost("ScanMultipleResultWithClassroomIdAndScoreTypeId")]
    public async Task<ActionResult<IEnumerable<StudentResultDTO>>> ScanMultipleResultWithClassroomIdAndScoreTypeId(List<TranscriptFileWithClassroomIdAndScoreTypeIdDTO> models)
    {
        var fileTasks = models.Select(m => Mediator.Send(new GetStudentScoresFromFileQuery(m.File)));
        var classroomTasks = models.Select(m => Mediator.Send(new GetClassroomQuery(m.ClassroomId)));
        var scoreTypeTasks = models.Select(m => Mediator.Send(new GetScoreTypeQuery(m.ScoreTypeId)));
        var fileResults = await Task.WhenAll(fileTasks);
        var classroomResults = await Task.WhenAll(classroomTasks);
        var scoreTypeResults = await Task.WhenAll(scoreTypeTasks);
        var results = new List<TranscriptWithClassroomAndScoreTypeDTO>();
        for (int i = 0; i < models.Count; i++)
        {
            results.Add(
                new TranscriptWithClassroomAndScoreTypeDTO(
                    fileResults[i],
                    classroomResults[i],
                    scoreTypeResults[i]));
        }
        return Ok(results);
    }
}

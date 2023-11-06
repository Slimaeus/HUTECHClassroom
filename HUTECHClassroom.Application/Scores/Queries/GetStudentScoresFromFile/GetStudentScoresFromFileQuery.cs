using HUTECHClassroom.Application.Scores.DTOs;
using HUTECHClassroom.Domain.Constants.Services;
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace HUTECHClassroom.Application.Scores.Queries.GetStudentScoresFromFile;

public sealed record GetStudentScoresFromFileQuery(IFormFile File) : IRequest<IEnumerable<StudentResultWithOrdinalDTO>>;
public sealed class Handler : IRequestHandler<GetStudentScoresFromFileQuery, IEnumerable<StudentResultWithOrdinalDTO>>
{
    private readonly IPhotoAccessor _photoAccessor;
    private readonly IAzureComputerVisionService _azureComputerVisionService;

    public Handler(IPhotoAccessor photoAccessor, IAzureComputerVisionService azureComputerVisionService)
    {
        _photoAccessor = photoAccessor;
        _azureComputerVisionService = azureComputerVisionService;
    }
    public async Task<IEnumerable<StudentResultWithOrdinalDTO>> Handle(GetStudentScoresFromFileQuery request, CancellationToken cancellationToken)
    {
        var result = await _photoAccessor.AddPhoto(request.File, $"{ServiceConstants.ROOT_IMAGE_FOLDER}/{ServiceConstants.TRANSCRIPT_FOLDER}");

        if (!result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        var url = result.Data.Url;
        var fileData = await _azureComputerVisionService.ReadFileUrl(url);

        await _photoAccessor
                .DeletePhoto(result.Data.PublicId);

        var resultDtos = new List<StudentResultWithOrdinalDTO>();

        foreach (var page in fileData)
        {
            var ordinalNumberRegex = @"^\d+$";
            var idRegex = @"^\d{10}$";
            var scoreRegex = @"^(?:100(?:[.,]0)?|[1-9](?:\.\d|[0-9])?|0(?:[.,]0)?)$";

            int ordinalNumber = 0;
            int previousOrdinalNumber = -1;
            string id = null;
            double? score = null;

            var skipped = false;

            foreach (var line in page.OptimizedLines)
            {
                var texts = line.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (texts.Contains("Diem"))
                {
                    skipped = true;
                    continue;
                }
                if (!skipped)
                {
                    continue;
                }
                foreach (var text in texts)
                {
                    if (Regex.IsMatch(text, idRegex))
                    {
                        id = text;
                    }
                    else if (Regex.IsMatch(text, ordinalNumberRegex))
                    {
                        var newOrdinalNumber = int.Parse(text);
                        if (previousOrdinalNumber == -1)
                        {
                            previousOrdinalNumber = newOrdinalNumber - 1;
                        }
                        if (newOrdinalNumber == previousOrdinalNumber + 1)
                        {
                            if (ordinalNumber != 0 && id != null)
                            {
                                resultDtos.Add(new StudentResultWithOrdinalDTO(ordinalNumber, id, score));

                                ordinalNumber = 0;
                                id = null;
                                score = null;
                            }
                            previousOrdinalNumber = newOrdinalNumber;
                            ordinalNumber = newOrdinalNumber;
                        }
                        else if (Regex.IsMatch(text, scoreRegex))
                        {
                            var newScore = double.Parse(text.Replace(',', '.'));
                            if (newScore > 10)
                            {
                                newScore /= 10;
                            }
                            score = newScore;
                        }
                    }
                    else if (Regex.IsMatch(text, scoreRegex))
                    {
                        var newScore = double.Parse(text.Replace(',', '.'));
                        if (newScore > 10)
                        {
                            newScore /= 10;
                        }
                        score = newScore;
                    }

                    if (ordinalNumber != 0 && id != null && score != null)
                    {
                        resultDtos.Add(new StudentResultWithOrdinalDTO(ordinalNumber, id, score));
                        ordinalNumber = 0;
                        id = null;
                        score = null;
                    }
                }

            }
        }
        return resultDtos;
    }
}
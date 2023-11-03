using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Interfaces;
using HUTECHClassroom.Domain.Models.ComputerVision;
using Newtonsoft.Json;
using System.Net;
using System.Text.RegularExpressions;
using FileIO = System.IO.File;


namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
[Authorize(Roles = RoleConstants.ADMIN)]
public sealed class FeaturesController : BaseApiController
{
    private readonly IAzureComputerVisionService _azureComputerVisionService;
    private static readonly string _resultFilePath = "output.json";
    public FeaturesController(IAzureComputerVisionService azureComputerVisionService)
        => _azureComputerVisionService = azureComputerVisionService;

    [HttpGet("vision/read")]
    public async Task<ActionResult<IEnumerable<OptimizedPage>>> Read([FromQuery] string a)
    {
        var decodedUrl = WebUtility.UrlDecode(a);
        var result = await _azureComputerVisionService.ReadFileUrl(decodedUrl);
        var serializedResult = JsonConvert.SerializeObject(result);
        await FileIO.WriteAllTextAsync(_resultFilePath, serializedResult);
        return Ok(result);
    }
    private static bool IsScore(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return false;
        if (text.Any(x => (x < '0' || x > '9') && x != ',' && x != '.'))
        {
            return false;
        }
        var value = double.Parse(text);
        if (value > 100 || value < 0)
        {
            return false;
        }
        return true;
    }
    [HttpGet("vision/write")]
    public async Task<ActionResult<IEnumerable<ResultDto>>> Write()
    {
        var str = await FileIO.ReadAllTextAsync(_resultFilePath);
        var pages = JsonConvert.DeserializeObject<IEnumerable<OptimizedPage>>(str);

        var resultDtos = new List<ResultDto>();

        foreach (var page in pages)
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
                                resultDtos.Add(new ResultDto(ordinalNumber, id, score));

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
                        resultDtos.Add(new ResultDto(ordinalNumber, id, score));
                        ordinalNumber = 0;
                        id = null;
                        score = null;
                    }
                }


            }
        }

        return Ok(resultDtos);
    }

    public sealed record ResultDto(int OrdinalNumber, string Id, double? Score);

}

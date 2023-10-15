using HUTECHClassroom.Domain.Interfaces;
using HUTECHClassroom.Domain.Models.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace HUTECHClassroom.Infrastructure.Services.ComputerVision;

public sealed class AzureComputerVisionService : IAzureComputerVisionService
{
    private readonly IComputerVisionClient _computerVisionClient;

    public AzureComputerVisionService(IComputerVisionClient computerVisionClient)
        => _computerVisionClient = computerVisionClient;

    public async Task<IEnumerable<OptimizedPage>> ReadFileUrl(string url)
    {
        var textHeaders = await _computerVisionClient.ReadAsync(url);

        string operationLocation = textHeaders.OperationLocation;

        const int numberOfCharsInOperationId = 36;
        string operationId = operationLocation[^numberOfCharsInOperationId..];

        ReadOperationResult results;
        do
        {
            results = await _computerVisionClient.GetReadResultAsync(Guid.Parse(operationId));
        }
        while (results.Status == OperationStatusCodes.Running ||
            results.Status == OperationStatusCodes.NotStarted);

        var textFileUrlResults = results.AnalyzeResult.ReadResults;

        var optimizedPages = new List<OptimizedPage>();

        foreach (var resultItem in textFileUrlResults)
        {
            var optimizedLines = new List<OptimizedLine>();
            foreach (var line in resultItem.Lines)
            {
                optimizedLines.Add(new OptimizedLine(line.Text, line.Appearance.Style.Name));
            }
            optimizedPages.Add(new OptimizedPage(resultItem.Page, optimizedLines));
        }
        return optimizedPages;
    }
}

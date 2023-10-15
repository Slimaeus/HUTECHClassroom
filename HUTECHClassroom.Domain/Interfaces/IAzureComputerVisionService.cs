using HUTECHClassroom.Domain.Models.ComputerVision;

namespace HUTECHClassroom.Domain.Interfaces;
public interface IAzureComputerVisionService
{
    Task<IEnumerable<OptimizedPage>> ReadFileUrl(string url);
}
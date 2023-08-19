using HUTECHClassroom.Domain.Common;
using Microsoft.AspNetCore.Http;

namespace HUTECHClassroom.Domain.Interfaces;
public interface IPhotoAccessor
{
    Task<PhotoUploadResult> AddPhoto(IFormFile file);
    Task<string> DeletePhoto(string publicId);
}

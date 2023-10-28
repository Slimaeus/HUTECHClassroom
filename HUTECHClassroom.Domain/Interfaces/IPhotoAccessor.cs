using HUTECHClassroom.Domain.Common;
using HUTECHClassroom.Domain.Constants.Services;
using HUTECHClassroom.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace HUTECHClassroom.Domain.Interfaces;
public interface IPhotoAccessor
{
    Task<ServiceResult<PhotoUploadResult>> AddPhoto(IFormFile file, string folder = "", double height = CloudinaryConstants.DEFAULT_HEIGHT, double width = CloudinaryConstants.DEFAULT_WIDTH);
    Task<ServiceResult<string>> DeletePhoto(string publicId);
}

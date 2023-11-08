using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HUTECHClassroom.Domain.Common;
using HUTECHClassroom.Domain.Constants.Services;
using HUTECHClassroom.Domain.Interfaces;
using HUTECHClassroom.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace HUTECHClassroom.Infrastructure.Services.Photos;

public sealed class CloudinaryPhotoAccessor : IPhotoAccessor
{
    private readonly Cloudinary _cloudinary;
    public CloudinaryPhotoAccessor(Cloudinary cloudinary)
        => _cloudinary = cloudinary;
    public async Task<ServiceResult<PhotoUploadResult>> AddPhoto(IFormFile file, string folder = "", double height = CloudinaryConstants.DEFAULT_HEIGHT, double width = CloudinaryConstants.DEFAULT_WIDTH)
    {
        if (file is { Length: > 0 })
        {
            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                Folder = folder,
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(height).Width(width).Crop(CloudinaryConstants.FILL_CROP_MODE)
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                throw new Exception(uploadResult.Error.Message);
            }

            return ServiceResult<PhotoUploadResult>.Success(new PhotoUploadResult
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.SecureUrl.ToString()
            });
        }

        return ServiceResult<PhotoUploadResult>.Error("Add Photo Failed");
    }

    public async Task<ServiceResult<string>> DeletePhoto(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deleteParams);
        return result.Result == CloudinaryConstants.OK_RESULT
            ? ServiceResult<string>.Success(result.Result)
            : ServiceResult<string>.Error("Delete Photo Failed");
    }
}

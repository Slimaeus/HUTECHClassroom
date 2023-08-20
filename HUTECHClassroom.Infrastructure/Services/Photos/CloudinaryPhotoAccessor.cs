using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HUTECHClassroom.Domain.Common;
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace HUTECHClassroom.Infrastructure.Services.Photos;

public class CloudinaryPhotoAccessor : IPhotoAccessor
{
    private readonly Cloudinary _cloudinary;
    public CloudinaryPhotoAccessor(IOptions<CloudinarySettings> config)
    {
        var account = new Account(
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret
        );
        _cloudinary = new Cloudinary(account);
    }
    public async Task<PhotoUploadResult> AddPhoto(IFormFile file, string folder = "")
    {
        if (file.Length > 0)
        {
            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                Folder = folder,
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                throw new Exception(uploadResult.Error.Message);
            }

            return new PhotoUploadResult
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.SecureUrl.ToString()
            };
        }

        return null;
    }

    public async Task<string> DeletePhoto(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deleteParams);
        return result.Result == "ok" ? result.Result : null;
    }
}

﻿using CloudinaryDotNet.Actions;

namespace RunGroopApp.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoasync(string publicId);
    }
}

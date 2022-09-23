using FileUploadApi.Services.ServiceResponse;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadApi.Services.FileService
{
    public interface IFileService
    {
      Task<ServiceResponse<List<ImageUpload>>> getAll();
      Task<ServiceResponse<List<ImageUpload>>> UploadFile([FromForm]List<IFormFile> files);
    }
}

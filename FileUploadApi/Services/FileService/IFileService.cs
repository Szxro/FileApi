using FileUploadApi.Dto_s;
using FileUploadApi.Services.ServiceResponse;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadApi.Services.FileService
{
    public interface IFileService
    {
      Task<ServiceResponse<List<ImageUploadDTO>>> getAll();
      Task<ServiceResponse<List<ImageUpload>>> UploadFile([FromForm]List<IFormFile> files);
    }
}

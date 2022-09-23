using FileUploadApi.Data;
using FileUploadApi.Services.ServiceResponse;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FileUploadApi.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;
        private readonly FileContext _context;
        public FileService(IWebHostEnvironment webHostEnvironment, FileContext fileContext)
        {
            _env = webHostEnvironment;
            _context = fileContext;
        }

        public async Task<ServiceResponse<List<ImageUpload>>> getAll()
        {
            return new ServiceResponse<List<ImageUpload>>() { Data = await _context.imageUploads.ToListAsync() };
        }

        public async Task<ServiceResponse<List<ImageUpload>>> UploadFile([FromForm] List<IFormFile> files)
        {
            try
            {
             
                List<ImageUpload> result = new List<ImageUpload>();
                DateTime dateTime = DateTime.Now;

                foreach (var file in files)
                {
                    ImageUpload ImageResult = new ImageUpload();//Creating a instance for each one of the imageUpload to not override it.
                    //Creating the FileName
                    var fileName = Guid.NewGuid();
                    var realName = file.FileName;
                    var extension = Path.GetExtension(realName);
  
                    //Making the Path and Saving it
                    string pathFileName = Path.GetRandomFileName();
                    var path = Path.Combine("Upload", $"{fileName}{extension}");
                    await using FileStream fs = new(path, FileMode.Create);
                    await file.CopyToAsync(fs);

                    //Adding the result
                    ImageResult.FileName = fileName;
                    ImageResult.FilePath = path;
                    ImageResult.OrginalName = realName;
                    ImageResult.FileType = extension;
                    ImageResult.CreationDate = dateTime;

                    //Adding it in the List
                    result.Add(ImageResult);
                    await _context.imageUploads.AddAsync(ImageResult);
                }
                await _context.SaveChangesAsync();
                return new ServiceResponse<List<ImageUpload>>() { Data = result };

            }
            catch (Exception e)
            {
                return new ServiceResponse<List<ImageUpload>>() { Data = null, Success = false, Message = e.Message };
            }
        }
    }
}

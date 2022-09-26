using AutoMapper;
using FileUploadApi.Data;
using FileUploadApi.Dto_s;
using FileUploadApi.Services.ServiceResponse;
using Microsoft.AspNetCore.Mvc;


namespace FileUploadApi.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;
        private readonly FileContext _context;
        private readonly IMapper _mapper;

        public FileService(IWebHostEnvironment webHostEnvironment, FileContext fileContext,IMapper mapper)
        {
            _env = webHostEnvironment;
            _context = fileContext;
            _mapper = mapper;
        }

        public async Task<FileDownload> downloadFile(string filename)
        {
            try
            {
                //Getting the file by the name
                var fileResult = await _context.imageUploads.Where(x => x.OriginalName == filename).FirstOrDefaultAsync();
                //The first who find it will return 

                //Getting the path to download
                var path = Path.Combine(_env.ContentRootPath, "Upload", $"{fileResult.FileName}{fileResult.FileType}");

                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open)) //Open the file and saving it
                {
                    await stream.CopyToAsync(memory);//Copy in the memory

                }
                memory.Position = 0;//In the first position is save

                return new FileDownload() { FileName = fileResult.OriginalName, FileType = fileResult.FileType, Memory = memory, Path = path };
                //Saving it in the FileDownload model.

            } catch (Exception e)
            {
                return new FileDownload() {Error = true }; 
            }
        }

        public async Task<ServiceResponse<List<ImageUploadDTO>>> getAll()
        {
            return new ServiceResponse<List<ImageUploadDTO>>() { Data = await _context.imageUploads.Select(res => _mapper.Map<ImageUploadDTO>(res)).ToListAsync()};
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
                    ImageResult.OriginalName = realName;
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

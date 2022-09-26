using AutoMapper;
using FileUploadApi.Dto_s;
using FileUploadApi.Services.ServiceResponse;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Net.Sockets;
using Microsoft.AspNetCore.StaticFiles;

namespace FileUploadApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
       private readonly IFileService _fileService;

        public FileController(IFileService fileService,IMapper mapper)
        {
            _fileService = fileService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<ImageUploadDTO>>>> getAll()
        {
            var response = await _fileService.getAll();
            if (response == null)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("Upload")]
        public async Task<ActionResult<ServiceResponse<List<ImageUpload>>>> uploadFile([FromForm] List<IFormFile> files)
        {
            var response = await _fileService.UploadFile(files);    
            if(response.Data == null)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("download/{filename}")]
        public async Task<IActionResult> DowloadFile(string filename)
        {
            
            var result = await _fileService.downloadFile(filename);//Waiting the result
            var contentType = new FileExtensionContentTypeProvider();//ContentType to download 

            if (!contentType.TryGetContentType(result.FileType, out var defaultType))//Getting the ContentType 
            {
                defaultType = "application/octet-stream";//ContentType by default
            }

            if(result.Error == true)
                return BadRequest(result);
            return File(result.Memory,defaultType, Path.GetFileName(result.Path));//Returning and Downloading the file
        }
    
    }
}

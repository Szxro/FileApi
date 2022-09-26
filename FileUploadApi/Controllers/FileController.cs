using AutoMapper;
using FileUploadApi.Dto_s;
using FileUploadApi.Services.ServiceResponse;
using Microsoft.AspNetCore.Mvc;

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
    }
}

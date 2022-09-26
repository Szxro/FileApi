using AutoMapper;
using FileUploadApi.Dto_s;

namespace FileUploadApi.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
           CreateMap<ImageUpload, ImageUploadDTO>();
        }
    }
}

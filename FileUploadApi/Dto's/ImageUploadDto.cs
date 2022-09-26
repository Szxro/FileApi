namespace FileUploadApi.Dto_s
{
    public class ImageUploadDTO
    {
        public Guid? FileName { get; set; }

        public string OriginalName { get; set; } = string.Empty;
    }
}

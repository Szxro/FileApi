namespace FileUploadApi.Models
{
    public class FileDownload
    {
        public string FileName { get; set; } = string.Empty;

        public string FileType { get; set; } = string.Empty;

        public Stream? Memory { get; set; }

        public string Path { get; set; } = string.Empty;

        public bool Error { get; set; } = false;
    }
}

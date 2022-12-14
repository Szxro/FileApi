using FileUploadApi;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileUploadApi.Models
{
    public class ImageUpload : Common
    {
        public Guid? FileName { get; set; }

        public string? FilePath { get; set; }

        public string OriginalName { get; set; } = string.Empty;

        public string? FileType { get; set; }

        public DateTime CreationDate { get; set; }
    }
}

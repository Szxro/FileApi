
using FileUploadApi.Services.ServiceResponse;

namespace FileUploadApi.Data
{
    public class FileContext : DbContext
    {
        public FileContext(DbContextOptions<FileContext> options): base(options)
        {
                
        }

        public DbSet<ImageUpload> imageUploads { get; set; }

       //protected override void OnModelCreating(ModelBuilder modelBuilder)
       // {
       //    modelBuilder.Entity<ImageUpload>().ToTable("imagePath");
       // }
    }
}

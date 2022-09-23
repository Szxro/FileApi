using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileUploadApi.Migrations
{
    public partial class initDB3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "imageUploads",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "imageUploads",
                newName: "ID");
        }
    }
}

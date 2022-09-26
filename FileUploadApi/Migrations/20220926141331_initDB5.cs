using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileUploadApi.Migrations
{
    public partial class initDB5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrginalName",
                table: "imageUploads");

            migrationBuilder.AddColumn<string>(
                name: "OriginalName",
                table: "imageUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalName",
                table: "imageUploads");

            migrationBuilder.AddColumn<string>(
                name: "OrginalName",
                table: "imageUploads",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

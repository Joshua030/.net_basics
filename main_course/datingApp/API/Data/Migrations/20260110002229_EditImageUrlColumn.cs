using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditImageUrlColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImgUrl",
                table: "Users",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImgUrl",
                table: "Members",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Users",
                newName: "ImgUrl");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Members",
                newName: "ImgUrl");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStore.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedProductsPictureIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Picture_PictureId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "PictureId",
                table: "Products",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Picture_PictureId",
                table: "Products",
                column: "PictureId",
                principalTable: "Picture",
                principalColumn: "PictureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Picture_PictureId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "PictureId",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Picture_PictureId",
                table: "Products",
                column: "PictureId",
                principalTable: "Picture",
                principalColumn: "PictureId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

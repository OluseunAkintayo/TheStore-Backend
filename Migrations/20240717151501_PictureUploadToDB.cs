using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStore.Migrations
{
    /// <inheritdoc />
    public partial class PictureUploadToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Picture_PictureId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PictureId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Picture",
                table: "Picture");

            migrationBuilder.RenameTable(
                name: "Picture",
                newName: "Pictures");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Pictures",
                newName: "Filename");

            migrationBuilder.RenameColumn(
                name: "FileSrc",
                table: "Pictures",
                newName: "ContentType");

            migrationBuilder.AddColumn<int>(
                name: "PictureId1",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Pictures",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Pictures",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pictures",
                table: "Pictures",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PictureId1",
                table: "Products",
                column: "PictureId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Pictures_PictureId1",
                table: "Products",
                column: "PictureId1",
                principalTable: "Pictures",
                principalColumn: "PictureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Pictures_PictureId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PictureId1",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pictures",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "PictureId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Pictures");

            migrationBuilder.RenameTable(
                name: "Pictures",
                newName: "Picture");

            migrationBuilder.RenameColumn(
                name: "Filename",
                table: "Picture",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "ContentType",
                table: "Picture",
                newName: "FileSrc");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Picture",
                table: "Picture",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PictureId",
                table: "Products",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Picture_PictureId",
                table: "Products",
                column: "PictureId",
                principalTable: "Picture",
                principalColumn: "PictureId");
        }
    }
}

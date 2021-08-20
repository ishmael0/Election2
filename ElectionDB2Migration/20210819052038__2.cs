using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Election2.ElectionDB2Migration
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOK",
                table: "Engineer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Ozviat",
                table: "Engineer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Parvane",
                table: "Engineer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Engineer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Shenasname",
                table: "Engineer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TempCode",
                table: "Engineer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TempCodeExpire",
                table: "Engineer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Engineer",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOK",
                table: "Engineer");

            migrationBuilder.DropColumn(
                name: "Ozviat",
                table: "Engineer");

            migrationBuilder.DropColumn(
                name: "Parvane",
                table: "Engineer");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Engineer");

            migrationBuilder.DropColumn(
                name: "Shenasname",
                table: "Engineer");

            migrationBuilder.DropColumn(
                name: "TempCode",
                table: "Engineer");

            migrationBuilder.DropColumn(
                name: "TempCodeExpire",
                table: "Engineer");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Engineer");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Election2.ElectionDB2Migration
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CandidaesId",
                table: "Vote",
                newName: "CandidaesId2");

            migrationBuilder.AddColumn<string>(
                name: "CandidaesId1",
                table: "Vote",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CandidaesId1",
                table: "Vote");

            migrationBuilder.RenameColumn(
                name: "CandidaesId2",
                table: "Vote",
                newName: "CandidaesId");
        }
    }
}

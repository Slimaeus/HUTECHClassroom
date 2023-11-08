using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUTECHClassroom.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOrdinalNumberForStudentResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrdinalNumber",
                table: "StudentResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrdinalNumber",
                table: "StudentResults");
        }
    }
}

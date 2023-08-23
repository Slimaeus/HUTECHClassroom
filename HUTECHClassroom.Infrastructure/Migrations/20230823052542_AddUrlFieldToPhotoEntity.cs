using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUTECHClassroom.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUrlFieldToPhotoEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Photos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Exercises",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 8, 24, 5, 25, 41, 899, DateTimeKind.Utc).AddTicks(6787),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 8, 24, 5, 8, 57, 686, DateTimeKind.Utc).AddTicks(1785));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Photos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Exercises",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 8, 24, 5, 8, 57, 686, DateTimeKind.Utc).AddTicks(1785),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 8, 24, 5, 25, 41, 899, DateTimeKind.Utc).AddTicks(6787));
        }
    }
}

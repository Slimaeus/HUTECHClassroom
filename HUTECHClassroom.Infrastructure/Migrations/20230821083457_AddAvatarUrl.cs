using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUTECHClassroom.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAvatarUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Exercises",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 8, 22, 8, 34, 57, 462, DateTimeKind.Utc).AddTicks(7701),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 1, 8, 41, 47, 359, DateTimeKind.Utc).AddTicks(47));

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Exercises",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 1, 8, 41, 47, 359, DateTimeKind.Utc).AddTicks(47),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 8, 22, 8, 34, 57, 462, DateTimeKind.Utc).AddTicks(7701));
        }
    }
}

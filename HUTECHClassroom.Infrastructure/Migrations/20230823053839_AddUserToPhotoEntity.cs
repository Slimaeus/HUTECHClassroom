using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUTECHClassroom.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToPhotoEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AvatarId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Photos",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Exercises",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 8, 24, 5, 38, 39, 539, DateTimeKind.Utc).AddTicks(7344),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 8, 24, 5, 25, 41, 899, DateTimeKind.Utc).AddTicks(6787));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AvatarId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Photos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Exercises",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 8, 24, 5, 25, 41, 899, DateTimeKind.Utc).AddTicks(6787),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 8, 24, 5, 38, 39, 539, DateTimeKind.Utc).AddTicks(7344));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUTECHClassroom.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClassEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Photos_AvatarId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AvatarId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Class",
                table: "Classrooms",
                newName: "ClassId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Exercises",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 20, 4, 17, 24, 253, DateTimeKind.Utc).AddTicks(9546),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 8, 24, 5, 38, 39, 539, DateTimeKind.Utc).AddTicks(7344));

            migrationBuilder.AddColumn<string>(
                name: "ClassId",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                table: "Photos",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_ClassId",
                table: "Classrooms",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClassId",
                table: "AspNetUsers",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Classes_ClassId",
                table: "AspNetUsers",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_Classes_ClassId",
                table: "Classrooms",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_UserId",
                table: "Photos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Classes_ClassId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_Classes_ClassId",
                table: "Classrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_UserId",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Photos_UserId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Classrooms_ClassId",
                table: "Classrooms");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ClassId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "Classrooms",
                newName: "Class");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Exercises",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 8, 24, 5, 38, 39, 539, DateTimeKind.Utc).AddTicks(7344),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 20, 4, 17, 24, 253, DateTimeKind.Utc).AddTicks(9546));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Photos_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId",
                principalTable: "Photos",
                principalColumn: "Id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUTECHClassroom.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddScoreTypeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentScore_ScoreType_ScoreTypeId",
                table: "StudentScore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScoreType",
                table: "ScoreType");

            migrationBuilder.RenameTable(
                name: "ScoreType",
                newName: "ScoreTypes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Exercises",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 10, 33, 53, 83, DateTimeKind.Utc).AddTicks(1646),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 10, 22, 59, 812, DateTimeKind.Utc).AddTicks(9393));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScoreTypes",
                table: "ScoreTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentScore_ScoreTypes_ScoreTypeId",
                table: "StudentScore",
                column: "ScoreTypeId",
                principalTable: "ScoreTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentScore_ScoreTypes_ScoreTypeId",
                table: "StudentScore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScoreTypes",
                table: "ScoreTypes");

            migrationBuilder.RenameTable(
                name: "ScoreTypes",
                newName: "ScoreType");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Exercises",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2023, 10, 24, 10, 22, 59, 812, DateTimeKind.Utc).AddTicks(9393),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 10, 24, 10, 33, 53, 83, DateTimeKind.Utc).AddTicks(1646));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScoreType",
                table: "ScoreType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentScore_ScoreType_ScoreTypeId",
                table: "StudentScore",
                column: "ScoreTypeId",
                principalTable: "ScoreType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

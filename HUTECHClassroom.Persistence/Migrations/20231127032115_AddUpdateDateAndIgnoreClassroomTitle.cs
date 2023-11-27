using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUTECHClassroom.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdateDateAndIgnoreClassroomTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Classrooms");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Subjects",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Score",
                table: "StudentResults",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "ScoreTypes",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Projects",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Photos",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Missions",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FacultyId",
                table: "Majors",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Majors",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Groups",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Faculties",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Exercises",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Comments",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Classrooms",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Classes",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Answers",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Majors_FacultyId",
                table: "Majors",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Majors_Faculties_FacultyId",
                table: "Majors",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Majors_Faculties_FacultyId",
                table: "Majors");

            migrationBuilder.DropIndex(
                name: "IX_Majors_FacultyId",
                table: "Majors");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "ScoreTypes");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Majors");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Majors");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Answers");

            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "StudentResults",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Classrooms",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}

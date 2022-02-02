using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFlashCards.Infrastructure.Persistence.Migrations
{
    public partial class QuestionTestPropertyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Tests_TestId",
                table: "Question");

            migrationBuilder.AlterColumn<Guid>(
                name: "TestId",
                table: "Question",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Tests_TestId",
                table: "Question",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Tests_TestId",
                table: "Question");

            migrationBuilder.AlterColumn<Guid>(
                name: "TestId",
                table: "Question",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Tests_TestId",
                table: "Question",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id");
        }
    }
}

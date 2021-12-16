using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Polls.Migrations
{
    public partial class QuestionPollId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PollId",
                table: "QuestionAnswers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PollId",
                table: "QuestionAnswers");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace jklintan_minions.Migrations
{
    public partial class RemoveNullConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Highscores",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Highscores",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}

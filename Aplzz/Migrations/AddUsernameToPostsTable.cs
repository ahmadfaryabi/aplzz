using Microsoft.EntityFrameworkCore.Migrations;

public partial class AddUsernameToPostsTable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Username",
            table: "Posts",
            type: "TEXT",
            nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Username",
            table: "Posts");
    }
}
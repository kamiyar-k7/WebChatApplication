using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfruStructure.Migrations
{
    /// <inheritdoc />
    public partial class addseenandsendtomessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSeen",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSend",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSeen",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "IsSend",
                table: "Messages");
        }
    }
}

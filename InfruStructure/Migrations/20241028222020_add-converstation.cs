using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfruStructure.Migrations
{
    /// <inheritdoc />
    public partial class addconverstation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConverstationId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "converstations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User1Id = table.Column<int>(type: "int", nullable: false),
                    User2Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_converstations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_converstations_Users_User1Id",
                        column: x => x.User1Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_converstations_Users_User2Id",
                        column: x => x.User2Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConverstationId",
                table: "Messages",
                column: "ConverstationId");

            migrationBuilder.CreateIndex(
                name: "IX_converstations_User1Id",
                table: "converstations",
                column: "User1Id");

            migrationBuilder.CreateIndex(
                name: "IX_converstations_User2Id",
                table: "converstations",
                column: "User2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_converstations_ConverstationId",
                table: "Messages",
                column: "ConverstationId",
                principalTable: "converstations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_converstations_ConverstationId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "converstations");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ConverstationId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ConverstationId",
                table: "Messages");
        }
    }
}

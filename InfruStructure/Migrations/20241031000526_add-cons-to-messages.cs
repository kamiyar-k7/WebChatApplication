using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfruStructure.Migrations
{
    /// <inheritdoc />
    public partial class addconstomessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_converstations_ConverstationId",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_converstations_ConverstationId",
                table: "Messages",
                column: "ConverstationId",
                principalTable: "converstations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_converstations_ConverstationId",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_converstations_ConverstationId",
                table: "Messages",
                column: "ConverstationId",
                principalTable: "converstations",
                principalColumn: "Id");
        }
    }
}

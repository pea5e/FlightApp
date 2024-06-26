using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkings_Pilots_PilotId",
                table: "Checkings");

            migrationBuilder.DropIndex(
                name: "IX_Checkings_PilotId",
                table: "Checkings");

            migrationBuilder.RenameColumn(
                name: "PilotId",
                table: "Checkings",
                newName: "SessionID");

            migrationBuilder.CreateIndex(
                name: "IX_Checkings_SessionID",
                table: "Checkings",
                column: "SessionID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Checkings_Sessions_SessionID",
                table: "Checkings",
                column: "SessionID",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkings_Sessions_SessionID",
                table: "Checkings");

            migrationBuilder.DropIndex(
                name: "IX_Checkings_SessionID",
                table: "Checkings");

            migrationBuilder.RenameColumn(
                name: "SessionID",
                table: "Checkings",
                newName: "PilotId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkings_PilotId",
                table: "Checkings",
                column: "PilotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkings_Pilots_PilotId",
                table: "Checkings",
                column: "PilotId",
                principalTable: "Pilots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

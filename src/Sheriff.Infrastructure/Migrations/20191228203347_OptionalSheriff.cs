using Microsoft.EntityFrameworkCore.Migrations;

namespace Sheriff.Infrastructure.Migrations
{
    public partial class OptionalSheriff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rounds_SheriffId",
                table: "Rounds");

            migrationBuilder.AlterColumn<int>(
                name: "SheriffId",
                table: "Rounds",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_SheriffId",
                table: "Rounds",
                column: "SheriffId",
                unique: true,
                filter: "[SheriffId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rounds_SheriffId",
                table: "Rounds");

            migrationBuilder.AlterColumn<int>(
                name: "SheriffId",
                table: "Rounds",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_SheriffId",
                table: "Rounds",
                column: "SheriffId",
                unique: true);
        }
    }
}

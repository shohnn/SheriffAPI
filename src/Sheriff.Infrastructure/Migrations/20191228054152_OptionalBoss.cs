using Microsoft.EntityFrameworkCore.Migrations;

namespace Sheriff.Infrastructure.Migrations
{
    public partial class OptionalBoss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bands_BossId",
                table: "Bands");

            migrationBuilder.AlterColumn<int>(
                name: "BossId",
                table: "Bands",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Bands_BossId",
                table: "Bands",
                column: "BossId",
                unique: true,
                filter: "[BossId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bands_BossId",
                table: "Bands");

            migrationBuilder.AlterColumn<int>(
                name: "BossId",
                table: "Bands",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bands_BossId",
                table: "Bands",
                column: "BossId",
                unique: true);
        }
    }
}

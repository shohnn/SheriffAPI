using Microsoft.EntityFrameworkCore.Migrations;

namespace Sheriff.Infrastructure.Migrations
{
    public partial class AddedInvitations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestId = table.Column<int>(nullable: false),
                    BandId = table.Column<int>(nullable: false),
                    HandlerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invitations_Bandits_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Bandits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invitations_Bandits_HandlerId",
                        column: x => x.HandlerId,
                        principalTable: "Bandits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_BandId",
                table: "Invitations",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_GuestId",
                table: "Invitations",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_HandlerId",
                table: "Invitations",
                column: "HandlerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invitations");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sheriff.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scorings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LootSize = table.Column<double>(nullable: false),
                    LootValue = table.Column<double>(nullable: false),
                    Service = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scorings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bandits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ScoringId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bandits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bandits_Scorings_ScoringId",
                        column: x => x.ScoringId,
                        principalTable: "Scorings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BanditId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Bandits_BanditId",
                        column: x => x.BanditId,
                        principalTable: "Bandits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BandMembers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BanditId = table.Column<int>(nullable: false),
                    BandId = table.Column<int>(nullable: false),
                    ScoringId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BandMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BandMembers_Bandits_BanditId",
                        column: x => x.BanditId,
                        principalTable: "Bandits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BandMembers_Scorings_ScoringId",
                        column: x => x.ScoringId,
                        principalTable: "Scorings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    BossId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bands_BandMembers_BossId",
                        column: x => x.BossId,
                        principalTable: "BandMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoundMembers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(nullable: false),
                    RoundId = table.Column<int>(nullable: false),
                    ScoringId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoundMembers_BandMembers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "BandMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoundMembers_Scorings_ScoringId",
                        column: x => x.ScoringId,
                        principalTable: "Scorings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Place = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    SheriffId = table.Column<int>(nullable: false),
                    BandId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rounds_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rounds_RoundMembers_SheriffId",
                        column: x => x.SheriffId,
                        principalTable: "RoundMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bandits_ScoringId",
                table: "Bandits",
                column: "ScoringId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BandMembers_BandId",
                table: "BandMembers",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_BandMembers_BanditId",
                table: "BandMembers",
                column: "BanditId");

            migrationBuilder.CreateIndex(
                name: "IX_BandMembers_ScoringId",
                table: "BandMembers",
                column: "ScoringId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bands_BossId",
                table: "Bands",
                column: "BossId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_BanditId",
                table: "Notifications",
                column: "BanditId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundMembers_MemberId",
                table: "RoundMembers",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundMembers_RoundId",
                table: "RoundMembers",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundMembers_ScoringId",
                table: "RoundMembers",
                column: "ScoringId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_BandId",
                table: "Rounds",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_SheriffId",
                table: "Rounds",
                column: "SheriffId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BandMembers_Bands_BandId",
                table: "BandMembers",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundMembers_Rounds_RoundId",
                table: "RoundMembers",
                column: "RoundId",
                principalTable: "Rounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bandits_Scorings_ScoringId",
                table: "Bandits");

            migrationBuilder.DropForeignKey(
                name: "FK_BandMembers_Scorings_ScoringId",
                table: "BandMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundMembers_Scorings_ScoringId",
                table: "RoundMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_BandMembers_Bands_BandId",
                table: "BandMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Bands_BandId",
                table: "Rounds");

            migrationBuilder.DropForeignKey(
                name: "FK_BandMembers_Bandits_BanditId",
                table: "BandMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundMembers_BandMembers_MemberId",
                table: "RoundMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundMembers_Rounds_RoundId",
                table: "RoundMembers");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Scorings");

            migrationBuilder.DropTable(
                name: "Bands");

            migrationBuilder.DropTable(
                name: "Bandits");

            migrationBuilder.DropTable(
                name: "BandMembers");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "RoundMembers");
        }
    }
}

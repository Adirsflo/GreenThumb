using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenThumb.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    plant_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Instructions_Plants_plant_id",
                        column: x => x.plant_id,
                        principalTable: "Plants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gardens",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gardens", x => x.id);
                    table.ForeignKey(
                        name: "FK_Gardens_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GardenModelPlantModel",
                columns: table => new
                {
                    GardensGardenId = table.Column<int>(type: "int", nullable: false),
                    PlantsPlantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GardenModelPlantModel", x => new { x.GardensGardenId, x.PlantsPlantId });
                    table.ForeignKey(
                        name: "FK_GardenModelPlantModel_Gardens_GardensGardenId",
                        column: x => x.GardensGardenId,
                        principalTable: "Gardens",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GardenModelPlantModel_Plants_PlantsPlantId",
                        column: x => x.PlantsPlantId,
                        principalTable: "Plants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GardenModelPlantModel_PlantsPlantId",
                table: "GardenModelPlantModel",
                column: "PlantsPlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Gardens_user_id",
                table: "Gardens",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_plant_id",
                table: "Instructions",
                column: "plant_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GardenModelPlantModel");

            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "Gardens");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

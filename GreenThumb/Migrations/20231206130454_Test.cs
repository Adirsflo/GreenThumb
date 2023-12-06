using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenThumb.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantGardens_Gardens_GardensGardenId",
                table: "PlantGardens");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantGardens_Plants_PlantsPlantId",
                table: "PlantGardens");

            migrationBuilder.DropIndex(
                name: "IX_PlantGardens_GardensGardenId",
                table: "PlantGardens");

            migrationBuilder.DropIndex(
                name: "IX_PlantGardens_PlantsPlantId",
                table: "PlantGardens");

            migrationBuilder.DropColumn(
                name: "GardensGardenId",
                table: "PlantGardens");

            migrationBuilder.DropColumn(
                name: "PlantsPlantId",
                table: "PlantGardens");

            migrationBuilder.CreateIndex(
                name: "IX_PlantGardens_garden_id",
                table: "PlantGardens",
                column: "garden_id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantGardens_Gardens_garden_id",
                table: "PlantGardens",
                column: "garden_id",
                principalTable: "Gardens",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantGardens_Plants_plant_id",
                table: "PlantGardens",
                column: "plant_id",
                principalTable: "Plants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantGardens_Gardens_garden_id",
                table: "PlantGardens");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantGardens_Plants_plant_id",
                table: "PlantGardens");

            migrationBuilder.DropIndex(
                name: "IX_PlantGardens_garden_id",
                table: "PlantGardens");

            migrationBuilder.AddColumn<int>(
                name: "GardensGardenId",
                table: "PlantGardens",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlantsPlantId",
                table: "PlantGardens",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlantGardens_GardensGardenId",
                table: "PlantGardens",
                column: "GardensGardenId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantGardens_PlantsPlantId",
                table: "PlantGardens",
                column: "PlantsPlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantGardens_Gardens_GardensGardenId",
                table: "PlantGardens",
                column: "GardensGardenId",
                principalTable: "Gardens",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantGardens_Plants_PlantsPlantId",
                table: "PlantGardens",
                column: "PlantsPlantId",
                principalTable: "Plants",
                principalColumn: "id");
        }
    }
}

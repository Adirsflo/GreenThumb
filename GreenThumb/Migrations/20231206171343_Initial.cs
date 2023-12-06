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
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "PlantGardens",
                columns: table => new
                {
                    plant_id = table.Column<int>(type: "int", nullable: false),
                    garden_id = table.Column<int>(type: "int", nullable: false),
                    date_at_garden = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantGardens", x => new { x.plant_id, x.garden_id });
                    table.ForeignKey(
                        name: "FK_PlantGardens_Gardens_garden_id",
                        column: x => x.garden_id,
                        principalTable: "Gardens",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantGardens_Plants_plant_id",
                        column: x => x.plant_id,
                        principalTable: "Plants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Plants",
                columns: new[] { "id", "description", "name", "type" },
                values: new object[,]
                {
                    { 1, "Roses are classic flowering plants known for their fragrant and colorful blooms. They come in various colors and varieties, each with its own unique scent and aesthetic appeal. Roses are often used in gardens, bouquets, and as ornamental plants.", "Rose", "Shrub" },
                    { 2, "Monstera deliciosa, also known as the Swiss cheese plant, is a popular tropical houseplant with large, glossy leaves that have distinctive splits and holes. It adds a touch of exotic beauty to indoor spaces and is relatively easy to care for.", "Monstera Deliciosa", "Houseplant" },
                    { 3, "Lavender is a fragrant herb known for its aromatic flowers and silvery-green foliage. It is often cultivated for its essential oils, used in perfumes and aromatherapy. Lavender is also a favorite in gardens, attracting pollinators and adding a soothing scent to the surroundings.", "Lavender", "Shrub" },
                    { 4, "The snake plant, also known as mother-in-law's tongue, is a hardy and low-maintenance indoor plant. It has tall, upright leaves that are often green with variegated patterns. Snake plants are known for their air-purifying qualities and can thrive in low-light conditions.", "Snake Plant", "Succulent" },
                    { 5, "Sunflowers are iconic annual plants with large, vibrant yellow flowers that resemble the sun. They are known for their heliotropic nature, with the flowers turning to face the sun. Sunflowers are commonly grown for their seeds, which are edible and often used in snacks and cooking.", "Sunflower", "Annual" },
                    { 6, "Bamboo is a versatile and fast-growing plant belonging to the grass family. It is known for its tall, slender stems (culms) and is used for various purposes, including construction, furniture, and as a decorative plant. Some bamboo species are also a source of food.", "Bamboo", "Grass" },
                    { 7, "Tulips are spring-blooming flowers with a wide range of colors and shapes. They are bulbous plants and are often associated with the arrival of spring. Tulips are popular in gardens and floral arrangements, symbolizing love and elegance.", "Tulip", "Bulb" },
                    { 8, "Cannabis is a genus of flowering plants, including sativa, indica, and ruderalis species. Known for psychoactive properties due to THC, it has distinctive palmate leaves and resinous flowers. Used historically for medicinal, recreational, and industrial purposes, cannabis contains cannabinoids like CBD. Legal status varies globally.", "Cannabis", "Herb" }
                });

            migrationBuilder.InsertData(
                table: "Instructions",
                columns: new[] { "id", "description", "name", "plant_id" },
                values: new object[,]
                {
                    { 1, "Water the rose plant 2 times a day, and pour 200 ml of water each time.", "Watering", 1 },
                    { 2, "Trim any dead or wilted flowers and leaves from the rose plant using clean pruning shears. Aim to promote healthy growth and maintain a tidy appearance.", "Pruning", 1 },
                    { 3, "Place the Monstera Deliciosa in a location with bright, indirect sunlight. Avoid direct sunlight, as it can scorch the leaves.", "Sunlight Exposure", 2 },
                    { 4, "Allow the top inch of the soil to dry out between waterings. Water the Monstera when the soil feels slightly dry, and ensure proper drainage to prevent waterlogged roots.", "Soil Moisture", 2 },
                    { 5, "After the first bloom, prune back one-third of the lavender plant to encourage bushier growth and more flowers.", "Pruning", 3 },
                    { 6, "Plant lavender in well-drained soil to prevent root rot. Add sand or perlite to improve drainage if needed.", "Well-Drained Soil", 3 },
                    { 7, "Snake plants thrive in low light but can tolerate indirect sunlight. Keep the plant in a spot with minimal light exposure.", "Low Light Tolerance", 4 },
                    { 8, "Water the snake plant sparingly, allowing the soil to dry out completely between waterings. Overwatering can lead to root rot.", "Infrequent Watering", 4 },
                    { 9, "Plant sunflowers in a location with full sun exposure for at least 6-8 hours a day for optimal growth and vibrant blooms.", "Sun Exposure", 5 },
                    { 10, "As sunflowers grow, provide support for tall stems by staking them to prevent bending or breaking in windy conditions.", "Support for Tall Stems", 5 },
                    { 11, "If growing bamboo outdoors, consider containing its spread by planting it within barriers or using a root control system to prevent it from becoming invasive.", "Containment", 6 },
                    { 12, "Fertilize bamboo with a balanced fertilizer during the growing season to promote healthy growth. Follow the recommended dosage on the fertilizer package.", "Fertilization", 6 },
                    { 13, "Remove faded or spent tulip flowers by deadheading them. This encourages the plant to redirect energy into bulb growth.", "Deadheading", 7 },
                    { 14, "Every few years, consider dividing tulip bulbs to prevent overcrowding. Lift the bulbs, separate them, and replant in well-prepared soil.", "Bulb Division", 7 },
                    { 15, "Prune lower branches of cannabis plants to improve airflow and reduce the risk of mold and pests.", "Pruning for Airflow", 8 },
                    { 16, "If cultivating cannabis indoors, maintain a consistent light schedule during the vegetative and flowering stages, providing 18 hours of light during vegetative growth and 12 hours during flowering.", "Light Schedule", 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gardens_user_id",
                table: "Gardens",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_plant_id",
                table: "Instructions",
                column: "plant_id");

            migrationBuilder.CreateIndex(
                name: "IX_PlantGardens_garden_id",
                table: "PlantGardens",
                column: "garden_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "PlantGardens");

            migrationBuilder.DropTable(
                name: "Gardens");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenThumb.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Plants",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 1, "Roses are classic flowering plants known for their fragrant and colorful blooms. They come in various colors and varieties, each with its own unique scent and aesthetic appeal. Roses are often used in gardens, bouquets, and as ornamental plants.", "Rose" },
                    { 2, "Monstera deliciosa, also known as the Swiss cheese plant, is a popular tropical houseplant with large, glossy leaves that have distinctive splits and holes. It adds a touch of exotic beauty to indoor spaces and is relatively easy to care for.", "Monstera Deliciosa" },
                    { 3, "Lavender is a fragrant herb known for its aromatic flowers and silvery-green foliage. It is often cultivated for its essential oils, used in perfumes and aromatherapy. Lavender is also a favorite in gardens, attracting pollinators and adding a soothing scent to the surroundings.", "Lavender" },
                    { 4, "The snake plant, also known as mother-in-law's tongue, is a hardy and low-maintenance indoor plant. It has tall, upright leaves that are often green with variegated patterns. Snake plants are known for their air-purifying qualities and can thrive in low-light conditions.", "Snake Plant" },
                    { 5, "Sunflowers are iconic annual plants with large, vibrant yellow flowers that resemble the sun. They are known for their heliotropic nature, with the flowers turning to face the sun. Sunflowers are commonly grown for their seeds, which are edible and often used in snacks and cooking.", "Sunflower" },
                    { 6, "Bamboo is a versatile and fast-growing plant belonging to the grass family. It is known for its tall, slender stems (culms) and is used for various purposes, including construction, furniture, and as a decorative plant. Some bamboo species are also a source of food.", "Bamboo" },
                    { 7, "Tulips are spring-blooming flowers with a wide range of colors and shapes. They are bulbous plants and are often associated with the arrival of spring. Tulips are popular in gardens and floral arrangements, symbolizing love and elegance.", "Tulip" },
                    { 8, "Cannabis is a genus of flowering plants, including sativa, indica, and ruderalis species. Known for psychoactive properties due to THC, it has distinctive palmate leaves and resinous flowers. Used historically for medicinal, recreational, and industrial purposes, cannabis contains cannabinoids like CBD. Legal status varies globally.", "Cannabis" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Plants",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Plants",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Plants",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Plants",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Plants",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Plants",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Plants",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Plants",
                keyColumn: "id",
                keyValue: 8);
        }
    }
}

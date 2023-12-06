using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using GreenThumb.Managers;
using GreenThumb.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenThumb.Database
{
    internal class GreenThumbDbContext : DbContext
    {
        private readonly IEncryptionProvider _provider;

        public GreenThumbDbContext()
        {
            _provider = new GenerateEncryptionProvider(KeyManager.GetEncryptionKey());
        }

        public DbSet<PlantModel> Plants { get; set; }
        public DbSet<InstructionModel> Instructions { get; set; }
        public DbSet<GardenModel> Gardens { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<PlantGardens> PlantGardens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GreenThumbDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.UseEncryption(_provider);

            modelBuilder.Entity<PlantGardens>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.PlantId,
                    e.GardenId
                });
            });

            // Seeding data

            modelBuilder.Entity<PlantModel>()
                .HasData(
                new PlantModel()
                {
                    PlantId = 1,
                    Name = "Rose",
                    Type = "Shrub",
                    Description = "Roses are classic flowering plants known for their fragrant and colorful blooms. They come in various colors and varieties, each with its own unique scent and aesthetic appeal. Roses are often used in gardens, bouquets, and as ornamental plants."
                },
                new PlantModel()
                {
                    PlantId = 2,
                    Name = "Monstera Deliciosa",
                    Type = "Houseplant",
                    Description = "Monstera deliciosa, also known as the Swiss cheese plant, is a popular tropical houseplant with large, glossy leaves that have distinctive splits and holes. It adds a touch of exotic beauty to indoor spaces and is relatively easy to care for."
                },
                new PlantModel()
                {
                    PlantId = 3,
                    Name = "Lavender",
                    Type = "Shrub",
                    Description = "Lavender is a fragrant herb known for its aromatic flowers and silvery-green foliage. It is often cultivated for its essential oils, used in perfumes and aromatherapy. Lavender is also a favorite in gardens, attracting pollinators and adding a soothing scent to the surroundings.",
                },
                new PlantModel()
                {
                    PlantId = 4,
                    Name = "Snake Plant",
                    Type = "Succulent",
                    Description = "The snake plant, also known as mother-in-law's tongue, is a hardy and low-maintenance indoor plant. It has tall, upright leaves that are often green with variegated patterns. Snake plants are known for their air-purifying qualities and can thrive in low-light conditions."
                },
                new PlantModel()
                {
                    PlantId = 5,
                    Name = "Sunflower",
                    Type = "Annual",
                    Description = "Sunflowers are iconic annual plants with large, vibrant yellow flowers that resemble the sun. They are known for their heliotropic nature, with the flowers turning to face the sun. Sunflowers are commonly grown for their seeds, which are edible and often used in snacks and cooking."
                },
                new PlantModel()
                {
                    PlantId = 6,
                    Name = "Bamboo",
                    Type = "Grass",
                    Description = "Bamboo is a versatile and fast-growing plant belonging to the grass family. It is known for its tall, slender stems (culms) and is used for various purposes, including construction, furniture, and as a decorative plant. Some bamboo species are also a source of food."
                },
                new PlantModel()
                {
                    PlantId = 7,
                    Name = "Tulip",
                    Type = "Bulb",
                    Description = "Tulips are spring-blooming flowers with a wide range of colors and shapes. They are bulbous plants and are often associated with the arrival of spring. Tulips are popular in gardens and floral arrangements, symbolizing love and elegance."
                },
                new PlantModel()
                {
                    PlantId = 8,
                    Name = "Cannabis",
                    Type = "Herb",
                    Description = "Cannabis is a genus of flowering plants, including sativa, indica, and ruderalis species. Known for psychoactive properties due to THC, it has distinctive palmate leaves and resinous flowers. Used historically for medicinal, recreational, and industrial purposes, cannabis contains cannabinoids like CBD. Legal status varies globally."
                }
                );

            modelBuilder.Entity<InstructionModel>()
                .HasData(
                new InstructionModel()
                {
                    InstructionId = 1,
                    Name = "Watering",
                    Description = "Water the rose plant 2 times a day, and pour 200 ml of water each time.",
                    PlantId = 1,
                },
                new InstructionModel()
                {
                    InstructionId = 2,
                    Name = "Pruning",
                    Description = "Trim any dead or wilted flowers and leaves from the rose plant using clean pruning shears. Aim to promote healthy growth and maintain a tidy appearance.",
                    PlantId = 1
                },
                new InstructionModel()
                {
                    InstructionId = 3,
                    Name = "Sunlight Exposure",
                    Description = "Place the Monstera Deliciosa in a location with bright, indirect sunlight. Avoid direct sunlight, as it can scorch the leaves.",
                    PlantId = 2
                },
                new InstructionModel()
                {
                    InstructionId = 4,
                    Name = "Soil Moisture",
                    Description = "Allow the top inch of the soil to dry out between waterings. Water the Monstera when the soil feels slightly dry, and ensure proper drainage to prevent waterlogged roots.",
                    PlantId = 2
                },
                new InstructionModel()
                {
                    InstructionId = 5,
                    Name = "Pruning",
                    Description = "After the first bloom, prune back one-third of the lavender plant to encourage bushier growth and more flowers.",
                    PlantId = 3
                },
                new InstructionModel()
                {
                    InstructionId = 6,
                    Name = "Well-Drained Soil",
                    Description = "Plant lavender in well-drained soil to prevent root rot. Add sand or perlite to improve drainage if needed.",
                    PlantId = 3
                },
                new InstructionModel()
                {
                    InstructionId = 7,
                    Name = "Low Light Tolerance",
                    Description = "Snake plants thrive in low light but can tolerate indirect sunlight. Keep the plant in a spot with minimal light exposure.",
                    PlantId = 4
                },
                new InstructionModel()
                {
                    InstructionId = 8,
                    Name = "Infrequent Watering",
                    Description = "Water the snake plant sparingly, allowing the soil to dry out completely between waterings. Overwatering can lead to root rot.",
                    PlantId = 4
                },
                new InstructionModel()
                {
                    InstructionId = 9,
                    Name = "Sun Exposure",
                    Description = "Plant sunflowers in a location with full sun exposure for at least 6-8 hours a day for optimal growth and vibrant blooms.",
                    PlantId = 5
                },
                new InstructionModel()
                {
                    InstructionId = 10,
                    Name = "Support for Tall Stems",
                    Description = "As sunflowers grow, provide support for tall stems by staking them to prevent bending or breaking in windy conditions.",
                    PlantId = 5
                },
                new InstructionModel()
                {
                    InstructionId = 11,
                    Name = "Containment",
                    Description = "If growing bamboo outdoors, consider containing its spread by planting it within barriers or using a root control system to prevent it from becoming invasive.",
                    PlantId = 6
                },
                new InstructionModel()
                {
                    InstructionId = 12,
                    Name = "Fertilization",
                    Description = "Fertilize bamboo with a balanced fertilizer during the growing season to promote healthy growth. Follow the recommended dosage on the fertilizer package.",
                    PlantId = 6
                },
                new InstructionModel()
                {
                    InstructionId = 13,
                    Name = "Deadheading",
                    Description = "Remove faded or spent tulip flowers by deadheading them. This encourages the plant to redirect energy into bulb growth.",
                    PlantId = 7
                },
                new InstructionModel()
                {
                    InstructionId = 14,
                    Name = "Bulb Division",
                    Description = "Every few years, consider dividing tulip bulbs to prevent overcrowding. Lift the bulbs, separate them, and replant in well-prepared soil.",
                    PlantId = 7
                },
                new InstructionModel()
                {
                    InstructionId = 15,
                    Name = "Pruning for Airflow",
                    Description = "Prune lower branches of cannabis plants to improve airflow and reduce the risk of mold and pests.",
                    PlantId = 8
                },
                new InstructionModel()
                {
                    InstructionId = 16,
                    Name = "Light Schedule",
                    Description = "If cultivating cannabis indoors, maintain a consistent light schedule during the vegetative and flowering stages, providing 18 hours of light during vegetative growth and 12 hours during flowering.",
                    PlantId = 8
                }
                );
        }
    }
}

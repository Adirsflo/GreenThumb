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
        }
    }
}

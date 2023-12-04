﻿// <auto-generated />
using System;
using GreenThumb.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GreenThumb.Migrations
{
    [DbContext(typeof(GreenThumbDbContext))]
    [Migration("20231204230012_SeedData")]
    partial class SeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GardenModelPlantModel", b =>
                {
                    b.Property<int>("GardensGardenId")
                        .HasColumnType("int");

                    b.Property<int>("PlantsPlantId")
                        .HasColumnType("int");

                    b.HasKey("GardensGardenId", "PlantsPlantId");

                    b.HasIndex("PlantsPlantId");

                    b.ToTable("GardenModelPlantModel");
                });

            modelBuilder.Entity("GreenThumb.Models.GardenModel", b =>
                {
                    b.Property<int>("GardenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GardenId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("start_date");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("GardenId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Gardens");
                });

            modelBuilder.Entity("GreenThumb.Models.InstructionModel", b =>
                {
                    b.Property<int>("InstructionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InstructionId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<int>("PlantId")
                        .HasColumnType("int")
                        .HasColumnName("plant_id");

                    b.HasKey("InstructionId");

                    b.HasIndex("PlantId");

                    b.ToTable("Instructions");
                });

            modelBuilder.Entity("GreenThumb.Models.PlantModel", b =>
                {
                    b.Property<int>("PlantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlantId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.HasKey("PlantId");

                    b.ToTable("Plants");

                    b.HasData(
                        new
                        {
                            PlantId = 1,
                            Description = "Roses are classic flowering plants known for their fragrant and colorful blooms. They come in various colors and varieties, each with its own unique scent and aesthetic appeal. Roses are often used in gardens, bouquets, and as ornamental plants.",
                            Name = "Rose"
                        },
                        new
                        {
                            PlantId = 2,
                            Description = "Monstera deliciosa, also known as the Swiss cheese plant, is a popular tropical houseplant with large, glossy leaves that have distinctive splits and holes. It adds a touch of exotic beauty to indoor spaces and is relatively easy to care for.",
                            Name = "Monstera Deliciosa"
                        },
                        new
                        {
                            PlantId = 3,
                            Description = "Lavender is a fragrant herb known for its aromatic flowers and silvery-green foliage. It is often cultivated for its essential oils, used in perfumes and aromatherapy. Lavender is also a favorite in gardens, attracting pollinators and adding a soothing scent to the surroundings.",
                            Name = "Lavender"
                        },
                        new
                        {
                            PlantId = 4,
                            Description = "The snake plant, also known as mother-in-law's tongue, is a hardy and low-maintenance indoor plant. It has tall, upright leaves that are often green with variegated patterns. Snake plants are known for their air-purifying qualities and can thrive in low-light conditions.",
                            Name = "Snake Plant"
                        },
                        new
                        {
                            PlantId = 5,
                            Description = "Sunflowers are iconic annual plants with large, vibrant yellow flowers that resemble the sun. They are known for their heliotropic nature, with the flowers turning to face the sun. Sunflowers are commonly grown for their seeds, which are edible and often used in snacks and cooking.",
                            Name = "Sunflower"
                        },
                        new
                        {
                            PlantId = 6,
                            Description = "Bamboo is a versatile and fast-growing plant belonging to the grass family. It is known for its tall, slender stems (culms) and is used for various purposes, including construction, furniture, and as a decorative plant. Some bamboo species are also a source of food.",
                            Name = "Bamboo"
                        },
                        new
                        {
                            PlantId = 7,
                            Description = "Tulips are spring-blooming flowers with a wide range of colors and shapes. They are bulbous plants and are often associated with the arrival of spring. Tulips are popular in gardens and floral arrangements, symbolizing love and elegance.",
                            Name = "Tulip"
                        },
                        new
                        {
                            PlantId = 8,
                            Description = "Cannabis is a genus of flowering plants, including sativa, indica, and ruderalis species. Known for psychoactive properties due to THC, it has distinctive palmate leaves and resinous flowers. Used historically for medicinal, recreational, and industrial purposes, cannabis contains cannabinoids like CBD. Legal status varies globally.",
                            Name = "Cannabis"
                        });
                });

            modelBuilder.Entity("GreenThumb.Models.UserModel", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("username");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GardenModelPlantModel", b =>
                {
                    b.HasOne("GreenThumb.Models.GardenModel", null)
                        .WithMany()
                        .HasForeignKey("GardensGardenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GreenThumb.Models.PlantModel", null)
                        .WithMany()
                        .HasForeignKey("PlantsPlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GreenThumb.Models.GardenModel", b =>
                {
                    b.HasOne("GreenThumb.Models.UserModel", "User")
                        .WithOne("Garden")
                        .HasForeignKey("GreenThumb.Models.GardenModel", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GreenThumb.Models.InstructionModel", b =>
                {
                    b.HasOne("GreenThumb.Models.PlantModel", "Plant")
                        .WithMany("Instructions")
                        .HasForeignKey("PlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plant");
                });

            modelBuilder.Entity("GreenThumb.Models.PlantModel", b =>
                {
                    b.Navigation("Instructions");
                });

            modelBuilder.Entity("GreenThumb.Models.UserModel", b =>
                {
                    b.Navigation("Garden");
                });
#pragma warning restore 612, 618
        }
    }
}

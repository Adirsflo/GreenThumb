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
    [Migration("20231204214929_Initial")]
    partial class Initial
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
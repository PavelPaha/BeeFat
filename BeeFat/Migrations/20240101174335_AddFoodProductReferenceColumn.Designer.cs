﻿// <auto-generated />
using System;
using BeeFat.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BeeFat.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240101174335_AddFoodProductReferenceColumn")]
    partial class AddFoodProductReferenceColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BeeFat.Data.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<Guid>("JournalId")
                        .HasColumnType("uuid");

                    b.Property<int>("RightCalories")
                        .HasColumnType("integer");

                    b.Property<Guid>("TrackId")
                        .HasColumnType("uuid");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("JournalId");

                    b.HasIndex("TrackId");

                    b.ToTable("BeeFatUsers");
                });

            modelBuilder.Entity("BeeFat.Domain.Infrastructure.Food", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("BeeFat.Domain.Infrastructure.FoodProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("integer");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("character varying(21)");

                    b.Property<Guid>("FoodId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsEaten")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PortionSize")
                        .HasColumnType("integer");

                    b.Property<Guid>("TrackId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("TrackId");

                    b.ToTable("FoodProducts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("FoodProduct");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("BeeFat.Domain.Infrastructure.Journal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Journals");
                });

            modelBuilder.Entity("BeeFat.Domain.Infrastructure.JournalFood", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("integer");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("character varying(21)");

                    b.Property<Guid>("FoodProductReference")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsEaten")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("JournalId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("PortionSize")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("JournalId");

                    b.ToTable("JournalFoods");

                    b.HasDiscriminator<string>("Discriminator").HasValue("JournalFood");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("BeeFat.Domain.Infrastructure.Track", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("BeeFat.Domain.Infrastructure.FoodProductGram", b =>
                {
                    b.HasBaseType("BeeFat.Domain.Infrastructure.FoodProduct");

                    b.Property<int>("Grams")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("FoodProductGram");
                });

            modelBuilder.Entity("BeeFat.Domain.Infrastructure.FoodProductPiece", b =>
                {
                    b.HasBaseType("BeeFat.Domain.Infrastructure.FoodProduct");

                    b.Property<int>("Pieces")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("FoodProductPiece");
                });

            modelBuilder.Entity("BeeFat.Domain.Infrastructure.JournalFoodGram", b =>
                {
                    b.HasBaseType("BeeFat.Domain.Infrastructure.JournalFood");

                    b.Property<int>("Grams")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("JournalFoodGram");
                });

            modelBuilder.Entity("BeeFat.Domain.Infrastructure.JournalFoodPiece", b =>
                {
                    b.HasBaseType("BeeFat.Domain.Infrastructure.JournalFood");

                    b.Property<int>("Pieces")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("JournalFoodPiece");
                });

            modelBuilder.Entity("BeeFat.Data.ApplicationUser", b =>
                {
                    b.HasOne("BeeFat.Domain.Infrastructure.Journal", "Journal")
                        .WithMany()
                        .HasForeignKey("JournalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeeFat.Domain.Infrastructure.Track", "Track")
                        .WithMany()
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("BeeFat.Domain.Models.User.PersonName", "PersonName", b1 =>
                        {
                            b1.Property<Guid>("ApplicationUserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("ApplicationUserId");

                            b1.ToTable("BeeFatUsers");

                            b1.WithOwner()
                                .HasForeignKey("ApplicationUserId");
                        });

                    b.Navigation("Journal");

                    b.Navigation("PersonName")
                        .IsRequired();

                    b.Navigation("Track");
                });

            modelBuilder.Entity("BeeFat.Domain.Infrastructure.Food", b =>
                {
                    b.OwnsOne("BeeFat.Domain.Infrastructure.Macronutrient", "Macronutrient", b1 =>
                        {
                            b1.Property<Guid>("FoodId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Calories")
                                .HasColumnType("integer");

                            b1.Property<int>("Carbohydrates")
                                .HasColumnType("integer");

                            b1.Property<int>("Fats")
                                .HasColumnType("integer");

                            b1.Property<int>("Proteins")
                                .HasColumnType("integer");

                            b1.HasKey("FoodId");

                            b1.ToTable("Foods");

                            b1.WithOwner()
                                .HasForeignKey("FoodId");
                        });

                    b.Navigation("Macronutrient")
                        .IsRequired();
                });

            modelBuilder.Entity("BeeFat.Domain.Infrastructure.FoodProduct", b =>
                {
                    b.HasOne("BeeFat.Domain.Infrastructure.Food", "Food")
                        .WithMany("FoodProducts")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeeFat.Domain.Infrastructure.Track", "Track")
                        .WithMany("FoodProducts")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("BeeFat.Domain.Infrastructure.JournalFood", b =>
                {
                    b.HasOne("BeeFat.Domain.Infrastructure.Journal", "Journal")
                        .WithMany("FoodProducts")
                        .HasForeignKey("JournalId");

                    b.OwnsOne("BeeFat.Domain.Infrastructure.Macronutrient", "Macronutrient", b1 =>
                        {
                            b1.Property<Guid>("JournalFoodId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Calories")
                                .HasColumnType("integer");

                            b1.Property<int>("Carbohydrates")
                                .HasColumnType("integer");

                            b1.Property<int>("Fats")
                                .HasColumnType("integer");

                            b1.Property<int>("Proteins")
                                .HasColumnType("integer");

                            b1.HasKey("JournalFoodId");

                            b1.ToTable("JournalFoods");

                            b1.WithOwner()
                                .HasForeignKey("JournalFoodId");
                        });

                    b.Navigation("Journal");

                    b.Navigation("Macronutrient")
                        .IsRequired();
                });

            modelBuilder.Entity("BeeFat.Domain.Infrastructure.Food", b =>
                {
                    b.Navigation("FoodProducts");
                });

            modelBuilder.Entity("BeeFat.Domain.Infrastructure.Journal", b =>
                {
                    b.Navigation("FoodProducts");
                });

            modelBuilder.Entity("BeeFat.Domain.Infrastructure.Track", b =>
                {
                    b.Navigation("FoodProducts");
                });
#pragma warning restore 612, 618
        }
    }
}

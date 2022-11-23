﻿// <auto-generated />
using System;
using MedPortal.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MedPortal.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221123143344_SetRoleToAdmin")]
    partial class SetRoleToAdmin
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MedPortal.Infrastructure.Entity.CardProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("PharamcyId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("PharamcyId");

                    b.HasIndex("ProductId");

                    b.ToTable("CardProduct");
                });

            modelBuilder.Entity("MedPortal.Infrastructure.Entity.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("MedPortal.Infrastructure.Entity.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Аlergy"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Headache"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Flu and cold"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Cough"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Vomiting"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Diarrhea"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Antibiotic"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Vitamins"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Immunostimulants"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Probiotic"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Memory and Dew"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Sleep and relaxation"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Beauty and skin"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Stress and anxiety"
                        });
                });

            modelBuilder.Entity("MedPortal.Infrastructure.Entity.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<DateTime>("YearFounded")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CountryName = "Canada",
                            Name = "Natural Factors",
                            YearFounded = new DateTime(1984, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CountryName = "Bulgaria",
                            Name = "Fortex",
                            YearFounded = new DateTime(1999, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            CountryName = "USA",
                            Name = "Centrum",
                            YearFounded = new DateTime(1999, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("MedPortal.Infrastructure.Entity.PharamcyProduct", b =>
                {
                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("PharmacyId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("PharamcyProduct");
                });

            modelBuilder.Entity("MedPortal.Infrastructure.Entity.Pharmacy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CloseTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("OpenTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pharmacies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CloseTime = "18:30",
                            Location = "Blvd.Alexander Stamboliyski 24, Center, Sofia",
                            Name = "SOpharmacy",
                            OpenTime = "8:30"
                        },
                        new
                        {
                            Id = 2,
                            CloseTime = "19:30",
                            Location = "str.Mesta 8001 zh.k.Brothers Miladinovi Burgas",
                            Name = "Framar",
                            OpenTime = "9:30"
                        });
                });

            modelBuilder.Entity("MedPortal.Infrastructure.Entity.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategotyId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<bool>("Prescription")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategotyId");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategotyId = 3,
                            Description = "Nurofen Forte is intended for symptomatic relief of mild to moderate pain such as: \r\n migraine headache, back pain, toothache, neuralgia, menstrual pain, rheumatic and muscle pain.\r\nNurofen Forte relieves pain, reduces inflammation and temperature.",
                            ImageUrl = "https://uploads.remediumapi.com/629af5c0ba14cc001a9a43b0/1/9e05403d419703a002da042be6cda776/480.jpeg",
                            ManufacturerId = 2,
                            Name = "Nurofen",
                            Prescription = false,
                            Price = 8.9m
                        },
                        new
                        {
                            Id = 2,
                            CategotyId = 2,
                            Description = "Analgin e is an analgesic medicinal product that is used to affect pain syndromes of various origins:\r\n toothache, neuralgia, neuritis, myalgia, trauma, burns, postoperative pain, phantom pain, dysmenorrhea, renal and biliary colic and headache",
                            ImageUrl = "https://cdn.epharm.bg/media/catalog/product/cache/eceadc04885f658154b13d5b2f18d6d8/s/o/sopharma-analgin-500mg-7633.jpg",
                            ManufacturerId = 1,
                            Name = "Analgin",
                            Prescription = false,
                            Price = 4.9m
                        },
                        new
                        {
                            Id = 3,
                            CategotyId = 1,
                            Description = "Centrum A-Z is a nutritional supplement with a combination of essential vitamins and minerals to maintain good health. B-vitamins (B1, B2, B6, B12) and magnesium contribute to normal metabolism and energy production. Vitamin A and C, copper and zinc contribute to the normal function of the immune system.",
                            ImageUrl = "https://cdn.epharm.bg/media/catalog/product/cache/eceadc04885f658154b13d5b2f18d6d8/c/e/centrum-ot-a-do-q-3060.png",
                            ManufacturerId = 3,
                            Name = "Centrum",
                            Prescription = false,
                            Price = 25.9m
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1f091cd0-b589-4b77-b951-480ed62fa46f",
                            ConcurrencyStamp = "5fce0a67-70da-4e40-af4e-42ef38d13348",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "dea12856-c198-4129-b3f3-b893d8395082",
                            RoleId = "1f091cd0-b589-4b77-b951-480ed62fa46f"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MedPortal.Infrastructure.Entity.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Address")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasDiscriminator().HasValue("User");

                    b.HasData(
                        new
                        {
                            Id = "dea12856-c198-4129-b3f3-b893d8395082",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f4815895-606a-41dc-ae42-d558df5f580e",
                            Email = "admin@mail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@MAIL.COM",
                            NormalizedUserName = "ADMIN@MAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEEC6+FGeZZWLAbetU1YPLzm1FcuLT8WqyRvmf2gDLnjww7+R6wtSKv6l90OCGfjpVw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "3dab6efc-fd31-4977-b655-22a83be6b630",
                            TwoFactorEnabled = false,
                            UserName = "admin@mail.com",
                            Address = "Mladost 1 bl 43 vh a et 4 ap 54"
                        },
                        new
                        {
                            Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "213228ef-b5bb-473f-888c-73b3e0711f91",
                            Email = "guest@mail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "GUEST@MAIL.COM",
                            NormalizedUserName = "GUEST@MAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEHu7w7Tkjr32zO+tORXdlMUxJe0cp6v7Mouy8tSRXSjMtkf0b80aOZX+CNRWRNX3+A==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "7518aeaf-b545-4ad3-8e00-e4262a88b5d0",
                            TwoFactorEnabled = false,
                            UserName = "guest@mail.com",
                            Address = "Mladost 3 bl 13 vh c et 7 ap 24"
                        });
                });

            modelBuilder.Entity("MedPortal.Infrastructure.Entity.CardProduct", b =>
                {
                    b.HasOne("MedPortal.Infrastructure.Entity.Cart", "Cart")
                        .WithMany("CardProducts")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedPortal.Infrastructure.Entity.Pharmacy", "Pharmacy")
                        .WithMany()
                        .HasForeignKey("PharamcyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedPortal.Infrastructure.Entity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Pharmacy");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MedPortal.Infrastructure.Entity.Cart", b =>
                {
                    b.HasOne("MedPortal.Infrastructure.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MedPortal.Infrastructure.Entity.PharamcyProduct", b =>
                {
                    b.HasOne("MedPortal.Infrastructure.Entity.Pharmacy", "Pharmacy")
                        .WithMany("PharamcyProducts")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedPortal.Infrastructure.Entity.Product", "Product")
                        .WithMany("PharamcyProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pharmacy");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MedPortal.Infrastructure.Entity.Product", b =>
                {
                    b.HasOne("MedPortal.Infrastructure.Entity.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategotyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedPortal.Infrastructure.Entity.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MedPortal.Infrastructure.Entity.Cart", b =>
                {
                    b.Navigation("CardProducts");
                });

            modelBuilder.Entity("MedPortal.Infrastructure.Entity.Pharmacy", b =>
                {
                    b.Navigation("PharamcyProducts");
                });

            modelBuilder.Entity("MedPortal.Infrastructure.Entity.Product", b =>
                {
                    b.Navigation("PharamcyProducts");
                });
#pragma warning restore 612, 618
        }
    }
}

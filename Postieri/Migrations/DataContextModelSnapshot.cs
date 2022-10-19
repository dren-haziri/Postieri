﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Postieri.Data;

#nullable disable

namespace Postieri.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);


            modelBuilder.Entity("Postieri.Models.DeliveryPrice", b =>
                {
                    b.Property<Guid>("DeliveryPriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CityTo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryTo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DimensionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PostCodeTo")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("DeliveryPriceId");

                    b.ToTable("DeliveryPrices");

                });

            modelBuilder.Entity("Postieri.Models.Business", b =>
                {
                    b.Property<int>("BusinessID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BusinessID"), 1L, 1);

                    b.Property<string>("BusinessName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.HasKey("BusinessID");

                    b.ToTable("Businesses");

                });

            modelBuilder.Entity("Postieri.Models.Dimension", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("height")
                        .HasColumnType("float");

                    b.Property<bool>("inUse")
                        .HasColumnType("bit");

                    b.Property<double>("length")
                        .HasColumnType("float");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Dimensions");
                });

            modelBuilder.Entity("Postieri.Models.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CourierId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OrderedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Sign")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrderId");

                    b.HasIndex("CourierId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Postieri.Models.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("ShelfId")
                        .HasColumnType("int");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ShelfId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Postieri.Models.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Postieri.Models.Shelf", b =>
                {
                    b.Property<int>("ShelfId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShelfId"), 1L, 1);

                    b.Property<int>("AvailableSlots")
                        .HasColumnType("int");

                    b.Property<string>("BinLetter")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("ShelfId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Shelves");
                });

            modelBuilder.Entity("Postieri.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("CourierId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasDefect")
                        .HasColumnType("bit");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<double>("LoadSpace")
                        .HasColumnType("float");

                    b.Property<double>("LoadWeight")
                        .HasColumnType("float");

                    b.Property<string>("PlateNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Postieri.Models.Warehouse", b =>
                {
                    b.Property<int>("WarehouseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WarehouseId"), 1L, 1);

                    b.Property<double>("Area")
                        .HasColumnType("float");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumOfShelves")
                        .HasColumnType("int");

                    b.HasKey("WarehouseId");

                    b.ToTable("Warehouse");
                });

            modelBuilder.Entity("Postieri.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSuspended")
                        .HasColumnType("bit");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Postieri.Models.Courier", b =>
                {
                    b.HasBaseType("Postieri.User");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasIndex("VehicleId");

                    b.ToTable("Couriers", (string)null);
                });

            modelBuilder.Entity("Postieri.Models.Order", b =>
                {
                    b.HasOne("Postieri.Models.Courier", null)
                        .WithMany("Orders")
                        .HasForeignKey("CourierId");
                });

            modelBuilder.Entity("Postieri.Models.Product", b =>
                {
                    b.HasOne("Postieri.Models.Order", null)
                        .WithMany("Products")
                        .HasForeignKey("OrderId");

                    b.HasOne("Postieri.Models.Shelf", "Shelf")
                        .WithMany("Products")
                        .HasForeignKey("ShelfId");

                    b.Navigation("Shelf");
                });

            modelBuilder.Entity("Postieri.Models.Shelf", b =>
                {
                    b.HasOne("Postieri.Models.Warehouse", "Warehouse")
                        .WithMany("Shelves")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Postieri.User", b =>
                {
                    b.HasOne("Postieri.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Postieri.Models.Courier", b =>
                {
                    b.HasOne("Postieri.User", null)
                        .WithOne()
                        .HasForeignKey("Postieri.Models.Courier", "UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Postieri.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Postieri.Models.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Postieri.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Postieri.Models.Shelf", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Postieri.Models.Warehouse", b =>
                {
                    b.Navigation("Shelves");
                });

            modelBuilder.Entity("Postieri.Models.Courier", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}

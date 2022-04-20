﻿// <auto-generated />
using System;
using ContactlessOrder.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContactlessOrder.DAL.Migrations
{
    [DbContext(typeof(ContactlessOrderContext))]
    [Migration("20220420181516_MenuModifications")]
    partial class MenuModifications
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.Catering", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<TimeSpan?>("CloseTime")
                        .HasColumnType("time");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int?>("CoordinatesId")
                        .HasColumnType("int");

                    b.Property<bool>("FullDay")
                        .HasColumnType("bit");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<TimeSpan?>("OpenTime")
                        .HasColumnType("time");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisteredDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Services")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CoordinatesId")
                        .IsUnique()
                        .HasFilter("[CoordinatesId] IS NOT NULL");

                    b.ToTable("Caterings", (string)null);
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.CateringMenuOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<int>("CateringId")
                        .HasColumnType("int");

                    b.Property<bool>("InheritPrice")
                        .HasColumnType("bit");

                    b.Property<int>("MenuOptionId")
                        .HasColumnType("int");

                    b.Property<int?>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CateringId");

                    b.HasIndex("MenuOptionId");

                    b.ToTable("CateringMenuOptions", (string)null);
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Companies", (string)null);
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.Coordinate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<float>("Lat")
                        .HasColumnType("real");

                    b.Property<float>("Lng")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Coordinates", (string)null);
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.MenuItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Menu", (string)null);
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.MenuItemOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MenuItemId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MenuItemId");

                    b.ToTable("MenuOptions", (string)null);
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.MenuItemPicture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MenuItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MenuItemId");

                    b.ToTable("MenuPictures", (string)null);
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.MenuModification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MenuItemId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MenuItemId");

                    b.ToTable("MenuModifications", (string)null);
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Orders.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Orders.OrderPosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("OptionId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OptionId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderPositions", (string)null);
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Orders.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("OrderStatuses", (string)null);
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("ProfilePhotoPath")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Email = "contactless.order@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "ContactlessOrder",
                            LastName = "Admin",
                            PasswordHash = "4297f44b13955235245b2497399d7a93",
                            PhoneNumber = "",
                            RegistrationDate = new DateTime(2022, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.Catering", b =>
                {
                    b.HasOne("ContactlessOrder.DAL.Entities.Companies.Company", "Company")
                        .WithMany("Caterings")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContactlessOrder.DAL.Entities.Companies.Coordinate", "Coordinates")
                        .WithOne("Catering")
                        .HasForeignKey("ContactlessOrder.DAL.Entities.Companies.Catering", "CoordinatesId");

                    b.Navigation("Company");

                    b.Navigation("Coordinates");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.CateringMenuOption", b =>
                {
                    b.HasOne("ContactlessOrder.DAL.Entities.Companies.Catering", "Catering")
                        .WithMany("MenuOptions")
                        .HasForeignKey("CateringId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContactlessOrder.DAL.Entities.Companies.MenuItemOption", "MenuOption")
                        .WithMany()
                        .HasForeignKey("MenuOptionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Catering");

                    b.Navigation("MenuOption");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.Company", b =>
                {
                    b.HasOne("ContactlessOrder.DAL.Entities.Users.User", "User")
                        .WithOne("Company")
                        .HasForeignKey("ContactlessOrder.DAL.Entities.Companies.Company", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.MenuItem", b =>
                {
                    b.HasOne("ContactlessOrder.DAL.Entities.Companies.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.MenuItemOption", b =>
                {
                    b.HasOne("ContactlessOrder.DAL.Entities.Companies.MenuItem", "MenuItem")
                        .WithMany("Options")
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuItem");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.MenuItemPicture", b =>
                {
                    b.HasOne("ContactlessOrder.DAL.Entities.Companies.MenuItem", "MenuItem")
                        .WithMany("Pictures")
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuItem");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.MenuModification", b =>
                {
                    b.HasOne("ContactlessOrder.DAL.Entities.Companies.MenuItem", "MenuItem")
                        .WithMany("Modifications")
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuItem");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Orders.Order", b =>
                {
                    b.HasOne("ContactlessOrder.DAL.Entities.Orders.OrderStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContactlessOrder.DAL.Entities.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Orders.OrderPosition", b =>
                {
                    b.HasOne("ContactlessOrder.DAL.Entities.Companies.CateringMenuOption", "Option")
                        .WithMany()
                        .HasForeignKey("OptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContactlessOrder.DAL.Entities.Orders.Order", "Order")
                        .WithMany("Positions")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Option");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.Catering", b =>
                {
                    b.Navigation("MenuOptions");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.Company", b =>
                {
                    b.Navigation("Caterings");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.Coordinate", b =>
                {
                    b.Navigation("Catering");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.MenuItem", b =>
                {
                    b.Navigation("Modifications");

                    b.Navigation("Options");

                    b.Navigation("Pictures");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Orders.Order", b =>
                {
                    b.Navigation("Positions");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Users.User", b =>
                {
                    b.Navigation("Company");
                });
#pragma warning restore 612, 618
        }
    }
}

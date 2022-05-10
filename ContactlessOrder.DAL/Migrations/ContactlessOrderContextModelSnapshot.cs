﻿// <auto-generated />
using System;
using ContactlessOrder.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContactlessOrder.DAL.Migrations
{
    [DbContext(typeof(ContactlessOrderContext))]
    partial class ContactlessOrderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.CateringModification", b =>
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

                    b.Property<int>("ModificationId")
                        .HasColumnType("int");

                    b.Property<int?>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CateringId");

                    b.HasIndex("ModificationId");

                    b.ToTable("CateringModifications", (string)null);
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

                    b.Property<int?>("ApprovedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ApprovedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApprovedById");

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

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.MenuItemModification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MenuItemId")
                        .HasColumnType("int");

                    b.Property<int>("ModificationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MenuItemId");

                    b.HasIndex("ModificationId");

                    b.ToTable("MenuItemModifications", (string)null);
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

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.Modification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Modifications", (string)null);
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.PaymentData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Bank")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("CurrentAccount")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("LegalEntityCode")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Mfo")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("TaxNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId")
                        .IsUnique();

                    b.ToTable("PaymentData", (string)null);
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

                    b.Property<int>("PaymentMethodId")
                        .HasColumnType("int");

                    b.Property<string>("PaymentNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaymentMethodId");

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

                    b.Property<int>("InMomentPrice")
                        .HasColumnType("int");

                    b.Property<int?>("OptionId")
                        .HasColumnType("int");

                    b.Property<string>("OptionName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OptionId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderPositions", (string)null);
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Orders.OrderPositionModification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("InMomentPrice")
                        .HasColumnType("int");

                    b.Property<int?>("ModificationId")
                        .HasColumnType("int");

                    b.Property<string>("ModificationName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("OrderPositionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ModificationId");

                    b.HasIndex("OrderPositionId");

                    b.ToTable("OrderPositionModification", (string)null);
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

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Orders.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethods", (string)null);
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Users.Complain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CateringId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CateringId");

                    b.HasIndex("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Complains", (string)null);
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Users.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);
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

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

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

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Email = "fS2AoXS3H0YDpNXj",
                            EmailConfirmed = true,
                            FirstName = "ContactlessOrder",
                            IsBlocked = false,
                            LastName = "Admin",
                            PasswordHash = "4297f44b13955235245b2497399d7a93",
                            PhoneNumber = "",
                            RegistrationDate = new DateTime(2022, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RoleId = 0
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

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.CateringModification", b =>
                {
                    b.HasOne("ContactlessOrder.DAL.Entities.Companies.Catering", "Catering")
                        .WithMany("CateringModifications")
                        .HasForeignKey("CateringId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ContactlessOrder.DAL.Entities.Companies.Modification", "Modification")
                        .WithMany()
                        .HasForeignKey("ModificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catering");

                    b.Navigation("Modification");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.Company", b =>
                {
                    b.HasOne("ContactlessOrder.DAL.Entities.Users.User", "ApprovedBy")
                        .WithMany()
                        .HasForeignKey("ApprovedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("ContactlessOrder.DAL.Entities.Users.User", "User")
                        .WithOne("Company")
                        .HasForeignKey("ContactlessOrder.DAL.Entities.Companies.Company", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApprovedBy");

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

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.MenuItemModification", b =>
                {
                    b.HasOne("ContactlessOrder.DAL.Entities.Companies.MenuItem", "MenuItem")
                        .WithMany("MenuItemModifications")
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ContactlessOrder.DAL.Entities.Companies.Modification", "Modification")
                        .WithMany()
                        .HasForeignKey("ModificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuItem");

                    b.Navigation("Modification");
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

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.Modification", b =>
                {
                    b.HasOne("ContactlessOrder.DAL.Entities.Companies.Company", "Company")
                        .WithMany("Modifications")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.PaymentData", b =>
                {
                    b.HasOne("ContactlessOrder.DAL.Entities.Companies.Company", "Company")
                        .WithOne("PaymentData")
                        .HasForeignKey("ContactlessOrder.DAL.Entities.Companies.PaymentData", "CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Orders.Order", b =>
                {
                    b.HasOne("ContactlessOrder.DAL.Entities.Orders.PaymentMethod", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

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

                    b.Navigation("PaymentMethod");

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Orders.OrderPosition", b =>
                {
                    b.HasOne("ContactlessOrder.DAL.Entities.Companies.CateringMenuOption", "Option")
                        .WithMany()
                        .HasForeignKey("OptionId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("ContactlessOrder.DAL.Entities.Orders.Order", "Order")
                        .WithMany("Positions")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Option");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Orders.OrderPositionModification", b =>
                {
                    b.HasOne("ContactlessOrder.DAL.Entities.Companies.Modification", "Modification")
                        .WithMany()
                        .HasForeignKey("ModificationId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("ContactlessOrder.DAL.Entities.Orders.OrderPosition", "OrderPosition")
                        .WithMany("Modifications")
                        .HasForeignKey("OrderPositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Modification");

                    b.Navigation("OrderPosition");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Users.Complain", b =>
                {
                    b.HasOne("ContactlessOrder.DAL.Entities.Companies.Catering", "Catering")
                        .WithMany()
                        .HasForeignKey("CateringId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("ContactlessOrder.DAL.Entities.Orders.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("ContactlessOrder.DAL.Entities.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Catering");

                    b.Navigation("Order");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Users.User", b =>
                {
                    b.HasOne("ContactlessOrder.DAL.Entities.Users.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.Catering", b =>
                {
                    b.Navigation("CateringModifications");

                    b.Navigation("MenuOptions");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.Company", b =>
                {
                    b.Navigation("Caterings");

                    b.Navigation("Modifications");

                    b.Navigation("PaymentData");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.Coordinate", b =>
                {
                    b.Navigation("Catering");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Companies.MenuItem", b =>
                {
                    b.Navigation("MenuItemModifications");

                    b.Navigation("Options");

                    b.Navigation("Pictures");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Orders.Order", b =>
                {
                    b.Navigation("Positions");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Orders.OrderPosition", b =>
                {
                    b.Navigation("Modifications");
                });

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.Users.User", b =>
                {
                    b.Navigation("Company");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using ContactlessOrder.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContactlessOrder.DAL.Migrations
{
    [DbContext(typeof(ContactlessOrderContext))]
    partial class ContactlessOrderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ContactlessOrder.DAL.Entities.User.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.ToTable("Users");

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
                            RegistrationDate = new DateTime(2022, 3, 26, 14, 54, 37, 153, DateTimeKind.Local).AddTicks(3388)
                        },
                        new
                        {
                            Id = 1,
                            Email = "contactless.order@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "ContactlessOrder",
                            LastName = "Admin",
                            PasswordHash = "4297f44b13955235245b2497399d7a93",
                            PhoneNumber = "",
                            RegistrationDate = new DateTime(2022, 3, 26, 14, 54, 37, 156, DateTimeKind.Local).AddTicks(3116)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

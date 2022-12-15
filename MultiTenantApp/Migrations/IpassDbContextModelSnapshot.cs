﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MultiTenantApp.Database;

#nullable disable

namespace MultiTenantApp.Migrations
{
    [DbContext(typeof(IpassDbContext))]
    partial class IpassDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MultiTenantApp.Models.Pass", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Kind")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tenant")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Passes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5afc5d78-a7c3-89e7-629a-3a08275db903"),
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Kind = "Dog",
                            Name = "Samson",
                            Tenant = "Khalid"
                        },
                        new
                        {
                            Id = new Guid("ee6b7029-03d7-517a-1e97-3a08275db903"),
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Kind = "Dog",
                            Name = "Guinness",
                            Tenant = "Khalid"
                        },
                        new
                        {
                            Id = new Guid("fa77153d-11b8-e783-ee41-3a08275db903"),
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Kind = "Cat",
                            Name = "Grumpy Cat",
                            Tenant = "Internet"
                        },
                        new
                        {
                            Id = new Guid("6498e94b-8e27-09df-76eb-3a08275db903"),
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Kind = "Cat",
                            Name = "Mr. Bigglesworth",
                            Tenant = "Internet"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

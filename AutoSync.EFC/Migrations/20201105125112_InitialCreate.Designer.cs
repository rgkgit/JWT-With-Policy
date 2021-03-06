﻿// <auto-generated />
using System;
using AutoSync.EFC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AutoSync.EFC.Migrations
{
    [DbContext(typeof(AutoSyncDbContext))]
    [Migration("20201105125112_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AutoSync.Core.Authorization.Roles", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("AuditDetailId")
                        .HasColumnType("bigint");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            RoleName = "SuperAdmin"
                        },
                        new
                        {
                            Id = 2L,
                            RoleName = "Admin"
                        },
                        new
                        {
                            Id = 3L,
                            RoleName = "Supervisor"
                        },
                        new
                        {
                            Id = 4L,
                            RoleName = "Quality Checker"
                        },
                        new
                        {
                            Id = 5L,
                            RoleName = "Technician"
                        });
                });

            modelBuilder.Entity("AutoSync.Core.Authorization.Setting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("AuditDetailId")
                        .HasColumnType("bigint");

                    b.Property<int?>("AutoDeleteInterval")
                        .HasColumnType("int");

                    b.Property<string>("AutoSyncDays")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AutoSyncTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FolderFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("SupervisorId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Setting");
                });

            modelBuilder.Entity("AutoSync.Core.Authorization.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("AuditDetailId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            IsActive = true,
                            Password = "superadmin@123",
                            Username = "superadmin"
                        });
                });

            modelBuilder.Entity("AutoSync.Core.Authorization.UserRoles", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("AuditDetailId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            RoleId = 1L,
                            UserId = 1L
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManagerAPI.EFCore;

#nullable disable

namespace TaskManagerAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231102111016_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TaskManagerAPI.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfCreation = new DateTime(2023, 11, 2, 14, 10, 16, 693, DateTimeKind.Local).AddTicks(8809),
                            Name = "Work"
                        },
                        new
                        {
                            Id = 2,
                            DateOfCreation = new DateTime(2023, 11, 2, 14, 10, 16, 693, DateTimeKind.Local).AddTicks(8824),
                            Name = "Self-development"
                        },
                        new
                        {
                            Id = 3,
                            DateOfCreation = new DateTime(2023, 11, 2, 14, 10, 16, 693, DateTimeKind.Local).AddTicks(8825),
                            Name = "Part-time job"
                        },
                        new
                        {
                            Id = 4,
                            DateOfCreation = new DateTime(2023, 11, 2, 14, 10, 16, 693, DateTimeKind.Local).AddTicks(8826),
                            Name = "Home"
                        },
                        new
                        {
                            Id = 5,
                            DateOfCreation = new DateTime(2023, 11, 2, 14, 10, 16, 693, DateTimeKind.Local).AddTicks(8826),
                            Name = "Immediately"
                        });
                });

            modelBuilder.Entity("TaskManagerAPI.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfCreation = new DateTime(2023, 11, 2, 14, 10, 16, 693, DateTimeKind.Local).AddTicks(8924),
                            Description = "",
                            Name = "Programm"
                        },
                        new
                        {
                            Id = 2,
                            DateOfCreation = new DateTime(2023, 11, 2, 14, 10, 16, 693, DateTimeKind.Local).AddTicks(8925),
                            Description = "",
                            Name = "Training"
                        },
                        new
                        {
                            Id = 3,
                            DateOfCreation = new DateTime(2023, 11, 2, 14, 10, 16, 693, DateTimeKind.Local).AddTicks(8926),
                            Description = "",
                            Name = "Orders"
                        },
                        new
                        {
                            Id = 4,
                            DateOfCreation = new DateTime(2023, 11, 2, 14, 10, 16, 693, DateTimeKind.Local).AddTicks(8927),
                            Description = "",
                            Name = "Cleaning"
                        });
                });

            modelBuilder.Entity("TaskManagerAPI.Models.TaskTag", b =>
                {
                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("TaskId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("TaskTags");

                    b.HasData(
                        new
                        {
                            TaskId = 1,
                            TagId = 1
                        },
                        new
                        {
                            TaskId = 1,
                            TagId = 5
                        },
                        new
                        {
                            TaskId = 2,
                            TagId = 2
                        },
                        new
                        {
                            TaskId = 3,
                            TagId = 3
                        },
                        new
                        {
                            TaskId = 4,
                            TagId = 4
                        });
                });

            modelBuilder.Entity("TaskManagerAPI.Models.TaskTag", b =>
                {
                    b.HasOne("TaskManagerAPI.Models.Tag", "Tag")
                        .WithMany("TaskTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskManagerAPI.Models.Task", "Task")
                        .WithMany("TaskTags")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TaskManagerAPI.Models.Tag", b =>
                {
                    b.Navigation("TaskTags");
                });

            modelBuilder.Entity("TaskManagerAPI.Models.Task", b =>
                {
                    b.Navigation("TaskTags");
                });
#pragma warning restore 612, 618
        }
    }
}

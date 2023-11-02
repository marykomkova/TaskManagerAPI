using System;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.EFCore
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Tag> Tages { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<TaskTag> TaskTags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskTag>()
                .HasKey(ur => new { ur.TaskId, ur.TagId });

            modelBuilder.Entity<TaskTag>()
                .HasOne(u => u.Task)
                .WithMany(ur => ur.TaskTags)
                .HasForeignKey(r => r.TaskId);

            modelBuilder.Entity<TaskTag>()
                .HasOne(r => r.Tag)
                .WithMany(ur => ur.TaskTags)
                .HasForeignKey(u => u.TagId);

            modelBuilder.Entity<Tag>().HasData(
                new Tag[]
                {
                    new Tag { Id=1, Name="Work"},
                    new Tag { Id=2, Name="Self-development"},
                    new Tag { Id=3, Name="Part-time job"},
                    new Tag { Id=4, Name="Home"},
                    new Tag { Id=5, Name="Immediately"}

                });

            modelBuilder.Entity<Models.Task>().HasData(
                new Models.Task[]
                {
                    new Models.Task { Id=1, Name="Programm", Description=""},
                    new Models.Task { Id=2, Name="Training", Description=""},
                    new Models.Task { Id=3, Name="Orders", Description=""},
                    new Models.Task { Id=4, Name="Cleaning", Description=""},
                });

            modelBuilder.Entity<TaskTag>().HasData(
                new TaskTag[]
                {
                    new TaskTag { TaskId=1, TagId=1},
                    new TaskTag { TaskId=1, TagId=5},
                    new TaskTag { TaskId=2, TagId=2},
                    new TaskTag { TaskId=3, TagId=3},
                    new TaskTag { TaskId=4, TagId=4},
                });
        }
    }
}


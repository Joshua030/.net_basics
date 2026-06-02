using System;

namespace DiaryApp.Data;

using Microsoft.EntityFrameworkCore;
using DiaryApp.Models;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{

    //NOTE -  Steps to add a table to the database:
    // 1. Create a new class in the Models folder (e.g. DiaryEntry.cs)
    // 2. Add a DbSet property to the AppDbContext class (e.g. public DbSet<DiaryEntry> DiaryEntries { get; set; })
    // 3. Create a new migration (e.g. AddDiaryEntryTable) using the Package Manager Console (PMC) or the dotnet CLI
    // 4. Update the database to apply the migration (e.g. Update-Database in PMC or dotnet ef database update in CLI)


    //SECTION -  Add a new table to the database
    public DbSet<DiaryEntry> DiaryEntries { get; set; }

    // SECTION - Override the OnModelCreating method to configure the model (optional)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //NOTE - Seeding the database with initial data (optional)

        modelBuilder.Entity<DiaryEntry>().HasData(
            new DiaryEntry
            {
                Id = 1,
                Title = "My First Diary Entry",
                Content = "Today I started my diary app. I'm excited to see how it turns out!",
                Created = new DateTime(2026, 1, 1, 9, 0, 0)
            },
            new DiaryEntry
            {
                Id = 2,
                Title = "A Day at the Park",
                Content = "I had a great day at the park with my friends. We played frisbee and had a picnic.",
                Created = new DateTime(2026, 1, 2, 14, 30, 0)
            }
        );
    }

}

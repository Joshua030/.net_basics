using Microsoft.EntityFrameworkCore;
using WebDiaryAPI.Models;

namespace WebDiaryAPI.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<DiaryEntry> DiaryEntries { get; set; }
    }
}

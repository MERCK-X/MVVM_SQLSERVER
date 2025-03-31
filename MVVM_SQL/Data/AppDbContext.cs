using Microsoft.EntityFrameworkCore;
using MVVM_SQL.Model;

namespace MVVM_SQL.Data
{
    public class AppDbContext : DbContext
    {
        private const string DatabaseFilename = "MauiApp.db";
        private static string DatabasePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseFilename);

        public DbSet<UserModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={DatabasePath}");
        }
    }
}

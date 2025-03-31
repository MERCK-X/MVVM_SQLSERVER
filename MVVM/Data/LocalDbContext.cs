using Foundation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Data
{
    public class LocalDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<SyncPending> SyncPendingOperations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databasePath = Path.Combine(FileSystem.AppDataDirectory, "localdb.db3");
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }
    }
}

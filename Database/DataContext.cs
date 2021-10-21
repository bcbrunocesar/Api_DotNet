using Dot.Models;
using Microsoft.EntityFrameworkCore;

namespace Dot.Database
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");
    }
}
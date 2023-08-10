namespace StoreMeDaddy.Models;
using Microsoft.EntityFrameworkCore;

public class StoreMeDaddyContext : DbContext
{
    public virtual DbSet<UserModel> Users { get; set; }
    public virtual DbSet<MetaDataModel> MetaData { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=storemedaddy.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

}
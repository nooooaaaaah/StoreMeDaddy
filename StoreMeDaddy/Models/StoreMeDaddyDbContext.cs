namespace StoreMeDaddy.Models;
using Microsoft.EntityFrameworkCore;

public partial class StoreMeDaddyContext : DbContext
{
    public StoreMeDaddyContext(DbContextOptions options) : base(options)
    {
    }

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
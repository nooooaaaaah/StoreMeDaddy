namespace StoreMeDaddy.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreMeDaddy.Models;



public partial class StoreMeDaddyContext : DbContext
{
    public StoreMeDaddyContext(DbContextOptions options) : base(options)
    {
    }
    // public DbSet<DataProtectionKeyModel> DataProtectionKeys { get; set; }
    public virtual DbSet<UserModel> Users { get; set; }
    public virtual DbSet<MetaDataModel> MetaData { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=storemedaddy.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        {
            modelBuilder.Entity<MetaDataModel>()
                .Property(m => m.FileName)
                .IsRequired();

            modelBuilder.Entity<MetaDataModel>()
                .Property(m => m.Description)
                .IsRequired();

            modelBuilder.Entity<MetaDataModel>()
                .Property(m => m.Path)
                .IsRequired();

            modelBuilder.Entity<MetaDataModel>()
                .Property(m => m.Size)
                .IsRequired();

            modelBuilder.Entity<MetaDataModel>()
                .Property(m => m.Type)
                .IsRequired();

            modelBuilder.Entity<MetaDataModel>()
                .Property(m => m.Version)
                .IsRequired();

            modelBuilder.Entity<MetaDataModel>()
                .Property(m => m.UserId)
                .IsRequired();

            // modelBuilder.Entity<MetaDataModel>()
            //     .HasOne(m => m.User)
            //     .WithMany(u => u.MetaData)
            //     .HasForeignKey(m => m.UserId)
            //     .OnDelete(DeleteBehavior.Cascade);
        }

        modelBuilder.Entity<MetaDataModel>()
            .Property(m => m.IV)
            .IsRequired();
        // modelBuilder.Entity<DataProtectionKeyModel>(entity =>
        // {
        //     entity.HasKey(e => e.Id);
        //     entity.Property(e => e.FriendlyName).IsRequired();
        //     entity.Property(e => e.Xml).IsRequired();
        // });
    }
    public MetaDataModel? GetMetaDataWithUser(string id)
    {
        return MetaData.Include(m => m.User)
            .FirstOrDefault(m => m.UserId == id);
    }
}
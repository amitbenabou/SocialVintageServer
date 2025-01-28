using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SocialVintageServer.Models;

public partial class SocialVintageDbContext : DbContext
{
    public SocialVintageDbContext()
    {
    }

    public SocialVintageDbContext(DbContextOptions<SocialVintageDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Catagory> Catagories { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemsImage> ItemsImages { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Shipping> Shippings { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB;Initial Catalog=SocialVintageDB;User ID=AdminLogin;Password=amitbe1011!;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Catagory>(entity =>
        {
            entity.HasKey(e => e.CatagoryId).HasName("PK__Catagory__3468E3B396B007E3");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Item__727E838B8344BC78");

            entity.HasOne(d => d.Store).WithMany(p => p.Items)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Item__StoreId__300424B4");
        });

        modelBuilder.Entity<ItemsImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ItemsIma__3214EC078B668286");

            entity.HasOne(d => d.Item).WithMany(p => p.ItemsImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ItemsImag__ItemI__32E0915F");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__C3905BCF224488C9");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__StatusId__38996AB5");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__UserId__37A5467C");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Review__74BC79CEFCE47D09");

            entity.HasOne(d => d.Store).WithMany(p => p.Reviews)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Review__StoreId__3C69FB99");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Review__UserId__3B75D760");
        });

        modelBuilder.Entity<Shipping>(entity =>
        {
            entity.HasKey(e => e.OptionId).HasName("PK__Shipping__92C7A1FF56E176C3");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status__C8EE206399F54917");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Store__3B82F101562C2171");

            entity.Property(e => e.StoreId).ValueGeneratedNever();

            entity.HasOne(d => d.Catagory).WithMany(p => p.Stores)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Store__CatagoryI__2D27B809");

            entity.HasOne(d => d.Option).WithMany(p => p.Stores)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Store__OptionId__2C3393D0");

            entity.HasOne(d => d.StoreNavigation).WithOne(p => p.Store)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Store__StoreId__2B3F6F97");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4CD74A0D62");

            entity.HasMany(d => d.Stores).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "FavoriteStore",
                    r => r.HasOne<Store>().WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__FavoriteS__Store__403A8C7D"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__FavoriteS__UserI__3F466844"),
                    j =>
                    {
                        j.HasKey("UserId", "StoreId").HasName("PK__Favorite__1430E35C60439249");
                        j.ToTable("FavoriteStores");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

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
            entity.HasKey(e => e.CatagoryId).HasName("PK__Catagory__3468E3B3A2F33DB6");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Item__727E838BF7168DDB");

            entity.HasOne(d => d.Store).WithMany(p => p.Items)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Item__StoreId__300424B4");
        });

        modelBuilder.Entity<ItemsImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ItemsIma__3214EC071E8AB2B9");

            entity.HasOne(d => d.Item).WithMany(p => p.ItemsImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ItemsImag__ItemI__32E0915F");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__C3905BCF351D2C68");

            entity.Property(e => e.OrderId).ValueGeneratedNever();

            entity.HasOne(d => d.OrderNavigation).WithOne(p => p.Order)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__OrderId__37A5467C");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__UserId__38996AB5");
        });

        modelBuilder.Entity<Shipping>(entity =>
        {
            entity.HasKey(e => e.OptionId).HasName("PK__Shipping__92C7A1FF25209424");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status__C8EE2063DE756CE8");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Store__3B82F10194223AA9");

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
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C2FC00EBD");

            entity.HasMany(d => d.Items).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "WishListItem",
                    r => r.HasOne<Item>().WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__WishListI__ItemI__3C69FB99"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__WishListI__UserI__3B75D760"),
                    j =>
                    {
                        j.HasKey("UserId", "ItemId").HasName("PK__WishList__B0AF2474C5D7DC1C");
                        j.ToTable("WishListItem");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

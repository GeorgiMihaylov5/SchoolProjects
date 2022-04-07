using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ParkingManager.Models
{
    public partial class ParkingManagerDatabaseContext : DbContext
    {
        public ParkingManagerDatabaseContext()
        {
            Database.EnsureCreated();
        }

        public ParkingManagerDatabaseContext(DbContextOptions<ParkingManagerDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ParkingSpot> ParkingSpot { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersToParkingSpot> UsersToParkingSpot { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=DESKTOP-92;Integrated Security=true;Database=ParkingManagerDB");
            }
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<UsersToParkingSpot>().HasKey(x => new { x.UserId, x.ParkingSpotId });
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParkingSpot>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.ParkingSpotNumber)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsFixedLength();

                entity.Property(e => e.TotalDaysInPark)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<UsersToParkingSpot>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.EndingRentDate).HasColumnType("datetime");

                entity.Property(e => e.StartingRentDate).HasColumnType("datetime");

                entity.HasOne(d => d.ParkingSpot)
                    .WithMany(p => p.UsersToParkingSpot)
                    .HasForeignKey(d => d.ParkingSpotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsersToParkingSpot_Users");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersToParkingSpot)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UsersToParkingSpot_ParkingSpot");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

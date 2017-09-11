using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace tvShowProject.Models.Entities
{
    public partial class TvContext : DbContext
    {
        public virtual DbSet<TvTable> TvTable { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserToTvTable> UserToTvTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = ProjTest; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TvTable>(entity =>
            {
                entity.ToTable("TvTable", "tv");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "tv");

                entity.HasIndex(e => e.AspNetUserId)
                    .HasName("UQ__User__F42021464F00CC48")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AspNetUserId)
                    .IsRequired()
                    .HasColumnName("AspNetUserID");
            });

            modelBuilder.Entity<UserToTvTable>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.TvTableId });

                entity.ToTable("UserToTvTable", "tv");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.TvTableId).HasColumnName("TvTableID");

                entity.HasOne(d => d.TvTable)
                    .WithMany(p => p.UserToTvTable)
                    .HasForeignKey(d => d.TvTableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserToTvT__TvTab__3D5E1FD2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserToTvTable)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserToTvT__UserI__3C69FB99");
            });
        }
    }
}

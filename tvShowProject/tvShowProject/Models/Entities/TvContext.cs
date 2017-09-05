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
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = ProjTest; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TvTable>(entity =>
            {
                entity.ToTable("TvTable", "tv");

                entity.HasIndex(e => e.ImdbId)
                    .HasName("UQ__TvTable__21608C188BF0E4B3")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ImdbId)
                    .IsRequired()
                    .HasColumnName("IMDB_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.NextReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.QueryString).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "tv");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AspNetUserId)
                    .IsRequired()
                    .HasColumnName("AspNetUserID")
                    .HasMaxLength(450);
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
                    .HasConstraintName("FK__UserToTvT__TvTab__4222D4EF");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserToTvTable)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserToTvT__UserI__412EB0B6");
            });
        }
    }
}

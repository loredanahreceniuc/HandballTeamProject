using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using exp.NET6.Infrastructure.Entities;

namespace exp.NET6.Infrastructure.Context
{
    public partial class FlowerPowerDbContext : DbContext
    {
        public FlowerPowerDbContext()
        {
        }

        public FlowerPowerDbContext(DbContextOptions<FlowerPowerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; } = null!;
        public virtual DbSet<ArticleGallery> ArticleGalleries { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<BlogCategory> BlogCategories { get; set; } = null!;
        public virtual DbSet<ClubDetail> ClubDetails { get; set; } = null!;
        public virtual DbSet<Competition> Competitions { get; set; } = null!;
        public virtual DbSet<Match> Matches { get; set; } = null!;
        public virtual DbSet<NextMatch> NextMatches { get; set; } = null!;
        public virtual DbSet<Player> Players { get; set; } = null!;
        public virtual DbSet<PlayerGallery> PlayerGalleries { get; set; } = null!;
        public virtual DbSet<PlayerHistory> PlayerHistories { get; set; } = null!;
        public virtual DbSet<Sponsor> Sponsors { get; set; } = null!;
        public virtual DbSet<Team> Teams { get; set; } = null!;
        public virtual DbSet<TeamCategory> TeamCategories { get; set; } = null!;
        public virtual DbSet<TeamsRanking> TeamsRankings { get; set; } = null!;
        public virtual DbSet<Trophy> Trophies { get; set; } = null!;
        public virtual DbSet<UserLocation> UserLocations { get; set; } = null!;
        public virtual DbSet<staff> staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=flowerpowerdb.database.windows.net;Initial Catalog=FlowerPowerDB;User ID=flowerpowerdb;Password=Database2023!;Database=FlowerPowerDb;Trusted_Connection=False;Encrypt=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Article");

                entity.Property(e => e.AprovedBy).HasMaxLength(450);

                entity.Property(e => e.Content).HasMaxLength(2000);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ShortDescription).HasMaxLength(200);

                entity.Property(e => e.Tags).HasMaxLength(200);

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.HasOne(d => d.AprovedByNavigation)
                    .WithMany(p => p.ArticleAprovedByNavigations)
                    .HasForeignKey(d => d.AprovedBy)
                    .HasConstraintName("FK__Article__Aproved__6E8B6712");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Article__Categor__6CA31EA0");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ArticleCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__Article__Created__6D9742D9");
            });

            modelBuilder.Entity<ArticleGallery>(entity =>
            {
                entity.ToTable("ArticleGallery");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleGalleries)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK__ArticleGa__Artic__73501C2F");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FidelityPoints).HasDefaultValueSql("((0))");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.ImgUrl).HasMaxLength(300);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<BlogCategory>(entity =>
            {
                entity.ToTable("BlogCategory");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<ClubDetail>(entity =>
            {
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Competition>(entity =>
            {
                entity.ToTable("Competition");

                entity.Property(e => e.Name).HasMaxLength(300);
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.Property(e => e.Away).HasMaxLength(200);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Home).HasMaxLength(200);

                entity.HasOne(d => d.Competition)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.CompetitionId)
                    .HasConstraintName("FK__Matches__Competi__1C1D2798");
            });

            modelBuilder.Entity<NextMatch>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Guests).HasMaxLength(200);

                entity.Property(e => e.Hosts).HasMaxLength(200);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.MatchDate)
                    .HasColumnType("date")
                    .HasColumnName("matchDate");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("Player");

                entity.Property(e => e.Biography).HasMaxLength(1000);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.FirstName).HasMaxLength(30);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastName).HasMaxLength(30);

                entity.Property(e => e.MainTeam)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nationality).HasMaxLength(50);

                entity.Property(e => e.Position).HasMaxLength(100);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Player__Category__7720AD13");
            });

            modelBuilder.Entity<PlayerGallery>(entity =>
            {
                entity.ToTable("PlayerGallery");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerGalleries)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PlayerGal__Playe__0EC32C7A");
            });

            modelBuilder.Entity<PlayerHistory>(entity =>
            {
                entity.ToTable("PlayerHistory");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Position).HasMaxLength(100);

                entity.Property(e => e.Results).HasMaxLength(300);

                entity.Property(e => e.Season).HasMaxLength(100);

                entity.Property(e => e.Team).HasMaxLength(200);

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerHistories)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK__PlayerHis__Playe__7AF13DF7");
            });

            modelBuilder.Entity<Sponsor>(entity =>
            {
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<TeamCategory>(entity =>
            {
                entity.ToTable("TeamCategory");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Title).HasMaxLength(200);
            });

            modelBuilder.Entity<TeamsRanking>(entity =>
            {
                entity.ToTable("TeamsRanking");

                entity.Property(e => e.Goals).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<Trophy>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(300);
            });

            modelBuilder.Entity<UserLocation>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_UserLocations_UserId");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.State).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLocations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserLocat__UserI__6B0FDBE9");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName).HasMaxLength(30);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastName).HasMaxLength(30);

                entity.Property(e => e.Role).HasMaxLength(100);
            });

            modelBuilder.HasSequence<int>("SalesOrderNumber", "SalesLT");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

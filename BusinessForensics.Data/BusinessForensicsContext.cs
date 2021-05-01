using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessForensics.Domain;
using Microsoft.EntityFrameworkCore;

namespace BusinessForensics.Data
{
    public class BusinessForensicsContext : DbContext
    {
        public BusinessForensicsContext()
        {
        }

        public BusinessForensicsContext(DbContextOptions<BusinessForensicsContext> options) : base(options)
        {
        }

        public DbSet<Game> Game { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Product Version", "1.0.0.0");
            
            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("Game");
                entity.HasKey(g => g.Id).HasName("PK_Game_Id");
                entity.Property(g => g.Id).ValueGeneratedNever();

                entity.OwnsMany(g => g.GameAttempts, connectorBuilder =>
                {
                    connectorBuilder
                        .WithOwner()
                        .HasForeignKey(g => g.GameId)
                        .HasConstraintName("FK_GameAttempt_Game_GameAttemptId");

                    connectorBuilder.Property(g => g.Id).ValueGeneratedNever();
                    connectorBuilder.HasKey(g => new { g.GameId, g.Id }).HasName("FK_GameAttempt_Game_GameAttemptId_Id");
                });
            });
        }
    }
}

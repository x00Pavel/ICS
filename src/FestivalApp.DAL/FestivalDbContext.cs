using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Seeds;
using Microsoft.EntityFrameworkCore;

namespace FestivalApp.DAL
{
    public class FestivalDbContext : DbContext
    {
        public FestivalDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<StageEntity> Stages { get; set; }
        public DbSet<BandEntity> Bands { get; set; }
        public DbSet<PerformanceEntity> Performances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PerformanceEntity>()
                .HasIndex(perf => new { perf.BandId });

            modelBuilder.Entity<PerformanceEntity>()
                .HasIndex(perf => new { perf.StageId });

            modelBuilder.Entity<PerformanceEntity>()
                .HasOne<BandEntity>(perf => perf.Band)
                .WithMany(i => i.Performances)
                .HasForeignKey(i => i.BandId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PerformanceEntity>()
                .HasOne<StageEntity>(perf => perf.Stage)
                .WithMany(i => i.Performances)
                .HasForeignKey(i => i.StageId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BandEntity>()
                .HasMany<PerformanceEntity>(band => band.Performances)
                .WithOne(perf => perf.Band)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StageEntity>()
                .HasMany<PerformanceEntity>(stage => stage.Performances)
                .WithOne(perf => perf.Stage)
                .OnDelete(DeleteBehavior.Cascade);

            BandSeeds.Seed(modelBuilder);
            StageSeeds.Seed(modelBuilder);
            PerformanceSeeds.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}

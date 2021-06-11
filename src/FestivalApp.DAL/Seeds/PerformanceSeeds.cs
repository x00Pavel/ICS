using System;
using System.Collections.Generic;
using System.Linq;
using FestivalApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace FestivalApp.DAL.Seeds
{
    public class PerformanceSeeds
    {
        public static readonly PerformanceEntity Perf1 = new()
        {
            Id = Guid.Parse("77a6f205-992e-4bc9-e461-08d91df9dde0"),
            BandId = BandSeeds.Band1.Id,
            StageId = StageSeeds.Stage1.Id,
            From = new DateTime(2021, 2, 1, 21, 0, 0),
            To = new DateTime(2021, 2, 1, 22, 0, 0)
        };
        public static readonly PerformanceEntity Perf2 = new()
        {
            Id = Guid.Parse("7e1d51a8-6840-44de-96ab-08d91dfb2c3a"),
            BandId = BandSeeds.Band1.Id,
            StageId = StageSeeds.Stage2.Id,
            From = new DateTime(2021, 1, 1, 10, 0, 0),
            To = new DateTime(2021, 1, 1, 16, 0, 0)
        };
        public static readonly PerformanceEntity Perf3 = new()
        {
            Id = Guid.Parse("3c2c1081-97e9-4b32-0d97-08d91dfb8a09"),
            BandId = BandSeeds.Band2.Id,
            StageId = StageSeeds.Stage1.Id,
            From = new DateTime(2021, 3, 1, 12, 59, 0),
            To = new DateTime(2021, 3, 1, 14, 21, 0)
        };
        public static readonly PerformanceEntity Perf4 = new()
        {
            Id = Guid.Parse("e6ec7542-aad4-466e-a44a-1ba8400bc60e"),
            BandId = BandSeeds.Band3.Id,
            StageId = StageSeeds.Stage2.Id,
            From = new DateTime(2021, 3, 10, 12, 59, 0),
            To = new DateTime(2021, 3, 10, 14, 21, 0)
        };

        public static readonly Dictionary<int, PerformanceEntity> Dict = new()
        { { 1, Perf1 }, { 2, Perf2 }, { 3, Perf3 }, { 4, Perf4 } };
        public static PerformanceEntity Get(int index, FestivalDbContext context)
        {
            var entity = Dict.GetValueOrDefault(index);

            entity.Band = context.Bands.SingleOrDefault(a => a.Id == entity.BandId);
            entity.Stage = context.Stages.SingleOrDefault(a => a.Id == entity.StageId);
            return entity;
        }

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PerformanceEntity>().HasData(Perf1, Perf2, Perf3, Perf4);
        }
    }
}

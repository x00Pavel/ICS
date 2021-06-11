using System;
using System.Collections.Generic;
using System.Linq;
using FestivalApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FestivalApp.DAL.Seeds
{
    public class StageSeeds
    {

        public static readonly StageEntity Stage1 = new()
        {
            Id = Guid.Parse("8a4fa0b6-2b26-42ce-2ce3-08d91d019f7c"),
            Name = "Blue hills",
            Capacity = 100,
            Description = "Stage on blue hills with best view",
            Photo = "https://img1.10bestmedia.com/Images/Photos/366598/GettyImages-1093700094_55_660x440.jpg",
            Performances = new List<PerformanceEntity>()
            // Performances = new List<PerformanceEntity>() { PerformanceSeeds.Perf1, PerformanceSeeds.Perf3 }
        };

        public static readonly StageEntity Stage2 = new()
        {
            Id = Guid.Parse("b6345eaa-ecec-42de-a3b8-e16a76bb171d"),
            Name = "Letnany",
            Capacity = 100,
            Description = "Letnany in Prague",
            Photo = "https://www.google.com/url?sa=i&url=http%3A%2F%2Fwww.pilotinfo.cz%2Fz-letist%2Fceska-republika%2Fjak-se-dela-propaganda-proti-letisti-praha-letnany&psig=AOvVaw18otsYEpIRFlN6krPFBg95&ust=1621456840456000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCLif9_6Y1PACFQAAAAAdAAAAABAK",
            Performances = new List<PerformanceEntity>()
            //Performances = new List<PerformanceEntity>() { PerformanceSeeds.Perf2, PerformanceSeeds.Perf4 }
        };

        public static readonly StageEntity Stage3 = new()
        {
            Id = Guid.Parse("99b333d5-20b3-418f-0a5e-08d91dfb2c3b"),
            Name = "Stage3",
            Capacity = 100,
            Description = "Stage3",
            Photo = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fvysilackymilin.cz%2Fdetail-produktu%2Fautoradia%2Freproduktory%2Fjbl%2F15595_reproduktory-jbl-stage3-427%2F&psig=AOvVaw3TH_XTLko-9dPqaErc05z3&ust=1621613902359000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCLC6pY3i2PACFQAAAAAdAAAAABAD",
            Performances = new List<PerformanceEntity>()
            //Performances = new List<PerformanceEntity>() { PerformanceSeeds.Perf2, PerformanceSeeds.Perf4 }
        };


        public static readonly Dictionary<int, StageEntity> Dict = new() { { 1, Stage1 }, { 2, Stage2 }, { 3, Stage3 } };
        public static StageEntity Get(int index, FestivalDbContext context)
        {
            var entity = Dict.GetValueOrDefault(index);

            entity.Performances = context.Performances.Where(a => a.StageId == entity.Id)
                                         .Include(a => a.Band)
                                         .Include(a => a.Stage)
                                         .ToList();

            return entity;
        }

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StageEntity>().HasData(Stage1, Stage2, Stage3);
        }
    }
}

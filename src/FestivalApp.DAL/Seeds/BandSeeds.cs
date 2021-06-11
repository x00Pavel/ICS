using System;
using System.Collections.Generic;
using System.Linq;
using FestivalApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FestivalApp.DAL.Seeds
{
    public class BandSeeds
    {
        public static readonly BandEntity Band1 = new()
        {
            Id = Guid.Parse("021ba853-d557-49e8-166b-08d91df9dde1"),
            Name = "Nirvana",
            Genre = "Rock",
            Photo = "https://happymag.tv/wp-content/uploads/2018/01/earlynirvanahappy.jpg",
            CountryOfOrigin = "USA",
            LongDescription = "Nirvana was an American rock band formed in Aberdeen, Washington in 1987. " +
                              "Founded by lead singer and guitarist Kurt Cobain and bassist Krist Novoselic, " +
                              "the band went through a succession of drummers before recruiting Dave Grohl in " +
                              "1990. Nirvana's success popularized alternative rock, and they were often referenced as" +
                              " the figurehead band of Generation X. Their music maintains a popular following and continues to " +
                              "influence modern rock and roll culture.",
            ShortDescription = "Culture Group",
            Performances = new List<PerformanceEntity>()
            //Performances = new List<PerformanceEntity>() { PerformanceSeeds.Perf1, PerformanceSeeds.Perf2 }
        };

        public static readonly BandEntity Band2 = new()
        {
            Id = Guid.Parse("7073c336-588a-4193-ccb1-08d91dfb2c3a"),
            Name = "Jay-Z",
            Genre = "Pop",
            Photo = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fclassicalbumsundays.com%2Fnirvanas-nevermind-a-memory-replay-presented-colleen-cosmo-murphy%2F&psig=AOvVaw3Y8MjLEMygFfHWH1hV0Rhv&ust=1621455736225000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCLCcv--U1PACFQAAAAAdAAAAABAD",
            CountryOfOrigin = "USA",
            LongDescription = "Shawn Corey Carter (born December 4, 1969), known professionally as Jay-Z " +
                              "(stylized as JAY-Z),[a] is an American rapper, songwriter, record executive, " +
                              "businessman, and record producer. He is widely regarded as one of the most " +
                              "influential hip-hop artists in history,[5] and often cited as one of the greatest " +
                              "rappers of all time.[6][7]",
            ShortDescription = "Best-selled rapper",
            Performances = new List<PerformanceEntity>()
            // Performances = new List<PerformanceEntity>() { PerformanceSeeds.Perf3 }
        };

        public static readonly BandEntity Band3 = new()
        {
            Id = Guid.Parse("09ab0c58-21fd-4245-316f-08d91dfb8a09"),
            Name = "Billie Eilish",
            Genre = "Pop",
            Photo = "https://cdn.myshoptet.com/usr/www.esuvenyry.cz/user/documents/upload/nirvana.jpg",
            CountryOfOrigin = "USA",
            LongDescription = "Billie Eilish Pirate Baird O'Connell (/ˈaɪlɪʃ/ EYE-lish;[1] born December 18, 2001) " +
                              "is an American singer and songwriter. She first gained attention in 2015 when she uploaded" +
                              " the song 'Ocean Eyes' to SoundCloud, which was subsequently released by the Interscope " +
                              "Records subsidiary Darkroom. The song was written and produced by her brother Finneas O'Connell, " +
                              "with whom she collaborates on music and live shows. Her debut EP, Don't Smile at Me (2017), " +
                              "became a sleeper hit, reaching the top 15 in the US, UK, Canada, and Australia.",
            ShortDescription = "Young nice singer",
            Performances = new List<PerformanceEntity>()
            // Performances = new List<PerformanceEntity>() { PerformanceSeeds.Perf4 }
        };

        public static readonly BandEntity Band4 = new()
        {
            Id = Guid.Parse("0d446d91-2474-492b-0a6a-08d91dfd4a6d"),
            Name = "Lil Grandma",
            Genre = "Raggy",
            Photo = "https://frontiersinblog.files.wordpress.com/2020/04/frontiers-psychology-free-from-dance-alternative-interaction-grandchildren-grandparents.jpg?w=1024",
            CountryOfOrigin = "USA",
            LongDescription = "Something very serious.",
            ShortDescription = "Rely short description",
            Performances = new List<PerformanceEntity>()
            // Performances = new List<PerformanceEntity>() { PerformanceSeeds.Perf4 }
        };

        public static readonly BandEntity Band5 = new()
        {
            Id = Guid.Parse("807d16e2-a65e-446d-ba7d-2adde9b7db71"),
            Name = "OMFG",
            Genre = "Metal",
            Photo = "https://frontiersinblog.files.wordpress.com/2020/04/frontiers-psychology-free-from-dance-alternative-interaction-grandchildren-grandparents.jpg?w=1024",
            CountryOfOrigin = "CZ",
            LongDescription = "Something very serious.",
            ShortDescription = "Rely short description",
            Performances = new List<PerformanceEntity>()
            // Performances = new List<PerformanceEntity>() { PerformanceSeeds.Perf4 }
        };

        public static readonly Dictionary<int, BandEntity> Dict = new() { { 1, Band1 }, { 2, Band2 }, { 3, Band3 }, { 4, Band4 }, { 5, Band5 } };
        public static BandEntity Get(int index, FestivalDbContext context)
        {
            var entity = Dict.GetValueOrDefault(index);

            entity.Performances = context.Performances.Where(a => a.BandId == entity.Id)
                                         .Include(a => a.Band)
                                         .Include(a => a.Stage)
                                         .ToList();

            return entity;
        }
        public static void Seed(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BandEntity>().HasData(Dict.Values);
        }
    }
}

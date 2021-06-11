using System;
using System.Collections.Generic;
using System.IO;
using FestivalApp.DAL.Entities;
using Nemesis.Essentials.Design;

namespace FestivalApp.DAL.Tests
{
    public static class TestUtilities
    {
        private static int _index;
        public static string GetResourcesPath()
        {
            return Path.Combine(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Directory.GetCurrentDirectory()))),
                "Resources");
        }

        private static DateTime CreateDefaultTimeFrom()
        {
            return new DateTime(2021, 2, 1, 21, 0, 0);
        }

        private static DateTime CreateDefaultTimeTo()
        {
            return new DateTime(2021, 2, 1, 21, 30, 0);
        }

        public static BandEntity CreateTestBand(int index = 0)
        {
            int curIndex = index == 0 ? ++_index : index;
            return new BandEntity
            {
                Name = "TestName" + curIndex,
                Genre = "TestGenre" + curIndex,
                CountryOfOrigin = "TestCountry" + curIndex,
                LongDescription = "TestLongDesc" + curIndex,
                ShortDescription = "TestShortDesc" + curIndex,
                Photo = Path.Combine(GetResourcesPath(), "test_in.jpg")

            };
        }

        public static StageEntity CreateTestStage(int index = 0)
        {
            int curIndex = index == 0 ? ++_index : index;
            return new StageEntity
            {
                Name = "TestName" + curIndex,
                Description = "TestDesc" + curIndex,
                Capacity = 100,
                Photo = Path.Combine(GetResourcesPath(), "test_in.jpg")

            };
        }


        public static PerformanceEntity CreateTestPerformance(int index = 0)
        {
            int curIndex = index == 0 ? ++_index : index;
            BandEntity band = CreateTestBand(curIndex);
            StageEntity stage = CreateTestStage(curIndex);

            return new PerformanceEntity
            {
                BandId = band.Id,
                Band = band,
                StageId = stage.Id,
                Stage = stage,
                From = CreateDefaultTimeFrom(),
                To = CreateDefaultTimeTo(),
            };
        }

        public static PerformanceEntity CreateTestPerformanceWithArgs(int index = 0, Dictionary<string, object> args = null)
        {
            int curIndex = index == 0 ? ++_index : index;
            if (args == null)
            {
                // Return the basic Performance Entity
                return CreateTestPerformance(curIndex);
            }

            BandEntity band = args.TryGetValue("Band", out _) ? (BandEntity)args["Band"] : CreateTestBand(curIndex);
            StageEntity stage = args.TryGetValue("Stage", out _) ? (StageEntity)args["Stage"] : CreateTestStage(curIndex);
            DateTime from = args.TryGetValue("From", out _) ? (DateTime)args["From"] : CreateDefaultTimeFrom();
            DateTime to = args.TryGetValue("To", out _) ? (DateTime)args["To"] : CreateDefaultTimeTo();

            return new PerformanceEntity()
            {
                BandId = band.Id,
                Band = band,
                StageId = stage.Id,
                Stage = stage,
                From = from,
                To = to
            };
        }

        public static ICollection<PerformanceEntity> CreateTestPerformanceWithSlots(int index = 0, int slots = 1)
        {
            int curIndex = index == 0 ? ++_index : index;
            ICollection<PerformanceEntity> performances = new ValueCollection<PerformanceEntity>();

            for (int i = 0; i < slots; i++)
            {
                PerformanceEntity entity = CreateTestPerformance(curIndex + i);
                performances.Add(entity);
            }

            return performances;
        }
    }
}

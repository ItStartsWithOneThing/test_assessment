
using Microsoft.EntityFrameworkCore;
using test_assessment.DataBase.Entities;

namespace test_assessment.DataBase.Repositories
{
    public class ReadRepository
    {
        public async Task<Location> CheckIfLocationsExistsAsync()
        {
            using (var _dbContext = new TestAssessmentDbContext())
            {
                var locations = _dbContext.Set<Location>();

                return await locations.FirstOrDefaultAsync();
            }
        }

        public async Task<int?> GetPULocationIDWithHighestAverageTipAmountAsync()
        {
            using (var _dbContext = new TestAssessmentDbContext())
            {
                var trips = _dbContext.Set<Trip>();

                var averageTipAmountsByLocation = await trips
                .GroupBy(t => t.PULocationID)
                .Select(g => new
                {
                    PULocationID = g.Key,
                    AverageTipAmount = g.Average(t => t.TipAmount)
                })
                .OrderByDescending(x => x.AverageTipAmount)
                .FirstOrDefaultAsync();

                return averageTipAmountsByLocation?.PULocationID;
            }
        }

        public async Task<List<Trip>> FindTop100MostExpensiveTripsByDistanceAsync()
        {
            using (var _dbContext = new TestAssessmentDbContext())
            {
                var trips = _dbContext.Set<Trip>();

                var top100MostExpensiveTrips = await trips
                    .FromSqlRaw("EXEC GetMostExpensiveTripsByDistance")
                    .ToListAsync();

                return top100MostExpensiveTrips;
            }
        }

        public async Task<List<Trip>> FindTop100MostExpensiveTripsPerMinuteAsync()
        {
            using (var _dbContext = new TestAssessmentDbContext())
            {
                var trips = _dbContext.Set<Trip>();

                var top100MostExpensiveTrips = await trips
                    .FromSqlRaw("EXEC GetMostExpensiveTripsByTime")
                    .ToListAsync();

                return top100MostExpensiveTrips;
            }
        }

        public async Task<int> GetAveragePassangerCountByPULocationIDAsync(int puLocationID)
        {
            using (var _dbContext = new TestAssessmentDbContext())
            {
                var trips = _dbContext.Set<Trip>();

                var average = await trips.Where(x => x.PULocationID == puLocationID).Select(x => x.PassangerCount).AverageAsync();

                int passangersCount = 0;

                if (average < 1.5)
                {
                    passangersCount = (int)Math.Floor(average);
                }
                else
                {
                    passangersCount = (int)Math.Ceiling(average);
                }

                return passangersCount;
            }
        }
    }
}

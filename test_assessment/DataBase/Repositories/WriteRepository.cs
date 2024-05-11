
using Microsoft.Data.SqlClient;
using System.Data;
using test_assessment.DataBase.Entities;

namespace test_assessment.DataBase.Repositories
{
    public class WriteRepository
    {
        public async Task AddLocationsAsync(IEnumerable<Location> records)
        {
            using (var _dbContext = new TestAssessmentDbContext())
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var _dbSet = _dbContext.Set<Location>();

                        _dbSet.AddRange(records);

                        await _dbContext.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Something went wrong wile saving data in DB. Error: {ex.Message}");
                        Console.WriteLine($"Inner exception. Error: {ex.InnerException}");
                    }
                }
            }
        }

        public async Task AddRecordsViaEFCoreAsync(IEnumerable<Trip> records)
        {
            using (var _dbContext = new TestAssessmentDbContext())
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var _dbSet = _dbContext.Set<Trip>();

                        _dbSet.AddRange(records);

                        await _dbContext.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Something went wrong wile saving data in DB. Error: {ex.Message}");
                        Console.WriteLine($"Inner exception. Error: {ex.InnerException}");
                    }
                }
            }
        }

        public void AddRecordsViaBulkCopy(IEnumerable<Trip> tripRecords)
        {
            ConvertESTToUTC(tripRecords);

            using (var connection = new SqlConnection(TestAssessmentDbContext.connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                        {
                            bulkCopy.DestinationTableName = "Trips";
                            var dataTable = ConvertToDataTable(tripRecords);
                            bulkCopy.WriteToServer(dataTable);
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Something went wrong wile saving data in DB. Error: {ex.Message}");
                        Console.WriteLine($"Inner exception. Error: {ex.InnerException}");
                    }
                }
            }
        }

        private DataTable ConvertToDataTable(IEnumerable<Trip> tripRecords)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("TripID", typeof(int));
            dataTable.Columns.Add("PickupDatetime", typeof(DateTime));
            dataTable.Columns.Add("DropoffDatetime", typeof(DateTime));
            dataTable.Columns.Add("PassangerCount", typeof(int));
            dataTable.Columns.Add("TripDistance", typeof(decimal));
            dataTable.Columns.Add("StoreAndFwdFlag", typeof(string));
            dataTable.Columns.Add("PULocationID", typeof(int));
            dataTable.Columns.Add("DOLocationID", typeof(int));
            dataTable.Columns.Add("FareAmount", typeof(decimal));
            dataTable.Columns.Add("TipAmount", typeof(decimal));

            foreach (var record in tripRecords)
            {
                dataTable.Rows.Add(record.TripID, record.PickupDatetime, record.DropoffDatetime, record.PassangerCount, record.TripDistance, record.StoreAndFwdFlag,
                    record.PULocationID, record.DOLocationID, record.FareAmount, record.TipAmount);
            }

            return dataTable;
        }

        private void ConvertESTToUTC(IEnumerable<Trip> records)
        {
            TimeZoneInfo estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            foreach (var trip in records)
            {
                DateTime pickupUtc = TimeZoneInfo.ConvertTimeToUtc(trip.PickupDatetime, estTimeZone);

                DateTime dropoffUtc = TimeZoneInfo.ConvertTimeToUtc(trip.DropoffDatetime, estTimeZone);
            }
        }
    }
}

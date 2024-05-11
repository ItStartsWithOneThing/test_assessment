
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using test_assessment.Models;
using test_assessment.Mapping.CSVMappers;

namespace test_assessment
{
    public static class CSVExtractor
    {
        public static List<TaxiTripModel> ExtractFromCSV(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Context.RegisterClassMap<TaxiTripModelMapper>();

               return csv.GetRecords<TaxiTripModel>().Where(x => x.VendorID != 0).ToList();
             }
        }

        public static void ExtractDuplications(List<TaxiTripModel> records, string filePath)
        {
            var duplicates = FindDuplicates(records);

            WriteDuplicates(duplicates, filePath);

            records = records.Except(duplicates).ToList(); // Removing all records that was duplicated

            records.AddRange(duplicates); // Adding single instances of records that were duplicated
        }

        private static List<TaxiTripModel> FindDuplicates(List<TaxiTripModel> trips)
        {
            var duplicates = trips
                .GroupBy(t => new { t.tpep_pickup_datetime, t.tpep_dropoff_datetime, t.passenger_count })
                .Where(g => g.Count() > 1)
                .SelectMany(g => g.Skip(1))
                .ToList();

            return duplicates;
        }

        private static void WriteDuplicates(List<TaxiTripModel> duplicates, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteHeader<TaxiTripModel>();
                csv.NextRecord();
                foreach (var record in duplicates)
                {
                    csv.WriteRecord(record);
                    csv.NextRecord();
                }
            }
        }
    }
}


using test_assessment;
using test_assessment.DataBase.Entities;
using test_assessment.DataBase.Repositories;
using test_assessment.Mapping.AutomapperProfiles;

var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
var csvFilePathRead = Path.Combine(desktopPath, "sample-cab-data.csv");
var csvFilePathWrite = Path.Combine(desktopPath, "duplicates.csv");

var records = CSVExtractor.ExtractFromCSV(csvFilePathRead);
CSVExtractor.ExtractDuplications(records, csvFilePathWrite);

var mapper = AutomapperProfile.GetMapper();

var recordsEntities = mapper.Map<IEnumerable<Trip>>(records);

var writeRepository = new WriteRepository();
var readRepository = new ReadRepository();

var existedLocation = await readRepository.CheckIfLocationsExistsAsync(); //Check if Locations exists

if (existedLocation == null)
{
    var doLocations = recordsEntities.Select(x => x.DOLocationID).Distinct().Select(x => new Location() { LocationID = x }).ToList();
    var puLocations = recordsEntities.Select(x => x.PULocationID).Distinct().Select(x => new Location() { LocationID = x }).ToList();

    var allLocations = doLocations.Union(puLocations);

    await writeRepository.AddLocationsAsync(allLocations);
}

await writeRepository.AddRecordsViaEFCoreAsync(recordsEntities); // 29951

var top1 = await readRepository.FindTop100MostExpensiveTripsByDistanceAsync();
foreach (var item in top1)
{
    Console.WriteLine(item.TripID);
}

Console.WriteLine("\nPause\n");
Thread.Sleep(4000);


var top2 = await readRepository.FindTop100MostExpensiveTripsPerMinuteAsync();
foreach (var item in top2)
{
    Console.WriteLine(item.TripID);
}

Console.ReadLine();
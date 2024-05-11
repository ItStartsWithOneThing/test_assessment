
using test_assessment;

var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
var csvFilePathRead = Path.Combine(desktopPath, "sample-cab-data.csv");
var csvFilePathWrite = Path.Combine(desktopPath, "duplicates.csv");

var records = CSVExtractor.ExtractFromCSV(csvFilePathRead);
CSVExtractor.ExtractDuplications(records, csvFilePathWrite);

Console.ReadLine();
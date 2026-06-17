<<<<<<< HEAD
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
=======
﻿using Newtonsoft.Json;
using System.Text;

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");
var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");

Directory.CreateDirectory(salesTotalDir);

var salesFiles = FindFiles(storesDirectory);

var salesTotal = CalculateSalesTotal(salesFiles);

File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

// Call the new summary function
GenerateSalesSummary(salesFiles, salesTotalDir);

IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();
    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);
    foreach (var file in foundFiles)
    {
        if (Path.GetExtension(file) == ".json")
            salesFiles.Add(file);
    }
    return salesFiles;
}

double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;
    foreach (var file in salesFiles)
    {
        string salesJson = File.ReadAllText(file);
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);
        salesTotal += data?.Total ?? 0;
    }
    return salesTotal;
}

void GenerateSalesSummary(IEnumerable<string> salesFiles, string outputDirectory)
{
    double grandTotal = 0;
    var details = new StringBuilder();

    foreach (var file in salesFiles)
    {
        string fileName = Path.GetFileName(file);
        string salesJson = File.ReadAllText(file);
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);
        double fileTotal = data?.Total ?? 0;

        grandTotal += fileTotal;
        details.AppendLine($"  {fileName}: {fileTotal.ToString("C")}");
    }

    var report = new StringBuilder();
    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine($" Total Sales: {grandTotal.ToString("C")}");
    report.AppendLine();
    report.AppendLine(" Details:");
    report.Append(details);

    string reportPath = Path.Combine(outputDirectory, "sales-summary.txt");
    File.WriteAllText(reportPath, report.ToString());

    Console.WriteLine($"Sales summary written to: {reportPath}");
    Console.WriteLine(report.ToString());
}

record SalesData(double Total);
>>>>>>> ced1b92ee7df7550a9141fc9789fbe13bc8783e5

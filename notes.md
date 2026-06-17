# W01 Assignment Notes – CSE 325

## Part 1 – Web API: Additional Pizza Record

Added the following record to PizzaService.cs:
new Pizza { Id = 3, Name = "BBQ Chicken", IsGlutenFree = false }

### API Test Results

| Operation | Request | Status Code |
|-----------|---------|-------------|
| GET all   | GET /pizza | 200 OK |
| GET by ID | GET /pizza/1 | 200 OK |
| POST      | POST /pizza | 201 Created |
| PUT       | PUT /pizza/1 | 204 No Content |
| DELETE    | DELETE /pizza/3 | 204 No Content |

## Part 2 – Sales Summary Function

static void GenerateSalesSummary(IEnumerable<string> salesFiles, string outputDirectory)
{
    double grandTotal = 0;
    var details = new System.Text.StringBuilder();

    foreach (var file in salesFiles)
    {
        string fileName = Path.GetFileName(file);
        string salesJson = File.ReadAllText(file);
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);
        double fileTotal = data?.Total ?? 0;
        grandTotal += fileTotal;
        details.AppendLine($"  {fileName}: {fileTotal.ToString("C")}");
    }

    var report = new System.Text.StringBuilder();
    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine($" Total Sales: {grandTotal.ToString("C")}");
    report.AppendLine();
    report.AppendLine(" Details:");
    report.Append(details);

    string reportPath = Path.Combine(outputDirectory, "sales-summary.txt");
    File.WriteAllText(reportPath, report.ToString());
    Console.WriteLine(report.ToString());
}
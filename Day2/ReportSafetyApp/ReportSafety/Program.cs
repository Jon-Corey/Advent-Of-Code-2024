string filePath = "C:\\Git\\Advent-Of-Code-2024\\Day2\\input.txt";

if(File.Exists(filePath) == false)
{
    Console.WriteLine("Specified file does not exist");
    return;
}

// Do some setup
int countSafe = 0;
int countSafeWithDampener = 0;

// Read the file
var input = File.ReadAllText(filePath).Split("\n");

// Check if each report is safe
foreach(var line in input)
{
    List<int> report = line.Split(' ').Select(int.Parse).ToList();

    if(IsReportValid(report))
    {
        countSafe++;
        countSafeWithDampener++;
    }
    else
    {
        for(int i = 0; i < report.Count; i++)
        {
            List<int> truncatedReport = new List<int>(report);
            truncatedReport.RemoveAt(i);

            if(IsReportValid(truncatedReport))
            {
                countSafeWithDampener++;

                break;
            }
        }
    }
}

// Output the info
Console.WriteLine($"The number of safe reports is {countSafe}");
Console.WriteLine($"The number of safe reports accounting for the dampener is {countSafeWithDampener}");

// Helper methods
int GetNormalizedDiff(int x, int y)
{
    int diff = x - y;

    if(diff == 0)
    {
        return 0;
    }
    else if(diff > 0)
    {
        return 1;
    }
    else
    {
        return -1;
    }
}

bool IsDiffSafe(int x, int y)
{
    int diff = Math.Abs(x - y);

    if(diff < 1)
    {
        return false;
    }
    else if(diff > 3)
    {
        return false;
    }
    else
    {
        return true;
    }
}

bool IsReportValid(List<int> report)
{
    // If there's only one item it's an automatic pass
    if(report.Count == 1)
    {
        return true;
    }

    int normalizedDiff = GetNormalizedDiff(report[0], report[1]);

    // If the first two values are the same then it's a fail
    if(normalizedDiff == 0)
    {
        return false;
    }

    bool isValid = true;

    for(int i = 1; i < report.Count; i++)
    {
        // Check if the direction (increasing/decreasing) switches
        if(GetNormalizedDiff(report[i - 1], report[i]) != normalizedDiff)
        {
            isValid = false;
            break;
        }

        // Check if the diff is to small or too large
        if(IsDiffSafe(report[i - 1], report[i]) == false)
        {
            isValid = false;
            break;
        }
    }

    if(isValid)
    {
        return true;
    }
    return false;
}

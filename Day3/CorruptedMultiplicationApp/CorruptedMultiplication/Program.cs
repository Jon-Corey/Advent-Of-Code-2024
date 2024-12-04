using System.Text.RegularExpressions;

string filePath = "C:\\Git\\Advent-Of-Code-2024\\Day3\\input.txt";

if(File.Exists(filePath) == false)
{
    Console.WriteLine("Specified file does not exist");
    return;
}

// Do some setup
int totalProduct = 0;
int totalEnabledProduct = 0;
bool enabled = true;

var regex = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)|(don't\(\))|(do\(\))", RegexOptions.Compiled);

// Read the file
var input = File.ReadAllText(filePath);

// For each match add the product to the totalProduct
foreach(Match match in regex.Matches(input))
{
    string value = match.Value;

    if(value == "don't()")
    {
        enabled = false;
    }
    else if(value == "do()")
    {
        enabled = true;
    }
    else
    {
        int x = int.Parse(string.Join("", match.Groups[1].Captures));
        int y = int.Parse(string.Join("", match.Groups[2].Captures));

        totalProduct += x * y;

        if(enabled == true)
        {
            totalEnabledProduct += x * y;
        }
    }
}

// Output the info
Console.WriteLine($"The total product is {totalProduct}");
Console.WriteLine($"The total enabled product is {totalEnabledProduct}");

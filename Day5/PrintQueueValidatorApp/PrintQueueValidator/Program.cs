string filePath = "C:\\Git\\Advent-Of-Code-2024\\Day5\\input.txt";

if(File.Exists(filePath) == false)
{
    Console.WriteLine("Specified file does not exist");
    return;
}

// Do some setup
int totalCorrect = 0;
int totalIncorrect = 0;

// Read the file
var input = File.ReadAllText(filePath).Split("\n").Select(line => line.Trim()).ToArray();

var splitIndex = Array.IndexOf(input, "");
var ruleStrings = input.Take(splitIndex).ToArray();
(int X, int Y)[] rules = new (int X, int Y)[ruleStrings.Length];
var updates = input.Skip(splitIndex + 1).ToArray();

for(int i = 0; i < rules.Length; i++)
{
    int x = int.Parse(ruleStrings[i].Split('|').First());
    int y = int.Parse(ruleStrings[i].Split('|').Last());

    rules[i] = (x, y);
}

// Validate all updates
foreach(var update in updates)
{
    var pages = update.Split(',').Select(int.Parse).ToArray();

    if(IsValid(pages))
    {
        totalCorrect += pages[pages.Length / 2];
    }
    else
    {
        var pagesLeft = pages.ToList();
        var orderedPages = new List<int>();

        while(pagesLeft.Count > 0)
        {
            int? addedPage = null;

            foreach(var page in pagesLeft)
            {
                bool canGoFirst = true;

                foreach(var rule in rules)
                {
                    if(rule.Y == page && pagesLeft.Contains(rule.X))
                    {
                        canGoFirst = false;
                        break;
                    }
                }

                if(canGoFirst == true)
                {
                    orderedPages.Add(page);
                    addedPage = page;
                }
            }

            if(addedPage == null)
            {
                Console.WriteLine("Unsortable list found");
                break;
            }
            else
            {
                pagesLeft.Remove(addedPage ?? -1);
            }
        }

        if(IsValid(orderedPages.ToArray()) == false)
        {
            Console.WriteLine("Error: still not valid");
        }
        else
        {
            totalIncorrect += orderedPages[orderedPages.Count / 2];
        }
    }
}

// Output the info
Console.WriteLine($"The total of all of the valid middle page numbers is {totalCorrect}");
Console.WriteLine($"The total of all of the invalid middle page numbers is {totalIncorrect}");

// Helper Methods
bool IsValid(int[] pages)
{
    bool isValid = true;

    foreach(var rule in rules)
    {
        var firstIndex = Array.IndexOf(pages, rule.X);
        var secondIndex = Array.IndexOf(pages, rule.Y);

        if(firstIndex == -1 || secondIndex == -1)
        {
            continue;
        }

        if(firstIndex >= secondIndex)
        {
            isValid = false;
            break;
        }
    }

    return isValid;
}

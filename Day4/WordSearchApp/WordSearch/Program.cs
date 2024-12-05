string filePath = "C:\\Git\\Advent-Of-Code-2024\\Day4\\input.txt";

if(File.Exists(filePath) == false)
{
    Console.WriteLine("Specified file does not exist");
    return;
}

// Do some setup
int totalXmasMatches = 0;
int totalMasXMatches = 0;

string match = "XMAS";
List<string> xMatches = [
    "MMASS",
    "MSAMS",
    "SMASM",
    "SSAMM"
];

(int X, int Y)[] directions = [
    (0, 1), // North
    (1, 1), // NorthEast
    (1, 0), // East
    (1, -1), // SouthEast
    (0, -1), // South
    (-1, -1), // SouthWest
    (-1, 0), // West
    (-1, 1) // NorthWest
];

// Read the file
var input = File.ReadAllText(filePath).ToUpper();
var lines = input.Split('\n').Select(line => line.Trim()).ToArray();

char[,] wordSearch = new char[lines[0].Length, lines.Length];

for(int y = 0; y < lines.Length; y++)
{
    for(int x = 0; x < lines[y].Length; x++)
    {
        wordSearch[x, y] = lines[y][x];
    }
}

// Find Matches
for(int y = 0; y < wordSearch.GetLength(1); y++)
{
    for(int x = 0; x < wordSearch.GetLength(0); x++)
    {
        // Start searching from each occurrence of the first character
        if(wordSearch[x, y] == match[0])
        {
            SearchFrom(x, y);
        }

        if(wordSearch[x, y] == 'A')
        {
            SearchAroundPoint(x, y);
        }
    }
}

Console.WriteLine($"The total number of matches for {match} in the word search is {totalXmasMatches}");
Console.WriteLine($"The total number of matches for X-MAS in the word search is {totalMasXMatches}");

// Helper Methods
void SearchFrom(int x, int y)
{
    foreach(var direction in directions)
    {
        SearchAlongDirection(x, y, direction);
    }
}

void SearchAlongDirection(int x, int y, (int X, int Y) direction)
{
    for(int i = 0; i < match.Length; i++)
    {
        int nextX = x + (i * direction.X);
        int nextY = y + (i * direction.Y);

        char? nextChar = GetChar(nextX, nextY);

        if(nextChar != match[i])
        {
            return;
        }

        // If the last character is a match, then increment totalMatches
        if((i + 1) == match.Length)
        {
            totalXmasMatches++;
        }
    }
}

void SearchAroundPoint(int x, int y)
{
    string? masX = GetX(x, y);

    if(masX != null && xMatches.Contains(masX))
    {
        totalMasXMatches++;
    }
}

string? GetX(int x, int y)
{
    /* 1-2
     * -3-
     * 4-5
     * 
     * Returns: 12345 
     */

    string output = "";

    output += GetChar(x - 1, y + 1);
    output += GetChar(x + 1, y + 1);
    output += GetChar(x, y);
    output += GetChar(x - 1, y - 1);
    output += GetChar(x + 1, y - 1);

    if(output.Length == 5)
    {
        return output;
    }
    else
    {
        return null;
    }
}

char? GetChar(int x, int y)
{
    try
    {
        return wordSearch[x, y];
    }
    catch(IndexOutOfRangeException e)
    {
        return null;
    }
}

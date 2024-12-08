string filePath = "C:\\Git\\Advent-Of-Code-2024\\Day8\\input.txt";

if(File.Exists(filePath) == false)
{
    Console.WriteLine("Specified file does not exist");
    return;
}

// Do some setup
int totalUniqueAntinodes = 0;
int totalUniqueResonantAntinodes = 0;
List<KeyValuePair<char, (int X, int Y)>> nodes = [];
HashSet<(int X, int Y)> antinodes = [];
HashSet<(int X, int Y)> resonantAntinodes = [];

// Read the file
var input = File.ReadAllText(filePath).Split('\n').Select(line => line.Trim()).ToArray();

char[,] map = new char[input[0].Length, input.Length];

// Read all node locations
for(int y = 0; y < input.Length; y++)
{
    for(int x = 0; x < input[y].Length; x++)
    {
        map[x, y] = input[y][x];

        if(input[y][x] != '.')
        {
            nodes.Add(new(input[y][x], (x, y)));
        }
    }
}

// Calculate all antinodes
HashSet<char> uniqueKeys = nodes.Select(n => n.Key).ToHashSet();

foreach(var key in uniqueKeys)
{
    List<(int X, int Y)> coords = nodes.Where(n => n.Key == key).Select(n => n.Value).ToList();

    foreach(var coord in coords)
    {
        if(coords.Count > 1)
        {
            resonantAntinodes.Add(coord);
        }

        foreach(var innerCoord in coords)
        {
            if(coord == innerCoord)
            {
                continue;
            }

            int diffX = coord.X - innerCoord.X;
            int diffY = coord.Y - innerCoord.Y;

            int steps = 1;

            while(true)
            {
                (int X, int Y) antinodeCoord = (coord.X + (diffX * steps), coord.Y + (diffY * steps));

                if(antinodeCoord.X < 0 || antinodeCoord.X >= map.GetLength(0))
                {
                    break;
                }
                if(antinodeCoord.Y < 0 || antinodeCoord.Y >= map.GetLength(1))
                {
                    break;
                }

                resonantAntinodes.Add(antinodeCoord);
                if(steps == 1)
                {
                    antinodes.Add(antinodeCoord);
                }

                steps++;
            }
        }
    }
}

// Count the antinodes
totalUniqueAntinodes = antinodes.Count;
totalUniqueResonantAntinodes = resonantAntinodes.Count;

// Output the info
Console.WriteLine($"The total number of unique antinodes is {totalUniqueAntinodes}");
Console.WriteLine($"The total number of unique antinodes accounting for resonance is {totalUniqueResonantAntinodes}");

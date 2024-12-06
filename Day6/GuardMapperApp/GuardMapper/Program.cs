string filePath = "C:\\Git\\Advent-Of-Code-2024\\Day6\\input.txt";

if(File.Exists(filePath) == false)
{
    Console.WriteLine("Specified file does not exist");
    return;
}

// Do some setup
int totalDistinctPoints = 0;
int totalLoops = 0;

// Read the file
var input = File.ReadAllText(filePath).ToUpper();
var lines = input.Split('\n').Select(line => line.Trim()).ToArray();

(int X, int Y) startPosition = (0, 0);

char[,] initialMap = new char[lines[0].Length, lines.Length];

for(int y = 0; y < lines.Length; y++)
{
    for(int x = 0; x < lines[y].Length; x++)
    {
        if(lines[y][x] == '^')
        {
            initialMap[x, y] = 'X';
            totalDistinctPoints++;
            startPosition = (x, y);
        }
        else
        {
            initialMap[x, y] = lines[y][x];
        }
    }
}

// Map the Guard's initial path
CheckPath(incrementDistinctPoints: true);

// Map each path with an additional obstacle
for(int y = 0; y < initialMap.GetLength(1); y++)
{
    for(int x = 0; x < initialMap.GetLength(0); x++)
    {
        bool isLoop = CheckPath((x, y));

        if(isLoop)
        {
            totalLoops++;
        }
    }
}

// Output the info
Console.WriteLine($"The total distinct points the guard travelled is {totalDistinctPoints}");
Console.WriteLine($"The total number of places to put an additional obstacle to cause a loop is {totalLoops}");

// Helper methods
bool Step(ref char[,] map, ref (int X, int Y) currentPosition, ref (int X, int Y) direction, bool incrementDistinctPoints)
{
    (int X, int Y) nextPosition = (currentPosition.X + direction.X, currentPosition.Y + direction.Y);

    // If the guard has left the map
    if(nextPosition.X < 0 || nextPosition.X >= map.GetLength(0)
        || nextPosition.Y < 0 || nextPosition.Y >= map.GetLength(1))
    {
        if(nextPosition.X < 0)
        {
            nextPosition.X = 0;
        }
        else if(nextPosition.X >= map.GetLength(0))
        {
            nextPosition.X = map.GetLength(0);
        }
        if(nextPosition.Y < 0)
        {
            nextPosition.Y = 0;
        }
        else if(nextPosition.Y >= map.GetLength(0))
        {
            nextPosition.Y = map.GetLength(0);
        }

        currentPosition = nextPosition;

        return false;
    }

    // If there is an obstacle
    if(map[nextPosition.X, nextPosition.Y] == '#')
    {
        if(direction == (0, -1))
        {
            direction = (1, 0);
        }
        else if(direction == (1, 0))
        {
            direction = (0, 1);
        }
        else if(direction == (0, 1))
        {
            direction = (-1, 0);
        }
        else if(direction == (-1, 0))
        {
            direction = (0, -1);
        }
        return true;
    }

    // Move forward
    currentPosition = nextPosition;
    if(map[currentPosition.X, currentPosition.Y] != 'X' && incrementDistinctPoints == true)
    {
        totalDistinctPoints++;
    }
    map[currentPosition.X, currentPosition.Y] = 'X';
    return true;
}

bool CheckPath((int X, int Y)? extraObstacleCoords = null, bool incrementDistinctPoints = false)
{
    char[,] map = (char[,])initialMap.Clone();
    (int X, int Y) currentPosition = startPosition;
    (int X, int Y) direction = (0, -1);

    int iterations = 0;

    if(extraObstacleCoords != null)
    {
        if(map[extraObstacleCoords.Value.X, extraObstacleCoords.Value.Y] == '#')
        {
            // Since the initial map doesn't have any loops, if there is already an obstacle there it isn't a loop
            return false;
        }
        else
        {
            map[extraObstacleCoords.Value.X, extraObstacleCoords.Value.Y] = '#';
        }
    }

    while(Step(ref map, ref currentPosition, ref direction, incrementDistinctPoints) && iterations < 10000)
    {
        iterations++;
    }

    if(iterations >= 10000)
    {
        // Loop detected
        return true;
    }
    else
    {
        return false;
    }
}

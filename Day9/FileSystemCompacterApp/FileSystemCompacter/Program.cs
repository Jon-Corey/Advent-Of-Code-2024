using System.Runtime.CompilerServices;

string filePath = "C:\\Git\\Advent-Of-Code-2024\\Day9\\input.txt";

if(File.Exists(filePath) == false)
{
    Console.WriteLine("Specified file does not exist");
    return;
}

// Do some setup
long fragmentedChecksum = 0;
long defraggedChecksum = 0;

// Read the file
string input = File.ReadAllText(filePath).Trim();

// Expand into more workable format
List<int> fragmentedData = [];
List<(int Id, int Length)> defraggedData = [];

for(int i = 0; i < input.Length; i++)
{
    int number = int.Parse(input[i].ToString());

    if(i % 2 == 0)
    {
        // File
        for(int j = 0; j < number; j++)
        {
            fragmentedData.Add(i / 2);
        }
        if(number > 0)
        {
            defraggedData.Add((i / 2, number));
        }
    }
    else
    {
        // Empty Space
        for(int j = 0; j < number; j++)
        {
            fragmentedData.Add(-1);
        }
        if(number > 0)
        {
            defraggedData.Add((-1, number));
        }
    }
}

// Compress the files
while(fragmentedData.Contains(-1))
{
    int firstEmptyIndex = fragmentedData.IndexOf(-1);
    fragmentedData[firstEmptyIndex] = fragmentedData.Last();
    fragmentedData.RemoveAt(fragmentedData.Count - 1);
}

int maxId = defraggedData.Last().Id;
for(int i = maxId; i >= 0; i--)
{
    int length = defraggedData.Where(d => d.Id == i).First().Length;
    int firstAvailableIndex = defraggedData.FindIndex(d => d.Id == -1 && d.Length >= length);

    if(firstAvailableIndex < 0)
    {
        continue;
    }

    int leftovers = defraggedData[firstAvailableIndex].Length - length;

    defraggedData[firstAvailableIndex] = defraggedData.Where(d => d.Id == i).First();

    defraggedData[defraggedData.FindLastIndex(d => d.Id == i)] = (-1, length);

    if(leftovers > 0)
    {
        defraggedData.Insert(firstAvailableIndex + 1, (-1, leftovers));
    }
}

// Expand the defragged data into a more workable format
List<int> expandedDefraggedData = [];

foreach(var item in defraggedData)
{
    for(int i = 0; i < item.Length; i++)
    {
        expandedDefraggedData.Add(item.Id);
    }
}

// Calculate the checksum
for(int i = 0; i < fragmentedData.Count; i++)
{
    fragmentedChecksum += i * fragmentedData[i];
}

for(int i = 0; i < expandedDefraggedData.Count; i++)
{
    if(expandedDefraggedData[i] != -1)
    {
        defraggedChecksum += i * expandedDefraggedData[i];
    }
}

// Output the info
Console.WriteLine($"The fragented checksum is {fragmentedChecksum}");
Console.WriteLine($"The defragged checksum is {defraggedChecksum}");

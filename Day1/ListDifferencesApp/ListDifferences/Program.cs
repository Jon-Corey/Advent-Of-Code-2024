// Get input file path
Console.Write("Input File Path: ");
string filePath = Console.ReadLine();

if(File.Exists(filePath) == false)
{
    Console.WriteLine("Specified file does not exist");
    return;
}

// Do some setup
List<int> listOne = [];
List<int> listTwo = [];
Dictionary<int, int> frequency = [];
int distance = 0;
int similarity = 0;

// Read the file
var input = File.ReadAllText(filePath).Split("\n");

// Add the contents to two lists and build the frequency dictionary
foreach(var line in input)
{
    int firstValue = int.Parse(line.Split(' ').First().Trim());
    int secondValue = int.Parse(line.Split(' ').Last().Trim());

    listOne.Add(firstValue);
    listTwo.Add(secondValue);

    if(frequency.ContainsKey(secondValue))
    {
        frequency[secondValue]++;
    }
    else
    {
        frequency[secondValue] = 1;
    }
}

// Sort the lists
listOne.Sort();
listTwo.Sort();

// Calculate the distance
for(int i = 0; i < listOne.Count; i++)
{
    distance += Math.Abs(listOne[i] - listTwo[i]);
}

// Calculate the similarity
foreach(int i in listOne)
{
    if(frequency.ContainsKey(i))
    {
        similarity += i * frequency[i];
    }
}

// Output the info
Console.WriteLine($"The total distance between the two lists is {distance}");
Console.WriteLine($"The similarity score of the two lists is {similarity}");

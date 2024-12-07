string filePath = "C:\\Git\\Advent-Of-Code-2024\\Day7\\input.txt";

if(File.Exists(filePath) == false)
{
    Console.WriteLine("Specified file does not exist");
    return;
}

// Do some setup
long totalCalibrationValue = 0;
long totalCalibrationValueWithConcatenation = 0;

// Read the file
var input = File.ReadAllText(filePath).Split('\n');

// Process the data
foreach(var line in input)
{
    long answer = long.Parse(line.Split(':').First().Trim());
    int[] items = line.Split(':').Last().Trim().Split(' ').Select(int.Parse).ToArray();
    Operation[] operations = new Operation[items.Length - 1];

    do
    {
        long result = items.First();

        for(int i = 1; i < items.Length; i++)
        {
            if(operations[i - 1] == Operation.Addition)
            {
                result += items[i];
            }
            else
            {
                result *= items[i];
            }
        }

        if(result == answer)
        {
            totalCalibrationValue += result;
            break;
        }
    }
    while(IncrementOperations(ref operations, false));

    do
    {
        long result = items.First();

        for(int i = 1; i < items.Length; i++)
        {
            if(operations[i - 1] == Operation.Addition)
            {
                result += items[i];
            }
            else if(operations[i - 1] == Operation.Multiplication)
            {
                result *= items[i];
            }
            else if(operations[i - 1] == Operation.Concatentation)
            {
                result = long.Parse($"{result}{items[i]}");
            }
        }

        if(result == answer)
        {
            totalCalibrationValueWithConcatenation += result;
            break;
        }
    }
    while(IncrementOperations(ref operations));
}

// Output the info
Console.WriteLine($"The total calibration value is {totalCalibrationValue}");
Console.WriteLine($"The total calibration value including concatenation is {totalCalibrationValueWithConcatenation}");

// Helper Methods
static bool IncrementOperations(ref Operation[] operations, bool includeConcatenation = true)
{
    int index = operations.Length - 1;

    while(index >= 0)
    {
        if(operations[index] == Operation.Addition)
        {
            operations[index] = Operation.Multiplication;
            break;
        }
        else if(operations[index] == Operation.Multiplication)
        {
            if(includeConcatenation)
            {
                operations[index] = Operation.Concatentation;
                break;
            }
            else
            {
                operations[index] = Operation.Addition;
                index--;
            }
        }
        else
        {
            operations[index] = Operation.Addition;
            index--;
        }
    }

    if(index == -1)
    {
        return false;
    }
    else
    {
        return true;
    }
}

// Enums
enum Operation
{
    Addition = 0,
    Multiplication = 1,
    Concatentation = 2
}

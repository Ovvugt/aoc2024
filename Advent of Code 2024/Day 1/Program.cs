/*
 * Task part 1:
 *   Reorder pairs such that left and right item of pair are the smallest number in order.
 *   Calculate distance between numbers in pairs
 *   Add up all distances, total distance is the solution of part one
 */

/*
 * Task part 2:
 *   For every left number, count how many times this number is in the right number list
 *   Multiply this number by that count
 *   Sum the result of this multiplication is the solution of part two
 */

/*
 * File content format:
 * List of pairs
 * Pair consists of 2 5-digit integers
 * X amount of pairs
 */
var input = File.ReadAllText("input.txt");

List<int> leftNumbers = [];
List<int> rightNumbers = [];

foreach (var unorderedPair in input.Split("\r\n"))
{ 
    var splitPair = unorderedPair.Split("   ");
    leftNumbers.Add(int.Parse(splitPair[0]));
    rightNumbers.Add(int.Parse(splitPair[1]));
}


Console.WriteLine($"Solution part 1: {SolutionPart1()}");
Console.WriteLine($"Solution part 2: {SolutionPart2()}");

//Suspend application so output can be shown

return;

int SolutionPart1()
{
    var sortedLeftNumbers = leftNumbers.Order().ToList();
    var sortedRightNumbers = rightNumbers.Order().ToList();

    var totalDistance1 = 0;
    for (var i = 0; i < leftNumbers.Count; i++)
    {
        var leftNumber = sortedLeftNumbers[i];
        var rightNumber = sortedRightNumbers[i];
        var distance = rightNumber > leftNumber ? rightNumber - leftNumber : leftNumber - rightNumber;
        totalDistance1 += distance;
    }

    return totalDistance1;
}

int SolutionPart2()
{
    return leftNumbers.Sum(leftNumber => rightNumbers.Count(x => x == leftNumber) * leftNumber);
}
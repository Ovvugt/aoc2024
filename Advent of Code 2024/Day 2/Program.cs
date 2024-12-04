var input = File.ReadAllText("input.txt");

/*
 * Task part 1:
 * Check if all numbers in row are all increasing or all decreasing
 * Numbers can only be increments or decrements between 1 and 3
 */
/*
 * Task part 2:
 * Same rules as previous task, however report would still be deemed safe if a single number
 * is the cause of the unsafe report
 */
var reports = input.Split("\n").Select(x => x.Split(' ').Select(int.Parse).ToArray()).ToList();

Console.WriteLine($"Solution part 1: {SolutionPart1()}");
Console.WriteLine($"Solution part 2: {SolutionPart2()}");
return;

int SolutionPart2()
{
    return reports.Count(report => report.Select((t, i) => report.Where((_, index) => i != index).ToArray()).Any(IsSafeReport));
}

int SolutionPart1()
{
    return reports.Where(IsSafeReport).Count();
}

bool IsSafeReport(int[] report)
{
    var safeReport = true;
    for (var i = 0; i < report.Length - 1; i++)
    {
        var currentNumber = report[i];
        var nextNumber = report[i + 1];

        var difference = Math.Abs(currentNumber - nextNumber);
            
        if (difference is <= 3 and > 0) 
            continue;
            
        safeReport = false;
        break;
    }

    if (safeReport)
    {
        safeReport = report.SequenceEqual(report.Order()) || report.SequenceEqual(report.OrderDescending());
    }

    return safeReport;
}
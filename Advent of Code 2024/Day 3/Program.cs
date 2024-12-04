using System.Text.RegularExpressions;

var input = File.ReadAllText("input.txt");

Console.WriteLine($"Solution part 1: {SolutionPart1()}");
Console.WriteLine($"Solution part 2: {SolutionPart2()}");

return;

int SolutionPart2()
{
    const string regexPattern = @"(?<instruction>don't\(\)|do\(\)|mul\(\d{1,3},\d{1,3}\))";

    var matches = Regex.Matches(input, regexPattern, RegexOptions.Multiline);

    var canExecute = true;
    var sumOfMultiples = 0;

    foreach (Match match in matches)
    {
        var instruction = match.Groups["instruction"].Value;

        if (instruction.StartsWith("don't"))
        {
            canExecute = false;
        }
        else if (instruction.StartsWith("do"))
        {
            canExecute = true;
        }
        else if (instruction.StartsWith("mul"))
        {
            if (!canExecute) continue;
            var mulMatch = Regex.Match(instruction, @"mul\((\d{1,3}),(\d{1,3})\)");
            sumOfMultiples += int.Parse(mulMatch.Groups[1].Value) * int.Parse(mulMatch.Groups[2].Value);
        }
    }

    return sumOfMultiples;
}

int SolutionPart1()
{
    const string regexPattern = @"mul\((\d{1,3}),(\d{1,3})\)";
    var sumOfMultiples = 0;
    foreach (Match match in Regex.Matches(input, regexPattern, RegexOptions.Multiline))
    {
        sumOfMultiples += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
    }

    return sumOfMultiples;
}
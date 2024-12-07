var input = File.ReadAllText("input.txt");

Console.WriteLine($"Solution part 1: {SolutionPart1()}");
Console.WriteLine($"Solution part 2: {SolutionPart2()}");

return;

long SolutionPart2()
{
    long sum = 0;
    var lines = input.Split(Environment.NewLine);

    foreach (var line in lines)
    {
        var lineParts = line.Split(":");
        var answer = long.Parse(lineParts[0]);
        var numbersOfEquation = lineParts[1].TrimStart().Split(" ").Select(int.Parse).ToArray();
        var operatorLists = GeneratePermutations(numbersOfEquation.Length - 1, ['+', '*', '|']);
        if (operatorLists.Any(
                operatorList => ParseAndCalculateEquationResult(numbersOfEquation, operatorList) == answer))
        {
            sum += answer;
        }
    }

    return sum;
}

long SolutionPart1()
{
    long sum = 0;
    var lines = input.Split(Environment.NewLine);

    foreach (var line in lines)
    {
        var lineParts = line.Split(":");
        var answer = long.Parse(lineParts[0]);
        var numbersOfEquation = lineParts[1].TrimStart().Split(" ").Select(int.Parse).ToArray();
        var operatorLists = GeneratePermutations(numbersOfEquation.Length - 1, ['+', '*']);
        if (operatorLists.Any(
                operatorList => ParseAndCalculateEquationResult(numbersOfEquation, operatorList) == answer))
        {
            sum += answer;
        }
    }

    return sum;
}

long ParseAndCalculateEquationResult(int[] numbers, string operators)
{
    if (numbers.Length == 0)
    {
        return 0;
    }
    long answer = numbers[0];

    for (var i = 0; i < operators.Length; i++)
    {
        var @operator = operators[i];
        switch (@operator)
        {
            case '+':
                answer += numbers[i + 1];
                continue;
            case '|':
                answer = long.Parse(answer.ToString() + numbers[i + 1]);
                continue;
            default:
                answer *= numbers[i + 1];
                break;
        }
    }
    
    return answer;
}

static List<string> GeneratePermutations(int length, char[] characters)
{
    var results = new List<string>();
    var queue = new Queue<string>();

    foreach (var ch in characters)
    {
        queue.Enqueue(ch.ToString());
    }

    while (queue.Count > 0)
    {
        var current = queue.Dequeue();

        if (current.Length == length)
        {
            results.Add(current);
        }
        else
        {
            foreach (var ch in characters)
            {
                queue.Enqueue(current + ch);
            }
        }
    }

    return results;
}
var input = File.ReadAllText("input.txt");

var map = ParseMap(input);

Console.WriteLine($"Solution part 1: {SolutionPart1()}");
Console.WriteLine($"Solution part 1: {SolutionPart2()}");

return;

int SolutionPart1()
{
    List<List<(int item, int x, int y)>> allTrails = [];

    for (var y = 0; y < map.Length; y++)
    {
        for (var x = 0; x < map[0].Length; x++)
        {
            var item = map[y][x];
            if (item != 0) continue;
            allTrails.AddRange( AddAdjacentIncrementingItems([(item, x, y)], map, [], true));
        }
    }

    return allTrails.Count;
}

int SolutionPart2()
{
    List<List<(int item, int x, int y)>> allTrails = [];

    for (var y = 0; y < map.Length; y++)
    {
        for (var x = 0; x < map[0].Length; x++)
        {
            var item = map[y][x];
            if (item != 0) continue;
            allTrails.AddRange( AddAdjacentIncrementingItems([(item, x, y)], map, [], false));
        }
    }

    return allTrails.Count;
}

List<List<(int item, int x, int y)>> AddAdjacentIncrementingItems(List<(int item, int x, int y)> trail, int[][] map, HashSet<(int x, int y)> visitedNines, bool checkVisitedNines)
{
    var (currentValue, x, y) = trail.Last();
    if (currentValue == 9)
    {
        if (checkVisitedNines && !visitedNines.Add((x, y)))
            return [];
        return [trail];
    }

    var allTrails = new List<List<(int item, int x, int y)>>();
    var directions = new (int dx, int dy)[]
    {
        (0, -1),
        (0, 1),
        (-1, 0),
        (1, 0)
    };

    foreach (var (dx, dy) in directions)
    {
        var newX = x + dx;
        var newY = y + dy;
        if (newX < 0 || newX >= map[0].Length || newY < 0 || newY >= map.Length) continue;

        var adjacentValue = map[newY][newX];
        
        if (adjacentValue != currentValue + 1) continue;

        var newTrail = new List<(int item, int x, int y)>(trail)
        {
            (adjacentValue, newX, newY)
        };
        
        allTrails.AddRange(AddAdjacentIncrementingItems(newTrail, map, visitedNines, checkVisitedNines));
    }

    return allTrails;
}

int[][] ParseMap(string s)
{
    return s.Split(Environment.NewLine).Select(row => row.Select(item => int.Parse(item.ToString())).ToArray()).ToArray();
}
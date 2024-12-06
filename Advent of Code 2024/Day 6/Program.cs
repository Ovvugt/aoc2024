var input = File.ReadAllText("input.txt");

char[][] map = [];
int guardX = -1;
int guardY = -1;
Direction guardDirection = Direction.Up;
bool finished;
bool previouslyVisitedObstruction = false;
var loopingObstructions = 0;
List<(int x, int y, Direction direction)> previouslyVisitedPlaces;

Console.WriteLine($"Solution part 1: {SolutionPart1()}");
Console.WriteLine($"Solution part 2: {SolutionPart2()}");

return;

int SolutionPart2()
{
    map = ParseMap(input);
    InitializeGuardPositionAndDirection();
    var (startX, startY) = (guardX, guardY);
    for (var y = 0; y < map.Length; y++)
    {
        for (var x = 0; x < map[y].Length; x++)
        {
            var mapWithObstruction = map.Select(row => row.ToArray()).ToArray();
            if (startX == x && startY == y) continue;
            mapWithObstruction[y][x] = '#';
            finished = false;
            previouslyVisitedObstruction = false;
            guardX = startX;
            guardY = startY;
            guardDirection = Direction.Up;
            previouslyVisitedPlaces = [];
            while (!finished)
            {
                UpdateGuardPositionPart2(mapWithObstruction, previouslyVisitedPlaces);
            }

            //Console.WriteLine(string.Join("\n", mapWithObstruction.Select(row => string.Join("", row))));
        }
        Console.WriteLine($"Finished obstacle at y:{y} {loopingObstructions}");
    }

    return loopingObstructions;
}

int SolutionPart1()
{ 
    map = ParseMap(input);
    InitializeGuardPositionAndDirection();
    finished = false;

    while (!finished)
    {
        UpdateGuardPositionPart1();
    }

    return CountBreadcrumbs();
}
int CountBreadcrumbs()
{
    var breadcrumbs = 0;
    for (var y = 0; y < map.Length; y++)
    {
        for (var x = 0; x < map[y].Length; x++)
        {
            if (map[y][x] != 'X') continue;
            breadcrumbs++;
        }
    }

    return breadcrumbs;
}

void UpdateGuardPositionPart2(char[][] map, List<(int x, int y, Direction direction)> previouslyVisitedPlaces)
{
    var nextPosition = GuardLookingAt();
    if (nextPosition.X == map[guardY].Length || nextPosition.X == -1 || nextPosition.Y == map.Length || nextPosition.Y == -1)
    {
        map[guardY][guardX] = 'X';
        finished = true;
        return;
    }

    if (previouslyVisitedPlaces.Any(place =>
            place.x == nextPosition.X && place.y == nextPosition.Y && guardDirection == place.direction))
    {
        finished = true;
        loopingObstructions++;
        return;
    }
    var nextBlock = map[nextPosition.Y][nextPosition.X];
    if (nextBlock is '.' or 'X')
    {
        previouslyVisitedPlaces.Add(new (nextPosition.X, nextPosition.Y, guardDirection));
        map[guardY][guardX] = 'X';
        guardX = nextPosition.X;
        guardY = nextPosition.Y;
        map[guardY][guardX] = '^';
        return;
    }

    guardDirection = (Direction) (((int) guardDirection + 1) % 4);
}
void UpdateGuardPositionPart1()
{
    var nextPosition = GuardLookingAt();
    if (nextPosition.X == map[guardY].Length || nextPosition.X == -1 || nextPosition.Y == map.Length || nextPosition.Y == -1)
    {
        map[guardY][guardX] = 'X';
        finished = true;
        return;
    }
    
    var nextBlock = map[nextPosition.Y][nextPosition.X];
    if (nextBlock is '.' or 'X')
    {
        map[guardY][guardX] = 'X';
        guardX = nextPosition.X;
        guardY = nextPosition.Y;
        map[guardY][guardX] = '^';
        return;
    }

    guardDirection = (Direction) (((int) guardDirection + 1) % 4);
}

Position GuardLookingAt()
{
    return guardDirection switch
    {
        Direction.Left => new(guardX - 1, guardY),
        Direction.Right => new(guardX + 1, guardY),
        Direction.Up => new(guardX, guardY - 1),
        Direction.Down => new (guardX, guardY + 1),
        _ => throw new Exception("Guard Position not yet initialized"),
    };
}

void InitializeGuardPositionAndDirection()
{
    for (var y = 0; y < map.Length; y++)
    {
        for (var x = 0; x < map[y].Length; x++)
        {
            if (map[y][x] != '^') continue;
            guardX = x;
            guardY = y;
            guardDirection = Direction.Up;
            return;
        }
    }
}

char[][] ParseMap(string s)
{
    return s.Split(Environment.NewLine).Select(row => row.ToArray()).ToArray();
}

internal enum Direction
{
    Up,
    Right,
    Down,
    Left
}

internal record Position(int X, int Y);

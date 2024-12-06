var input = File.ReadAllText("input.txt");

char[][] map = input.Split(Environment.NewLine).Select(row => row.ToArray()).ToArray();
int guardX = -1;
int guardY = -1;
Direction guardDirection = Direction.None;

void UpdateGuardPosition()
{
    if (guardX == -1 && guardY == -1)
    {
        InitializeGuardPositionAndDirection();
    }
    var nextPosition = GuardLookingAt();
}

Position GuardLookingAt()
{
    return guardDirection switch
    {
        Direction.Left => new(guardX - 1, guardY),
        Direction.Right => new(guardX + 1, guardY),
        Direction.Up => new(guardX, guardY + 1),
        Direction.Down => new(guardX, guardY - 1),
        _ => throw new Exception("Guard Position not yet initialized"),
    };
}

void InitializeGuardPositionAndDirection()
{
    for (int y = 0; y < map.Length; y++)
    {
        for (int x = 0; x < map[y].Length; x++)
        {
            if (map[y][x] == '^')
            {
                guardX = x;
                guardY = y;
                guardDirection = Direction.Up;
                return;
            }
        }
    }
}

enum Direction
{
    None,
    Up,
    Right,
    Down,
    Left
}

class Position(int x, int y)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
}

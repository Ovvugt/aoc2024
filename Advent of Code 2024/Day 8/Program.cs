var input = File.ReadAllText("input.txt");
var map = ParseMap(input);
var mapHeight = map.Length;
var mapWidth = map[0].Length;

Console.WriteLine($"Solution part 1: {SolutionPart1(mapHeight, mapWidth, map)}");
Console.WriteLine($"Solution part 2: {SolutionPart2(mapHeight, mapWidth, map)}");

return;

char[][] ParseMap(string s)
{
    return s.Split(Environment.NewLine).Select(row => row.ToArray()).ToArray();
}

int SolutionPart2(int mapHeight, int mapWidth, char[][] chars)
{
    List<(int x, int y)> antinodes = [];

    for (var y = 0; y < mapHeight; y++)
    {
        for (var x = 0; x < mapWidth; x++)
        {
            if (chars[y][x] == '.') continue;
            var antenna = chars[y][x];
            FindAntinodesForAntenna(antenna, x, y, antinodes);
        }
    }

    return antinodes.Count;

    void FindAntinodesForAntenna(char antenna, int antennaX, int antennaY, List<(int x, int y)> antinodes)
    {
        for (var y = 0; y < mapHeight; y++)
        {
            for (var x = 0; x < mapWidth; x++)
            {
                var antennaFrequency = chars[y][x];
                if (antennaFrequency == '.' || (x == antennaX && antennaY == y) || antennaFrequency != antenna) continue;
                var distanceX = x - antennaX;
                var distanceY = y - antennaY;
                for (var multiplier = 0; multiplier < int.MaxValue; multiplier++)
                {
                    var multipliedDistanceX = distanceX * multiplier;
                    var multipliedDistanceY = distanceY * multiplier;
                    
                    var antinode = (x: x + multipliedDistanceX, y: y + multipliedDistanceY);
                    
                    if (antinodes.Any(node => node.x == antinode.x && node.y == antinode.y)) continue;
                    
                    if (!(antinode.x < mapWidth && antinode.x >= 0 && antinode.y < mapHeight && antinode.y >= 0))
                    {
                        break;
                    }
                    
                    antinodes.Add(antinode);
                }
            }
        }
    }
}

int SolutionPart1(int mapHeight, int mapWidth, char[][] chars)
{
    List<(int x, int y)> antinodes = [];

    for (var y = 0; y < mapHeight; y++)
    {
        for (var x = 0; x < mapWidth; x++)
        {
            if (chars[y][x] == '.') continue;
            var antenna = chars[y][x];
            FindAntinodesForAntenna(antenna, x, y, antinodes);
        }
    }

    return antinodes.Count;

    void FindAntinodesForAntenna(char antenna, int antennaX, int antennaY, List<(int x, int y)> antinodes)
    {
        for (var y = 0; y < mapHeight; y++)
        {
            for (var x = 0; x < mapWidth; x++)
            {
                var antennaFrequency = chars[y][x];
                if (antennaFrequency == '.' || (x == antennaX && antennaY == y) || antennaFrequency != antenna) continue;
                var antinode = (x: x + x - antennaX, y: y + y - antennaY);
                if (antinode.x < mapWidth && antinode.x >= 0 && antinode.y < mapHeight && antinode.y >= 0
                    && !antinodes.Any(node => node.x == antinode.x && node.y == antinode.y))
                {
                    antinodes.Add(antinode);
                }
            }
        }
    }
}
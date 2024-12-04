var input = File.ReadAllText("input.txt");
/*
 * Task 1:
 * Count all occurrences of the word XMAS in any direction
 */
/*
 * Task 2:
 * Count all occurrences of the word MAS in an X pattern
 */

Console.WriteLine($"Solution part 1: {SolutionPart1()}");
Console.WriteLine($"Solution part 2: {SolutionPart2()}");

return;

int SolutionPart2()
{
    var rows = input.Split("\n");
    var xmasCount = 0;
    for (var i = 0; i < rows.Length - 2; i++)
    {
        for (var j = 0; j < rows[i].Length - 2; j++)
        {
            var wordTopLeftDiagonal = "" + rows[i][j] + rows[i + 1][j + 1] + rows[i + 2][j + 2];
            var wordTopRightDiagonal = "" + rows[i][j + 2] + rows[i + 1][j + 1] + rows[i + 2][j];
            if (wordTopLeftDiagonal is "MAS" or "SAM" && wordTopRightDiagonal is "SAM" or "MAS")
            {
                xmasCount++;
            }
        }
    }
    return xmasCount;
}
int SolutionPart1()
{
    var rows = input.Split("\n");
    var xmasCount = 0;

    for (var i = 0; i < rows.Length; i++)
    {
        for (var j = 0; j < rows[i].Length; j++)
        {
            if (j + 3 < rows[i].Length)
            {
                var wordHorizontal = "" + rows[i][j] + rows[i][j + 1] + rows[i][j + 2] + rows[i][j + 3];
             
                if (wordHorizontal is "XMAS" or "SAMX")
                {
                    xmasCount++;
                }
            }

            if (i + 3 < rows.Length)
            {
                var wordVertical = "" + rows[i][j] + rows[i + 1][j] + rows[i + 2][j] + rows[i + 3][j];

                if (wordVertical is "XMAS" or "SAMX")
                {
                    xmasCount++;
                }
            }

            if (i + 3 < rows.Length && j + 3 < rows[i].Length)
            {
                var wordTopLeftDiagonal = "" + rows[i][j] + rows[i + 1][j + 1] + rows[i + 2][j + 2] + rows[i + 3][j + 3];
             
                if (wordTopLeftDiagonal is "XMAS" or "SAMX")
                {
                    xmasCount++;
                }
            }

            if (i + 3 < rows.Length && j + 3 < rows[i].Length)
            {
                var wordTopRightDiagonal = "" + rows[i][j + 3] + rows[i + 1][j + 2] + rows[i + 2][j + 1] + rows[i + 3][j];
             
                if (wordTopRightDiagonal is "XMAS" or "SAMX")
                {
                    xmasCount++;
                }
            }
        }
    }

    return xmasCount;
}
var input = File.ReadAllText("input.txt");

List<int> fileMap = [];
List<FileBlock> fileMap2 = [];

for (var i = 0; i < input.Length; i++)
{
    int id;
    if (i % 2 == 0)
    {
        id = i / 2;
    }
    else
    {
        id = -1;
    }

    var blockSize = int.Parse(input[i].ToString());
    for (var j = 0; j < blockSize; j++)
    {
        fileMap.Add(id);
    }
    fileMap2.Add(new FileBlock
    {
        Id = id,
        Size = blockSize
    });
}

//Console.WriteLine(string.Join("", fileMap.Select(x => x == -1 ? "." : x.ToString())));

Console.WriteLine($"Solution part 1: {SolutionPart1(fileMap.ToList())}");
Console.WriteLine($"Solution part 2: {SolutionPart2(fileMap2.ToList())}");
return;

long SolutionPart2(List<FileBlock> fileMapCopy)
{
    for (var i = fileMapCopy.Count - 1; i >= 0; i--)
    {
        if(fileMapCopy[i].Id == -1) continue;
        for (var j = 0; j < i; j++)
        {
            if (fileMapCopy[j].Id != -1 || fileMapCopy[i].Size > fileMapCopy[j].Size) continue;
            fileMapCopy[j].Size -= fileMapCopy[i].Size;
            fileMapCopy.Insert(j, new FileBlock{ Id=fileMapCopy[i].Id, Size=fileMapCopy[i].Size });
            fileMapCopy[i + 1].Id = -1;
            /*List<int> finalFileMap2 = [];
            {
                foreach (var fileBlock in fileMapCopy)
                {
                    var blockSize = fileBlock.Size;
                    var id = fileBlock.Id;
                    if (blockSize == 1 && id == -1) id = -2;
                    for (var y = 0; y < blockSize; y++)
                    {
                        finalFileMap2.Add(id);
                    }
                }
            }
            Console.WriteLine(string.Join("", finalFileMap2.Select(x => x switch
            {
                -1 => ".",
                -2 => ",",
                _ => x.ToString()
            })));*/
            break;
        }
    }

    List<int> finalFileMap = [];
    {
        foreach (var fileBlock in fileMapCopy)
        {
            var blockSize = fileBlock.Size;
            for (var i = 0; i < blockSize; i++)
            {
                finalFileMap.Add(fileBlock.Id);
            }
        }
    }
    //Console.WriteLine(string.Join("", finalFileMap.Select(x => x == -1 ? "." : x.ToString())));

    var checksum = 0L;
    for (var i = 0; i < finalFileMap.Count; i++)
    {
        if (finalFileMap[i] == -1) continue;
        checksum += i * finalFileMap[i];
    }

    return checksum;
}

long SolutionPart1(List<int> fileMapCopy)
{
    for (var i = fileMapCopy.Count - 1; i >= 0; i--)
    {
        if (fileMapCopy[i] == -1) continue;
        for (var j = 0; j < i; j++)
        {
            if (fileMapCopy[j] != -1) continue;
            (fileMapCopy[i], fileMapCopy[j]) = (fileMapCopy[j], fileMapCopy[i]);
            break;
        }
    }

//Console.WriteLine(string.Join("", fileMap.Select(x => x == -1 ? "." : x.ToString())));


    var checksum = 0L;
    for (var i = 0; i < fileMapCopy.Count; i++)
    {
        if (fileMapCopy[i] == -1) break;
        checksum += i * fileMapCopy[i];
    }

    return checksum;
}

internal record FileBlock
{
    public required int Id { get; set; }
    public required int Size { get; set; }
}

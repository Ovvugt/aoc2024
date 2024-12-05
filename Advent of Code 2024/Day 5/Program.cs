var input = File.ReadAllText("input.txt");
/*
 * Task 1:
 * First part of the input defines the rules
 * Second part is the page updates which have to adhere to the first part of the rules
 * Rules is in format X|Y where page X must be update before page Y
 */
var splitInput = input.Split(Environment.NewLine + Environment.NewLine);
var rulesList = ParseRules(splitInput[0]);
var pagesToUpdateList = ParsePagesToUpdate(splitInput[1]);

Console.WriteLine($"Solution part 1: {SolutionPart1()}");
Console.WriteLine($"Solution part 2: {SolutionPart2()}");
return;

int SolutionPart2()
{
    var pagesNotInOrder = GetPageListsNotInOrder(pagesToUpdateList, rulesList);
    var orderedPages = new List<int[]>();
    while (pagesNotInOrder.Count > 0)
    {
        foreach (var pageList in pagesNotInOrder.ToList())
        {
            rulesList.ForEach(rule =>
            {
                var findFirstIndex = Array.IndexOf(pageList, rule.Item1);
                var findSecondIndex = Array.IndexOf(pageList, rule.Item2);
                if (findFirstIndex == -1 || findSecondIndex == -1) return;
                if (findFirstIndex > findSecondIndex)
                {
                    (pageList[findFirstIndex], pageList[findSecondIndex]) = (pageList[findSecondIndex], pageList[findFirstIndex]);
                }
            });
            
            if (!PageListIsInOrder(pageList, rulesList)) continue;
            
            pagesNotInOrder.Remove(pageList);
            orderedPages.Add(pageList);

        }
    } 
    return orderedPages.Sum(pages => pages[pages.Length / 2]);
}

// Was a fun attempt but sadly will never work as the runtime will take too long.
int SolutionPart2_BogoSort()
{
    var pagesNotInOrder = GetPageListsNotInOrder(pagesToUpdateList, rulesList);
    var orderedPages = pagesNotInOrder.Select((page, iterator) =>
    {
        Console.WriteLine($"Sorting page {iterator + 1}/{pagesNotInOrder.Count}");
        while (!PageListIsInOrder(page, rulesList))
        {
            page = Remap(page);
        }

        return page;
    }).ToList();
    return orderedPages.Sum(pages => pages[pages.Length / 2]);
}

int SolutionPart1()
{
    var pagesInOrder = GetPageListsInOrder(pagesToUpdateList, rulesList);
    return pagesInOrder.Sum(pages => pages[pages.Length / 2]);
}

List<Tuple<int, int>> ParseRules(string input)
{
    return input.Split(Environment.NewLine).Select(rule => new Tuple<int, int>(int.Parse(rule.Split('|')[0]), int.Parse(rule.Split('|')[1]))).ToList();
}

List<int[]> ParsePagesToUpdate(string input)
{
    return input.Split(Environment.NewLine)
        .Select(pagesToUpdate => pagesToUpdate.Split(',').Select(int.Parse).ToArray()).ToList();
}

List<int[]> GetPageListsInOrder(List<int[]> pages, List<Tuple<int, int>> rules)
{
    return pages.Where(page => PageListIsInOrder(page, rules)).ToList();
}
List<int[]> GetPageListsNotInOrder(List<int[]> pages, List<Tuple<int, int>> rules)
{
    return pages.Where(pageList =>
        !PageListIsInOrder(pageList, rules)).ToList();
}

int[] Remap(int[] page)
{
    //Console.WriteLine("Old order: " + string.Join(", ", page));
    var pageList = page.ToList();
    var newArray = new int[page.Length];
    var iterator = 0;
    var random = new Random();

    while (pageList.Count > 0)
    {
        var temp = random.Next(pageList.Count);
        newArray[iterator++] = pageList[temp];
        pageList.RemoveAt(temp);
    }
    
    //Console.WriteLine("New order: " + string.Join(", ", newArray));

    return newArray;
}

bool PageListIsInOrder(int[] pageList, List<Tuple<int, int>> rules)
{
    return rules.All(rule =>
        {
            var findFirstIndex = Array.IndexOf(pageList, rule.Item1);
            var findSecondIndex = Array.IndexOf(pageList, rule.Item2);
            if (findFirstIndex == -1 || findSecondIndex == -1)
                return true;
            return findFirstIndex <= findSecondIndex;
        });
}
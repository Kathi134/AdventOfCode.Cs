using System.Text.RegularExpressions;

namespace AdventOfCode;

public sealed class Day04 : BaseDay
{
    private readonly List<List<char>> _input;
    
    public Day04()
    {
        var input = File.ReadAllText(InputFilePath);
        _input = input.Split('\n').Select(l => l.ToCharArray().ToList()).ToList();
    }
    
    public override ValueTask<string> Solve_1()
    {
        var res = 0;
        return new ValueTask<string>(res.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var res = 1;
        return new ValueTask<string>(res.ToString());
    }
}

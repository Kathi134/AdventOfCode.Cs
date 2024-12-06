namespace AdventOfCode;

public sealed class Day01 : BaseDay
{
    private readonly List<Tuple<int, int>> _numbers;
    private readonly List<int> _lefts;
    private readonly List<int> _rights;
    
    private int Records => _numbers.Count;

    public Day01()
    {
        var input = File.ReadAllText(InputFilePath);
        _numbers = input.Split('\n')
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Split("   ").Select(y => int.Parse(y.Trim())))
            .Select(x => new Tuple<int, int>(x.First(), x.Last()))
            .ToList();
        
        _lefts = _numbers.Select(n => n.Item1).ToList();
        _lefts.Sort();
        
        _rights = _numbers.Select(n => n.Item2).ToList();
        _rights.Sort();
    }

    public override ValueTask<string> Solve_1()
    {
        var res = Enumerable.Range(0, Records)
            .Select(i => Math.Abs(_lefts[i] - _rights[i]))
            .Sum();
        return new ValueTask<string>(res.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var res = _lefts
            .Select(n => n * _rights.Count(x => x == n))
            .Sum();
        return new ValueTask<string>(res.ToString());
    }
}

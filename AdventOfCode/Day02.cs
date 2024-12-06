namespace AdventOfCode;

public sealed class Day02 : BaseDay
{
    private readonly List<List<int>> _reports;
    
    private int Records => _reports.Count;

    public Day02()
    {
        var input = File.ReadAllText(InputFilePath);
        _reports = input.Split('\n')
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Split(" ").Select(y => int.Parse(y.Trim())).ToList())
            .ToList();
    }

    private bool IsValidRecord(List<int> record)
    {
        var shifted = record.ToList();
        shifted.RemoveAt(0);
        var diffs = record.Zip(shifted, (a, b) => a-b).ToList();
        var monotone = diffs.All(diff => diff > 0) || diffs.All(diff => diff < 0);
        return monotone && diffs.Select(Math.Abs).All(diff => diff is >= 1 and <= 3);
    }

    private List<List<int>> GetSubReports(List<int> record)
    {
        return Enumerable.Range(0, record.Count)
            .Select(i =>
            {
                var sub = record.ToList();
                sub.RemoveAt(i);
                return sub;
            })
            .ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        var res = _reports.Count(IsValidRecord);
        return new ValueTask<string>(res.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var res = _reports
            .Select(GetSubReports)
            .Count(x => x.Any(IsValidRecord));
        return new ValueTask<string>(res.ToString());
    }
}

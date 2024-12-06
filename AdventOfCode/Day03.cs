using System.Text.RegularExpressions;

namespace AdventOfCode;

public sealed class Day03 : BaseDay
{
    private readonly string _instructions;
    
    public Day03()
    {
        var input = File.ReadAllText(InputFilePath);
        _instructions = string.Join(' ', input.Split('\n'));
    }
    
    public override ValueTask<string> Solve_1()
    {
        var res = Regex.Matches(_instructions, @"mul\((\d*),(\d*)\)")
            .Select(m => int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value))
            .Sum();
        return new ValueTask<string>(res.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        const string rDo = @"do\(\)";
        const string rDont = @"don't\(\)";
        const string rMul = @"mul\((\d*),(\d*)\)";
        var matches = Regex.Matches(_instructions, $"{rDo}|{rDont}|{rMul}");

        var res = 0;
        var enabled = true;
        foreach (var match in matches.ToList())
        {
            if (Regex.IsMatch(match.ToString()!, rDo))
            {
                enabled = true;
            }
            else if (Regex.IsMatch(match.ToString()!, rDont))
            {
                enabled = false;
            }
            else if(enabled)
            {
                res += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
            }
        }
        return new ValueTask<string>(res.ToString());
    }
}

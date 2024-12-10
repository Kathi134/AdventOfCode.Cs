namespace AdventOfCode;

public sealed class Day05 : BaseDay
{
    private readonly List<Tuple<int, int>> _instructions;
    private readonly List<List<int>> _updates;
    
    private List<int> AllNumbers => _instructions
        .Select(x => new List<int> {x.Item1, x.Item2})
        .SelectMany(x => x.ToList())
        .Distinct()
        .ToList();
    
    private readonly List<int> _order = [];

    record Number
    {
        public int n { get; set; }
        public List<int> prev { get; set; } = [];
        public List<int> next { get; set; } = [];
    }

    private Dictionary<int, Number> _numbers = new();
    
    public Day05()
    {
        var input = File.ReadAllText(InputFilePath);
        _instructions = input.Split("\n\r\n")
            .First()
            .Split('\n')
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(s => new List<int>(s.Split('|').Select(x => x.Trim()).Select(int.Parse)))
            .Select(l => new Tuple<int, int>(l[0], l[1]))
            .ToList();
        _updates = input.Split("\n\r\n")
            .Last()
            .Split('\n')
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(s => new List<int>(s.Split(',').Select(x => x.Trim()).Select(int.Parse)))
            .ToList();
    }

    private void Order()
    {
        while (AllNumbers.Count(n => _order.Contains(n))+1 != AllNumbers.Count)
        {
            var x = _instructions.First(t => !_order.Contains(t.Item1)).Item1;
            while (true)
            {
                var t = _instructions.Find(t => t.Item2 == x && !_order.Contains(t.Item1));
                if (t != null)
                    x = t.Item1;
                else
                    break;
                // 11 65 29 49 82 67
            }
            _order.Add(x);
        }
        _order.AddRange(AllNumbers.Where(n => !_order.Contains(n)));
    }

    public bool IsInOrder(List<int> update)
    {
        var order = _order.Where(update.Contains).ToList();
        return order.SequenceEqual(update);
    }

    private void Order2()
    {
        _instructions.ForEach(i =>
        {
            if (_numbers.TryGetValue(i.Item1, out var number))
            {
                number.next.Add(i.Item2);
            }
            else
            {
                var n = new Number {n = i.Item1};
                n.next.Add(i.Item2);
                _numbers.Add(i.Item1, n);
            }
            
            if (_numbers.TryGetValue(i.Item2, out number))
            {
                number.prev.Add(i.Item1);
            }
            else
            {
                var n = new Number {n = i.Item2};
                n.prev.Add(i.Item1);
                _numbers.Add(i.Item2, n);
            }
        });
    }

    private bool IsInOrder2(List<int> update)
    {
        return update.Select((n, idx) =>
            {
                var prevs = update.GetRange(0, idx);
                var nexts = update.GetRange(idx, update.Count - idx);
                var ok = !_numbers[n].prev.Any(nexts.Contains);
                ok &= !_numbers[n].next.Any(prevs.Contains);
                return ok;
            })
            .All(x => x);
    }
    
    public override ValueTask<string> Solve_1()
    {
        Order2();

        var res = _updates
            .Where(IsInOrder2)
            .Select(x => x[x.Count / 2])
            .Sum();
        return new ValueTask<string>(res.ToString());
    }
    
    private List<int> BringInOrder(List<int> update)
    {
        Comparison<int> comp = (x, y) => _numbers[x].next.Contains(y) ? 1 : _numbers[x].prev.Contains(y) ? -1 : 0;
        update.Sort(comp);
        return update;
    }

    public override ValueTask<string> Solve_2()
    {
        var res = _updates
            .Where(x => !IsInOrder2(x))
            .Select(BringInOrder)
            .Select(x => x[x.Count / 2])
            .Sum();
        return new ValueTask<string>(res.ToString());
    }
}

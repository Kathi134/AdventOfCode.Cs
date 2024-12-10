using AdventOfCode;

namespace AdventOfCode;

public record Coordinate(int y, int x) : IComparable<Coordinate>
{
    public int CompareTo(Coordinate other)
    {
        var y = this.y.CompareTo(other.y);
        return y == 0 ? x.CompareTo(other.x) : y;
    }

    public override string ToString()
    {
        return $"({x}, {y})";
    }
}

public enum Direction
{
    North,
    East,
    South,
    West
}

public static class Extensions
{
    public static Direction Turn90(this Direction direction)
    {
        return direction switch
        {
            Direction.North => Direction.East,
            Direction.East => Direction.South,
            Direction.South => Direction.West,
            Direction.West => Direction.North,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}

public sealed class Day06 : BaseDay
{
    private List<Coordinate> obstacles = [];
    private Coordinate init;
    private int maxX;
    private int maxY;

    private HashSet<Coordinate> visited = new();
    
    private HashSet<Tuple<Coordinate, Coordinate>> routes = new();

    public Day06()
    {
        var input = File.ReadAllText(InputFilePath);
        // input = """
        //         ....#.....
        //         .........#
        //         ..........
        //         ..#.......
        //         .......#..
        //         ..........
        //         .#..^.....
        //         ........#.
        //         #.........
        //         ......#...
        //         """;
        var data = input.Split("\n");
        
        
        for (var i = 0; i < data.Length; i++)
        {
            for (var j = 0; j < data[i].Length; j++)
            {
                var c = data[i][j];
                var coord = new Coordinate(i, j);
                switch (c)
                {
                    case '#':
                        obstacles.Add(coord);
                        break;
                    case '^':
                        init = coord;
                        break;
                }
            }
        }

        maxX = data[0].Length;
        maxY = data.Length;
    }

    private bool VisitRange(Coordinate start, Coordinate target, Direction direction)
    {
        // var loop = routes.Add(Tuple.Create(start, target));
        // if (loop)
        // {
        //     return true;
        // }
        
        if (direction is Direction.North or Direction.South)
        {
            var y = Math.Min(start.y, target.y);
            var bound = Math.Max(start.y, target.y);
            for (; y <= bound; y++)
            {
                visited.Add(start with { y = y });
            }
        }

        if (direction is Direction.West or Direction.East)
        {
            var x = Math.Min(start.x, target.x);
            var bound = Math.Max(start.x, target.x);
            for (; x <= bound; x++)
            {
                visited.Add(start with { x = x });
            }
        }

        return false;
    }

    public void FromHereToNextObstacle(Coordinate here, Direction dir)
    {
        if (here.y == -1 || here.x == -1 || here.y >= maxY || here.x >= maxX)
        {
            return;
        }

        if (dir == Direction.North)
        {
            var possibles = obstacles.FindAll(x => x.x == here.x && x.y < here.y);
            var now = here with { y = -1 };
            if (possibles.Count != 0)
            {
                var stop = possibles.MaxBy(x => x.y);
                now = here with { y = stop.y + 1 };
            }

            VisitRange(here, now, dir);
            FromHereToNextObstacle(now, dir.Turn90());
        }

        if (dir == Direction.East)
        {
            var possibles = obstacles.FindAll(x => x.y == here.y && x.x > here.x);
            var now = here with { x = maxX };
            if (possibles.Count != 0)
            {
                var stop = possibles.MinBy(x => x.x);
                now = here with { x = stop.x - 1 };
            }

            VisitRange(here, now, dir);
            FromHereToNextObstacle(now, dir.Turn90());
        }

        if (dir == Direction.South)
        {
            var possibles = obstacles.FindAll(x => x.x == here.x && x.y > here.y);
            var now = here with { y = maxY };
            if (possibles.Count != 0)
            {
                var stop = possibles.MinBy(x => x.y);
                now = here with { y = stop.y - 1 };
            }

            VisitRange(here, now, dir);
            FromHereToNextObstacle(now, dir.Turn90());
        }

        if (dir == Direction.West)
        {
            var possibles = obstacles.FindAll(x => x.y == here.y && x.x < here.x);
            var now = here with { x = -1 };
            if (possibles.Count != 0)
            {
                var stop = possibles.MaxBy(x => x.x);
                now = here with { x = stop.x + 1 };
            }

            VisitRange(here, now, dir);
            FromHereToNextObstacle(now, dir.Turn90());
        }
    }

    public override ValueTask<string> Solve_1()
    {
        FromHereToNextObstacle(init, Direction.North);
        var res = visited.Count - 1;

        return new ValueTask<string>(res.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var res = 2;
        return new ValueTask<string>(res.ToString());
    }
}
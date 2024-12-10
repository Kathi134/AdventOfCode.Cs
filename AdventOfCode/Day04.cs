namespace AdventOfCode;

public sealed class Day04 : BaseDay
{
    private readonly List<List<char>> _input;

    private readonly int _size;
    private readonly int _deepSize;
    
    public Day04()
    {
        var input = File.ReadAllText(InputFilePath);
        _input = input.Split('\n').Where(x => !string.IsNullOrEmpty(x)).Select(l => l.ToCharArray().ToList()).ToList();
        _size = _input.Count;
        _deepSize = _input.First().Count;
    }

    public bool IsXmas(char x, char m, char a, char s)
    {
        return x == 'X' && m == 'M' && a == 'A' && s == 'S';
    }

    public bool NorthXmas(int i, int j)
    {
        if(i-3 < 0)
            return false;
        var x = _input[i][j];
        var m = _input[i-1][j];
        var a = _input[i-2][j];
        var s = _input[i-3][j];
        return IsXmas(x, m, a, s);
    }
    
    public bool NorthEastXmas(int i, int j)
    {
        if(i-3 < 0 || j+3 >= _deepSize)
            return false;
        var x = _input[i][j];
        var m = _input[i-1][j+1];
        var a = _input[i-2][j+2];
        var s = _input[i-3][j+3];
        return IsXmas(x, m, a, s);
    }
    
    public bool EastXmas(int i, int j)
    {
        if (j + 3 >= _deepSize)
            return false;
        var x = _input[i][j];
        var m = _input[i][j + 1];
        var a = _input[i][j + 2];
        var s = _input[i][j + 3];
        return IsXmas(x, m, a, s);
    }
    
    public bool SouthEastXmas(int i, int j)
    {
        if(i+3 >= _size || j+3 >= _deepSize)
            return false;
        var x = _input[i][j];
        var m = _input[i+1][j+1];
        var a = _input[i+2][j+2];
        var s = _input[i+3][j+3];
        return IsXmas(x, m, a, s);
    }
    
    public bool SouthXmas(int i, int j)
    {
        if(i+3 >= _size)
            return false;
        var x = _input[i][j];
        var m = _input[i+1][j];
        var a = _input[i+2][j];
        var s = _input[i+3][j];
        return IsXmas(x, m, a, s);
    }
    
    public bool SouthWestXmas(int i, int j)
    {
        if (i + 3 >= _size || j - 3 < 0)
            return false;
        var x = _input[i][j];
        var m = _input[i+1][j-1];
        var a = _input[i+2][j-2];
        var s = _input[i+3][j-3];
        return IsXmas(x, m, a, s);
    }
    
    public bool WestXmas(int i, int j)
    {
        if (j - 3 < 0)
            return false;
        var x = _input[i][j];
        var m = _input[i][j-1];
        var a = _input[i][j-2];
        var s = _input[i][j-3];
        return IsXmas(x, m, a, s);
    }
    
    public bool NorthWestXmas(int i, int j)
    {
        if (i-3 <0 || j - 3 < 0)
            return false;
        var x = _input[i][j];
        var m = _input[i-1][j-1];
        var a = _input[i-2][j-2];
        var s = _input[i-3][j-3];
        return IsXmas(x, m, a, s);
    }

    public bool IsSamOrMas(char z, char a, char y)
    {
        return (z == 'S' && a == 'A' && y == 'M') 
            || (z == 'M' && a == 'A' && y == 'S');
    }
    
    public bool NorthWestSouthEastMas(int i, int j)
    {
        if (i - 1 < 0 || j - 1 < 0 || i + 1 >= _size || j + 1 >= _deepSize)
            return false;
        var nw = _input[i-1][j-1];
        var a = _input[i][j];
        var se = _input[i+1][j+1];
        return IsSamOrMas(nw, a, se);
    }
    
    public bool NorthEastSouthWestMas(int i, int j)
    {
        if (i - 1 < 0 || j - 1 < 0 || i + 1 >= _size || j + 1 >= _deepSize)
            return false;
        var ne = _input[i-1][j+1];
        var a = _input[i][j];
        var sw = _input[i+1][j-1];
        return IsSamOrMas(ne, a, sw);
    }
    
    public override ValueTask<string> Solve_1()
    {
        var xmasCounter = 0;
        for (var i = 0; i < _input.Count; i++)
        {
            for (var j = 0; j < _input[i].Count; j++)
            {
                if (_input[i][j] == 'X')
                {
                    if (NorthXmas(i,j))
                        xmasCounter++;
                    if (NorthEastXmas(i,j))
                        xmasCounter++;
                    if(EastXmas(i,j))
                        xmasCounter++;
                    if (SouthEastXmas(i,j))
                        xmasCounter++;
                    if (SouthXmas(i,j))
                        xmasCounter++;
                    if(SouthWestXmas(i,j))
                        xmasCounter++;
                    if(WestXmas(i,j))
                        xmasCounter++;
                    if(NorthWestXmas(i,j))
                        xmasCounter++;
                }
            }
        }
        return new ValueTask<string>(xmasCounter.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var xmasCounter = 0;
        for (var i = 0; i < _input.Count; i++)
        {
            for (var j = 0; j < _input[i].Count; j++)
            {
                if (_input[i][j] == 'A')
                {
                    if (NorthEastSouthWestMas(i, j) && NorthWestSouthEastMas(i, j))
                        xmasCounter++;
                }
            }
        }

        return new ValueTask<string>(xmasCounter.ToString());
    }
}

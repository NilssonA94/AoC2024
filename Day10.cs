using System.IO;

namespace AoC2024;

public class Day10 : Day
{
    public override long Expected1 { get; set; } = 36;
    public override long Expected2 { get; set; } = 81;
    public List<(int y, int x)> paths = [];
    public override void Run()
    {
        for (int i = 0; i < Input.Length; i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {

                if (Input[i][j] == '0')
                {
                    PathFind(0, i, j);
                    Result1 += paths.Count;
                    paths = [];
                }
            }
        }
    }
    public void PathFind(int hikeLength, int y, int x)
    {
        if (Input[y][x] == '9' && !paths.Contains((y, x))) paths.Add((y, x));
        if (Input[y][x] == '9') Result2++;
        if (y > 0 && int.Parse(Input[y - 1][x].ToString()) == hikeLength + 1) PathFind(hikeLength + 1, y - 1, x);
        if (x < Input[y].Length - 1 && int.Parse(Input[y][x + 1].ToString()) == hikeLength + 1) PathFind(hikeLength + 1, y, x + 1);
        if (y < Input.Length - 1 && int.Parse(Input[y + 1][x].ToString()) == hikeLength + 1) PathFind(hikeLength + 1, y + 1, x);
        if (x > 0 && int.Parse(Input[y][x - 1].ToString()) == hikeLength + 1) PathFind(hikeLength + 1, y, x - 1);
    }
}

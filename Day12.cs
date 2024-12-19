namespace AoC2024;

public class Day12 : Day
{
    public override long Expected1 { get; set; } = 1930;
    public override long Expected2 { get; set; } = 1206;

    private readonly HashSet<(int y, int x)> visited = [];
    public override void Run()
    {
        Result1 = 0;
        Result2 = 0;
        for (int i = 0; i < Input.Length; i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {
                if (visited.Contains((i, j))) continue;
                int area = 0;
                int perimeter = 0;
                int sides = 0;
                Flood(Input[i][j], i, j, ref area, ref perimeter, ref sides);
                Result1 += area * perimeter;
                Result2 += area * sides;
            }
        }
        visited.Clear();
    }

    public void Flood(char type, int y, int x, ref int area, ref int perimeter, ref int sides)
    {
        if (visited.Contains((y, x))) return;
        visited.Add((y, x));
        area++;
        sides += CheckCorner(y, x);
        if (y == 0 || Input[y - 1][x] != type) perimeter++; else Flood(type, y - 1, x, ref area, ref perimeter, ref sides);
        if (x == Input[y].Length - 1 || Input[y][x + 1] != type) perimeter++; else Flood(type, y, x + 1, ref area, ref perimeter, ref sides);
        if (y == Input.Length - 1 || Input[y + 1][x] != type) perimeter++; else Flood(type, y + 1, x, ref area, ref perimeter, ref sides);
        if (x == 0 || Input[y][x - 1] != type) perimeter++; else Flood(type, y, x - 1, ref area, ref perimeter, ref sides);
    }

    public int CheckCorner(int i, int j)
    {
        int outsideCorners = ((i == 0 || Input[i][j] != Input[i - 1][j]) && (j == 0 || Input[i][j] != Input[i][j - 1]) ? 1 : 0) +
                             ((i == 0 || Input[i][j] != Input[i - 1][j]) && (j == Input[i].Length - 1 || Input[i][j] != Input[i][j + 1]) ? 1 : 0) +
                             ((i == Input.Length - 1 || Input[i][j] != Input[i + 1][j]) && (j == 0 || Input[i][j] != Input[i][j - 1]) ? 1 : 0) +
                             ((i == Input.Length - 1 || Input[i][j] != Input[i + 1][j]) && (j == Input[i].Length - 1 || Input[i][j] != Input[i][j + 1]) ? 1 : 0);

        int insideCorners = (i != 0 && j != Input[i].Length - 1 && Input[i][j] == Input[i - 1][j] && Input[i][j] == Input[i][j + 1] && Input[i][j] != Input[i - 1][j + 1] ? 1 : 0) +
                             (i != Input.Length - 1 && j != Input[i].Length - 1 && Input[i][j] == Input[i + 1][j] && Input[i][j] == Input[i][j + 1] && Input[i][j] != Input[i + 1][j + 1] ? 1 : 0) +
                             (i != Input.Length - 1 && j != 0 && Input[i][j] == Input[i + 1][j] && Input[i][j] == Input[i][j - 1] && Input[i][j] != Input[i + 1][j - 1] ? 1 : 0) +
                             (i != 0 && j != 0 && Input[i][j] == Input[i - 1][j] && Input[i][j] == Input[i][j - 1] && Input[i][j] != Input[i - 1][j - 1] ? 1 : 0);

        return outsideCorners + insideCorners;
    }
}
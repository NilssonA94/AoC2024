namespace AoC2024;

public class Day12 : Day
{
    public override long Expected1 { get; set; } = 1930;
    public override long Expected2 { get; set; } = 0;

    private readonly HashSet<(int y, int x)> visited = [];
    public override void Run()
    {
        for (int i = 0; i < Input.Length; i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {
                if (visited.Contains((i, j))) continue;
                int area = 0;
                int perimeter = 0;
                Flood(i, j, ref area, ref perimeter);
                Console.WriteLine($"Area type: {Input[i][j]}, Perimeter: {perimeter}, Area: {area}, Price: {area * perimeter}");
                Result1 += area * perimeter;
            }
        }
    }

    public void Flood(int y, int x, ref int area, ref int perimeter)
    {
        visited.Add((y, x));
        char type = Input[y][x];

        area++;

        if (y == 0 || Input[y - 1][x] != type) perimeter++; else if (!visited.Contains((y - 1, x))) Flood(y - 1, x, ref area, ref perimeter);
        if (x == Input[y].Length - 1 || Input[y][x + 1] != type) perimeter++; else if (!visited.Contains((y, x + 1))) Flood(y, x + 1, ref area, ref perimeter);
        if (y == Input.Length - 1 || Input[y + 1][x] != type) perimeter++; else if (!visited.Contains((y + 1, x))) Flood(y + 1, x, ref area, ref perimeter);
        if (x == 0 || Input[y][x - 1] != type) perimeter++; else if (!visited.Contains((y, x - 1))) Flood(y, x - 1, ref area, ref perimeter);

        /*if (y > 0 && Input[y - 1][x] == type && !visited.Contains((y - 1, x))) Flood(y - 1, x, ref area, ref perimeter);
        if (x < Input[y].Length - 1 && Input[y][x + 1] == type && !visited.Contains((y, x + 1))) Flood(y, x + 1, ref area, ref perimeter);
        if (y < Input.Length - 1 && Input[y + 1][x] == type && !visited.Contains((y + 1, x))) Flood(y + 1, x, ref area, ref perimeter);
        if (x > 0 && Input[y][x - 1] == type && !visited.Contains((y, x - 1))) Flood(y, x - 1, ref area, ref perimeter);*/
    }
}
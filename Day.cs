namespace AoC2024;

public abstract class Day
{
    public string[] Input { get; set; }
    public abstract long Expected1 { get; set; }
    public abstract long Expected2 { get; set; }
    public long Result1 { get; set; }
    public long Result2 { get; set; }

    public Day()
    {
        var name = this.GetType().Name;
        Input = System.IO.File.ReadAllLines($"./AoC2024/missions/{name}/{name}.test");
        Run();
        if (Expected1 + Expected2 != Result1 + Result2)
        {
            Console.WriteLine($"Test failed! Expected {Expected1} and {Expected2} but got {Result1} and {Result2}");
            return;
        }
        Result1 = 0;
        Result2 = 0;
        Input = System.IO.File.ReadAllLines($"./AoC2024/missions/{name}/{name}.input");
        Run();
        Console.WriteLine($"{name} Result 1: {Result1} Result 2: {Result2}");
    }

    public abstract void Run();
}
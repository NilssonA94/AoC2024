namespace AoC2024;
public class Day11 : Day
{
    public override long Expected1 { get; set; } = 22;
    public override long Expected2 { get; set; } = 22;
    public Dictionary<long, (long first, long second)> cache = [];
    public override void Run()
    {
        cache.Clear();
        Dictionary<long, long> stones = [];
        foreach (var stone in Input[0].Split(" ", StringSplitOptions.RemoveEmptyEntries))
        {
            stones.Add(long.Parse(stone), 1);
        }

        for (int i = 0; i < 75; i++)
        {
            stones = Blink(stones);

            if (i == 5)
            {
                Result1 = stones.Values.Sum();
                Result2 = stones.Values.Sum();
            }

            if (i == 24) Console.WriteLine("The real answer for Result1 is: " + stones.Values.Sum());
        }
        Console.WriteLine("The real answer for Result2 is: " + stones.Values.Sum());
    }

    public Dictionary<long, long> Blink(Dictionary<long, long> stones)
    {
        Dictionary<long, long> stonesAfterBlink = [];
        foreach (var stone in stones)
        {
            if (stone.Key == 0)
            {
                stonesAfterBlink[1] = stonesAfterBlink.TryGetValue(1, out long value) ? value + stone.Value : stone.Value;
                continue;

            }

            if (cache.TryGetValue(stone.Key, out var halves))
            {
                stonesAfterBlink.TryAdd(halves.first, 0);
                stonesAfterBlink[halves.first] += stone.Value;

                stonesAfterBlink.TryAdd(halves.second, 0);
                stonesAfterBlink[halves.second] += stone.Value;
                continue;
            }

            long stoneLength = CountDigits(stone.Key);
            if (stoneLength % 2 == 0)
            {
                long divisor = 1;
                for (int i = 0; i < stoneLength / 2; i++)
                {
                    divisor *= 10;
                }

                long first = stone.Key / divisor;
                long second = stone.Key % divisor;

                stonesAfterBlink.TryAdd(first, 0);
                stonesAfterBlink[first] += stone.Value;

                stonesAfterBlink.TryAdd(second, 0);
                stonesAfterBlink[second] += stone.Value;

                cache[stone.Key] = (first, second);
                continue;
            }

            stonesAfterBlink.TryAdd(stone.Key * 2024, 0);
            stonesAfterBlink[stone.Key * 2024] += stone.Value;
        }
        return stonesAfterBlink;
    }

    public static long CountDigits(long stone)
    {
        if (stone < 10) return 1;
        long amountOfDigits = 0;
        while (stone > 0)
        {
            stone /= 10;
            amountOfDigits++;
        }
        return amountOfDigits;
    }
}

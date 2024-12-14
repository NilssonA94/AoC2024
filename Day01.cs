namespace AoC2024;

public class Day01 : Day
{
    public override long Expected1 { get; set; } = 11;
    public override long Expected2 { get; set; } = 31;
    public override void Run()
    {
        Result1 = 0;
        Result2 = 0;
        List<int> left = [];
        List<int> right = [];

        foreach (var line in Input)
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            left.Add(Int32.Parse(numbers[0]));
            right.Add(Int32.Parse(numbers[1]));
        }

        left.Sort((a, b) => a.CompareTo(b));
        right.Sort((a, b) => a.CompareTo(b));

        for (int i = 0; i < left.Count; i++)
        {
            if (left[i] > right[i])
            {
                Result1 += left[i] - right[i];
            }
            else
            {
                Result1 += right[i] - left[i];
            }
        }

        foreach (var leftNumber in left)
        {
            int count = 0;
            foreach (var rightNumber in right)
            {
                if (leftNumber == rightNumber) count++;
            }
            Result2 += leftNumber * count;
        }
    }
}
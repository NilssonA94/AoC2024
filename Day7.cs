namespace AoC2024;

public class Day7 : Day
{
    public override long Expected1 { get; set; } = 3749;
    public override long Expected2 { get; set; } = 11387;
    public override void Run()
    {
        Result1 = 0;
        Result2 = 0;

        foreach (var line in Input)
        {
            var stringNumbers = line.Split(':', ' ');
            long controlNumber = long.Parse(stringNumbers[0]);
            List<long> numbers = [];
            for (int i = 1; i < stringNumbers.Length; i++)
            {
                if (stringNumbers[i] == "") continue;
                numbers.Add(long.Parse(stringNumbers[i]));
            }

            if (CalculateCombinations(numbers, controlNumber, false)) Result1 += controlNumber;
            if (CalculateCombinations(numbers, controlNumber, true)) Result2 += controlNumber;
        }
    }

    private static bool CalculateCombinations(List<long> numbers, long controlNumber, bool concatenating, int i = 0, long testNumber = 0)
    {
        if (testNumber > controlNumber) return false;
        if (i == numbers.Count) return testNumber == controlNumber;

        if (concatenating) if (CalculateCombinations(numbers, controlNumber, concatenating, i + 1, long.Parse(testNumber.ToString() + numbers[i].ToString()))) return true;

        if (CalculateCombinations(numbers, controlNumber, concatenating, i + 1, testNumber + numbers[i])) return true;
        if (CalculateCombinations(numbers, controlNumber, concatenating, i + 1, testNumber * numbers[i])) return true;

        return false;
    }
}

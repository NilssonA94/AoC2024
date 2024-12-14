namespace AoC2024;

public class Day02 : Day
{
    public override long Expected1 { get; set; } = 2;
    public override long Expected2 { get; set; } = 4;
    public override void Run()
    {
        Result1 = ValidateLevels();
        Result2 = ValidateLevelsWithExtraLife();
    }

    public long ValidateLevels()
    {
        var validLevels = new List<List<int>>();
        foreach (var line in Input)
        {
            List<int> numbers = [];
            var stringNumbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var stringNumber in stringNumbers)
            {
                numbers.Add(int.Parse(stringNumber));
            }
            bool isValid = false;
            for (int i = 0; i < numbers.Count; i++)
            {
                isValid = CheckValidity(numbers);
            }
            if (isValid)
            {
                validLevels.Add(numbers);
            }
        }
        return validLevels.Count;
    }

    public long ValidateLevelsWithExtraLife()
    {
        var validLevels = new List<List<int>>();
        var invalidLevels = new List<List<int>>();

        foreach (var line in Input)
        {
            List<int> numbers = [];
            var stringNumbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var stringNumber in stringNumbers)
            {
                numbers.Add(int.Parse(stringNumber));
            }
            bool isValid = false;
            for (int i = 0; i < numbers.Count; i++)
            {
                isValid = CheckValidity(numbers);
            }
            if (isValid)
            {
                validLevels.Add(numbers);
            }
            else
            {
                invalidLevels.Add(numbers);
            }
        }

        foreach (var level in invalidLevels)
        {
            bool isValid = false;
            for (int i = 0; i < level.Count; i++)
            {
                var copiedLevel = new List<int>(level);
                copiedLevel.RemoveAt(i);

                isValid = CheckValidity(copiedLevel);
                if (isValid)
                {
                    break;
                }
            }
            if (isValid)
            {
                validLevels.Add(level);
            }
        }

        return validLevels.Count;
    }

    public static bool CheckValidity(List<int> numbers)
    {
        bool asc = true;
        bool desc = true;
        for (int i = 1; i < numbers.Count; i++)
        {
            int difference = numbers[i - 1] - numbers[i];

            if (difference > 0)
            {
                desc = false;
                if (difference > 3)
                {
                    return false;
                }
            }
            else if (difference < 0)
            {
                asc = false;
                if (difference < -3)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        return asc || desc;
    }
}
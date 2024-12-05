﻿namespace AoC2024;

public class Day5 : Day
{
    public override long Expected1 { get; set; } = 143;
    public override long Expected2 { get; set; } = 123;
    public override void Run()
    {
        Result1 = 0;
        Result2 = 0;

        bool isRules = true;
        List<string> rules = [];

        foreach (var line in Input)
        {
            bool isValid = false;
            if (string.IsNullOrEmpty(line))
            {
                isRules = false;
                continue;
            }

            if (isRules)
            {
                    rules.Add(line);
            }
            else
            {
                string[] numbers = line.Split(',');

                for (var i = 0; i < numbers.Length - 1; i++)
                {
                    if (rules.Contains(numbers[i + 1] + "|" + numbers[i]))
                    {
                        List<string> sortedNumbers = [.. numbers];
                        sortedNumbers.Sort((a, b) =>
                        {
                            string rule = a + "|" + b;
                            return rules.Contains(rule) ? -1 : 1;
                        });
                        Result2 += Int32.Parse(sortedNumbers[numbers.Length / 2]);
                        isValid = false;
                        break;
                    }
                    isValid = true;
                }

                if (isValid) Result1 += Int32.Parse(numbers[numbers.Length/2]);
            }
        }
    }
}
namespace AoC2024;

public class Day03 : Day
{
    public override long Expected1 { get; set; } = 161;
    public override long Expected2 { get; set; } = 48;
    public override void Run()
    {
        Result1 = FindMul();
        Result2 = FindMulWithDoAndDoNot();
    }

    public long FindMul()
    {
        long product = 0;
        foreach (var line in Input)
        {
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] != 'm')
                {
                    continue;
                }

                if (line[i + 4] < line.Length &&
                    line[i] == 'm' &&
                    line[i + 1] == 'u' &&
                    line[i + 2] == 'l' &&
                    line[i + 3] == '(')
                {
                    i += 4;
                    bool isNumber = true;
                    string firstNumberString = "";
                    while (isNumber)
                    {
                        if (char.IsDigit(line[i]))
                        {
                            firstNumberString += line[i];
                            i++;
                        }
                        else
                        {
                            isNumber = false;
                        }
                    }
                    if (line[i] == ',' && char.IsDigit(line[i + 1]))
                    {
                        isNumber = true;
                        i++;
                    }
                    else
                    {
                        continue;
                    }

                    string secondNumberString = "";
                    while (isNumber)
                    {
                        if (char.IsDigit(line[i]))
                        {
                            secondNumberString += line[i];
                            i++;
                        }
                        else
                        {
                            isNumber = false;
                        }
                    }

                    if (line[i] != ')')
                    {
                        continue;
                    }

                    if (int.TryParse(firstNumberString, out int num1) && int.TryParse(secondNumberString.ToString(), out int num2))
                    {
                        product += num1 * num2;
                    }
                }
            }
        }
        return product;
    }

    public long FindMulWithDoAndDoNot()
    {
        long product = 0;
        bool enabled = true;

        foreach (var line in Input)
        {
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == 'd')
                {
                    enabled = DoDoNot(line, i, enabled);
                    continue;
                }

                if (enabled)
                {
                    if (line[i] != 'm')
                    {
                        continue;
                    }

                    if (i + 4 < line.Length &&
                        line[i] == 'm' &&
                        line[i + 1] == 'u' &&
                        line[i + 2] == 'l' &&
                        line[i + 3] == '(')
                    {
                        i += 4;
                        bool isNumber = true;
                        string firstNumberString = "";
                        while (isNumber)
                        {
                            if (char.IsDigit(line[i]))
                            {
                                firstNumberString += line[i];
                                i++;
                            }
                            else
                            {
                                isNumber = false;
                            }
                        }
                        if (line[i] == ',' && char.IsDigit(line[i + 1]))
                        {
                            isNumber = true;
                            i++;
                        }
                        else
                        {
                            continue;
                        }

                        string secondNumberString = "";
                        while (isNumber)
                        {
                            if (char.IsDigit(line[i]))
                            {
                                secondNumberString += line[i];
                                i++;
                            }
                            else
                            {
                                isNumber = false;
                            }
                        }

                        if (line[i] != ')')
                        {
                            continue;
                        }

                        if (int.TryParse(firstNumberString, out int num1) && int.TryParse(secondNumberString.ToString(), out int num2))
                        {
                            product += num1 * num2;
                        }
                    }
                }
            }
        }
        return product;
    }

    public static bool DoDoNot(string line, int index, bool enabled)
    {
        if (index + 4 < line.Length &&
                        line[index + 1] == 'o' &&
                        line[index + 2] == '(' &&
                        line[index + 3] == ')')
        {
            return true;
        }
        else if (index + 7 < line.Length &&
                        line[index + 2] == 'n' &&
                        line[index + 3] == '\'' &&
                        line[index + 4] == 't' &&
                        line[index + 5] == '(' &&
                        line[index + 6] == ')')
        {
            return false;
        }

        return enabled;
    }
}
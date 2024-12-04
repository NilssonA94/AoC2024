namespace AoC2024;

public class Day4 : Day
{
    public override long Expected1 { get; set; } = 18;
    public override long Expected2 { get; set; } = 9;
    public override void Run()
    {
        Result1 = 0;
        Result2 = 0;

        string word = "XMAS";
        string reversedWord = ReverseWord(word);
        List<(int Line, int Char)> firstLetterIndexes = FindIndex(word, Input, true);
        List<(int Line, int Char)> lastLetterIndexes = FindIndex(word, Input, false);

        List<(int Line, int Char)> indexOfA = FindIndex("A", Input, true);

        int verticals = CheckVertical(Input, word, firstLetterIndexes);
        int horizontals = CheckHorizontal(Input, word, firstLetterIndexes);
        int descendingDiagonals = CheckDescendingDiagonal(Input, word, firstLetterIndexes);
        int ascendingDiagonals = CheckAscendingDiagonal(Input, word, firstLetterIndexes);
        int reversedVerticals = CheckVertical(Input, reversedWord, lastLetterIndexes);
        int reversedHorizontals = CheckHorizontal(Input, reversedWord, lastLetterIndexes);
        int reversedDescendingDiagonals = CheckDescendingDiagonal(Input, reversedWord, lastLetterIndexes);
        int reversedAscendingDiagonals = CheckAscendingDiagonal(Input, reversedWord, lastLetterIndexes);
        Console.WriteLine($"Verticals: {verticals}, Horizontals: {horizontals}, Descending diagonals: {descendingDiagonals}, Ascending diagonals: {ascendingDiagonals}");
        Console.WriteLine($"Reversed verticals: {reversedVerticals}, Reversed horizontals: {reversedHorizontals}, Reversed descending diagonals: {reversedDescendingDiagonals}, Reversed ascending diagonals: {reversedAscendingDiagonals}");

        Result1 = verticals + horizontals + descendingDiagonals + ascendingDiagonals + reversedVerticals + reversedHorizontals + reversedDescendingDiagonals + reversedAscendingDiagonals;
        Result2 = CheckCrossMAS(indexOfA, Input);
    }

    public static int CheckVertical(string[] input, string word, List<(int Line, int Char)> indexes)
    {
        int score = 0;
        bool safe = false;
        for (int i = 0; i < indexes.Count; i++)
        {
            for (int j = 1; j < word.Length; j++)
            {
                if (j + indexes[i].Line < input[indexes[i].Line].Length)
                {
                    if (input[indexes[i].Line + j][indexes[i].Char] == word[j])
                    {
                        safe = true;
                    }
                    else
                    {
                        safe = false;
                        break;
                    }
                }
                else
                {
                    safe = false;
                }
            }
            if (safe) score++;
        }

        return score;
    }

    public static int CheckHorizontal(string[] input, string word, List<(int Line, int Char)> indexes)
    {
        int score = 0;
        bool safe = false;
        for (int i = 0; i < indexes.Count; i++)
        {
            for (int j = 1; j < word.Length; j++)
            {
                if (j + indexes[i].Char < input[indexes[i].Line].Length)
                {
                    if (input[indexes[i].Line][indexes[i].Char + j] == word[j])
                    {
                        safe = true;
                    }
                    else
                    {
                        safe = false;
                        break;
                    }
                }
                else
                {
                    safe = false;
                }
            }
            if (safe) score++;
        }

        return score;
    }

    public static int CheckDescendingDiagonal(string[] input, string word, List<(int Line, int Char)> indexes)
    {
        int score = 0;
        bool safe = false;
        for (int i = 0; i < indexes.Count; i++)
        {
            for (int j = 1; j < word.Length; j++)
            {
                if (j + indexes[i].Line < input[indexes[i].Line].Length && j + indexes[i].Char < input[indexes[i].Line].Length)
                {
                    if (input[indexes[i].Line + j][indexes[i].Char + j] == word[j])
                    {
                        safe = true;
                    }
                    else
                    {
                        safe = false;
                        break;
                    }
                }
                else
                {
                    safe = false;
                }
            }
            if (safe) score++;
        }

        return score;
    }

    public static int CheckAscendingDiagonal(string[] input, string word, List<(int Line, int Char)> indexes)
    {
        int score = 0;
        bool safe = false;
        for (int i = indexes.Count - 1; i >= 0; i--)
        {
            for (int j = 1; j < word.Length; j++)
            {
                int bigLine = indexes[i].Line - j;
                int smallLine = j - indexes[i].Line;
                int difference = j - indexes[i].Line - j < 0 ? bigLine : smallLine;

                if (indexes[i].Line - j >= 0 && indexes[i].Line - j < input[indexes[i].Line].Length && j + indexes[i].Char < input[indexes[i].Line].Length)
                {
                    if (input[difference][indexes[i].Char + j] == word[j])
                    {
                        safe = true;
                    }
                    else
                    {
                        safe = false;
                        break;
                    }
                }
                else
                {
                    safe = false;
                }
            }
            if (safe) score++;
        }

        return score;
    }

    public static int CheckCrossMAS(List<(int Line, int Char)> indexes, string[] input)
    {
        int score = 0;
        for (int i = 0; i < indexes.Count; i++)
        {
            if (indexes[i].Line + 1 < input.GetLength(0) && indexes[i].Char + 1 < input[0].Length && 
                indexes[i].Line - 1 >= 0 && indexes[i].Char - 1 >= 0)
            {
                // S on top
                if (input[indexes[i].Line + 1][indexes[i].Char + 1] == 'M' && input[indexes[i].Line + 1][indexes[i].Char - 1] == 'M' && input[indexes[i].Line - 1][indexes[i].Char + 1] == 'S' && input[indexes[i].Line - 1][indexes[i].Char - 1] == 'S')
                {
                    score++;
                }

                // S on right
                if (input[indexes[i].Line + 1][indexes[i].Char - 1] == 'M' && input[indexes[i].Line - 1][indexes[i].Char - 1] == 'M' && input[indexes[i].Line + 1][indexes[i].Char + 1] == 'S' && input[indexes[i].Line - 1][indexes[i].Char + 1] == 'S')
                {
                    score++;
                }

                // S on bottom
                if (input[indexes[i].Line - 1][indexes[i].Char + 1] == 'M' && input[indexes[i].Line - 1][indexes[i].Char - 1] == 'M' && input[indexes[i].Line + 1][indexes[i].Char + 1] == 'S' && input[indexes[i].Line + 1][indexes[i].Char - 1] == 'S')
                {
                    score++;
                }

                // S on left
                if (input[indexes[i].Line + 1][indexes[i].Char + 1] == 'M' && input[indexes[i].Line - 1][indexes[i].Char + 1] == 'M' && input[indexes[i].Line + 1][indexes[i].Char - 1] == 'S' && input[indexes[i].Line - 1][indexes[i].Char - 1] == 'S')
                {
                    score++;
                }
            }
        }

        return score;
    }

    public static string ReverseWord(string word)
    {
        char[] charArray = word.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    public static List<(int Line, int Char)> FindIndex(string word, string[] input, bool first)
    {
        List<(int Line, int Char)> list = [];
        var order = first ? 0 : ^1;
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == word[order])
                {
                    list.Add((i, j));
                }
            }
        }
        return list;
    }
}

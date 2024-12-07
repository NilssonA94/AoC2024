using System;

namespace AoC2024;

public class Day6 : Day
{
    public override long Expected1 { get; set; } = 41;
    public override long Expected2 { get; set; } = 6;
    public override void Run()
    {
        Result1 = 0;
        Result2 = 0;

        var guard = (0, 0);
        var staticGuard = (0, 0);
        var isGuardPresent = true;
        int direction = 0;
        List<(int, int)> visitedAreas = [];

        for (int i = 0; i < Input.Length; i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {
                if (Input[i][j] == '^')
                {
                    guard = (i, j);
                    staticGuard = guard;
                    visitedAreas.Add(guard);
                    Result1++;
                    break;
                }
            }
        }

        while (isGuardPresent)
        {
            if ((direction == 0 && guard.Item1 - 1 < 0) ||
                (direction == 1 && guard.Item2 + 1 >= Input[guard.Item1].Length) ||
                (direction == 2 && guard.Item1 + 1 >= Input.Length) ||
                (direction == 3 && guard.Item2 - 1 < 0))
            {
                break;
            }

            if (direction == 0 && Input[guard.Item1 - 1][guard.Item2] == '#')
            {
                direction = 1;
                continue;
            }
            else if (direction == 1 && Input[guard.Item1][guard.Item2 + 1] == '#')
            {
                direction = 2;
                continue;
            }
            else if (direction == 2 && Input[guard.Item1 + 1][guard.Item2] == '#')
            {
                direction = 3;
                continue;
            }
            else if (direction == 3 && Input[guard.Item1][guard.Item2 - 1] == '#')
            {
                direction = 0;
                continue;
            }

            if (direction == 0)
            {
                guard = (guard.Item1 - 1, guard.Item2);
                if (visitedAreas.Contains(guard))
                {
                    continue;
                }
                else
                {
                    Result1++;
                    visitedAreas.Add(guard);
                }
            }
            else if (direction == 1)
            {
                guard = (guard.Item1, guard.Item2 + 1);
                if (visitedAreas.Contains(guard))
                {
                    continue;
                }
                else
                {
                    Result1++;
                    visitedAreas.Add(guard);
                }
            }
            else if (direction == 2)
            {
                guard = (guard.Item1 + 1, guard.Item2);
                if (visitedAreas.Contains(guard))
                {
                    continue;
                }
                else
                {
                    Result1++;
                    visitedAreas.Add(guard);
                }
            }
            else if (direction == 3)
            {
                guard = (guard.Item1, guard.Item2 - 1);
                if (visitedAreas.Contains(guard))
                {
                    continue;
                }
                else
                {
                    Result1++;
                    visitedAreas.Add(guard);
                }
            }
        }

        for (int i = 0; i < Input.Length; i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {
                if ((i, j) == staticGuard) continue;
                string[] obstacles = [.. Input];
                obstacles[i] = obstacles[i].Remove(j, 1).Insert(j, "#");
                Result2 += IsBlocking(staticGuard, obstacles);
            }
        }
    }

    public static int IsBlocking((int, int) guard, string[] input)
    {
        bool isGuardPresent = true;
        bool isInfinite = false;
        int direction = 0;
        HashSet<(int, int, int)> visitedAreas = [];
        while (isGuardPresent)
        {
            if ((direction == 0 && guard.Item1 - 1 < 0) ||
            (direction == 1 && guard.Item2 + 1 >= input[guard.Item1].Length) ||
                (direction == 2 && guard.Item1 + 1 >= input.Length) ||
                (direction == 3 && guard.Item2 - 1 < 0))
            {
                break;
            }

            if (visitedAreas.Contains((guard.Item1, guard.Item2, direction)))
            {
                isInfinite = true;
                break;
            }

            visitedAreas.Add((guard.Item1, guard.Item2, direction));

            if (direction == 0 && input[guard.Item1 - 1][guard.Item2] == '#')
            {
                direction = 1;
                continue;
            }
            else if (direction == 1 && input[guard.Item1][guard.Item2 + 1] == '#')
            {
                direction = 2;
                continue;
            }
            else if (direction == 2 && input[guard.Item1 + 1][guard.Item2] == '#')
            {
                direction = 3;
                continue;
            }
            else if (direction == 3 && input[guard.Item1][guard.Item2 - 1] == '#')
            {
                direction = 0;
                continue;
            }

            if (direction == 0)
            {
                guard = (guard.Item1 - 1, guard.Item2);
            }
            else if (direction == 1)
            {
                guard = (guard.Item1, guard.Item2 + 1);
            }
            else if (direction == 2)
            {
                guard = (guard.Item1 + 1, guard.Item2);
            }
            else if (direction == 3)
            {
                guard = (guard.Item1, guard.Item2 - 1);
            }
        }
        return isInfinite ? 1 : 0;
    }
}
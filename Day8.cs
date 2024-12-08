using System.Collections.ObjectModel;

namespace AoC2024;

public class Day8 : Day
{
    public override long Expected1 { get; set; } = 14;
    public override long Expected2 { get; set; } = 34;
    public override void Run()
    {
        Dictionary<(int line, int index), char> antennas = [];
        Dictionary<(int line, int index), char> existingAntennas = [];
        Collection<(int, int)> firsts = [];
        Collection<(int, int)> antinodes = [];
        Collection<(int, int)> extraAntinodes = [];

        for (int i = 0; i < Input.Length; i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {
                if (Input[i][j] != '.')
                {
                    antennas.Add((i, j), Input[i][j]);
                }
            }
        }
        
        foreach (var antenna in antennas)
        {
            if (existingAntennas.ContainsValue(antenna.Value))
            {
                foreach (var existingAntenna in existingAntennas)
                {
                    if (existingAntenna.Value == antenna.Value)
                    {
                        foreach (var ea in existingAntennas)
                        {
                            if (ea.Value == existingAntenna.Value)
                            {
                                firsts.Add(ea.Key);
                                break;
                            }
                        }
                        int lineDifference = antenna.Key.line - existingAntenna.Key.line;
                        int indexDifference = antenna.Key.index - existingAntenna.Key.index;

                        var firstAntinode = (existingAntenna.Key.line - lineDifference, existingAntenna.Key.index - indexDifference);
                        var secondAntinode = (antenna.Key.line + lineDifference, antenna.Key.index + indexDifference);

                        TryAddAntinode(Input, antinodes, firstAntinode);
                        TryAddAntinode(Input, antinodes, secondAntinode);

                        TryAddRecursiveAntinode(Input, extraAntinodes, firstAntinode, lineDifference, indexDifference, false);
                        TryAddRecursiveAntinode(Input, extraAntinodes, secondAntinode, lineDifference, indexDifference, true);
                    }
                }
                if (!extraAntinodes.Contains(antenna.Key)) extraAntinodes.Add(antenna.Key);
            }
            existingAntennas.Add(antenna.Key, antenna.Value);
        }
        foreach (var first in firsts)
        {
            if (!extraAntinodes.Contains(first))
            {
                extraAntinodes.Add(first);
            }
        }
        Result1 = antinodes.Count;
        Result2 = extraAntinodes.Count;
    }

    private static void TryAddAntinode(string[] input, Collection<(int, int)> antinodes, (int, int) antinode)
    {
        if (antinode.Item1 >= 0 &&
            antinode.Item1 < input.Length &&
            antinode.Item2 >= 0 &&
            antinode.Item2 < input[antinode.Item1].Length &&
            !antinodes.Contains(antinode)) antinodes.Add(antinode);
    }

    private static void TryAddRecursiveAntinode(string[] input, Collection<(int, int)> antinodes, (int, int) antinode, int lineDifference, int indexDifference, bool isAsc)
    {
        if (antinode.Item1 >= 0 &&
            antinode.Item1 < input.Length &&
            antinode.Item2 >= 0 &&
            antinode.Item2 < input[antinode.Item1].Length)
        {
            if (!antinodes.Contains(antinode))
            {
                antinodes.Add(antinode);
            }
            var nextAntinode = isAsc ? (antinode.Item1 + lineDifference, antinode.Item2 + indexDifference) : (antinode.Item1 - lineDifference, antinode.Item2 - indexDifference);
            TryAddRecursiveAntinode(input, antinodes, nextAntinode, lineDifference, indexDifference, isAsc);
        }
    }
}
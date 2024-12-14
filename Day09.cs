namespace AoC2024;

public class Day09 : Day
{
    public override long Expected1 { get; set; } = 1928;
    public override long Expected2 { get; set; } = 2858;
    public override void Run()
    {
        List<(long index, long length, bool hasMoved)> map = [];
        List<string> blocks = [];
        long index = -1;

        foreach (var block in Input[0])
        {
            if (map.Count % 2 == 0 && block != '0') index++;
            map.Add(map.Count % 2 == 0 ? (index, long.Parse(block.ToString()), false) : (-1, long.Parse(block.ToString()), true));
        }

        foreach (var block in map)
        {
            for (int i = 0; i < block.length; i++)
            {
                blocks.Add(block.index == -1 ? "." : block.index.ToString());
            }
        }

        for (int i = blocks.Count - 1; i > 0; i--)
        {
            if (blocks[i] == ".") continue;
            for (int j = 0; j < i; j++)
            {
                if (blocks[j] == ".")
                {
                    blocks[j] = blocks[i];
                    blocks[i] = ".";
                    break;
                }
            }
        }

        for (int i = map.Count - 1; i > 0; i--)
        {
            if (map[i].hasMoved) continue;
            map[i] = (map[i].index, map[i].length, true);

            for (int j = 0; j < map.Count - 1; j++)
            {
                if (j == i) break;
                while (map[j].index == map[j + 1].index)
                {
                    map[j] = (-1, map[j].length + map[j + 1].length, true);
                    map.RemoveAt(j + 1);
                    i--;
                }
            }

            for (int j = 0; j < map.Count - 1; j++)
            {
                if (j == i) break;
                if (map[j].index == -1 && map[j].length >= map[i].length)
                {
                    long difference = map[j].length - map[i].length;
                    map[j] = map[i];
                    map[i] = (-1, map[i].length, true);
                    if (difference != 0)
                    {
                        map.Insert(j + 1, (-1, difference, true));
                        i++;
                    }
                    break;
                }
            }
        }

        for (int i = 0; i < blocks.Count; i++)
        {
            if (blocks[i] == ".") continue;
            Result1 += i * int.Parse(blocks[i]);
        }

        long counter = -1;
        for (int i = 0; i < map.Count; i++)
        {
            for (int j = 0; j < map[i].length; j++)
            {
                counter++;
                if (map[i].index == -1) continue;

                Result2 += counter * map[i].index;
            }
        }
    }
}
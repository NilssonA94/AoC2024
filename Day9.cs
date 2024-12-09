namespace AoC2024;

public class Day9 : Day
{
    public override long Expected1 { get; set; } = 1928;
    public override long Expected2 { get; set; } = 0;
    public override void Run()
    {
        string diskMap = "";
        int index = 0;
        for (int i = 0; i < Input[0].Length; i++)
        {
            bool isFile = i % 2 == 0;
            for (int j = 0; j < int.Parse(Input[0][i].ToString()); j++)
            {
                if (isFile)
                {
                    // file
                    diskMap += index.ToString();
                }
                else
                {
                    // free space
                    diskMap += ".";
                }
            }
            if (isFile) index++;
        }

        char[] diskArray = diskMap.ToCharArray();
        for (int i = diskArray.Length - 1; i > 0; i--)
        {
            if (diskArray[i] == '.') continue;

            int idx = Array.IndexOf(diskArray, '.');

            if (idx > i) break;

            diskArray[idx] = diskArray[i];
            diskArray[i] = '.';
        }
        diskMap = new string(diskArray).Replace(".", String.Empty);

        for (int i = 0; i < diskMap.Length; i++)
        {
            Result1 += i * int.Parse(diskMap[i].ToString());
        }
    }
}
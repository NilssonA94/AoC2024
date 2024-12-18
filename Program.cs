using System.Diagnostics;

var watch = Stopwatch.StartNew();

watch.Start();
_ = new AoC2024.Day12();
watch.Stop();

Console.WriteLine(watch.Elapsed);
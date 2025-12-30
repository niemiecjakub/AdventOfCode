
using AdventOfCode.Year2025.Day11;
using System.Diagnostics;

void WithStopwatch(Action action)
{
  var stopwatch = Stopwatch.StartNew();
  action();
  stopwatch.Stop();
  Console.WriteLine($"Elapsed Time: {stopwatch.ElapsedMilliseconds} ms");
}

WithStopwatch(Solution.PartTwo);
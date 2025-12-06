using System;
using System.Linq;

namespace AdventOfCode.Day05;

public class Solution
{
  public static void PartOne()
  {
    string[] input = File.ReadAllText("Day05/input.txt").Split("\n");

    Dictionary<long, long[]> ranges = input
      .Where(s => s.Contains("-"))
      .Select(c =>
      {
        var range = c.Split("-");
        return (long.Parse(range[0]), long.Parse(range[1]));
      })
      .GroupBy(c => c.Item1)
      .OrderBy(t => t.Key)
      .ToDictionary(
        c => c.Key,
        c => c.Select(v => v.Item2).ToArray()
       );

    int counter = input
      .Where(s => !s.Contains("-") && !string.IsNullOrWhiteSpace(s))
      .Select(long.Parse)
      .Count(number => ranges.FirstOrDefault(kvp => number >= kvp.Key && number <= kvp.Value.Max()).Value != null);

    Console.WriteLine($"There are {counter} fresh ingredient IDs");
  }

  public static void PartTwo()
  {
    string[] input = File.ReadAllText("Day05/input.txt").Split("\n");

    IEnumerable<(long, long)> ranges = input
      .Where(l => l.Contains("-"))
      .Select(l =>
      {
        var range = l.Split("-");
        return (long.Parse(range[0]), long.Parse(range[1]));
      })
      .GroupBy(r => r.Item1)
      .Select(r => (r.Key, r.Max(v => v.Item2)));

    foreach (var range in ranges)
    {
      long start = range.Item1;
      long end = range.Item2;

      var overlappingRanges = ranges.Where(r => r.Item2 >= start && r.Item1 <= end);

      foreach (var overlap in overlappingRanges)
      {
        start = Math.Min(start, overlap.Item1);
        end = Math.Max(end, overlap.Item2);
        ranges = ranges.Except([overlap]);
      }
      ranges = ranges.Except([range]).Append((start, end));
    }

    var counter = ranges.Sum(r => r.Item2 - r.Item1 + 1);

    Console.WriteLine($"There are {counter} fresh ingredient IDs");
  }
}
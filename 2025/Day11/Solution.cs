using System.Runtime.CompilerServices;

namespace AdventOfCode.Year2025.Day11;
public class Solution
{
  public static void PartOne()
  {
    string[] input = File.ReadAllLines("2025/Day11/input.txt");
    Dictionary<string, string[]> map = ParseInput(input);
    long pathsCount = PathCount(map, "you", "out", new Dictionary<string, long>());
    Console.WriteLine($"Number of different paths: {pathsCount}");
  }


  public static void PartTwo()
  {
    string[] input = File.ReadAllLines("2025/Day11/input.txt");
    var map = ParseInput(input);

    long pathsCount =
      PathCount(map, "svr", "dac", new Dictionary<string, long>()) *
      PathCount(map, "dac", "fft", new Dictionary<string, long>()) *
      PathCount(map, "fft", "out", new Dictionary<string, long>()) 
      +
      PathCount(map, "svr", "fft", new Dictionary<string, long>()) *
      PathCount(map, "fft", "dac", new Dictionary<string, long>()) *
      PathCount(map, "dac", "out", new Dictionary<string, long>());


    Console.WriteLine($"Number of paths visiting both dac and fft: {pathsCount}");
  }

  private static long PathCount(Dictionary<string, string[]> map, string from, string to, Dictionary<string, long> cache)
  {
    if (!cache.ContainsKey(from))
    {
      if (from == to)
      {
        cache[from] = 1;
      }

      else
      {
        long res = 0;
        foreach (var next in map.GetValueOrDefault(from) ?? [])
        {
          res += PathCount(map, next, to, cache);
        }
        cache[from] = res;
      }
    }

    return cache[from];
  }

  private static Dictionary<string, string[]> ParseInput(string[] lines)
  {
    var map = new Dictionary<string, string[]>();
    foreach (var line in lines)
    {
      var parts = line.Split(":");
      var from = parts[0];
      var to = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
      map[from] = to;
    }

    return map;
  }
}
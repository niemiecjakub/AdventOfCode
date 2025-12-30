using System.Text.RegularExpressions;

namespace AdventOfCode.Year2025.Day10;

public class Solution
{
  public static void PartOne()
  {
    var lines = File.ReadAllLines("2025/Day10/input.txt");
    long totalMinPresses = 0;

    foreach (var line in lines)
    {
      var diagram = ParseLine(line);
      var singlePresses = GetSinglePresses(diagram);

      int minForThisProblem = int.MaxValue;

      foreach (var press in singlePresses)
      {
        bool matches = true;
        for (int i = 0; i < diagram.Target.Length; i++)
        {
          if (diagram.Target[i] != (press.JoltageChange[i] % 2))
          {
            matches = false;
            break;
          }
        }

        if (matches && press.ButtonCount < minForThisProblem)
        {
          minForThisProblem = press.ButtonCount;
        }
      }

      if (minForThisProblem != int.MaxValue)
      {
        totalMinPresses += minForThisProblem;
      }
    }

    Console.WriteLine($"Part One Result: {totalMinPresses}");
  }

  public static void PartTwo()
  {
    var lines = File.ReadAllLines("2025/Day10/input.txt");
    long totalResult = 0;

    foreach (var line in lines)
    {
      var diagram = ParseLine(line);
      var singlePresses = GetSinglePresses(diagram);
      var cache = new Dictionary<string, int>();

      totalResult += SolveRecursive(diagram.Joltage, singlePresses, cache);
    }

    Console.WriteLine($"Part Two Result: {totalResult}");
  }

  private static int SolveRecursive(int[] currentJoltages, List<SinglePress> singlePresses, Dictionary<string, int> cache)
  {
    bool allZero = true;
    foreach (var j in currentJoltages)
    {
      if (j != 0)
      {
        allZero = false;
        break;
      }
    }

    if (allZero) return 0;

    string key = string.Join("-", currentJoltages);
    if (cache.TryGetValue(key, out int cachedValue))
    {
      return cachedValue;
    }

    int best = 10_000_000;

    foreach (var press in singlePresses)
    {
      bool canDivide = true;
      for (int i = 0; i < currentJoltages.Length; i++)
      {
        int diff = currentJoltages[i] - press.JoltageChange[i];
        if (diff < 0 || diff % 2 != 0)
        {
          canDivide = false;
          break;
        }
      }

      if (canDivide)
      {
        int[] nextJoltages = new int[currentJoltages.Length];
        for (int i = 0; i < currentJoltages.Length; i++)
        {
          nextJoltages[i] = (currentJoltages[i] - press.JoltageChange[i]) / 2;
        }

        int result = press.ButtonCount + 2 * SolveRecursive(nextJoltages, singlePresses, cache);
        if (result < best)
        {
          best = result;
        }
      }
    }

    cache[key] = best;
    return best;
  }

  private static List<SinglePress> GetSinglePresses(Diagram diagram)
  {
    var list = new List<SinglePress>();
    int combinations = 1 << diagram.Buttons.Length;

    for (int mask = 0; mask < combinations; mask++)
    {
      int[] change = new int[diagram.Joltage.Length];
      int count = 0;

      for (int b = 0; b < diagram.Buttons.Length; b++)
      {
        if (((mask >> b) & 1) == 1)
        {
          count++;
          int buttonVal = diagram.Buttons[b];
          for (int j = 0; j < diagram.Joltage.Length; j++)
          {
            if (((buttonVal >> j) & 1) == 1)
            {
              change[j]++;
            }
          }
        }
      }
      list.Add(new SinglePress(count, change));
    }
    return list;
  }

  private static Diagram ParseLine(string line)
  {
    var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    string targetStr = parts[0].Trim('[', ']');
    int[] target = new int[targetStr.Length];
    for (int i = 0; i < targetStr.Length; i++)
    {
      target[i] = targetStr[i] == '#' ? 1 : 0;
    }

    var buttonList = new List<int>();
    for (int i = 1; i < parts.Length - 1; i++)
    {
      var matches = Regex.Matches(parts[i], @"\d+");
      int mask = 0;
      foreach (Match m in matches)
      {
        mask |= (1 << int.Parse(m.Value));
      }
      buttonList.Add(mask);
    }

    var joltageMatches = Regex.Matches(parts[^1], @"\d+");
    int[] joltage = new int[joltageMatches.Count];
    for (int i = 0; i < joltageMatches.Count; i++)
    {
      joltage[i] = int.Parse(joltageMatches[i].Value);
    }

    return new Diagram(target, buttonList.ToArray(), joltage);
  }

  private record SinglePress(int ButtonCount, int[] JoltageChange);
  private record Diagram(int[] Target, int[] Buttons, int[] Joltage);

}
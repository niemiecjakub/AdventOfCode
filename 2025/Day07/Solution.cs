namespace AdventOfCode.Year2025.Day07;

public class Solution
{
  public static void PartOne()
  {
    var lines = File.ReadAllLines("2025/Day07/Input.txt")
                    .Select(line => line.ToCharArray())
                    .ToArray();

    var beamLocationIndexes = new HashSet<int>
    {
      Array.FindIndex(lines[0], c => c == 'S')
    };

    var newLocations = new HashSet<int>();
    var locationsToBeRemoved = new HashSet<int>();
    int beamSplitCounter = 0;

    for (int i = 1; i < lines.Length; i++)
    {
      newLocations.Clear();
      locationsToBeRemoved.Clear();
      foreach (var beamIndex in beamLocationIndexes)
      {
        if (lines[i][beamIndex] == '^')
        {
          locationsToBeRemoved.Add(beamIndex);
          beamSplitCounter++;

          newLocations.Add(beamIndex - 1);
          newLocations.Add(beamIndex + 1);
        }
      }

      beamLocationIndexes.RemoveWhere(locationsToBeRemoved.Contains);
      beamLocationIndexes.UnionWith(newLocations);
    }

    Console.WriteLine($"Total Splits: {beamSplitCounter}");
  }

  public static void PartTwo()
  {
    var lines = File.ReadAllLines("2025/Day07/Input.txt")
                        .Select(line => line.ToCharArray())
                        .ToArray();

    var beamLocations = new Dictionary<int, long>
    {
      [Array.FindIndex(lines[0], c => c == 'S')] = 1
    };

    for (int row = 1; row < lines.Length; row++)
    {
      var nextLocations = new Dictionary<int, long>();

      foreach (var kv in beamLocations)
      {
        int col = kv.Key;
        long count = kv.Value;

        if (lines[row][col] == '^')
        {
          AddTimelines(nextLocations, col - 1, count);
          AddTimelines(nextLocations, col + 1, count);
          continue;
        }

        AddTimelines(nextLocations, col, count);
      }

      beamLocations = nextLocations;
    }

    long totalTimelines = beamLocations.Values.Sum();
    Console.WriteLine($"Total timelines: {totalTimelines}");
  }

  private static void AddTimelines(Dictionary<int, long> locationMap, int column, long timelines)
  {
    if (locationMap.ContainsKey(column))
    {
      locationMap[column] += timelines;
      return;
    }
    locationMap[column] = timelines;
  }
}
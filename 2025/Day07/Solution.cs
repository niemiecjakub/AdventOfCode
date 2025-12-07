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

    int beamSplitCounter = 0;

    for (int i = 1; i < lines.Length; i++)
    {
      var newLocations = new HashSet<int>();
      var locationsToBeRemoved = new HashSet<int>();

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

  }
}
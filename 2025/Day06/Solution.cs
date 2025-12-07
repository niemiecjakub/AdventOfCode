namespace AdventOfCode.Year2025.Day06;

public class Solution
{
  public static void PartOne()
  {
    string[] input = File.ReadAllLines("2025/Day06/input.txt");
    var operators = input[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

    //column - math result
    Dictionary<int, long> map = new();
    for (int i = 0; i < input.Length - 1; i++)
    {
      var lineArgs = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
      for (int j = 0; j < lineArgs.Count; j++)
      {
        var @operator = operators[j];
        long value = long.Parse(lineArgs[j]);

        if (map.ContainsKey(j))
        {
          switch (@operator)
          {
            case "+":
              map[j] += value;
              break;
            case "*":
              map[j] *= value;
              break;
          }
        }
        else
        {
          map[j] = value;
        }
      }
    }

    long total = map.Values.Sum();

    Console.WriteLine($"Total: {total}");
  }

  public static void PartTwo()
  {
    var lines = File.ReadAllLines("Day06/Input.txt");
    string[] operators = lines[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    string[] rows = lines[..^1];

    int columnCount = rows[0].Length;
    long total = 0;
    int groupIndex = 0;

    List<string> currentGroup = new();

    for (int i = 0; i < columnCount; i++)
    {
      string column = string.Concat(rows.Select(r => r[i]));

      if (string.IsNullOrWhiteSpace(column))
      {
        total += CalculateGroupResult(currentGroup, operators[groupIndex++]);
        currentGroup.Clear();
      }
      else
      {
        currentGroup.Add(column);
      }
    }

    total += CalculateGroupResult(currentGroup, operators[groupIndex]);

    Console.WriteLine($"Total: {total}");
  }

  private static long CalculateGroupResult(List<string> values, string @operator)
  {
    var nums = values.Select(long.Parse);

    return @operator switch
    {
      "+" => nums.Sum(),
      "*" => nums.Aggregate(1, (long a, long b) => a * b),
      _ => throw new Exception("Unexcepted operator")
    };
  }
}
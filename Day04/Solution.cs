using System.Text;

namespace AdventOfCode.Day04;

public class Solution
{
  public static void PartOne()
  {
    var input = File.ReadAllLines("Day04/input.txt");
    var lines = input.Select(c => c.ToCharArray()).ToArray();

    var offsets = new (int, int)[]
    {
      (-1, -1),
      (-1, 0),
      (-1, 1),
      (0, -1),
      (0, 1),
      (1, -1),
      (1, 0),
      (1, 1)
    };

    int counter = 0;
    var rowCount = lines.Length;
    for (int i = 0; i < rowCount; i++)
    {
      char[] currentRow = lines[i];
      var columnCount = currentRow.Length;
      for (int j = 0; j < columnCount; j++)
      {
        char currentElement = currentRow[j];

        if (currentElement != '@')
        {
          continue;
        }

        var adjacentRollsCounter = 0;
        foreach (var (rowOffset, colOffset) in offsets)
        {
          var rowNumber = i + rowOffset;
          var columnNumber = j + colOffset;
          if (rowNumber < 0 || rowNumber >= rowCount || columnNumber < 0 || columnNumber >= columnCount)
          {
            continue;
          }

          if (lines[rowNumber][columnNumber] == '@')
          {
            adjacentRollsCounter++;
          }

          if (adjacentRollsCounter >= 4)
          {
            break;
          }
        }

        if (adjacentRollsCounter < 4)
        {
          counter++;
        }
      }
    }

    Console.WriteLine($"{counter} rolls of paper that can be accessed");
  }

  public static void PartTwo()
  {
    var input = File.ReadAllLines("Day04/input.txt");
    var lines = input.Select(c => c.ToCharArray()).ToArray();

    var offsets = new (int, int)[]
    {
      (-1, -1),
      (-1, 0),
      (-1, 1),
      (0, -1),
      (0, 1),
      (1, -1),
      (1, 0),
      (1, 1)
    };

    int counter = 0;
    var rowCount = lines.Length;

    var fieldsToBeRemoved = new List<(int, int)>();
    while (true)
    {
      for (int i = 0; i < rowCount; i++)
      {
        char[] currentRow = lines[i];
        var columnCount = currentRow.Length;
        for (int j = 0; j < columnCount; j++)
        {
          char currentElement = currentRow[j];

          if (currentElement != '@')
          {
            continue;
          }

          var adjacentRollsCounter = 0;
          foreach (var (rowOffset, colOffset) in offsets)
          {
            var rowNumber = i + rowOffset;
            var columnNumber = j + colOffset;
            if (rowNumber < 0 || rowNumber >= rowCount || columnNumber < 0 || columnNumber >= columnCount)
            {
              continue;
            }

            if (lines[rowNumber][columnNumber] == '@')
            {
              adjacentRollsCounter++;
            }

            if (adjacentRollsCounter >= 4)
            {
              break;
            }
          }

          if (adjacentRollsCounter < 4)
          {
            counter++;
            fieldsToBeRemoved.Add((i, j));
          }
        }
      }

      if (fieldsToBeRemoved.Count == 0)
      {
        break;
      }

      foreach (var (i, j) in fieldsToBeRemoved)
      {
        lines[i][j] = '.';
      }

      fieldsToBeRemoved.Clear();
    }

    Console.WriteLine($"{counter} rolls of paper that can be removed.");
  }
}

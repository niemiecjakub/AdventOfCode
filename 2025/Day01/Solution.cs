namespace AdventOfCode.Year2025.Day01;

public class Solution
{
  public static void PartOne()
  {
    int position = 50;
    int count = 0;

    string[] lines = File.ReadAllLines("2025/Day01/input.txt");
    foreach (string line in lines)
    {
      int value = int.Parse(line[1..]);

      if (line.StartsWith("R"))
      {
        position += value;
      }
      else
      {
        position -= value;
      }

      position %= 100;

      if (position == 0)
      {
        count++;
      }
    }

    Console.WriteLine($"Number of times at position 0: {count}");
  }

  public static void PartTwo()
  {
    int position = 50;
    int count = 0;

    string[] lines = File.ReadAllLines("Day01/input.txt");
    foreach (string line in lines)
    {
      var direction = line[0];
      int value = int.Parse(line[1..]);

      for (int i = 0; i < value; i++)
      {
        if (direction == 'R')
        {
          position++;
        }
        else
        {
          position--;
        }

        if (position == -1)
        {
          position = 99;
        }

        if (position == 100)
        {
          position = 0;
        }

        if (position == 0)
        {
          count++;
        }
      }
    }

    Console.WriteLine($"Number of times at position 0: {count}");
  }
}


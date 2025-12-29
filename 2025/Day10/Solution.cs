using System.Drawing;

namespace AdventOfCode.Year2025.Day10;

public class Solution
{
  public static void PartOne()
  {
    //var coordinates = File.ReadAllLines("2025/Day10/input.txt")
    //  .Select(l =>
    //  {
    //    var coords = l.Split(',');
    //    return new Point(int.Parse(coords[0]), int.Parse(coords[1]));
    //  })
    //  .ToList();

    var diagram = new Diagram(10);
    diagram.Print();
    diagram.Press(new int[] { 0, 2, 4, 6, 8 });
    diagram.Print();
  }

  public static void PartTwo()
  {

  }

  protected class Diagram
  {
    public List<Light> Lights { get; set; } = new List<Light>();
    public Diagram(int numberOfLights)
    {
      Lights = Enumerable.Range(0, numberOfLights).Select(position => new Light(position)).ToList();
    }
    public void Press(int[] lightPositions)
    {
      foreach (var light in Lights.Where(l => lightPositions.Contains(l.Position)))
      {
        light.Toggle();
      }
    }

    public void Print()
    {
      Console.Write("[");
      foreach (var light in Lights.OrderBy(l => l.Position))
      {
        Console.Write(light.ToString());
      }
      Console.Write("]");
      Console.WriteLine();
    }
  }

  protected class Light(int Position)
  {
    public bool IsOn { get; private set; } = false;
    public int Position { get; } = Position;
    public void Toggle() => IsOn = !IsOn;
    override public string ToString() => IsOn ? "#" : ".";
  }
}
namespace AdventOfCode.Year2025.Day09;

public class Solution
{
  public static void PartOne()
  {
    var coordinates = File.ReadAllLines("2025/Day09/input.txt")
          .Select(l =>
          {
            var coords = l.Split(',');
            return new Point(int.Parse(coords[0]), int.Parse(coords[1]));
          })
          .ToList();
    long area = long.MinValue;
    for (int i = 0; i < coordinates.Count; i++)
    {
      for (int j = i + 1; j < coordinates.Count; j++)
      {
        var rectangle = new Rectangle(coordinates[i], coordinates[j]);
        if (rectangle.Area > area)
        {
          area = rectangle.Area;
        }
      }
    }

    Console.WriteLine($"Max area is: {area}");
  }

  public static void PartTwo()
  {
    var redTiles = File.ReadAllLines("2025/Day09/input.txt")
      .Select(l =>
      {
        var p = l.Split(',');
        return new Point(int.Parse(p[0]), int.Parse(p[1]));
      })
      .ToList();

    var greenTiles = new HashSet<Point>();
    for (int i = 0; i < redTiles.Count; i++)
    {
      var p1 = redTiles[i];
      var p2 = redTiles[(i + 1) % redTiles.Count];

      if (p1.X == p2.X)
      {
        int minY = Math.Min(p1.Y, p2.Y);
        int maxY = Math.Max(p1.Y, p2.Y);
        for (int y = minY + 1; y < maxY; y++)
          greenTiles.Add(new Point(p1.X, y));
      }
      else if (p1.Y == p2.Y)
      {
        int minX = Math.Min(p1.X, p2.X);
        int maxX = Math.Max(p1.X, p2.X);
        for (int x = minX + 1; x < maxX; x++)
          greenTiles.Add(new Point(x, p1.Y));
      }
    }

    var boundaryTiles = redTiles.Concat(greenTiles).ToList();
    var rowRanges = boundaryTiles
      .GroupBy(p => p.Y)
      .ToDictionary(
        g => g.Key,
        g => (Min: g.Min(p => p.X), Max: g.Max(p => p.X))
      );

    long area = 0;

    for (int i = 0; i < redTiles.Count; i++)
    {
      for (int j = i + 1; j < redTiles.Count; j++)
      {
        var r = new Rectangle(redTiles[i], redTiles[j]);

        if (r.Bounds.MinX == r.Bounds.MaxX || r.Bounds.MinY == r.Bounds.MaxY)
        {
          continue;
        }

        if (r.Area > area && IsRectangleInsidePolygon(r, rowRanges))
        {
          area = r.Area;
        }
      }
    }

    Console.WriteLine($"Max area is: {area}");
  }


  private static bool IsRectangleInsidePolygon(Rectangle r, Dictionary<int, (int Min, int Max)> rowRanges)
  {
    for (int y = r.Bounds.MinY; y <= r.Bounds.MaxY; y++)
    {
      if (!rowRanges.TryGetValue(y, out var span))
      {
        return false;
      }

      if (span.Min > r.Bounds.MinX)
      {
        return false;
      }

      if (span.Max < r.Bounds.MaxX)
        return false;
    }

    return true;
  }

  private readonly record struct Rectangle
  {
    public long Area { get; }
    public (int MinX, int MaxX, int MinY, int MaxY) Bounds { get; }
    public Rectangle(Point p1, Point p2)
    {
      int minX = Math.Min(p1.X, p2.X);
      int maxX = Math.Max(p1.X, p2.X);
      int minY = Math.Min(p1.Y, p2.Y);
      int maxY = Math.Max(p1.Y, p2.Y);

      long width = maxX - minX + 1;
      long height = maxY - minY + 1;
      Area = width * height;
      Bounds = (minX, maxX, minY, maxY);
    }
  }

  private record struct Point(int X, int Y);

}
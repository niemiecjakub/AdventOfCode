namespace AdventOfCode.Year2025.Day08;

public class Solution
{
  public static void PartOne()
  {
    var positions = File.ReadAllLines("2025/Day08/input.txt")
      .Select(l =>
      {
        var coords = l.Split(',');
        return new Position(int.Parse(coords[0]), int.Parse(coords[1]), int.Parse(coords[2]));
      })
      .ToList();

    var edges = new List<Edge>();
    for (int i = 0; i < positions.Count; i++)
    {
      for (int j = i + 1; j < positions.Count; j++)
      {
        var p1 = positions[i];
        var p2 = positions[j];
        edges.Add(new Edge(p1, p2));
      }
    }

    var edgesSorted = edges.OrderBy(e => e.Distance).ToList();
    var unionFind = new UnionFind(positions);

    for (int i = 0; i < 1000; i++)
    {
      var edge = edgesSorted[i];
      unionFind.Union(edge);
    }

    var circuits = positions
        .GroupBy(unionFind.Find)
        .Select(g => g.Count())
        .OrderByDescending(count => count)
        .ToList();

    var top3 = circuits.Take(3).ToList();
    long result = 1;
    foreach (var size in top3)
    {
      result *= size;
    }

    Console.WriteLine($"Result: {result}");
  }

  public static void PartTwo()
  {
    var positions = File.ReadAllLines("2025/Day08/input.txt")
      .Select(l =>
      {
        var coords = l.Split(',');
        return new Position(int.Parse(coords[0]), int.Parse(coords[1]), int.Parse(coords[2]));
      })
      .ToList();

    var edges = new List<Edge>();
    for (int i = 0; i < positions.Count; i++)
    {
      for (int j = i + 1; j < positions.Count; j++)
      {
        var p1 = positions[i];
        var p2 = positions[j];
        edges.Add(new Edge(p1, p2));
      }
    }

    var edgesSorted = edges.OrderBy(e => e.Distance).ToList();
    var unionFind = new UnionFind(positions);

    for (int i = 0; i < edgesSorted.Count; i++)
    {
      var edge = edgesSorted[i];
      unionFind.Union(edge);

      if (unionFind.UnionCount == positions.Count - 1)
      {
        long result = edge.P1.X * edge.P2.X;
        Console.WriteLine($"Result: {result}");
        break;
      }
    }
  }

  private record Edge
  {
    public Position P1;
    public Position P2;
    public double Distance;
    public Edge(Position p1, Position p2)
    {
      P1 = p1;
      P2 = p2;
      Distance = Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2) + Math.Pow(p1.Z - p2.Z, 2));
    }
  }

  private record Position(long X, long Y, long Z);

  private class UnionFind
  {
    private Dictionary<Position, Position> _parent = new();
    public int UnionCount { get; private set; } = 0;
    public UnionFind(List<Position> positions)
    {
      foreach (var p in positions)
      {
        _parent[p] = p;
      }
    }

    public Position Find(Position p)
    {
      if (_parent[p] != p)
      {
        _parent[p] = Find(_parent[p]);
      }
      return _parent[p];
    }

    public bool Union(Edge edge)
    {
      var root1 = Find(edge.P1);
      var root2 = Find(edge.P2);

      if (root1 == root2)
      {
        return false;
      }

      UnionCount++;
      _parent[root1] = root2;
      return true;
    }
  }
}
namespace AdventOfCode.Day02;

public class Solution
{
	public static void PartOne()
	{
		string fileText = File.ReadAllText("Day02/input.txt");
		IEnumerable<(long, long)> idRanges = fileText.Split(",").Select(x =>
		{
			var range = x.Split("-");
			var start = long.Parse(range[0]);
			var end = long.Parse(range[1]);

			return (start, end);
		});

		long invalidIdSum = 0;

		foreach ((long start, long end) in idRanges)
		{
			for (long i = start; i <= end; i++)
			{
				string x = i.ToString();
				if (x.Length % 2 != 0)
				{
					continue;
				}
				var part1 = x.Substring(0, x.Length / 2);
				var part2 = x.Substring(x.Length / 2);

				if (part1.Equals(part2))
				{
					invalidIdSum += i;
				}
			}
		}

		Console.WriteLine($"InvalidId sum: {invalidIdSum}");
	}

	public static void PartTwo()
	{
		string fileText = File.ReadAllText("Day02/input.txt");
		IEnumerable<(long, long)> idRanges = fileText.Split(",").Select(x =>
		{
			var range = x.Split("-");
			var start = long.Parse(range[0]);
			var end = long.Parse(range[1]);

			return (start, end);
		});

		long invalidIdSum = 0;

		foreach ((long start, long end) in idRanges)
		{
			for (long i = start; i <= end; i++)
			{
				string id = i.ToString();
				for (int chunkSize = 1; chunkSize < id.Length; chunkSize++)
				{
					var parts = id.Chunk(chunkSize).Select(c => new string(c));
					var pattern = parts.First();
					if (parts.All(p => p == pattern))
					{
						invalidIdSum += i;
						break;
					}
				}
			}
		}

		Console.WriteLine($"InvalidId sum: {invalidIdSum}");
	}
}

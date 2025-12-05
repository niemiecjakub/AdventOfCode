using System.Text;

namespace AdventOfCode.Day03;

public class Solution
{
	public static void PartOne()
	{
		string[] lines = File.ReadAllLines("Day03/input.txt");

		int totalJoltage = 0;
		foreach (string input in lines)
		{
			var nmumberOfTens = input.Substring(0, input.Length - 1).Select(c => int.Parse([c])).Max();
			var numberOfTensIndex = input.IndexOf(nmumberOfTens.ToString());

			var numberOfUnits = input.Substring(numberOfTensIndex + 1).Select(c => int.Parse([c])).Max();

			var joltage = nmumberOfTens * 10 + numberOfUnits;
			totalJoltage += joltage;
		}

		Console.WriteLine($"Total is: {totalJoltage}");
	}


	public static void PartTwo()
	{
		string[] lines = File.ReadAllLines("Day03/input.txt");

		long totalJoltage = 0;
		foreach (string input in lines)
		{
			StringBuilder joltage = new();
			var bank = input;
			for (int i = 12; i > 0; i--)
			{

				bank = bank.Select(c => c.ToString())
										.Distinct()
										.OrderByDescending(c => c)
										.Select(c =>
										{
											var index = bank.IndexOf(c);
											var substring = bank.Substring(index);
											if (substring.Length < i)
											{
												return "";
											}
											return substring;
										})
										.OrderByDescending(c => c)
										.First();

				joltage.Append(bank[0]);
				bank = bank[1..];
			}

			totalJoltage += long.Parse(joltage.ToString());
		}

		Console.WriteLine($"Total is: {totalJoltage}");
	}
}

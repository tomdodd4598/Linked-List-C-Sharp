using System;
using System.Numerics;
using System.Text.RegularExpressions;

namespace LinkedList
{
	static class EntryPoint
	{
		static readonly Regex ValidRegex = new("^(0|-?[1-9][0-9]*|[A-Za-z][0-9A-Z_a-z]*)$", RegexOptions.Compiled);

		static bool IsValidString(string str) => ValidRegex.IsMatch(str);

		static bool InsertBefore(string val, Item<string> item)
		{
			if (BigInteger.TryParse(val, out var x) && BigInteger.TryParse(item.value, out var y))
			{
				return x <= y;
			}
			else
			{
				return val.CompareTo(item.value) < 1;
			}
		}

		static bool ValueEquals(Item<string> item, string val) => item.value == val;

		static void Main()
		{
			Item<string>? start = null;

			var begin = true;

			while (true)
			{
				if (!begin)
				{
					Console.WriteLine();
				}
				else
				{
					begin = false;
				}

				Console.WriteLine("Awaiting input...");
				string input = Console.ReadLine()!;

				if (input.Length == 0)
				{
					Console.WriteLine("\nProgram terminated!");
					Helpers.RemoveAll(out start);
					return;
				}
				else if (input.StartsWith('~'))
				{
					if (input.Length == 1)
					{
						Console.WriteLine("\nDeleting list...");
						Helpers.RemoveAll(out start);
					}
					else
					{
						input = input[1..];
						if (IsValidString(input))
						{
							Console.WriteLine("\nRemoving item...");
							Helpers.RemoveItem(ref start, input, ValueEquals);
						}
						else
						{
							Console.WriteLine("\nCould not parse input!");
						}
					}
				}
				else if (input.Equals("l"))
				{
					Console.WriteLine("\nLoop print...");
					Helpers.PrintLoop(start);
				}
				else if (input.Equals("i"))
				{
					Console.WriteLine("\nIterator print...");
					Helpers.PrintIterator(start);
				}
				else if (input.Equals("a"))
				{
					Console.WriteLine("\nArray print...");
					Helpers.PrintArray(start);
				}
				else if (input.Equals("r"))
				{
					Console.WriteLine("\nRecursive print...");
					Helpers.PrintRecursive(start);
				}
				else if (input.Equals("f"))
				{
					Console.WriteLine("\nFold print...");
					Helpers.PrintFold(start);
				}
				else if (input.Equals("b"))
				{
					Console.WriteLine("\nFoldback print...");
					Helpers.PrintFoldback(start);
				}
				else if (IsValidString(input))
				{
					Console.WriteLine("\nInserting item...");
					Helpers.InsertItem(ref start, input, InsertBefore);
				}
				else
				{
					Console.WriteLine("\nCould not parse input!");
				}
			}
		}
	}
}

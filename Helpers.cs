﻿using System;

namespace LinkedList
{
	public static class Helpers
	{
		public static void InsertItem<T>(ref Item<T> start, T val, Func<T, Item<T>, bool> insertBefore)
		{
			Console.WriteLine($"Creating item: {val}");
			Item<T> current = start, previous = null;

			while (current != null && !insertBefore(val, current))
			{
				previous = current;
				current = current.next;
			}
			var item = new Item<T>(val, current);

			if (previous == null)
			{
				start = item;
			}
			else
			{
				previous.next = item;
			}
		}

		public static void RemoveItem<T>(ref Item<T> start, T val, Func<Item<T>, T, bool> valueEquals)
		{
			Item<T> current = start, previous = null;

			while (current != null && !valueEquals(current, val))
			{
				previous = current;
				current = current.next;
			}

			if (current == null)
			{
				Console.WriteLine($"Item {val} does not exist!");
			}
			else
			{
				if (previous == null)
				{
					start = current.next;
				}
				else
				{
					previous.next = current.next;
				}
				Console.WriteLine($"Removed item: {val}");
			}
		}

		public static void RemoveAll<T>(out Item<T> start) => start = null;

		public static void PrintLoop<T>(Item<T> start)
		{
			while (start != null)
			{
				start = start.PrintGetNext();
			}
		}

		public static void PrintIterator<T>(Item<T> start)
		{
			if (start != null)
			{
				foreach (var item in start.GetIterator())
				{
					item.PrintGetNext();
				}
			}
		}

		public static void PrintArray<T>(Item<T> start)
		{
			var item = start;
			for (int i = 0; item != null; ++i)
			{
				item = start[i].PrintGetNext();
			}
		}

		public static void PrintRecursive<T>(Item<T> start)
		{
			if (start != null)
			{
				PrintRecursive(start.PrintGetNext());
			}
		}

		public static void PrintFold<T>(Item<T> start)
		{
			Func<Item<T>, Item<T>, string, string> fSome = (current, next, accumulator) => $"{accumulator}{current.value}, ";
			Func<Item<T>, string, string> fLast = (current, accumulator) => $"{accumulator}{current.value}\n";
			Func<string, string> fEmpty = accumulator => accumulator;
			Console.Write(Item<T>.Fold(fSome, fLast, fEmpty, "", start));
		}

		public static void PrintFoldback<T>(Item<T> start)
		{
			Func<Item<T>, Item<T>, string, string> fSome = (current, next, innerVal) => $"{current.value}, {innerVal}";
			Func<Item<T>, string> fLast = current => $"{current.value}\n";
			Func<string> fEmpty = () => "";
			Console.Write(Item<T>.Foldback(fSome, fLast, fEmpty, x => x, start));
		}
	}
}

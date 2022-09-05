using System;
using System.Collections.Generic;

namespace LinkedList
{
	public class Item<T>
	{
		public readonly T value;
		public Item<T> next;

		public Item(T value, Item<T> next)
		{
			this.value = value;
			this.next = next;
		}

		~Item() => Console.WriteLine($"Garbage collecting item: {value}");

		public Item<T> this[int n]
		{
			get
			{
				var item = this;
				for (int i = 0; i < n; i++)
				{
					item = item?.next;
				}
				return item;
			}
		}

		public Item<T> PrintGetNext()
		{
			Console.Write(value);
			Console.Write(next == null ? "\n" : ", ");
			return next;
		}

		public IEnumerable<Item<T>> GetIterator()
		{
			var item = this;
			while (item != null)
			{
				yield return item;
				item = item.next;
			}
		}

		public static R Fold<A, R>(Func<Item<T>, Item<T>, A, A> fSome, Func<Item<T>, A, R> fLast, Func<A, R> fEmpty, A accumulator, Item<T> item)
		{
			if (item != null)
			{
				Item<T> next = item.next;
				if (next != null)
				{
					return Fold(fSome, fLast, fEmpty, fSome(item, next, accumulator), next);
				}
				else
				{
					return fLast(item, accumulator);
				}
			}
			else
			{
				return fEmpty(accumulator);
			}
		}

		public static R Foldback<A, R>(Func<Item<T>, Item<T>, A, A> fSome, Func<Item<T>, A> fLast, Func<A> fEmpty, Func<A, R> generator, Item<T> item)
		{
			if (item != null)
			{
				Item<T> next = item.next;
				if (next != null)
				{
					return Foldback(fSome, fLast, fEmpty, innerVal => generator(fSome(item, next, innerVal)), next);
				}
				else
				{
					return generator(fLast(item));
				}
			}
			else
			{
				return generator(fEmpty());
			}
		}
	}
}

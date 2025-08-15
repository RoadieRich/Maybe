using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadieRich.Maybe
{
	public static class Extensions
	{
		public static Maybe<T> AsSome<T>(this T value)
		{
			return new Maybe<T>.Some(value);
		}
		public static Maybe<T> AsNone<T>(this T _)
		{
			return new Maybe<T>.None();
		}

		public static Maybe<TValue> TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
		{
			ArgumentNullException.ThrowIfNull(dict);
			if (dict.TryGetValue(key, out var value))
			{
				return new Maybe<TValue>.Some(value);
			}
			else
			{
				return new Maybe<TValue>.None();
			}
		}

		public static Maybe<T> ItemAt<T>(this IList<T> list, int index)
		{
			if (list.Count > index && index >= 0)
			{
				return new Maybe<T>.Some(list[index]);
			}
			else
			{
				return new Maybe<T>.None();
			}
		}

		public static Maybe<T> ItemAt<T>(this T[] array, int index)
		{
			if (array.Length > index && index >= 0)
			{
				return new Maybe<T>.Some(array[index]);
			}
			else
			{
				return new Maybe<T>.None();
			}
		}

		public static Maybe<T> ItemAt<T>(this IReadOnlyList<T> list, int index)
		{
			if (list.Count > index && index >= 0)
			{
				return new Maybe<T>.Some(list[index]);
			}
			else
			{
				return new Maybe<T>.None();
			}
		}

		public static Maybe<IEnumerable<T>> MaybeAny<T>(this IEnumerable<T> collection)
		{
			if (System.Linq.Enumerable.Any(collection))
			{
				return new Maybe<IEnumerable<T>>.Some(collection);
			}
			else
			{
				return new Maybe<IEnumerable<T>>.None();
			}
		}

		public static Maybe<IEnumerable<T>> MaybeAny<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
		{
			if (System.Linq.Enumerable.Any(collection, predicate))
			{
				return new Maybe<IEnumerable<T>>.Some(collection);
			}
			else
			{
				return new Maybe<IEnumerable<T>>.None();
			}
		}

		public static Maybe<IEnumerable<T>> MaybeAll<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
		{
			if (System.Linq.Enumerable.All(collection, predicate))
			{
				return new Maybe<IEnumerable<T>>.Some(collection);
			}
			else
			{
				return new Maybe<IEnumerable<T>>.None();
			}
		}

		public static IEnumerable<Maybe<T>> AnyMaybe<T>(this IEnumerable<T> collection)
		{
			foreach (var item in collection)
			{
				yield return new Maybe<T>.Some(item);
			}
		}

		public static IEnumerable<Maybe<T>> Maybe<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
		{
			foreach (var item in collection)
			{
				if (predicate(item))
				{
					yield return new Maybe<T>.Some(item);
				}
				else
				{
					yield return new Maybe<T>.None();
				}
			}
		}

		public static IEnumerable<T> Flatten<T>(this IEnumerable<Maybe<T>> collection)
		{
			foreach (var item in collection)
			{
				if (item is Maybe<T>.Some some)
				{
					yield return some.Value;
				}
			}
		}
		}
}

namespace RoadieRich.Maybe;

public static class IEnumerableExtensions
{
	public static Maybe<T> FirstOrNone<T>(this IEnumerable<T> source) where T : IEquatable<T>
	{
		foreach (var item in source ?? [])
		{
			return new Maybe<T>(item);
		}
		return new Maybe<T>();
	}

	public static Maybe<T> FirstOrNone<T>(this IEnumerable<T> source, Func<T, bool> func) where T : IEquatable<T>
	{
		foreach (var item in source ?? [])
		{
			if (func(item))
				return new Maybe<T>(item);
		}
		return new Maybe<T>();
	}

	public static Maybe<T> SingleOrNone<T>(this IEnumerable<T> source) where T : IEquatable<T>
	{
		var count = 0;
		T? foundItem = default;
		foreach (var item in source ?? [])
		{

			count++;
			foundItem = item;
			if (count > 1)
				break;

		}
		if (count == 1) return new Maybe<T>(foundItem!);

		return new Maybe<T>();
	}

	public static Maybe<T> SingleOrNone<T>(this IEnumerable<T> source, Func<T, bool> func) where T : IEquatable<T>
	{
		var count = 0;
		T? foundItem = default;
		foreach (var item in source ?? [])
		{
			if (func(item))
			{
				count++;
				foundItem = item;
				if (count > 1)
					break;
			}
		}
		if (count == 1) return new Maybe<T>(foundItem!);
		return new Maybe<T>();
	}
	public static Maybe<T> LastOrNone<T>(this IEnumerable<T> source) where T : IEquatable<T>
	{
		T? foundItem = default;
		bool found = false;
		foreach (var item in source ?? [])
		{
			found = true;
			foundItem = item;
		}
		if (found) return new Maybe<T>(foundItem!);

		return new Maybe<T>();
	}


	public static Maybe<T> LastOrNone<T>(this IEnumerable<T> source, Func<T, bool> predicate) where T : IEquatable<T>
	{
		T? foundItem = default;
		bool found = false;
		foreach (var item in source ?? [])
		{
			if (predicate(item))
			{
				found = true;
				foundItem = item;
			}
		}
		if (found) return new Maybe<T>(foundItem!);

		return new Maybe<T>();
	}

	public static Maybe<T> ElementAtOrNone<T>(this IEnumerable<T> source, int index)
	{
		if (index < 0) return new Maybe<T>();
		int currentIndex = 0;
		foreach (var item in source ?? [])
		{
			if (currentIndex == index)
			{
				return new Maybe<T>(item);
			}
			currentIndex++;
		}
		return new Maybe<T>();
	}

	public static Maybe<IEnumerable<T>> NoneIfEmpty<T>(this IEnumerable<T> source)
	{
		if (source == null || !source.Any()) return new Maybe<IEnumerable<T>>();

		return new Maybe<IEnumerable<T>>(source);
	}

	public static Maybe<IEnumerable<T>> Somes<T>(this IEnumerable<Maybe<T>> source)
	{
		if (source == null || !source.Any()) return new Maybe<IEnumerable<T>>();
		return new Maybe<IEnumerable<T>>(Iter(source));

		static IEnumerable<T> Iter(IEnumerable<Maybe<T>> source)
		{
			foreach (var item in source)
			{
				if (item.HasValue)
				{
					yield return item.Value;
				}
			}
		}
	}

	public static Maybe<T> ItemAt<T>(this IList<T> list, int index)
	{
		if (list.Count > index && index >= 0)
		{
			return new Maybe<T>(list[index]);
		}
		else
		{
			return new Maybe<T>();
		}
	}

	public static Maybe<T> ItemAt<T>(this T[] array, int index)
	{
		if (array.Length > index && index >= 0)
		{
			return new Maybe<T>(array[index]);
		}
		else
		{
			return new Maybe<T>();
		}
	}

	public static Maybe<T> ItemAt<T>(this IReadOnlyList<T> list, int index)
	{
		if (list.Count > index && index >= 0)
		{
			return new Maybe<T>(list[index]);
		}
		else
		{
			return new Maybe<T>();
		}
	}

	public static Maybe<IEnumerable<T>> MaybeAny<T>(this IEnumerable<T> collection)
	{
		if (System.Linq.Enumerable.Any(collection))
		{
			return new Maybe<IEnumerable<T>>(collection);
		}
		else
		{
			return new Maybe<IEnumerable<T>>();
		}
	}

	public static Maybe<IEnumerable<T>> MaybeAny<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
	{
		if (System.Linq.Enumerable.Any(collection, predicate))
		{
			return new Maybe<IEnumerable<T>>(collection);
		}
		else
		{
			return new Maybe<IEnumerable<T>>();
		}
	}

	public static Maybe<IEnumerable<T>> MaybeAll<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
	{
		if (System.Linq.Enumerable.All(collection, predicate))
		{
			return new Maybe<IEnumerable<T>>(collection);
		}
		else
		{
			return new Maybe<IEnumerable<T>>();
		}
	}

	public static IEnumerable<Maybe<T>> AnyMaybe<T>(this IEnumerable<T> collection)
	{
		foreach (var item in collection)
		{
			yield return new Maybe<T>(item);
		}
	}

	public static IEnumerable<Maybe<T>> AnyMaybe<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
	{
		foreach (var item in collection)
		{
			if (predicate(item))
			{
				yield return new Maybe<T>(item);
			}
			else
			{
				yield return new Maybe<T>();
			}
		}
	}

	public static IEnumerable<Maybe<T>> Maybe<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
	{
		foreach (var item in collection)
		{
			if (predicate(item))
			{
				yield return new Maybe<T>(item);
			}
			else
			{
				yield return new Maybe<T>();
			}
		}
	}

	public static IEnumerable<Maybe<T>> Maybe<T>(this IEnumerable<T> collection, Func<T, Maybe<T>> predicate)
	{
		foreach (var item in collection)
		{
			yield return predicate(item);
		}
	}

	public static IEnumerable<T> Flatten<T>(this IEnumerable<Maybe<T>> collection)
	{
		foreach (var item in collection)
		{
			if (item.HasValue)
			{
				yield return item.Value;
			}
		}
	}

	public static int CountSome<T>(this IEnumerable<Maybe<T>> source)
	{
		if (source is null) return 0;
		int count = 0;
		foreach (var item in source)
		{
			if (item.HasValue)
			{
				count++;
			}
		}
		return count;
	}

	public static Maybe<T> FirstOrNone<T>(this IEnumerable<T> source, Func<T, Maybe<T>> predicate)
	{
		if (source is null) return new Maybe<T>();
		foreach (var item in source)
		{
			if (predicate(item) is Maybe<T> some )
			{
				return some;
			}
		}
		return new Maybe<T>();
	}
}
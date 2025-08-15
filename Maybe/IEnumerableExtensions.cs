namespace RoadieRich.Maybe;

public static class IEnumerableExtensions
{
	public static Maybe<T> FirstOrNone<T>(this IEnumerable<T> source) where T : IEquatable<T>
	{
		foreach (var item in source ?? [])
		{
			return Maybe.Some(item);
		}
		return Maybe.None;
	}

	public static Maybe<T> FirstOrNone<T>(this IEnumerable<T> source, Func<T, bool> func) where T : IEquatable<T>
	{
		foreach (var item in source ?? [])
		{
			if (func(item))
				return Maybe.Some(item);
		}
		return Maybe.None;
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
		if (count == 1) return Maybe.Some(foundItem!);
		return Maybe.None;
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
		if (count == 1) return Maybe.Some(foundItem!);
		return Maybe.None;
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
		if (found) return Maybe.Some(foundItem!);

		return Maybe.None;
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
		if (found) return Maybe.Some(foundItem!);

		return Maybe.None;
	}

	public static Maybe<T> ElementAtOrNone<T>(this IEnumerable<T> source, int index)
	{
		if (index < 0) return Maybe.None;
		int currentIndex = 0;
		foreach (var item in source ?? [])
		{
			if (currentIndex == index)
			{
				return Maybe.Some(item);
			}
			currentIndex++;
		}
		return Maybe.None;
	}

	public static Maybe<IEnumerable<T>> NoneIfEmpty<T>(this IEnumerable<T> source)
	{
		if (source == null || !source.Any()) return Maybe.None;

		return Maybe.Some(source);
	}

	public static Maybe<IEnumerable<T>> Somes<T>(this IEnumerable<Maybe<T>> source)
	{
		if (source == null || !source.Any()) return Maybe.None;
		return Maybe.Some(Iter(source));

		static IEnumerable<T> Iter(IEnumerable<Maybe<T>> source)
		{
			foreach (var item in source)
			{
				if (item is Maybe<T>.Some some)
				{
					yield return some.Value;
				}
			}
		}
	}
}
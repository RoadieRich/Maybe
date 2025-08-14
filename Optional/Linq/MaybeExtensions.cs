namespace RoadieRich.Maybe.Linq;

public static class MaybeExtensions
{
	public static Maybe<TResult> Select<TSource, TResult>(this Maybe<TSource> maybe, Func<TSource, TResult> selector)
	{
		if (maybe is Maybe<TSource>.Some some)
		{
			return new Maybe<TResult>.Some(selector(some.Value));
		}
		return new Maybe<TResult>.None();
	}

	public static Maybe<TResult> SelectMany<TSource, T2, TResult>(this Maybe<TSource> maybe, Func<TSource, Maybe<T2>> convert, Func<TSource, T2, TResult> selector)
	{
		if (maybe is Maybe<TSource>.Some some)
		{
			var inner = convert(some.Value);
			if (inner is Maybe<T2>.Some innerSome)
			{
				return new Maybe<TResult>.Some(selector(some.Value, innerSome.Value));
			}
		}
		return new Maybe<TResult>.None();
	}

	public static Maybe<TSource> Where<TSource>(this Maybe<TSource> maybe, Func<TSource, bool> predicate)
	{
		if (maybe is Maybe<TSource>.Some some && predicate(some.Value))
		{
			return maybe;
		}
		return new Maybe<TSource>.None();
	}
}

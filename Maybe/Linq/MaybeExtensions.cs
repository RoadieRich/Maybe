using System.Runtime;
using System.Runtime.CompilerServices;

namespace RoadieRich.Maybe.Linq;

public static class MaybeExtensions
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TSource"></typeparam>
	/// <typeparam name="TResult"></typeparam>
	/// <param name="maybe"></param>
	/// <param name="selector"></param>
	/// <returns></returns>
	public static Maybe<TResult> Select<TSource, TResult>(this Maybe<TSource> maybe, Func<TSource, TResult> selector)
	{
		if (maybe is Maybe<TSource>.Some some)
		{
			return new Maybe<TResult>.Some(selector(some.Value));
		}
		return new Maybe<TResult>.None();
	}

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TSource"></typeparam>
	/// <typeparam name="T2"></typeparam>
	/// <typeparam name="TResult"></typeparam>
	/// <param name="maybe"></param>
	/// <param name="convert"></param>
	/// <param name="selector"></param>
	/// <returns></returns>
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

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TSource"></typeparam>
	/// <param name="maybe"></param>
	/// <param name="predicate"></param>
	/// <returns></returns>
	public static Maybe<TSource> Where<TSource>(this Maybe<TSource> maybe, Func<TSource, bool> predicate)
	{
		if (maybe is Maybe<TSource>.Some some && predicate(some.Value))
		{
			return maybe;
		}
		return new Maybe<TSource>.None();
	}

	public static bool Contains<T>(this Maybe<T> maybe, T value)
	{
		if (maybe is Maybe<T>.Some some)
		{
			return EqualityComparer<T>.Default.Equals(some.Value, value);
		}
		return false;
	}
}

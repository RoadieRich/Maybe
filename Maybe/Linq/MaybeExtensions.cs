using System.Diagnostics;
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
	[DebuggerNonUserCode]
	public static Maybe<TResult> Select<TSource, TResult>(this Maybe<TSource> maybe, Func<TSource, TResult> selector)
	{
		if (maybe.HasValue)
		{
			return new Maybe<TResult>(selector(maybe.Value));
		}
		return new Maybe<TResult>();
	}

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TSource"></typeparam>
	/// <typeparam name="TResult"></typeparam>
	/// <param name="maybe"></param>
	/// <param name="selector"></param>
	/// <returns></returns>
	[DebuggerNonUserCode]
	public static Maybe<TResult> Select<TSource, TResult>(this Maybe<TSource> maybe, Func<TSource, Maybe<TResult>> selector)
	{
		if (maybe.HasValue)
		{
			return selector(maybe.Value);
		}
		return new Maybe<TResult>();
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
	[DebuggerNonUserCode]
	public static Maybe<TResult> SelectMany<TSource, T2, TResult>(this Maybe<TSource> maybe, Func<TSource, Maybe<T2>> convert, 
		Func<TSource, T2, TResult> selector)
	{
		if (maybe.HasValue)
		{
			var inner = convert(maybe.Value);
			if (inner.HasValue)
			{
				return new Maybe<TResult>(selector(maybe.Value, inner.Value));
			}
		}
		return new Maybe<TResult>();
	}

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TSource"></typeparam>
	/// <param name="maybe"></param>
	/// <param name="predicate"></param>
	/// <returns></returns>
	[DebuggerNonUserCode]
	public static Maybe<TSource> Where<TSource>(this Maybe<TSource> maybe, Func<TSource, bool> predicate)
	{
		if (maybe.HasValue && predicate(maybe.Value))
		{
			return maybe;
		}
		return new Maybe<TSource>();
	}

	[DebuggerNonUserCode]
	public static bool Contains<T>(this Maybe<T> maybe, T value)
	{
		if (maybe.HasValue)
		{
			return EqualityComparer<T>.Default.Equals(maybe.Value, value);
		}
		return false;
	}
}

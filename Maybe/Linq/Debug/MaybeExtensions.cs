using System.Runtime.CompilerServices;
using _Debug = System.Diagnostics.Debug;

namespace RoadieRich.Maybe.Linq.Debug;

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
	public static Maybe<TResult> Select<TSource, TResult>(this Maybe<TSource> maybe, Func<TSource, TResult> selector, [CallerArgumentExpression(nameof(selector))] string selectorExpression = "")
	{
		_Debug.WriteLine($"Maybe<{typeof(TSource).Name}>.Select({selectorExpression})");
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
	/// <typeparam name="TResult"></typeparam>
	/// <param name="maybe"></param>
	/// <param name="selector"></param>
	/// <returns></returns>
	public static Maybe<TResult> Select<TSource, TResult>(this Maybe<TSource> maybe, Func<TSource, Maybe<TResult>> selector, [CallerArgumentExpression(nameof(selector))] string selectorExpression = "")
	{
		_Debug.WriteLine($"Maybe<{typeof(TSource).Name}>.Select({selectorExpression})");
		if (maybe is Maybe<TSource>.Some some)
		{
			return selector(some.Value);
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
	public static Maybe<TResult> SelectMany<TSource, T2, TResult>(this Maybe<TSource> maybe, Func<TSource, Maybe<T2>> convert,
		Func<TSource, T2, TResult> selector, [CallerArgumentExpression(nameof(convert))] string convertExpression = "", 
		[CallerArgumentExpression(nameof(selector))] string selectorExpression = "")
	{
		_Debug.WriteLine($"Maybe<{typeof(TSource).Name}>.SelectMany({convertExpression}, {selectorExpression})");
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
	public static Maybe<TSource> Where<TSource>(this Maybe<TSource> maybe, Func<TSource, bool> predicate, [CallerArgumentExpression(nameof(predicate))] string predicateExpression = "")
	{
		_Debug.WriteLine($"Maybe<{typeof(TSource).Name}>.Where({predicateExpression})");
		if (maybe is Maybe<TSource>.Some some && predicate(some.Value))
		{
			return maybe;
		}
		return new Maybe<TSource>.None();
	}
}


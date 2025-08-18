using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadieRich.Maybe.Linq;

public static class EitherExtensions
{
	// Only one Select, returning a plain TResult
	public static Either<TLeft, TResult> Select<TLeft, TRight, TResult>(
		this Either<TLeft, TRight> either,
		Func<TRight, TResult> selector)
	{
		return either.IsLeft
			? Either<TLeft, TResult>.Left(either.LeftValue)
			: Either<TLeft, TResult>.Right(selector(either.RightValue));
	}

	public static Either<TLeft, TResult> SelectMany<TLeft, TRight, TIntermediate, TResult>(
		this Either<TLeft, TRight> either,
		Func<TRight, Either<TLeft, TIntermediate>> bind,
		Func<TRight, TIntermediate, TResult> project)
	{
		if (either.IsLeft)
			return Either<TLeft, TResult>.Left(either.LeftValue);

		var intermediate = bind(either.RightValue);
		return intermediate.IsLeft
			? Either<TLeft, TResult>.Left(intermediate.LeftValue)
			: Either<TLeft, TResult>.Right(project(either.RightValue, intermediate.RightValue));
	}

	public static Either<L, R> Where<L, R>(
		this Either<L, R> either,
		Func<R, bool> predicate, L error = default!)
	{
		if (either.IsLeft)
			return either; // preserve existing Left

		return predicate(either.RightValue)
			? either // passes, keep Right
			: Either<L, R>.Left(error); // fails, turn into Left
	}
}
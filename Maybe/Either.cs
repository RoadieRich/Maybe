using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadieRich.Maybe;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public readonly struct Either<L, R>
{
	private readonly bool isRight;
	private readonly L left;
	private readonly R right;

	private Either(L left)
	{
		this.left = left;
		this.right = default!;
		isRight = false;
	}

	private Either(R right)
	{
		this.right = right;
		this.left = default!;
		isRight = true;
	}

	// Factory methods
	public static Either<L, R> Left(L value) => new Either<L, R>(value);
	public static Either<L, R> Right(R value) => new Either<L, R>(value);

	// Bind for monadic chaining (right-biased)
	public Either<L, U> Bind<U>(Func<R, Either<L, U>> f) =>
		isRight ? f(right) : Either<L, U>.Left(left);

	// Map for simple transformation
	public Either<L, U> Map<U>(Func<R, U> f) =>
		isRight ? Either<L, U>.Right(f(right)) : Either<L, U>.Left(left);

	private string GetDebuggerDisplay()
	{
		return ToString();
	}

	// Accessors
	public bool IsRight => isRight;
	public bool IsLeft => !isRight;

	public R RightValue => isRight ? right : throw new InvalidOperationException("Not a Right value");
	public L LeftValue => !isRight ? left : throw new InvalidOperationException("Not a Left value");

	public static implicit operator Either<L, R>(L left) => Left(left);
	public static implicit operator Either<L, R>(R right) => Right(right);
	public static explicit operator R(Either<L, R> either) => either.IsRight ? either.right : throw new InvalidOperationException("Cannot convert Left to Right");
	public static explicit operator L(Either<L, R> either) => either.IsLeft ? either.left : throw new InvalidOperationException("Cannot convert Right to Left");

	public T Match<T>(Func<L, T> onLeft, Func<R, T> onRight) =>
		isRight ? onRight(right) : onLeft(left);

	public void Match(Action<L> onLeft, Action<R> onRight)
	{
		if (isRight)
			onRight(right);
		else
			onLeft(left);
	}

	public R Or(R defaultValue)
	{
		return isRight ? defaultValue : right;
	}

	public R Or(Func<R> defaultValueFunc)
	{
		return isRight ? right : defaultValueFunc();
	}

	public Either<L, R> OrElse(R defaultValue) =>
		this.IsRight ? this : Either<L, R>.Right(defaultValue);

	public Either<L, R> OrElse(Func<R> defaultValueFunc) =>
		IsRight ? this : Either<L, R>.Right(defaultValueFunc());

	public override string ToString()
	{
		return isRight ? $"Right({right})" : $"Left({left})";
	}
}


using System.Diagnostics;
using System.Numerics;

namespace RoadieRich.Maybe;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public abstract class Maybe<T>
{
	private Maybe()
	{
	}

	public sealed class Some(T value) : Maybe<T> { public T Value { get; } = value; }

	public sealed class None : Maybe<T> { }

	public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
	{
		if (this is Some s)
			return some(s.Value);

		return none();
	}

	public void Match(Action<T> some, Action none)
	{
		if (this is Some s)
		{
			some(s.Value);
		}
		else
		{
			none();
		}
	}

	public static implicit operator Maybe<T>(T value)
	{
		return new Some(value);
	}

	public static implicit operator Maybe<T>(Maybe.MaybeNone _)
	{
		return new None();
	}

	public static explicit operator T(Maybe<T> maybe)
	{
		return maybe.Match(
			some: value => value,
			none: () => throw new InvalidOperationException("Cannot convert None to a value."));
	}

	public bool TryGetValue(out T? value)
	{
		if (this is Some some)
		{
			value = some.Value;
			return true;
		}

		value = default;
		return false;
	}

	public static bool operator ==(Maybe<T> left, Maybe<T> right)
	{
		if (left is None && right is None)
			return true;
		if (left is Some l && right is Some r)
			return EqualityComparer<T>.Default.Equals(l.Value, r.Value);
		return false;
	}

	public static bool operator !=(Maybe<T> left, Maybe<T> right)
	{
		return !(left == right);
	}
	public static bool operator ==(Maybe<T> left, T right)
	{
		if (left is None && right is None)
			return true;
		if (left is Some l)
			return EqualityComparer<T>.Default.Equals(l.Value, right);
		return false;
	}

	public static bool operator !=(Maybe<T> left, T right)
	{
		return !(left == right);
	}

	public static bool operator ==(T left, Maybe<T> right)
	{
		if (left is None && right is None)
			return true;
		if (right is Some r)
			return EqualityComparer<T>.Default.Equals(left, r.Value);
		return false;
	}

	public static bool operator !=(T left, Maybe<T> right)
	{
		return !(left == right);
	}

	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(this, obj))
		{
			return true;
		}

		if (obj is null)
		{
			return false;
		}

		return (this, obj) switch
		{
			(Some s, Some o) => EqualityComparer<T>.Default.Equals(s.Value, o.Value),
			(None, None) => true,
			_ => false
		};
	}

	public override int GetHashCode()
	{
		if (this is Some s)
		{
			return EqualityComparer<T>.Default.GetHashCode(s.Value!);
		}
		throw new InvalidOperationException("Cannot get hash code for None.");
	}

	private string GetDebuggerDisplay()
	{
		return (this is Some s)
			? $"[Some({s.Value})]"
			: "[None]";
	}
}
public static class Maybe
{
	public class MaybeNone { }
	public static readonly MaybeNone None = new();
	
	public static Maybe<T> Some<T>(T value)
	{
		return new Maybe<T>.Some(value);
	}
}

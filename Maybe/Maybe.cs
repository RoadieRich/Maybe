using System.Diagnostics;
namespace RoadieRich.Maybe;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public readonly struct Maybe<T>
{
	public bool HasValue { get; } = false;

	private readonly T? _value = default;
	public T Value { get { if (HasValue) return _value!; else throw new InvalidOperationException(); } }

	public Maybe() { HasValue = false; }

	public Maybe(T value)
	{
		HasValue = true;
		_value = value;
	}


	public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
	{
		if (HasValue) return some(Value);
		else return none();
	}

	public void Match(Action<T> some, Action none)
	{
		if (HasValue) some(Value);
		else none();
	}

	public static implicit operator Maybe<T>(T value)
	{
		return new(value);
	}

	public static implicit operator Maybe<T>(Maybe.MaybeNone _)
	{
		return new();
	}

	public static explicit operator T(Maybe<T> maybe)
	{
		return maybe.HasValue switch
		{
			true => maybe.Value,
			false => throw new InvalidOperationException("Cannot convert None to a value."),
		};
	}

	public bool TryGetValue(out T? value)
	{
		if (this.HasValue)
		{
			value = Value;
			return true;
		}

		value = default;
		return false;
	}

	public static bool operator ==(Maybe<T> left, Maybe<T> right)
	{
		if (!left.HasValue && !right.HasValue)
			return true;
		if (left.HasValue && right.HasValue)
			return EqualityComparer<T>.Default.Equals(left.Value, right.Value);
		return false;
	}

	public static bool operator !=(Maybe<T> left, Maybe<T> right)
	{
		return !(left == right);
	}
	public static bool operator ==(Maybe<T> left, T right)
	{
		if (!left.HasValue)
			return false;
		if (left.HasValue)
			return EqualityComparer<T>.Default.Equals(left.Value, right);
		return false;
	}

	public static bool operator !=(Maybe<T> left, T right)
	{
		return !(left == right);
	}

	public static bool operator ==(T left, Maybe<T> right)
	{
		if (!right.HasValue)
			return false;
		if (right.HasValue)
			return EqualityComparer<T>.Default.Equals(left, right.Value);
		return false;
	}

	public static bool operator !=(T left, Maybe<T> right)
	{
		return !(left == right);
	}

	public override bool Equals(object? obj)
	{
		if (obj is null)
		{
			return false;
		}
		else if (obj is Maybe<T> maybe)
		{
			return this == maybe;
		}
		else if (obj is T value)
		{
			return this == value;
		}
		else return false;
	}

	public override int GetHashCode()
	{
		if (HasValue)
		{
			return Value is null ? 0 : Value.GetHashCode();
		}
		else
		{
			return -1;
		}
	}

	public T? Or(T defaultValue)
	{
		if (HasValue)
		{
			return Value;
		}
		else
		{
			return defaultValue;
		}
	}

	public T? Or(Func<T> defaultValueFactory)
	{
		if (HasValue)
		{
			return Value;
		}
		else
		{
			return defaultValueFactory();
		}
	}

	private string GetDebuggerDisplay()
	{
		if (HasValue)
		{
			return $"Maybe.Some({Value})";
		}
		else
		{
			return "Maybe.None";
		}
	}
}
public static class Maybe
{
	public class MaybeNone { }
	public static MaybeNone None { get; } = new();

	public static Maybe<T> Some<T>(T value)
	{
		return new Maybe<T>(value);
	}
}

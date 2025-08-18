namespace RoadieRich.Maybe.StringParsers;

public static class StringExtensions
{
	public static Maybe<int> ToInt(this string? value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return new Maybe<int>();
		}
		if (int.TryParse(value, out var result))
		{
			return new Maybe<int>(result);
		}
		else
		{
			return new Maybe<int>();
		}
	}

	public static Maybe<double> ToDouble(this string? value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return new Maybe<double>();
		}
		if (double.TryParse(value, out var result))
		{
			return new Maybe<double>(result);
		}
		else
		{
			return new Maybe<double>();
		}
	}

	public static Maybe<decimal> ToDecimal(this string? value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return new Maybe<decimal>();
		}
		if (decimal.TryParse(value, out var result))
		{
			return new Maybe<decimal>(result);
		}
		else
		{
			return new Maybe<decimal>();
		}
	}

	public static Maybe<bool> ToBool(this string? value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return new Maybe<bool>();
		}
		if (bool.TryParse(value, out var result))
		{
			return new Maybe<bool>(result);
		}
		else
		{
			return new Maybe<bool>();
		}
	}

	public static Maybe<DateTime> ToDateTime(this string? value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return new Maybe<DateTime>();
		}
		if (DateTime.TryParse(value, out var result))
		{
			return new Maybe<DateTime>(result);
		}
		else
		{
			return new Maybe<DateTime>();
		}
	}

	public static Maybe<DateTime> ToDateTime(this string? value, IFormatProvider provider)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return new Maybe<DateTime>();
		}
		if (DateTime.TryParse(value, provider, out var result))
		{
			return new Maybe<DateTime>(result);
		}
		else
		{
			return new Maybe<DateTime>();
		}
	}

	public static Maybe<T> ToEnum<T>(this string? value) where T : struct, Enum
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return new Maybe<T>();
		}
		if (Enum.TryParse<T>(value, out var result))
		{
			return new Maybe<T>(result);
		}
		else
		{
			return new Maybe<T>();
		}
	}

	public static Maybe<float> ToFloat(this string? value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return new Maybe<float>();
		}
		if (float.TryParse(value, out var result))
		{
			return new Maybe<float>(result);
		}
		else
		{
			return new Maybe<float>();
		}
	}
	public static Maybe<Guid> ToGuid(this string? value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return new Maybe<Guid>();
		}
		if (Guid.TryParse(value, out var result))
		{
			return new Maybe<Guid>(result);
		}
		else
		{
			return new Maybe<Guid>();
		}
	}

	public static Maybe<TimeSpan> ToTimeSpan(this string? value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return new Maybe<TimeSpan>();
		}
		if (TimeSpan.TryParse(value, out var result))
		{
			return new Maybe<TimeSpan>(result);
		}
		else
		{
			return new Maybe<TimeSpan>();
		}
	}

	public static Maybe<TimeSpan> ToTimeSpan(this string? value, IFormatProvider provider)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return new Maybe<TimeSpan>();
		}
		if (TimeSpan.TryParse(value, provider, out var result))
		{
			return new Maybe<TimeSpan>(result);
		}
		else
		{
			return new Maybe<TimeSpan>();
		}
	}


}

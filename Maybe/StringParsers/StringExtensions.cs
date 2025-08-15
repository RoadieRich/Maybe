namespace RoadieRich.Maybe.StringParsers
{
	public static class StringExtensions
	{
		public static Maybe<int> ToInt(this string? value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return new Maybe<int>.None();
			}
			if (int.TryParse(value, out var result))
			{
				return new Maybe<int>.Some(result);
			}
			else
			{
				return new Maybe<int>.None();
			}
		}

		public static Maybe<double> ToDouble(this string? value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return new Maybe<double>.None();
			}
			if (double.TryParse(value, out var result))
			{
				return new Maybe<double>.Some(result);
			}
			else
			{
				return new Maybe<double>.None();
			}
		}

		public static Maybe<decimal> ToDecimal(this string? value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return new Maybe<decimal>.None();
			}
			if (decimal.TryParse(value, out var result))
			{
				return new Maybe<decimal>.Some(result);
			}
			else
			{
				return new Maybe<decimal>.None();
			}
		}

		public static Maybe<bool> ToBool(this string? value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return new Maybe<bool>.None();
			}
			if (bool.TryParse(value, out var result))
			{
				return new Maybe<bool>.Some(result);
			}
			else
			{
				return new Maybe<bool>.None();
			}
		}

		public static Maybe<DateTime> ToDateTime(this string? value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return new Maybe<DateTime>.None();
			}
			if (DateTime.TryParse(value, out var result))
			{
				return new Maybe<DateTime>.Some(result);
			}
			else
			{
				return new Maybe<DateTime>.None();
			}
		}

		public static Maybe<DateTime> ToDateTime(this string? value, IFormatProvider provider)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return new Maybe<DateTime>.None();
			}
			if (DateTime.TryParse(value, provider, out var result))
			{
				return new Maybe<DateTime>.Some(result);
			}
			else
			{
				return new Maybe<DateTime>.None();
			}
		}

		public static Maybe<T> ToEnum<T>(this string? value) where T : struct, Enum
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return new Maybe<T>.None();
			}
			if (Enum.TryParse<T>(value, out var result))
			{
				return new Maybe<T>.Some(result);
			}
			else
			{
				return new Maybe<T>.None();
			}
		}

		public static Maybe<float> ToFloat(this string? value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return new Maybe<float>.None();
			}
			if (float.TryParse(value, out var result))
			{
				return new Maybe<float>.Some(result);
			}
			else
			{
				return new Maybe<float>.None();
			}
		}
		public static Maybe<Guid> ToGuid(this string? value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return new Maybe<Guid>.None();
			}
			if (Guid.TryParse(value, out var result))
			{
				return new Maybe<Guid>.Some(result);
			}
			else
			{
				return new Maybe<Guid>.None();
			}
		}

		public static Maybe<TimeSpan> ToTimeSpan(this string? value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return new Maybe<TimeSpan>.None();
			}
			if (TimeSpan.TryParse(value, out var result))
			{
				return new Maybe<TimeSpan>.Some(result);
			}
			else
			{
				return new Maybe<TimeSpan>.None();
			}
		}

		public static Maybe<TimeSpan> ToTimeSpan(this string? value, IFormatProvider provider)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return new Maybe<TimeSpan>.None();
			}
			if (TimeSpan.TryParse(value, provider, out var result))
			{
				return new Maybe<TimeSpan>.Some(result);
			}
			else
			{
				return new Maybe<TimeSpan>.None();
			}
		}


	}
}

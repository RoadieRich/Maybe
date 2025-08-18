using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RoadieRich.Maybe;

public static class Extensions
{
	public static Maybe<T> AsSome<T>(this T value)
	{
		return new Maybe<T>(value);
	}

	public static Maybe<T> AsNone<T>(this T _)
	{
		return new Maybe<T>();
	}

	public static Maybe<TValue> TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
	{
		ArgumentNullException.ThrowIfNull(dict);
		if (dict.TryGetValue(key, out var value))
		{
			return new Maybe<TValue>(value);
		}
		else
		{
			return new Maybe<TValue>();
		}
	}

	public static Maybe<T> NoneIfNull<T>(T? obj) where T : struct
	{
		if (obj is null)
		{
			return new Maybe<T>();
		}
		else
		{
			return new Maybe<T>(obj.Value);
		}
	}

	public static Maybe<string> NoneIfNullOrWhiteSpace(string? str)
	{
		if (string.IsNullOrWhiteSpace(str))
		{
			return new Maybe<string>();
		}
		else
		{
			return new Maybe<string>(str);
		}
	}

	public static Maybe<string> NoneIfNullOrEmpty(string? str)
	{
		if (string.IsNullOrEmpty(str))
		{
			return new Maybe<string>();
		}
		else
		{
			return new Maybe<string>(str);
		}
	}

	public static Maybe<T> NoneIfNull<T>(this T? obj) where T : class
	{
		if (obj is null)
		{
			return new Maybe<T>();
		}
		else
		{
			return new Maybe<T>(obj);
		}
	}
}

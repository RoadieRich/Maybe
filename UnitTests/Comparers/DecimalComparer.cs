using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace UnitTests.Comparers;

public class DecimalComparer : IEqualityComparer<decimal>
{
	public decimal Tolerance { get; }

	public bool Equals(decimal x, decimal y)
	{
		return Math.Abs(x - y) <= Tolerance;
	}

	public int GetHashCode([DisallowNull] decimal obj)
	{
		return obj.GetHashCode();
	}

	public static DecimalComparer Default { get; } = new DecimalComparer(0.0m);

	private DecimalComparer(decimal tolerance)
	{
		Tolerance = tolerance;
	}

	public static DecimalComparer WithTolerance(decimal tolerance)
	{
		return new DecimalComparer(tolerance);
	}
}
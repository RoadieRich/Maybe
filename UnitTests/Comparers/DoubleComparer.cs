using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace UnitTests.Comparers;

public class DoubleComparer : IEqualityComparer<double>
{
	public double Tolerance { get; }

	public bool Equals(double x, double y)
	{
		return Math.Abs(x - y) <= Tolerance;
	}

	public int GetHashCode([DisallowNull] double obj)
	{
		return obj.GetHashCode();
	}

	public static DoubleComparer Default { get; } = new DoubleComparer(double.Epsilon);

	private DoubleComparer(double tolerance)
	{
		Tolerance = tolerance;
	}

	public static DoubleComparer WithTolerance(double tolerance)
	{
		return new DoubleComparer(tolerance);
	}
}
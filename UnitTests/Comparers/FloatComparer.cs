using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace UnitTests.Comparers;

public class FloatComparer : IEqualityComparer<float>
{
	public float Tolerance { get; }

	public bool Equals(float x, float y)
	{
		return MathF.Abs(x - y) <= Tolerance;
	}

	public int GetHashCode([DisallowNull] float obj)
	{
		return obj.GetHashCode();
	}

	public static FloatComparer Default { get; } = new FloatComparer(float.Epsilon);

	private FloatComparer(float tolerance)
	{
		Tolerance = tolerance;
	}

	public static FloatComparer WithTolerance(float tolerance)
	{
		return new FloatComparer(tolerance);
	}
}
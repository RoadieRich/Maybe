using NUnit.Framework.Constraints;
using RoadieRich.Maybe;
using System.Collections.Generic;

namespace UnitTests;

public class MaybeIsSomeConstraint<T>(T value) : Constraint
{
	private readonly T _value = value;

	public override string Description => $"to be Some({_value})";

	public override ConstraintResult ApplyTo<TActual>(TActual actual)
	{
		if (actual is Maybe<T>.Some some )
		{
			return new ConstraintResult(this, actual, EqualityComparer<T>.Default.Equals(some.Value, _value));
		}
		else
		{
			return new ConstraintResult(this, actual, false);
		}
	}
}

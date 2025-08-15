using NUnit.Framework.Constraints;
using RoadieRich.Maybe;
using System.Collections.Generic;

namespace UnitTests;


public class MaybeIsSomeWithoutTypeConstraint : Constraint
{
	public MaybeIsSomeWithoutTypeConstraint()
	{
	}

	public override string Description => "Maybe<*>.Some(...)";

	public override ConstraintResult ApplyTo<TActual>(TActual actual)
	{

		if (actual == null)
		{
			return new ConstraintResult(this, actual, false);
		}
		string? typeName = actual.GetType().FullName ?? "";
		return new ConstraintResult(this, actual, typeName.Contains("RoadieRich.Maybe.Maybe`1+Some[["));
	}
}
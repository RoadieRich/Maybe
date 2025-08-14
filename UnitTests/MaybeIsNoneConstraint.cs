using NUnit.Framework.Constraints;

namespace UnitTests;

public class MaybeIsNoneConstraint : Constraint
{
	public override string Description => $"to be None";

	public override ConstraintResult ApplyTo<TActual>(TActual actual)
	{
		if (actual == null)
		{
			return new ConstraintResult(this, actual, false);
		}
		string? typeName = actual.GetType().FullName ?? "";
		return new ConstraintResult(this, actual, typeName.Contains("RoadieRich.Maybe.Maybe`1+None[["));
	}
}

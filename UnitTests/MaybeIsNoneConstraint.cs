using NUnit.Framework.Constraints;
using RoadieRich.Maybe;
using System.Diagnostics;

namespace UnitTests;

/// <summary>
/// Represents a constraint that verifies whether a given value is an instance of <c>Maybe.None</c>.
/// </summary>
/// <remarks>This constraint is used to determine if the provided value is a "None" instance of the <c>Maybe</c>
/// type. It evaluates the type of the input and checks if it matches the internal representation of
/// <c>Maybe.None</c>.</remarks>
//[DebuggerNonUserCode]
public class MaybeIsNoneConstraint : Constraint
{
	public override string Description => $"Maybe.None";

	public override ConstraintResult ApplyTo<TActual>(TActual actual)
	{
		if (actual == null)
		{
			return new ConstraintResult(this, actual, false);
		}

		if (actual.GetType().IsGenericType == false || actual.GetType().GetGenericTypeDefinition() != typeof(Maybe<>))
		{
			return new ConstraintResult(this, actual, false);
		}
		else
		{
			// It's a Maybe<T>
			var hasValueProperty = actual.GetType().GetProperty("HasValue");
			if (hasValueProperty == null)
			{
				return new ConstraintResult(this, actual, false);
			}
			var hasValue = (bool)hasValueProperty.GetValue(actual)!;
			bool isMaybeNone = !hasValue;

			return new ConstraintResult(this, actual, isMaybeNone);
		}

	}
}

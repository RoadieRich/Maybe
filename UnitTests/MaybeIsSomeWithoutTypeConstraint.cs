using NUnit.Framework.Constraints;
using RoadieRich.Maybe;
using System.Collections.Generic;

namespace UnitTests;

/// <summary>
/// Represents a constraint that determines whether an object is an instance of a "Maybe<T>.Some" type.
/// </summary>
/// <remarks>This constraint checks if the provided object is a "Some" instance of a generic "Maybe<T>" type without knowing <c>T</c>. It
/// evaluates the object's type and its declaring type to verify the naming conventions associated with
/// "Maybe<T>.Some".</remarks>
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
			bool isMaybeSome = hasValue;
			return new ConstraintResult(this, actual, isMaybeSome);
		}

	}
}
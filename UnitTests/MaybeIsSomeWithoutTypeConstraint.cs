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
		var type = actual.GetType();
		// Check if this is a Maybe<T>.Some instance by looking for a containing type named Maybe`1 and a name "Some"
		var declaringType = type.DeclaringType;
		bool isMaybeSome = declaringType != null
			&& declaringType.Name.StartsWith(nameof(Maybe<>))
			&& type.Name == nameof(Maybe<>.Some);
		return new ConstraintResult(this, actual, isMaybeSome);
	}
}
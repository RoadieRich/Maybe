using NUnit.Framework.Constraints;
using RoadieRich.Maybe;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnitTests;

/// <summary>
/// Represents a constraint that verifies whether a <see cref="Maybe{T}"/> instance is in the "Some" state and
/// optionally matches a specified value.
/// </summary>
/// <remarks>This constraint is used to validate that a <see cref="Maybe{T}"/> object is in the "Some" state.  If
/// a specific value is provided during construction, the constraint also checks whether the value  matches the expected
/// value using an equality comparer. By default, the equality comparer is  <see cref="EqualityComparer{T}.Default"/>,
/// but a custom comparer can be specified using the  <see cref="Using"/> method.</remarks>
/// <typeparam name="T"></typeparam>
[DebuggerNonUserCode]
public class MaybeIsSomeConstraint<T> : Constraint
{
	private readonly Maybe<T> _value;

	public override string Description => $"Maybe.Some({(_value is Maybe<T>.Some some ? some.Value : "...")})";

	private IEqualityComparer<T>? _comparer = null;

	public MaybeIsSomeConstraint(T value)
	{
		_value = value;
	}

	public MaybeIsSomeConstraint()
	{
		_value = Maybe.None;
	}

	public override ConstraintResult ApplyTo<TActual>(TActual actual)
	{

		var comparer = _comparer ?? EqualityComparer<T>.Default;

		if (actual is Maybe<T>.Some some)
		{
			if (_value is Maybe<T>.Some someValue)
			{
				return new ConstraintResult(this, actual, comparer.Equals(some.Value, someValue.Value));
			}
			else
				return new ConstraintResult(this, actual, true);
		}
		else
		{
			return new ConstraintResult(this, actual, false);
		}
	}

	public Constraint Using(IEqualityComparer<T> comparer)
	{
		_comparer = comparer;
		return this;
	}
}

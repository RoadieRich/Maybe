using NUnit.Framework.Constraints;
using RoadieRich.Maybe;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnitTests;

[DebuggerNonUserCode]
public class MaybeIsSomeConstraint<T> : Constraint
{
	private readonly Maybe<T> _value;

	public override string Description => $"Maybe.Some({_value})";

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

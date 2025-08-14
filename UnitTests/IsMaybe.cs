using NUnit.Framework.Constraints;

namespace UnitTests;

public static class IsMaybe<T>
{
	public static Constraint Some(T value)
	{
		return new MaybeIsSomeConstraint<T>(value);
	}
	public static Constraint None => new MaybeIsNoneConstraint();
}

public class Is : NUnit.Framework.Is
{
	public static Constraint Some<T>(T value)
	{
		return new MaybeIsSomeConstraint<T>(value);
	}
	public static Constraint None => new MaybeIsNoneConstraint();
}
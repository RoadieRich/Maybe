using NUnit.Framework.Constraints;

namespace UnitTests;

public class Is : NUnit.Framework.Is
{
	public static class Maybe<T>
	{

		public static MaybeIsSomeConstraint<T> Some(T value)
		{
			return new MaybeIsSomeConstraint<T>(value);
		}

		public static MaybeIsSomeConstraint<T> Some()
		{
			return new MaybeIsSomeConstraint<T>();
		}

		public static MaybeIsNoneConstraint None => new();
	}

	public static class Maybe
	{
		public static MaybeIsNoneConstraint None => new();
		public static MaybeIsSomeWithoutTypeConstraint Some() => new();

		public static MaybeIsSomeConstraint<T> Some<T>(T value) => new(value);
	}
}
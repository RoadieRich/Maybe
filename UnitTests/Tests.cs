using RoadieRich.Maybe;

namespace UnitTests;

[TestFixture]
public class Tests
{
	[Test]
	public void CanCreateOptionalInt()
	{
		Assert.That(() => Maybe.Some(1), Throws.Nothing);
	}

	[Test]
	public void OptionalIntHasValue()
	{
		var opt = Maybe.Some(1);
		Assert.That(() => opt, Is.Maybe.Some(1));
	}

	[Test]
	public void OptionalIntsAreEqual()
	{
		var opt1 = Maybe.Some(1);
		var opt2 = Maybe.Some(1);
		Assert.That(() => opt1 == opt2, Is.True);
	}

	[Test]
	public void OptionalIntsAreNotEqual()
	{
		var opt1 = Maybe.Some(1);
		var opt2 = Maybe.Some(2);
		Assert.That(() => opt1 == opt2, Is.False);
	}

	[Test]
	public void OptionalIntCanEqualInt()
	{
		var opt1 = Maybe.Some(1);
		int val = 1;
		Assert.That(() => opt1 == val, Is.True);
	}

	[Test]
	public void OptionalIntCanNotEqualInt()
	{
		var opt1 = Maybe.Some(1);
		int val = 2;
		Assert.That(() => opt1 == val, Is.False);
	}

	[Test]
	public void OptionalIntCanBeNothing()
	{
		Assert.That(() => { Maybe<int> opt = Maybe.None; }, Throws.Nothing);
	}

	[Test]
	public void OptionalIntNothingHasNoValue()
	{
		Maybe<int> opt = Maybe.None;
		Assert.That(() => opt, Is.Maybe<int>.None);
	}

	[Test]
	public void OptionalIntNothingsAreEqual()
	{
		var opt1 = Maybe.None;
		var opt2 = Maybe.None;
		Assert.That(() => opt1 == opt2, Is.True);
	}

	[Test]
	public void OptionalStringsCanBeEqual()
	{
		var opt1 = Maybe.Some("test");
		var opt2 = Maybe.Some("test");
		Assert.That(() => opt1 == opt2, Is.True);
	}

	[Test]
	public void OptionalStringCanHaveNullValue()
	{
		var opt = Maybe.Some<string>(null!);
		Assert.That(() => opt, Is.Maybe<string>.Some(null!));
	}

	[Test]
	public void OptionalStringCanBeEquatedWithNull()
	{
		var opt = Maybe.Some<string?>(null);
		Assert.That(() => opt == null!, Is.True);
	}

	[Test]
	public void OptionalStringsWithNullAreEqual()
	{
		var opt1 = Maybe.Some<string>(null!);
		var opt2 = Maybe.Some<string>(null!);

		Assert.That(() => opt1 == opt2, Is.True);
	}

	[Test]
	public void OptionalStringNothingIsNotEqualToOptionalStringValue()
	{
		Maybe<string> opt1 = Maybe.None;
		Maybe<string> opt2 = Maybe.Some("test");
		Assert.That(() => opt1 == opt2, Is.False);
	}

	[Test]
	public void OptionalStringNothingIsNotEqualToNull()
	{
		Maybe<string> opt = Maybe.None;
		Assert.That(() => opt == null!, Is.False);
	}

	[Test]
	public void OptionalStringNothingIsNotEqualToOptionalStringWithNull()
	{
		Maybe<string> opt1 = Maybe.None;
		Maybe<string> opt2 = Maybe.Some((string?)null!);
		Assert.That(() => opt1 == opt2, Is.False);
	}

	[Test]
	public void OptionalStringNothingIsNotEqualToOptionalStringWithValue()
	{
		Maybe<string> opt1 = Maybe.None;
		Maybe<string> opt2 = Maybe.Some("test");
		Assert.That(() => opt1 == opt2, Is.False);
	}

	[Test]
	public void OptionalStringWithNullIsNotEqualToOptionalStringWithValue()
	{
		Maybe<string> opt1 = Maybe.Some<string>(null!);
		Maybe<string> opt2 = Maybe.Some("test");
		Assert.That(() => opt1 == opt2, Is.False);
	}

	[Test]
	public void OptionalStringWithValueIsNotEqualToOptionalStringWithNull()
	{
		Maybe<string> opt1 = Maybe.Some("test");
		Maybe<string> opt2 = Maybe.Some<string>(null!);
		Assert.That(() => opt1 == opt2, Is.False);
	}

	[Test]
	public void ImplicitConversionFromIntToOptionalInt()
	{
		Maybe<int> opt = 42;
		Assert.That(() => opt, Is.Maybe<int>.Some(42));
	}

	[Test]
	public void ExplicitConversionFromOptionalIntToInt()
	{
		Maybe<int> opt = new(42);
		int value = (int)opt;
		Assert.That(() => value, Is.EqualTo(42));
	}

	[Test]
	public void ExplicitConversionFromNoneToIntThrows()
	{
		Maybe<int> opt = Maybe.None;
		Assert.That(() => (int)opt, Throws.InvalidOperationException);
	}

	[Test]
	public void ImplicitConversionOfNullableIntToOptionalInt()
	{
		Maybe<int?> opt = (int?)null!;
		Assert.That(() => opt, Is.Maybe<int?>.Some(null!));
	}

	[Test]
	public void OrReturnsDefaultValueWhenNone()
	{
		Maybe<int> opt = Maybe.None;
		int defaultValue = 42;
		Assert.That(() => opt.Or(defaultValue), Is.EqualTo(defaultValue));
	}

	[Test]
	public void OrReturnsValueWhenSome()
	{
		Maybe<int> opt = Maybe.Some(42);
		int defaultValue = 0;
		Assert.That(() => opt.Or(defaultValue), Is.EqualTo(42));
	}
}
using RoadieRich.Maybe;
using RoadieRich.Maybe.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests;

[TestFixture]
public class EitherTests
{
	[Test]
	public void CanCreateEitherLeft()
	{
		Either<string, int> either = "left value";
		Assert.Multiple(() =>
		{
			Assert.That(either.IsLeft, Is.True);
			Assert.That(either.IsRight, Is.False);
			Assert.That(either.LeftValue, Is.EqualTo("left value"));
			Assert.That(() => { var right = either.RightValue; }, Throws.InvalidOperationException);
		});
	}

	[Test]
	public void CanCreateEitherRight()
	{
		Either<string, int> either = 42;
		Assert.Multiple(() =>
		{
			Assert.That(either.IsRight, Is.True);
			Assert.That(either.IsLeft, Is.False);
			Assert.That(either.RightValue, Is.EqualTo(42));
			Assert.That(() => { var left = either.LeftValue; }, Throws.InvalidOperationException);
		});
	}

	[Test]
	public void CanUseTernary([Values] bool b)
	{
		Either<string, int> either = b ? 42 : "Fail";
		Assert.That(either.IsRight, Is.EqualTo(b));
	}

	[Test]
	public void SelectManyWorksOnRight()
	{
		Either<string, int> either1 = 10;
		Either<string, int> either2 = 2;
		var result = from v1 in either1
					 from v2 in either2
					 select v1 * v2;

		Assert.Multiple(() =>
		{
			Assert.That(result.IsRight, Is.True);
			Assert.That(result.RightValue, Is.EqualTo(20));
		});
	}


}
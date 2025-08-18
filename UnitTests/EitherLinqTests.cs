using RoadieRich.Maybe;
using RoadieRich.Maybe.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests;

[TestFixture]
public class EitherLinqTests
{
	[Test]
	public void CanUseLinqSelectOnRight()
	{
		var either = Either<string,int>.Right(42);
		var result = from v in either
					 select v * 2;
		Assert.Multiple(() =>
		{
			Assert.That(result.IsRight, Is.True);
			Assert.That(result.RightValue, Is.EqualTo(84));
		});
	}

	[Test]
	public void WhereWorksOnRight()
	{
		var either = Either<string, int>.Right(42);
		var result = from v in either
					 where v % 2 == 0
					 select v * 2;
		Assert.Multiple(() =>
		{
			Assert.That(result.IsRight, Is.True);
			Assert.That(result.RightValue, Is.EqualTo(84));
		});
	}

	[Test]
	public void WhereFiltersOutRight()
	{
		var either = Either<string, int>.Right(42);
		var result = from v in either
					 where v % 2 == 1
					 select v * 2;
		Assert.Multiple(() =>
		{
			Assert.That(result.IsLeft, Is.True);
			Assert.That(result.LeftValue, Is.EqualTo(default(string)));
		});
	}

	[Test]
	public void SelectManyWorksOnRight()
	{
		var either1 = Either<string, int>.Right(1);
		var either2 = Either<string, int>.Right(2);
		var result = from v1 in either1
					 from v2 in either2
					 select v1 + v2;
		Assert.Multiple(() =>
		{
			Assert.That(result.IsRight, Is.True);
			Assert.That(result.RightValue, Is.EqualTo(3));
		});
	}
}

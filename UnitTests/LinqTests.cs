using RoadieRich.Maybe;
using RoadieRich.Maybe.Linq;
using System.Linq;
using UnitTests.Comparers;

namespace UnitTests;

[TestFixture]
public class LinqTests
{
	[Test]
	public void CanUseLinqSelect()
	{
		var opt = Maybe.Some(42);
		var result = from v in opt
					 select v * 2;
		Assert.That(() => result, Is.Maybe<int>.Some(84));
	}

	[Test]
	public void LinqWithNothingResultsInNothing()
	{
		Maybe<int> opt = Maybe.None;
		var result = from v in opt
					 select v * 2;
		Assert.That(() => result, Is.Maybe<int>.None);
	}

	[Test]
	public void LinqSelectWorks()
	{
		var opt1 = Maybe.Some(21);
		var result = from v1 in opt1
					 let v2 = 2
					 select v1 * v2;
		Assert.That(() => result, Is.Maybe<int>.Some(42));
	}

	[Test]
	public void LinqSelectManyWorksWithTwoSomes()
	{
		var opt1 = Maybe.Some(21);
		var opt2 = Maybe.Some(2);
		var result = from v1 in opt1
					 from v2 in opt2
					 select v1 * v2;
		Assert.That(() => result, Is.Maybe<int>.Some(42));
	}

	[Test]
	public void LinqSelectManyWorksWithNoneAndSome()
	{
		Maybe<int> opt1 = Maybe.None;
		var opt2 = Maybe.Some(2);
		var result = from v1 in opt1
					 from v2 in opt2
					 select v1 * v2;
		Assert.Multiple(() =>
		{
			Assert.That(() => result, Is.Maybe<int>.None);
		});
	}

	[Test]
	public void LinqSelectManyWorksWithSomeAndNone()
	{
		Maybe<int> opt1 = Maybe.Some(21);
		Maybe<int> opt2 = Maybe.None;
		var result = from v1 in opt1
					 from v2 in opt2
					 select v1 * v2;
		Assert.That(() => result, Is.Maybe<int>.None);
	}

	[Test]
	public void LinqSelectManyWorksWithTwoNones()
	{
		Maybe<int> opt1 = Maybe.None;
		Maybe<int> opt2 = Maybe.None;
		var result = from v1 in opt1
					 from v2 in opt2
					 select v1 * v2;
		Assert.That(() => result, Is.Maybe<int>.None);
	}

	[Test]
	public void LinqWhereWorks()
	{
		var opt = Maybe.Some(42);
		var result = from v in opt
					 where v > 40
					 select v;
		Assert.That(() => result, Is.Maybe<int>.Some(42));
	}

	[Test]
	public void LinqWhereWorks2()
	{
		var opt = Maybe.Some(42);
		var result = from v in opt
					 where v < 40
					 select v;
		Assert.That(() => result, Is.Maybe<int>.None);
	}

	[Test]
	public void LinqWhereWorks3()
	{
		Maybe<int> opt = Maybe.None;
		var result = from v in opt
					 where v < 40
					 select v;
		Assert.That(() => result, Is.Maybe<int>.None);
	}

	[Test]
	public void LinqSelectManyWithWhereWorks1()
	{
		var opt1 = Maybe.Some(21);
		var opt2 = Maybe.Some(2);
		var result = from v1 in opt1
					 from v2 in opt2
					 where v1 * v2 > 40
					 select v1 * v2;
		Assert.That(() => result, Is.Maybe<int>.Some(42));
	}

	[Test]
	public void LinqSelectManyWithWhereWorks2()
	{
		var opt1 = Maybe.Some(21);
		var opt2 = Maybe.Some(2);
		var result = from v1 in opt1
					 where v1 > 20
					 from v2 in opt2
					 select v1 * v2;
		Assert.That(() => result, Is.Maybe<int>.Some(42));
	}

	[Test]
	public void LinqSelectManyWithWhereWorks3()
	{
		var opt1 = Maybe.Some(21);
		var opt2 = Maybe.Some(2);
		var result = from v1 in opt1
					 where v1 < 20
					 from v2 in opt2
					 select v1 * v2;
		Assert.That(() => result, Is.Maybe<int>.None);
	}

	[Test]
	public void LinqSelectManyWithLetWorks()
	{
		var opt1 = Maybe.Some(1);
		var opt2 = Maybe.Some(2);
		var result = from v1 in opt1
					 from v2 in opt2
					 let s1 = v1.ToString()
					 let s2 = v2.ToString()
					 select s1 + " " + s2;
		Assert.That(() => result, Is.Maybe<string>.Some("1 2"));
	}

	[Test]
	public void LinqSelectMethodWorks()
	{
		Assert.That(() => Maybe.Some(1).Select(x => x + 1), Is.Maybe<int>.Some(2));
	}

	[Test]
	public void LinqSelectMethodWithFunctionReturningMaybe()
	{
		var opt0f = Maybe.Some(0.0);
		var opt1f = Maybe.Some(1.0);
		Assert.Multiple(() =>
		{
			Assert.That(() => opt0f.Select(Reciprocal), Is.Maybe<double>.None);
			Assert.That(() => opt1f.Select(Reciprocal), Is.Maybe<double>.Some(1.0).Using(DoubleComparer.WithTolerance(0.0001)));

		});

		static Maybe<double> Reciprocal(double x)
		{
			if (x == 0)
			{
				return Maybe.None;
			}
			else
			{
				return Maybe.Some(1.0 / x);
			}
		}
	}
}

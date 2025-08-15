using RoadieRich.Maybe;
using RoadieRich.Maybe.Linq;
using System.Linq;

namespace UnitTests;

[TestFixture]
public class LinqTests
{
	[Test]
	public void CanUseLinq()
	{
		var opt = Maybe.Some(42);
		var result = from v in opt
					 select v * 2;
		Assert.That(() => result, Is.Some(84));
	}

	[Test]
	public void LinqWithNothingResultsInNothing()
	{
		Maybe<int> opt = Maybe.None;
		var result = from v in opt
					 select v * 2;
		Assert.That(() => result, Is.None);
	}

	[Test]
	public void LinqSelectWorks()
	{
		var opt1 = Maybe.Some(21);
		var result = from v1 in opt1
					 let v2 = 2
					 select v1 * v2;
		Assert.That(() => result, Is.Some(42));
	}

	[Test]
	public void LinqSelectManyWorksWithTwoSomes()
	{
		var opt1 = Maybe.Some(21);
		var opt2 = Maybe.Some(2);
		var result = from v1 in opt1
					 from v2 in opt2
					 select v1 * v2;
		Assert.That(() => result, Is.Some(42));
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
			Assert.That(() => result, Is.None);
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
		Assert.That(() => result, Is.None);
	}

	[Test]
	public void LinqSelectManyWorksWithTwoNones()
	{
		Maybe<int> opt1 = Maybe.None;
		Maybe<int> opt2 = Maybe.None;
		var result = from v1 in opt1
					 from v2 in opt2
					 select v1 * v2;
		Assert.That(() => result, Is.None);
	}

	[Test]
	public void LinqWhereWorks()
	{
		var opt = Maybe.Some(42);
		var result = from v in opt
					 where v > 40
					 select v;
		Assert.That(() => result, Is.Some(42));
	}

	[Test]
	public void LinqWhereWorks2()
	{
		var opt = Maybe.Some(42);
		var result = from v in opt
					 where v < 40
					 select v;
		Assert.That(() => result, Is.None);
	}

	[Test]
	public void LinqWhereWorks3()
	{
		Maybe<int> opt = Maybe.None;
		var result = from v in opt
					 where v < 40
					 select v;
		Assert.That(() => result, Is.None);
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
		Assert.That(() => result, Is.Some(42));
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
		Assert.That(() => result, Is.Some(42));
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
		Assert.That(() => result, Is.None);
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
		Assert.That(() => result, Is.Some("1 2"));
	}

	[Test]
	public void LinqSelectMethodWorks()
	{
		Assert.That(() => Maybe.Some(1).Select(x => x + 1), Is.Some(2));
	}
}

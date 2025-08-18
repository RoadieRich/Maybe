using RoadieRich.Maybe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests;

public class ExtensionTests
{
	[Test]
	public void IEnumerableExtensions_Maybe()
	{
		var list = new List<int> { 1, 2, 3 };
		var maybeList = list.Maybe(x => x % 2 == 1).ToList();
		Assert.Multiple(() =>
		{
			Assert.That(maybeList, Contains.Item(Maybe.Some(1)));
			Assert.That(maybeList, Contains.Item(new Maybe<int>.None()));
			Assert.That(maybeList, Contains.Item(Maybe.Some(3)));
		});
	}

	[Test]
	public void IEnumerableExtensions_Flatten()
	{
		var list = new List<Maybe<int>> 
		{ 
			Maybe.Some(1), 
			Maybe.None, 
			Maybe.Some(2) 
		};
		var flattened = list.Flatten().ToList();
		Assert.Multiple(() =>
		{
			Assert.That(flattened, Contains.Item(1));
			Assert.That(flattened, Contains.Item(2));
			Assert.That(flattened, Has.Exactly(2).Items);
		});
	}

	[Test]
	public void ExtensionMethodAsSome()
	{
		var i = 42;
		var result = i.AsSome();
		Assert.That(result, Is.Maybe<int>.Some(42));
	}
}

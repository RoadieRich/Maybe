namespace RoadieRich.Maybe
{
	public interface IMaybe
	{
		object ValueObject { get; }
		bool HasValue { get; }
}
}
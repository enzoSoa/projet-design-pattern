namespace Pizeria
{
	public record PizzaCommand
	{
		public string name { get; init; }

		public int quantity { get; init; }
	};
}
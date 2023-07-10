namespace Pizeria
{
	public record PizzaIngredient
	{
		public Ingredient Ingredient { get; init; }
		public Quantity Quantity { get; init; }
	}
}
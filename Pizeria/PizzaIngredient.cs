namespace Pizeria
{
	public record PizzaIngredient
	{
		public Ingredient Ingredient { get; set; }
		public string Quantity { get; set; }
	}
}
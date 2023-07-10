using System.Collections.Generic;

namespace Pizeria
{
	public record InvoicePizza
	{
		public List<PizzaIngredient> Ingredients { get; init; }

		public int price { get; init; }

		public string name { get; init; }

		public double quantity { get; init; }
	}
}
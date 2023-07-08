using System.Collections.Generic;

namespace System.Runtime.CompilerServices
{
	internal static class IsExternalInit {}
}


namespace Pizeria
{
	public record Pizza
	{
		public List<PizzaIngredient> Ingredients { get; init; }

		public int price { get; init; }

		public string name { get; init; }
	}
}
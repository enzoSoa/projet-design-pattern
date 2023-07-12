using System;
using System.Collections.Generic;

namespace System.Runtime.CompilerServices
{
	internal static class IsExternalInit {}
}


namespace Pizeria
{
	public enum PizzaType
	{
		NORMAL,
		CALZONE
	}
	public record Pizza
	{
		public List<PizzaIngredient> Ingredients { get; init; }

		public int price { get; set; }

		public string name { get; set; }
		
		public PizzaType Type { get; set; }
	}

	public record PizzaEntityIngredient
	{
		public string name { get; init; }

		public Quantity Quantity { get; init; }
	}
	
	public record PizzaEntity
	{
		public List<PizzaEntityIngredient> ingredients { get; init; }
		
		public int price { get; init; }
		
		public string name { get; init; }
		
		public PizzaType Type { get; init; }
	}
}
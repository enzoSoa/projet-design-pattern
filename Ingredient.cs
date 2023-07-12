using System.Text.Json.Serialization;

namespace Pizeria
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum IngredientType
	{
		HOT,
		COLD
	}
	
	public record Ingredient
	{
		public IngredientType type { get; init; }
		
		public string name { get; init; }

		public Ingredient Copy()
		{
			return new Ingredient
			{
				type = type,
				name = name
			};
		}
		// TOMATO,
		// CHERRY_TOMATO,
		// MOZARELLA,
		// CHEESE,
		// HAM,
		// MUSHROOM,
		// OLIVE_OIL,
		// RED_PEPPER,
		// YELLOW_PEPPER,
		// OLIVE,
		// ZUCCHINI
	}
}
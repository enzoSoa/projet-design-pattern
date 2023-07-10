using System.Collections.Generic;

namespace Pizeria
{
	public class PizzaRepository
	{
		private List<Pizza> pizzas = new List<Pizza>
		{
			new Pizza
			{
				Ingredients = new List<PizzaIngredient>
				{
					new PizzaIngredient { Ingredient = Ingredient.TOMATO, Quantity = new Quantity { quantity = 150, unit = "g"} },
					new PizzaIngredient { Ingredient = Ingredient.MOZARELLA, Quantity = new Quantity { quantity = 125, unit = "g"} },
					new PizzaIngredient { Ingredient = Ingredient.CHEESE, Quantity = new Quantity { quantity = 100, unit = "g"} },
					new PizzaIngredient { Ingredient = Ingredient.HAM, Quantity = new Quantity { quantity = 2, unit = "tranches"} },
					new PizzaIngredient { Ingredient = Ingredient.MUSHROOM, Quantity = new Quantity { quantity = 4, unit = "unit"} },
					new PizzaIngredient { Ingredient = Ingredient.OLIVE_OIL, Quantity = new Quantity { quantity = 2, unit = "cuillères à soupe"} }
				},
				price = 800,
				name = "Regina"
			},
			new Pizza
			{
				Ingredients = new List<PizzaIngredient>
				{
					new PizzaIngredient { Ingredient = Ingredient.TOMATO, Quantity = new Quantity { quantity = 150, unit = "g"} },
					new PizzaIngredient { Ingredient = Ingredient.MOZARELLA, Quantity = new Quantity { quantity = 125, unit = "g"} },
					new PizzaIngredient { Ingredient = Ingredient.HAM, Quantity = new Quantity { quantity = 2, unit = "tranches"} },
					new PizzaIngredient { Ingredient = Ingredient.MUSHROOM, Quantity = new Quantity { quantity = 125, unit = "g"} },
					new PizzaIngredient { Ingredient = Ingredient.RED_PEPPER, Quantity = new Quantity { quantity = 0.5, unit = "unit"} },
					new PizzaIngredient { Ingredient = Ingredient.OLIVE, Quantity = new Quantity { quantity = 1, unit = "poigné"} }
				},
				price = 900,
				name = "4 saisons"
			},
			new Pizza
			{
				Ingredients = new List<PizzaIngredient>
				{
					new PizzaIngredient { Ingredient = Ingredient.TOMATO, Quantity = new Quantity { quantity = 150, unit = "g"} },
					new PizzaIngredient { Ingredient = Ingredient.MOZARELLA, Quantity = new Quantity { quantity = 100, unit = "g"} },
					new PizzaIngredient { Ingredient = Ingredient.ZUCCHINI, Quantity = new Quantity { quantity = 0.5, unit = "unit"} },
					new PizzaIngredient { Ingredient = Ingredient.YELLOW_PEPPER, Quantity = new Quantity { quantity = 1, unit = "unit"} },
					new PizzaIngredient { Ingredient = Ingredient.CHERRY_TOMATO, Quantity = new Quantity { quantity = 6, unit = "unit"} },
					new PizzaIngredient { Ingredient = Ingredient.OLIVE, Quantity = new Quantity { quantity = 1, unit = "quelques"} },
				},
				price = 750,
				name = "Végétarienne"
			},
		};

		public List<Pizza> getAll()
		{
			return pizzas;
		}

		public Pizza? get(string name)
		{
			return pizzas.Find(pizza => pizza.name.Equals(name));
		}
	}
}
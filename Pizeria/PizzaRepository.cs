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
					new PizzaIngredient { Ingredient = Ingredient.TOMATO, Quantity = "150g" },
					new PizzaIngredient { Ingredient = Ingredient.MOZARELLA, Quantity = "125g" },
					new PizzaIngredient { Ingredient = Ingredient.CHEESE, Quantity = "100g" },
					new PizzaIngredient { Ingredient = Ingredient.HAM, Quantity = "2 tranches" },
					new PizzaIngredient { Ingredient = Ingredient.MUSHROOM, Quantity = "4" },
					new PizzaIngredient { Ingredient = Ingredient.OLIVE_OIL, Quantity = "2 cuillères à soupe" }
				},
				price = 800,
				name = "Regina"
			},
			new Pizza
			{
				Ingredients = new List<PizzaIngredient>
				{
					new PizzaIngredient { Ingredient = Ingredient.TOMATO, Quantity = "150g" },
					new PizzaIngredient { Ingredient = Ingredient.MOZARELLA, Quantity = "125g" },
					new PizzaIngredient { Ingredient = Ingredient.HAM, Quantity = "2 tranches" },
					new PizzaIngredient { Ingredient = Ingredient.MUSHROOM, Quantity = "100g" },
					new PizzaIngredient { Ingredient = Ingredient.RED_PEPPER, Quantity = "0,5" },
					new PizzaIngredient { Ingredient = Ingredient.OLIVE, Quantity = "1 poignée" }
				},
				price = 900,
				name = "4 saisons"
			},
			new Pizza
			{
				Ingredients = new List<PizzaIngredient>
				{
					new PizzaIngredient { Ingredient = Ingredient.TOMATO, Quantity = "150g" },
					new PizzaIngredient { Ingredient = Ingredient.MOZARELLA, Quantity = "100g" },
					new PizzaIngredient { Ingredient = Ingredient.ZUCCHINI, Quantity = "0,5" },
					new PizzaIngredient { Ingredient = Ingredient.YELLOW_PEPPER, Quantity = "1" },
					new PizzaIngredient { Ingredient = Ingredient.CHERRY_TOMATO, Quantity = "6" },
					new PizzaIngredient { Ingredient = Ingredient.OLIVE, Quantity = "quelques" },
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
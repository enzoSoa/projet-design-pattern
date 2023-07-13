using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Pizeria
{
	public class PizzaRepository
	{
		private static readonly string File = "./pizza.json";
		
		private static Lazy<PizzaRepository> _lazy = new Lazy<PizzaRepository>(() => new PizzaRepository());

		private PizzaRepository()
		{
			var tmp = JsonSerializer.Deserialize<List<PizzaEntity>>(System.IO.File.ReadAllText(File, Encoding.UTF8));
			if (tmp != null)
			{
				_pizzas = tmp.ConvertAll(v =>
				{
					return new Pizza
					{
						Ingredients = v.ingredients.ConvertAll(v => new PizzaIngredient
							{ Ingredient = Ingredients.Find(v.name)!, Quantity = v.Quantity }),
						name = v.name,
						price = v.price,
						Type = v.Type
					};
				});
			}
			else
			{
				_pizzas = new List<Pizza>();
			}
		}
		
		private static readonly IngredientRepository Ingredients = IngredientRepository.Instance;

		public static PizzaRepository Instance = _lazy.Value;

		private List<Pizza> _pizzas;

		public Pizza? Get(string name)
		{
			return _pizzas.Find(pizza => pizza.name.Equals(name));
		}

		public List<Pizza> GetAll()
		{
			return _pizzas;
		}

		public void Add(Pizza pizza)
		{
			_pizzas.Add(pizza);
			Save();
		}

		public void Delete(Pizza pizza)
		{
			_pizzas.Remove(pizza);
			Save();
		}

		public void Save()
		{
			var sw = System.IO.File.CreateText(File);
			sw.Write(JsonSerializer.Serialize(_pizzas.ConvertAll(v => new PizzaEntity { name = v.name, price = v.price, Type = v.Type, ingredients = v.Ingredients.ConvertAll(i => new PizzaEntityIngredient { name = i.Ingredient.name, Quantity = i.Quantity })})));
			sw.Flush();
			sw.Close();
		}
	}
}
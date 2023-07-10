using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pizeria
{
	public class Command
	{
		private Dictionary<Pizza, int> _command;

		private Command(Dictionary<Pizza, int> command)
		{
			_command = command;
		}

		public Invoice ToInvoice()
		{
			var list = new List<InvoicePizza>();
			foreach (var pair in _command)
			{
				list.Add(new InvoicePizza
				{
					quantity = pair.Value,
					name = pair.Key.name,
					Ingredients = pair.Key.Ingredients,
					price = pair.Key.price
				});
			}
			return new Invoice(list);
		}

		public string ToIngredientsList()
		{
			Dictionary<Ingredient, List<Tuple<string, Quantity>>> list = new Dictionary<Ingredient, List<Tuple<string, Quantity>>>();
			foreach (var pair in _command)
			{
				foreach (var ingredient in pair.Key.Ingredients)
				{
					for (int i = 0; i < pair.Value; i++)
					{
						if (!list.ContainsKey(ingredient.Ingredient))
						{
							list.Add(ingredient.Ingredient, new List<Tuple<string, Quantity>>());

						}

						list[ingredient.Ingredient].Add(new Tuple<string, Quantity>(pair.Key.name, ingredient.Quantity));
					}
				}
			}

			StringBuilder builder = new StringBuilder();
			foreach (var pair in list)
			{
				builder.Append($"{pair.Key} : ");
				builder.AppendLine(string.Join(" + ", pair.Value.Aggregate(new List<Quantity>(), (quantities, tuple) =>
				{
					var index = quantities.FindIndex(v => v.unit == tuple.Item2.unit);
					if (index == -1)
					{
						quantities.Add(new Quantity { unit = tuple.Item2.unit, quantity = tuple.Item2.quantity });
					}
					else
					{
						quantities[index].quantity += tuple.Item2.quantity;
					}

					return quantities;
				}).ConvertAll(q =>
				{
					return $"{q.quantity} {q.unit}";
				})));
				pair.Value.ConvertAll(tuple => $" - {tuple.Item1} : {tuple.Item2.quantity} {tuple.Item2.unit}").ForEach(s =>
				{
					builder.AppendLine(s);
				});
			}

			return builder.ToString();
		}

		public string ToPreparation()
		{
			var builder = new StringBuilder();
			foreach (var pair in _command)
			{
				builder.AppendLine($"{pair.Key.name}");
				builder.AppendLine("Préparer la pâte");
				pair.Key.Ingredients.ForEach(ingredient => builder.AppendLine($"Ajouter {ingredient.Ingredient}"));
				builder.AppendLine("Cuire la pizza");
				builder.AppendLine();
			}

			return builder.ToString();
		}

		public class Builder
		{
			public Builder(PizzaRepository pizzas)
			{
				_pizzas = pizzas;
			}

			private PizzaRepository _pizzas;
			
			private Dictionary<string, int> _command = new Dictionary<string, int>();

			public Builder AddPizza(string pizza, int quantity)
			{
				if (_command.ContainsKey(pizza))
				{
					_command[pizza] += quantity;
				}
				else
				{
					_command.Add(pizza, quantity);
				}

				return this;
			}

			public Command? Build()
			{
				Dictionary<Pizza, int> commandPizza = new Dictionary<Pizza, int>();
				foreach (var pair in _command)
				{
					var pizza = _pizzas.get(pair.Key);
					if (pizza == null)
					{
						Console.Error.WriteLine("La pizza \"{0}\" n'existe pas", pair.Key);
						return null;
					}
					commandPizza.Add(pizza, pair.Value);
				}

				return new Command(commandPizza);
			}
		}
	}
}
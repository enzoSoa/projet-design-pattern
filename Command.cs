using System;
using System.Collections.Generic;
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

		public string ToInvoice()
		{
			var builder = new StringBuilder(); 
			foreach (var pair in _command)
			{
				builder.AppendFormat("{0} {1} : {0} * {2},{3}€\n", pair.Value, pair.Key.name, pair.Key.price / 100, pair.Key.price % 100);
				pair.Key.Ingredients.ForEach(ingredient => builder.AppendLine($"{ingredient.Ingredient} {ingredient.Quantity}"));
			}

			builder.AppendLine();
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
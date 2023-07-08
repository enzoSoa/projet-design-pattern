using System;
using System.Collections.Generic;
using System.Data;

namespace Pizeria
{
	public class Writer
	{
		public void WriteInvoice(Dictionary<Pizza, int> command)
		{
			foreach (var pair in command)
			{
				Console.Out.WriteLine("{0} {1} : {0} * {2},{3}€", pair.Value, pair.Key.name, pair.Key.price / 100, pair.Key.price % 100);
				pair.Key.Ingredients.ForEach(ingredient => Console.Out.WriteLine($"{ingredient.Ingredient} {ingredient.Quantity}"));
			}
			Console.Out.WriteLine();
		}

		public void WritePreparation(Dictionary<Pizza, int> command)
		{
			foreach (var pair in command)
			{
				Console.Out.WriteLine($"{pair.Key.name}");
				Console.Out.WriteLine("Préparer la pâte");
				pair.Key.Ingredients.ForEach(ingredient => Console.Out.WriteLine($"Ajouter {ingredient.Ingredient}"));
				Console.Out.WriteLine("Cuire la pizza");
				Console.Out.WriteLine();
			}
		}
	}
}
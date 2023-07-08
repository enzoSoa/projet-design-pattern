using System;
using System.Collections.Generic;

namespace Pizeria
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			Reader reader = new Reader();
			Writer writer = new Writer();
			PizzaRepository pizzas = new PizzaRepository();
			Dictionary<Pizza, int> command = new Dictionary<Pizza, int>();
			bool commandIsValid;
			while (true)
			{
				commandIsValid = true;
				command.Clear();
				Dictionary<string, int>? commandString = reader.readCommand();
				if (commandString == null)
				{
					continue;
				}
				foreach (var pair in commandString)
				{
					var pizza = pizzas.get(pair.Key);
					if (pizza == null)
					{
						commandIsValid = false;
						Console.Error.WriteLine("La pizza \"{0}\" n'existe pas", pair.Key);
						break;
					}
					command.Add(pizza, pair.Value);
				}
				if (commandIsValid)
				{
					writer.WriteInvoice(command);
					writer.WritePreparation(command);
				}
			}
		}
	}
}
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
			while (true)
			{
				command.Clear();
				Dictionary<string, int> commandString = reader.readCommand();
				foreach (var pair in commandString)
				{
					var pizza = pizzas.get(pair.Key)!;
					command.Add(pizza, pair.Value);
				}
				writer.WriteInvoice(command);
				writer.WritePreparation(command);
			}
		}
	}
}
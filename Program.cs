using System;
using System.Collections.Generic;

namespace Pizeria
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			PizzaRepository pizzas = new PizzaRepository();
			Writer writer = new Writer();
			while (true)
			{
				Command.Builder commandBuilder = new Command.Builder(pizzas);
				var pizzaCommands = Reader.ReadCommands();
				if (pizzaCommands == null)
				{
					continue;
				}
				pizzaCommands.ForEach(pizzaCommand =>
				{
					commandBuilder.AddPizza(pizzaCommand.name, pizzaCommand.quantity);
				});
				Command? command = commandBuilder.Build();
				if (command == null)
				{
					continue;
				}
				writer.Write(command.ToIngredientsList());
				writer.Write(command.ToInvoice());
				writer.Write(command.ToPreparation());
			}
		}
	}
}
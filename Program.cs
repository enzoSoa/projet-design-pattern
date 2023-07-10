using System;
using System.Linq;
using Mono.Options;

namespace Pizeria
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			string file = "-";
			Format format = Format.TEXT;
			var p = new OptionSet()
			{
				{"i|input=", "The file to read from", v => file = v },
				{"f|format=", "The format of input, [JSON, TEXT, XML] (default TEXT)", s =>
					{
						format =
							s == "JSON" ? Format.JSON :
							s == "XML" ? Format.XML :
							s == "TEXT" ? Format.TEXT : throw new OptionException("Invalid value", "format");
					}
				}
			};
			p.Parse(args);
			PizzaRepository pizzas = new PizzaRepository();
			ProxyWriter writer = new ProxyWriter(new ConsoleWriter());
			ProxyReader reader;
			if (file != "-")
			{
				reader = new ProxyReader(new FileReader(file, format));
			}
			else
			{
				reader = new ProxyReader(new ConsoleReader());
			}
			while (true)
			{
				Command.Builder commandBuilder = new Command.Builder(pizzas);
				var pizzaCommands = reader.Read();
				if (pizzaCommands == null)
				{
					return;
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
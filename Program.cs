using Mono.Options;

namespace Pizeria
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			string file = "-";
			string output = "-";
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
				},
				{"o|output=", "The file to output data", s =>
				{
					output = s;
				}}
			};
			p.Parse(args);
			PizzaRepository pizzas = PizzaRepository.Instance;
			ProxyWriter writer;
			ProxyReader reader;
			ConsoleWriter consoleWriter = new ConsoleWriter(Format.TEXT);
			if (file != "-")
			{
				reader = new ProxyReader(new FileReader(file, format));
			}
			else
			{
				reader = new ProxyReader(new ConsoleReader());
			}

			if (output != "-")
			{
				writer = new ProxyWriter(new FileWriter(output));
			}
			else
			{
				writer = new ProxyWriter(new ConsoleWriter(format));
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
				consoleWriter.Write(command.ToIngredientsList());
				writer.Write(command.ToInvoice());
				consoleWriter.Write(command.ToPreparation());
			}
		}
	}
}
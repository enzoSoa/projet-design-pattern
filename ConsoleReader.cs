using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pizeria
{
	public class ConsoleReader: Reader
	{
		public override List<PizzaCommand>? Read()
		{
			string? s = Console.In.ReadLine();
			if (s != null)
			{
				try
				{
					return s.Split(",".ToCharArray()).ToList().ConvertAll(command =>
					{
						var data = command.Trim().Split(" ".ToCharArray(), 2);
						if (data.Length != 2)
						{
							Console.Out.WriteLine("Commande \"{0}\" invalide", command);
							throw new InvalidDataException("Invalid format");
						}
						int quantity;
						if (!Int32.TryParse(data[0], out quantity))
						{
							throw new InvalidDataException("Quantity not an int");
						}

						return new PizzaCommand
						{
							name = data[1],
							quantity = quantity
						};
					});
				}
				catch (Exception e)
				{
					Console.Error.WriteLine(e.Message);
					return null;
				}
			}

			return null;
		}
	}
}
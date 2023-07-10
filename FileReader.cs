using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Pizeria
{
	public class FileReader : Reader
	{
		private readonly string[] _lines;
		private readonly List<List<PizzaCommand>>? _commands;
		private int _index;

		public FileReader(string file, Format format)
		{
			_format = format;
			_lines = File.ReadAllLines(file);
			_index = 0;
			if (format == Format.JSON)
			{
				_commands = JsonSerializer.Deserialize<List<List<PizzaCommand>>>(String.Join("", _lines));
			}

			if (format == Format.XML)
			{
				var serializer = new XmlSerializer(typeof(PizzaCommands));
				var tmp = (PizzaCommands)serializer.Deserialize(new XmlTextReader(file));
				_commands = tmp.Commands.ConvertAll(v => v.PizzaCommands);
			}
		}

		private FileReader()
		{
			throw new Exception("Cannot instantiate this class with this constructor");
		}

		private FileReader(Format format)
		{
			throw new Exception("Cannot instantiate this class with this constructor");
		}

		public FileReader(string file)
		{
			_lines = File.ReadAllLines(file);
			_index = 0;
		}

		public override List<PizzaCommand>? Read()
		{
			if (_format == Format.JSON)
			{
				return ReadJson();
			}

			if (_format == Format.TEXT)
			{
				return ReadText();
			}

			if (_format == Format.XML)
			{
				return ReadXml();
			}

			return null;
		}

		private List<PizzaCommand>? ReadJson()
		{
			if (_commands == null)
			{
				return null;
			}
			if (_index >= _commands.Count)
			{
				return null;
			}

			var command = _commands[_index];
			_index++;
			return command;
		}

		private List<PizzaCommand>? ReadText()
		{
			if (_index >= _lines.Length)
			{
				return null;
			}
			var s = _lines[_index];
			_index++;
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

		private List<PizzaCommand>? ReadXml()
		{
			if (_commands == null)
			{
				return null;
			}
			if (_index >= _commands.Count)
			{
				return null;
			}

			var command = _commands[_index];
			_index++;
			return command;
		}
	}
}
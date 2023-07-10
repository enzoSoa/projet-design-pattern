using System.Collections.Generic;
using System.Xml.Serialization;

namespace Pizeria
{
	public record PizzaCommand
	{
		[XmlAttribute("name")]
		public string name { get; init; }

		[XmlAttribute("quantity")]
		public int quantity { get; init; }
	}

	[XmlRoot("PizzaCommands")]
	public record PizzaCommands
	{
		[XmlElement("Command")]
		public List<Commands> Commands = new List<Commands>();
	}
	
	public record Commands
	{
		[XmlElement("PizzaCommand")] public List<PizzaCommand> PizzaCommands { get; init; } = new List<PizzaCommand>();
	}
}
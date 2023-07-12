using System;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Pizeria
{
	public class ConsoleWriter: Writer
	{

		public ConsoleWriter(Format format)
		{
			Format = format;
		}

		public override void Write<T>(T toWrite)
		{
			if (toWrite == null)
			{
				return;
			}

			if (Format == Format.TEXT)
			{
				Console.Out.WriteLine(toWrite.ToString());
			} 
			else if (Format == Format.JSON)
			{
				Console.Out.WriteLine(JsonSerializer.Serialize(toWrite));
			}
			else if(Format == Format.XML)
			{
				new XmlSerializer(typeof(T)).Serialize(new XmlTextWriter(Console.OpenStandardOutput(), Encoding.UTF8), toWrite);
			}
		}
	}
}
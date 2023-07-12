using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Pizeria
{
	public class FileWriter: Writer
	{

		private FileStream _sw;
		
		public FileWriter(string file)
		{
			if (file.EndsWith(".json"))
			{
				Format = Format.JSON;
			}
			else if (file.EndsWith(".txt"))
			{
				Format = Format.TEXT;
			}
			else if (file.EndsWith(".xml"))
			{
				Format = Format.XML;
			}
			else
			{
				throw new Exception("File type not supported");
			}
			if (File.Exists(file))
			{
				File.Delete(file);
			}

			_sw = File.Create(file);
		}

		public override void Write<T>(T toWrite)
		{
			if (toWrite == null)
			{
				return;
			}

			if (Format == Format.TEXT)
			{
				byte[] array = Encoding.UTF8.GetBytes(toWrite.ToString());
				_sw.Write(array, 0, array.Length);
			} 
			else if (Format == Format.JSON)
			{
				byte[] array = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(toWrite));
				_sw.Write(array, 0, array.Length);
			}
			else if(Format == Format.XML)
			{
				new XmlSerializer(typeof(T)).Serialize(new XmlTextWriter(_sw, Encoding.UTF8), toWrite);
			}
		}
	}
}
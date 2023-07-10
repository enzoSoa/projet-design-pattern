using System;

namespace Pizeria
{
	public class ConsoleWriter: IWriter
	{
		public void Write(string s)
		{
			Console.Out.WriteLine(s);
		}
	}
}
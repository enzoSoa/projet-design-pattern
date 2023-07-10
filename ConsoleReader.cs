using System;

namespace Pizeria
{
	public class ConsoleReader: IReader
	{
		public string? Read()
		{
			return Console.In.ReadLine();
		}
	}
}
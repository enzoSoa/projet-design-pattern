using System.Collections.Generic;

namespace Pizeria
{
	public abstract class Reader
	{
		protected Format _format = Format.TEXT;

		public Reader(){}

		public Reader(Format format)
		{
			_format = format;
		}
		
		public abstract List<PizzaCommand>? Read();
	}
}
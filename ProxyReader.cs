using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pizeria
{
	public class ProxyReader: Reader
	{
		public override List<PizzaCommand>? Read()
		{
			return _reader.Read();
		}
		
		private Reader _reader;
		
		public ProxyReader(Reader reader)
		{
			_reader = reader;
		}
	}
}
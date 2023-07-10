using System;
using System.Collections.Generic;
using System.Data;

namespace Pizeria
{
	public interface IWriter
	{
		public void Write(string s);
	}
	
	public abstract class Writer
	{
		protected Format _format = Format.TEXT;

		public Writer(){}

		public Writer(Format format)
		{
			_format = format;
		}
		
		public abstract void Write<T>(T toWrite);	
	}
}
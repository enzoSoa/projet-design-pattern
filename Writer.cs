namespace Pizeria
{
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
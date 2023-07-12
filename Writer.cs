namespace Pizeria
{
	public abstract class Writer
	{
		protected Format Format = Format.TEXT;

		public Writer(){}

		public Writer(Format format)
		{
			Format = format;
		}
		
		public abstract void Write<T>(T toWrite);	
	}
}
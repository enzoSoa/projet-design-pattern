namespace Pizeria
{
	public class ProxyWriter: Writer
	{
		private Writer _writer;
		
		public ProxyWriter(Writer writer)
		{
			_writer = writer;
		}

		public override void Write<T>(T toWrite)
		{
			_writer.Write(toWrite);
		}
	}
}
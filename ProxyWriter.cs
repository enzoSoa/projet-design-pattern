namespace Pizeria
{
	public class ProxyWriter: IWriter
	{
		private IWriter _writer;
		
		public ProxyWriter(IWriter writer)
		{
			_writer = writer;
		}

		public void Write(string s)
		{
			_writer.Write(s);
		}
	}
}
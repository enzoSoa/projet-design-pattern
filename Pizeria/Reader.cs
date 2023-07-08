using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pizeria
{
	public class Reader
	{
		public Dictionary<string, int>? readCommand()
		{
			bool validInput = true;
			string s = Console.In.ReadLine();
			Dictionary<string, int> res = new Dictionary<string, int>();
			if (s != null)
			{
				s.Split(",".ToCharArray()).ToList().ForEach(command =>
				{
					var data = command.Trim().Split(" ".ToCharArray(), 2);
					if (data.Length != 2)
					{
						Console.Out.WriteLine("Commande \"{0}\" invalide", command);
						validInput = false;
						return;
					}
					if (res.ContainsKey(data[1]))
					{
						res[data[1]] += Int32.Parse(data[0]);
					}
					else
					{
						res.Add(data[1], Int32.Parse(data[0]));
					}
				});
			}

			if (!validInput)
			{
				return null;
			}
			return res;
		}
	}
}
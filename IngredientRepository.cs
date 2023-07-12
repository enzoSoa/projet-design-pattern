using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace Pizeria
{
	public class IngredientRepository
	{
		private static readonly string File = "./ingredients.json";

		private static readonly Lazy<IngredientRepository> _lazy =
			new Lazy<IngredientRepository>(() => new IngredientRepository());

		public static IngredientRepository Instance = _lazy.Value;

		private readonly List<Ingredient> _ingredients;


		private IngredientRepository()
		{	
			if (System.IO.File.Exists(File))
			{
				var tmp = JsonSerializer.Deserialize<List<Ingredient>>(System.IO.File.ReadAllText(File, Encoding.UTF8));
				if (tmp != null)
				{
					_ingredients = tmp;
				}
				else
				{
					_ingredients = new List<Ingredient>();
				}
			}
		}

		public Ingredient? Find(string ingredient)
		{	
			return _ingredients.Find(v => v.name == ingredient)?.Copy();
		}

		public List<Ingredient> GetAll()
		{
			return _ingredients;
		}
		
		public void Add(Ingredient ingredient)
		{
			_ingredients.Add(ingredient);
			Save();
		}

		public bool Update(Ingredient ingredient)
		{
			var i = _ingredients.FindIndex(v => v.name == ingredient.name);
			if (i == -1)
			{
				return false;
			}

			_ingredients[i] = ingredient;
			Save();
			return true;
		}

		public bool Delete(string name)
		{
			var i = _ingredients.FindIndex(v => v.name == name);
			if (i == -1)
			{
				return false;
			}

			_ingredients.RemoveAt(i);
			return true;
		}
		
		private void Save()
		{
			var sw = System.IO.File.CreateText(File);
			sw.Write(JsonSerializer.Serialize(_ingredients));
			sw.Flush();
			sw.Close();
		}
	}
}
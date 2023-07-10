using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Pizeria
{
	[XmlRoot("Invoice")]
	public class Invoice
	{
		public Invoice(List<InvoicePizza> pizzas)
		{
			this.pizzas = pizzas;
		}

		public Invoice()
		{
			pizzas = new List<InvoicePizza>();
		}
		
		[XmlArray("Pizzas")]
		public List<InvoicePizza> pizzas { get; set; }

		public override string ToString()
		{
			var builder = new StringBuilder();
			foreach (var pair in pizzas)
			{
				builder.AppendFormat("{0} {1} : {0} * {2},{3}€\n", pair.quantity, pair.name, pair.price / 100, pair.price % 100);
				pair.Ingredients.ForEach(ingredient => builder.AppendLine($"{ingredient.Ingredient} {ingredient.Quantity.quantity} {ingredient.Quantity.unit}"));
			}
			
			builder.AppendLine();
			return builder.ToString();
		}
	}
}
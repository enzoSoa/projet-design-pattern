using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Mono.Options;

namespace Pizeria
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			string file = "-";
			string output = "-";
			Format format = Format.TEXT;
			bool edit = false;
			var p = new OptionSet
			{
				{ "i|input=", "The file to read from", v => file = v },
				{
					"f|format=", "The format of input, [JSON, TEXT, XML] (default TEXT)", s =>
					{
						format =
							s == "JSON" ? Format.JSON :
							s == "XML" ? Format.XML :
							s == "TEXT" ? Format.TEXT : throw new OptionException("Invalid value", "format");
					}
				},
				{ "o|output=", "The file to output data", s => { output = s; } },
				{ "e|edit", "Edit mode to add update remove Pizza/Ingredient", _ => { edit = true; } }
			};
			p.Parse(args);
			PizzaRepository pizzas = PizzaRepository.Instance;
			ProxyWriter writer;
			ProxyReader reader;
			ConsoleWriter consoleWriter = new ConsoleWriter(Format.TEXT);
			ConsoleReader consoleReader = new ConsoleReader();
			if (file != "-")
			{
				reader = new ProxyReader(new FileReader(file, format));
			}
			else
			{
				reader = new ProxyReader(new ConsoleReader());
			}

			if (output != "-")
			{
				writer = new ProxyWriter(new FileWriter(output));
			}
			else
			{
				writer = new ProxyWriter(new ConsoleWriter(format));
			}

			if (edit)
			{
				Edit(consoleWriter, consoleReader);
			}
			else
			{
				while (true)
				{
					Command.Builder commandBuilder = new Command.Builder(pizzas);
					var pizzaCommands = reader.Read();
					if (pizzaCommands == null)
					{
						return;
					}

					pizzaCommands.ForEach(pizzaCommand =>
					{
						commandBuilder.AddPizza(pizzaCommand.name, pizzaCommand.quantity);
					});
					Command? command = commandBuilder.Build();
					if (command == null)
					{
						continue;
					}

					consoleWriter.Write(command.ToIngredientsList());
					writer.Write(command.ToInvoice());
					consoleWriter.Write(command.ToPreparation());
				}
			}
		}

		private static void Edit(ConsoleWriter consoleWriter, ConsoleReader consoleReader)
		{
			while (true)
			{
				consoleWriter.Write("[1] Ingrédients\n[2] Pizzas");
				var choice = consoleReader.ReadString();
				if (choice == null)
				{
					return;
				}

				if (Int32.Parse(choice) == 1)
				{
					if(!Ingredient(consoleWriter, consoleReader))
					{
						return;
					}
				}
				else if (Int32.Parse(choice) == 2)
				{
					if(!Pizza(consoleWriter, consoleReader))
					{
						return;
					}
				}
				else
				{
					consoleWriter.Write("Erreur");
				}
			}
		}

		private static bool Ingredient(ConsoleWriter consoleWriter, ConsoleReader consoleReader)
		{
			IngredientRepository.Instance.GetAll()
				.ForEach(v => { consoleWriter.Write($"{v.name} : {v.type}"); });
			consoleWriter.Write("[1] Éditer\n[2] Créer\n[3] Supprimer\n");
			var action = consoleReader.ReadString();
			if (action == null)
			{
				return false;
			}

			switch (Int32.Parse(action))
			{
				case 1:
				{
					consoleWriter.Write("Éditer quel ingrédient ?");
					var ingregient = consoleReader.ReadString();
					if (ingregient == null)
					{
						return false;
					}

					var i = IngredientRepository.Instance.Find(ingregient);
					if (i == null)
					{
						consoleWriter.Write("Cet ingrédient n'existe pas");
					}
					else
					{
						consoleWriter.Write("[H]ot ou [C]old ?");
						var type = consoleReader.ReadString();
						if (type == null)
						{
							return false;
						}

						if (type == "H")
						{
							IngredientRepository.Instance.Update(new Ingredient
								{ name = ingregient, type = IngredientType.HOT });
							consoleWriter.Write("Modifié");
						}
						else if (type == "C")
						{
							IngredientRepository.Instance.Update(new Ingredient
								{ name = ingregient, type = IngredientType.COLD });
							consoleWriter.Write("Modifié");
						}
						else
						{
							consoleWriter.Write("Erreur");
						}
					}

					break;
				}
				case 2:
				{
					consoleWriter.Write("Nom de l'ingrédient :");
					var name = consoleReader.ReadString();
					if (name == null)
					{
						return false;
					}

					if (IngredientRepository.Instance.Find(name) != null)
					{
						consoleWriter.Write("L'ingrédient existe déjà");
						return true;
					}

					consoleWriter.Write("Type : [H]ot/[C]old");
					var type = consoleReader.ReadString();
					if (type == null)
					{
						return false;
					}

					if (type == "H")
					{
						IngredientRepository.Instance.Add(new Ingredient { name = name, type = IngredientType.HOT });
						consoleWriter.Write("Créer");
					}
					else if (type == "C")
					{
						IngredientRepository.Instance.Add(new Ingredient { name = name, type = IngredientType.COLD });
						consoleWriter.Write("Créer");
					}
					else
					{
						consoleWriter.Write("Erreur");
					}

					break;
				}
				case 3:
				{
					consoleWriter.Write("Nom de l'ingrédient :");
					var name = consoleReader.ReadString();
					if (name == null)
					{
						return false;
					}

					if (IngredientRepository.Instance.Delete(name))
					{
						consoleWriter.Write("Supprimé");
					}
					else
					{
						consoleWriter.Write("L'ingrédient n'existe pas");
					}

					break;
				}
			}

			return true;
		}

		private static bool Pizza(ConsoleWriter consoleWriter, ConsoleReader consoleReader)
		{
			PizzaRepository.Instance.GetAll()
				.ForEach(v => { consoleWriter.Write($"{v.name}"); });
			consoleWriter.Write("[1] Éditer\n[2] Créer\n[3] Supprimer\n");
			var action = consoleReader.ReadString();
			if (action == null)
			{
				return false;
			}
			switch (Int32.Parse(action))
			{
				case 1:
				{
					consoleWriter.Write("Nom de la pizza :");
					var p = consoleReader.ReadString();
					if (p == null)
					{
						return false;
					}

					var pizza = PizzaRepository.Instance.Get(p);
					if (pizza == null)
					{
						consoleWriter.Write("La pizza n'existe pas");
						return true;
					}
					consoleWriter.Write("[1] Changer le prix\n[2] Changer le type\n[3] Changer les ingredients");
					var underaction = consoleReader.ReadString();
					if (underaction == null)
					{
						return false;
					}

					switch (Int32.Parse(underaction))
					{
						case 1:
						{
							consoleWriter.Write("Prix :");
							var prix = consoleReader.ReadString();
							if (prix == null)
							{
								return false;
							}
							pizza.price = (int)(Double.Parse(prix, CultureInfo.InvariantCulture) * 100);
							PizzaRepository.Instance.Save();
							break;
						}
						case 2:
						{
							consoleWriter.Write("Type : [N]ormal/[C]alzone");
							var type = consoleReader.ReadString();
							if (type == null)
							{
								return false;
							}

							if (type == "N")
							{
								pizza.Type = PizzaType.NORMAL;
							}
							else if (type == "C")
							{
								pizza.Type = PizzaType.CALZONE;
							}
							else
							{
								consoleWriter.Write("Erreur");
								return true;
							}
							PizzaRepository.Instance.Save();
							break;
						}
						case 3:
						{
							pizza.Ingredients.ForEach(v =>
							{
								consoleWriter.Write(v.Ingredient.name);
							});
							consoleWriter.Write("[1] Ajouter un ingrédient\n[2] Modifier la quantité d'un ingredient\n[3] Supprimer un ingrédient");
							var ingredientAction = consoleReader.ReadString();
							if (ingredientAction == null)
							{
								return false;
							}

							switch (Int32.Parse(ingredientAction))
							{
								case 1:
								{
									while (true)
									{
										consoleWriter.Write("Ingredient :");
										var ingredientString = consoleReader.ReadString();
										if (ingredientString == null)
										{
											return false;
										}

										var ingredient = IngredientRepository.Instance.Find(ingredientString);
										if (ingredient == null)
										{
											consoleWriter.Write("L'ingrédient n'existe pas");
											continue;
										}
										consoleWriter.Write("Quantité");
										var quantityString = consoleReader.ReadString();
										if (quantityString == null)
										{
											return false;
										}

										try
										{
											var quantity = Double.Parse(quantityString.Split(' ')[0], CultureInfo.InvariantCulture);
											pizza.Ingredients.Add(new PizzaIngredient
											{
												Ingredient = ingredient,
												Quantity = new Quantity
												{
													quantity = quantity,
													unit = quantityString.Split(' ')[1]
												}
											});
											PizzaRepository.Instance.Save();
											break;
										}
										catch (FormatException _)
										{
											consoleWriter.Write("Ce n'est pas un nombre valide");
										}
									}
									break;
								}
								case 2:
								{
									while (true)
									{
										consoleWriter.Write("Ingredient :");
										var ingredientString = consoleReader.ReadString();
										if (ingredientString == null)
										{
											return false;
										}

										var ingredient = pizza.Ingredients.Find(v => v.Ingredient.name == ingredientString);
										if (ingredient == null)
										{
											consoleWriter.Write("Cette pizza n'a pas cet ingrédient");
											continue;
										}
										consoleWriter.Write("Quantité");
										var quantityString = consoleReader.ReadString();
										if (quantityString == null)
										{
											return false;
										}

										try
										{
											var quantity = Double.Parse(quantityString.Split(' ')[0], CultureInfo.InvariantCulture);
											ingredient.Quantity = new Quantity
											{
												quantity = quantity,
												unit = quantityString.Split(' ')[1]
											};
											PizzaRepository.Instance.Save();
										}
										catch (FormatException _)
										{
											consoleWriter.Write("Ce n'est pas un nombre valide");
										}
										break;
									}
									break;
								}
								case 3:
								{
									while (true)
									{
										consoleWriter.Write("Ingredient :");
										var ingredientString = consoleReader.ReadString();
										if (ingredientString == null)
										{
											return false;
										}

										var ingredient =
											pizza.Ingredients.Find(v => v.Ingredient.name == ingredientString);
										if (ingredient == null)
										{
											consoleWriter.Write("Cette pizza n'a pas cet ingrédient");
											continue;
										}

										pizza.Ingredients.Remove(ingredient);
										PizzaRepository.Instance.Save();
										break;
									}
									break;
								}
							}
							break;
						}
					}
					break;
				}
				case 2:
				{
					string? name;
					while (true)
					{
						consoleWriter.Write("Nom de la pizza :");
						name = consoleReader.ReadString();
						if (name == null)
						{
							return false;
						}

						if (PizzaRepository.Instance.Get(name) != null)
						{
							consoleWriter.Write("Cette pizza existe déjà");
							continue;
						}
						break;
					}

					string? typeString;
					PizzaType type;
					string? priceString;
					int price;
					
					while (true)
					{
						consoleWriter.Write("Type de pizza : [N]ormal/[C]alzone");
						typeString = consoleReader.ReadString();
						if (typeString == null)
						{
							return false;
						}

						if (typeString == "N")
						{
							type = PizzaType.NORMAL;
						}
						else if (typeString == "C")
						{
							type = PizzaType.CALZONE;
						}
						else
						{
							consoleWriter.Write("Le type de pizza est invalide");
							continue;
						}

						break;
					}

					while (true)
					{
						consoleWriter.Write("Prix :");
						priceString = consoleReader.ReadString();
						if (priceString == null)
						{
							return false;
						}

						try
						{
							price = (int)(double.Parse(priceString, CultureInfo.InvariantCulture) * 100);
						}
						catch (FormatException _)
						{
							consoleWriter.Write("Le prix n'est pas un nombre valide");
							continue;
						}

						break;
					}

					var ingredients = new List<PizzaIngredient>();
					while (true)
					{
						consoleWriter.Write("Nom de l'ingredient (done pour terminer)");
						var ingredientString = consoleReader.ReadString();
						if (ingredientString == null)
						{
							return false;
						}

						if (ingredientString == "done")
						{
							break;
						}

						if (ingredients.Find(v => v.Ingredient.name == ingredientString) != null)
						{
							consoleWriter.Write("L'ingrédient est déjà dans la liste");
							continue;
						}

						var ingredient = IngredientRepository.Instance.Find(ingredientString);
						if (ingredient == null)
						{
							consoleWriter.Write("L'ingrédient n'existe pas");
							continue;
						}
						consoleWriter.Write("Quantité");
						var quantityString = consoleReader.ReadString();
						if (quantityString == null)
						{
							return false;
						}

						try
						{
							var quantity = Double.Parse(quantityString.Split(' ')[0], CultureInfo.InvariantCulture);
							ingredients.Add(new PizzaIngredient
							{
								Ingredient = ingredient,
								Quantity = new Quantity
								{
									quantity = quantity,
									unit = quantityString.Split(' ')[1]
								}
							});
							PizzaRepository.Instance.Save();
						}
						catch (FormatException _)
						{
							consoleWriter.Write("Ce n'est pas un nombre valide");
						}
					}
					PizzaRepository.Instance.Add(new Pizza
					{
						name = name,
						Type = type,
						price = price,
						Ingredients = ingredients
					});
					break;
				}
				case 3:
				{
					while (true)
					{
						consoleWriter.Write("Nom de la pizza :");
						var name = consoleReader.ReadString();
						if (name == null)
						{
							return false;
						}

						var pizza = PizzaRepository.Instance.Get(name);
						if (pizza == null)
						{
							consoleWriter.Write("Cette pizza n'existe pas");
							continue;
						}

						PizzaRepository.Instance.Delete(pizza);
						break;
					}
					break;
				}
			}
			return true;
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
	public class Reader
	{
		/// <summary>
		/// Holds the file names used to audit and input
		/// </summary>
		private static string InputFile = "input.csv";
		private static string AuditFile = "audit.csv";

		/// <summary>
		/// Represent the number of items to place in stock from each item in input
		/// </summary>
		private static int AmountToStock = 5;

		/// <summary>
		/// Reads from the input.txt file and passes a List back with the contents/prices of the vending machine.
		/// </summary>
		/// <returns>A list of vending machine contents and prices</returns>
		public static Dictionary<string, List<VendingItem>> StockMachine()
		{
			// Declaring list for vending stocking
			Dictionary<string, List<VendingItem>> vendingDictionary = new Dictionary<string, List<VendingItem>>();

			// Declaring/assigning path
			string path = Path.Combine(Environment.CurrentDirectory, InputFile);

			// Instantiating StreamReader
			try
			{
				using (StreamReader sr = new StreamReader(path))
				{
					while (!sr.EndOfStream)
					{
						// Adding each line to the dictionary.
						VendingItem item = new VendingItem(sr.ReadLine());
						if (vendingDictionary.ContainsKey(item.Slot))
						{
							for (int i = 0; i < AmountToStock; i++)
							{
								vendingDictionary[item.Slot].Add(item);
							}
						}
						else
						{
							vendingDictionary.Add(item.Slot, new List<VendingItem>() { item });
							for (int i = 0; i < AmountToStock - 1; i++)
							{
								vendingDictionary[item.Slot].Add(item);
							}
						}
					}
				}
			}
			catch (IOException ex)
			{
				Console.WriteLine(ex.Message);
			}


			// Returning the list.
			return vendingDictionary;
		}

		/// <summary>
		/// Creates a dictionary of the audit log contents.
		/// </summary>
		/// <returns>A Dictionary of the item names and number of sales.</returns>
		public static Dictionary<string, int> AuditLogRead()
		{
			// Creating the dictionary
			Dictionary<string, int> logDictionary = new Dictionary<string, int>();

			// Declaring the path.
			string path = Path.Combine(Environment.CurrentDirectory, AuditFile);

			if (!File.Exists(path))
			{
				File.Create(path);
			}

			// Instantiating the StreamReader
			try
			{
				using (StreamReader sr = new StreamReader(path))
				{
					while (!sr.EndOfStream)
					{
						// Read the line, if it contains a |, process it
						string line = sr.ReadLine();
						if (line.Contains("|"))
						{
							// As the lines are being read, the string is split and passed to the list.
							string[] temp = line.Split('|');
							// Adds to the dictionary
							logDictionary.Add(temp[0], int.Parse(temp[1]));
						}
					}
				}
			}
			catch (IOException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return logDictionary;
		}

		/// <summary>
		/// Pulls the currentTotalSales.
		/// </summary>
		/// <returns>A Dictionary of the item names and number of sales.</returns>
		public static decimal CurrentTotalSales()
		{
			decimal currentTotalSales = 0;

			// Declaring the path.
			string path = Path.Combine(Environment.CurrentDirectory, AuditFile);

			if (!File.Exists(path))
			{
				File.Create(path);
			}

			// Instantiating the StreamReader
			try
			{
				using (StreamReader sr = new StreamReader(path))
				{
					while (!sr.EndOfStream)
					{
						string line = sr.ReadLine();
						if (line.Contains("TOTAL"))
						{
							string total = line.Substring(17);
							currentTotalSales = decimal.Parse(total);
						}
					}
				}
			}
			catch (IOException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return currentTotalSales;
		}
	}
}

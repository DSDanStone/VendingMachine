using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
	public class Writer
	{
		/// <summary>
		/// Represent the file names to use
		/// </summary>
		private static string AuditFile = "audit.csv";
		private static string LogFile = "log.csv";

		public static void UpdateAudit(decimal recentSales, string vendingItemName)
		{
			// Declare and assign the dictionary from the Read class.
			Dictionary<string, int> logDictionary = Reader.AuditLogRead();

			// Add to the counter of the specified vending item. 
			if (logDictionary.ContainsKey(vendingItemName))
			{
				logDictionary[vendingItemName]++;
			}
			// Or add it to the dictionary, if it doesn't exist
			else
			{
				logDictionary.Add(vendingItemName, 1);
			}

			// Get the previous total sales from the file
			decimal totalSales = Reader.CurrentTotalSales();
			// Add in the recent sale
			totalSales += +recentSales;

			// Declaring the path.
			string path = Path.Combine(Environment.CurrentDirectory, AuditFile);

			// Instantiating Streamwriter
			try
			{
				using (StreamWriter sw = new StreamWriter(path, false))
				{
					foreach (var kvp in logDictionary)
					{   // Writing the key/value of the dictionary
						sw.WriteLine($"{kvp.Key}|{kvp.Value}");
					}
					// Putting in the extra space and then the passed in totalSales.
					sw.WriteLine();
					sw.WriteLine($"**TOTAL SALES** {totalSales.ToString("C")}");
				}
			}
			catch (IOException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public static void LogTransaction(string operation, decimal startingFunds, decimal finalFunds)
		{
			// Declaring/assigning path.
			string path = Path.Combine(Environment.CurrentDirectory, LogFile);
			// Instantiating StreamWriter
			try
			{
				using (StreamWriter sw = new StreamWriter(path, true))
				{
					// Writing to the log file.
					sw.WriteLine($"{DateTime.Now.ToString().PadRight(22)} {operation.PadRight(21)} {startingFunds.ToString("C").PadRight(7)} {finalFunds.ToString("C")}");
				}
			}
			catch (IOException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}

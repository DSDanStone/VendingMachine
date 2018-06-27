using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
	public class CLI
	{
		/// <summary>
		/// Represents the Vending Machine
		/// </summary>
		private VendingMachine VendingMachine;

		/// <summary>
		/// The default Constructor creates a new vending Machine object
		/// </summary>
		public CLI()
		{
			this.VendingMachine = new VendingMachine();
		}

		/// <summary>
		/// Runs the user interface
		/// </summary>
		public void Run()
		{
			this.PrintGreeting();
			this.VendingMachine.VisableStock.Clear();
			this.MainMenu();
		}

		/// <summary>
		/// Prints a splash screen greeting
		/// </summary>
		private void PrintGreeting()
		{
			Console.Clear();
			Console.WriteLine("  Welcome to");
			Console.WriteLine();
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(@"         /$$$$$$    /$$ /$$   /$$    /$$                          /$$");
			Console.WriteLine(@"        /$$__  $$  / $$/ $$  | $$   | $$                         | $$");
			Console.WriteLine(@"       | $$  \__/ /$$$$$$$$$$| $$   | $$ /$$$$$$  /$$$$$$$   /$$$$$$$");
			Console.WriteLine(@"       | $$      |   $$  $$_/|  $$ / $$//$$__  $$| $$__  $$ /$$__  $$");
			Console.WriteLine(@"       | $$       /$$$$$$$$$$ \  $$ $$/| $$$$$$$$| $$  \ $$| $$  | $$");
			Console.WriteLine(@"       | $$    $$|_  $$  $$_/  \  $$$/ | $$_____/| $$  | $$| $$  | $$");
			Console.WriteLine(@"       |  $$$$$$/  | $$| $$     \  $/  |  $$$$$$$| $$  | $$|  $$$$$$$");
			Console.WriteLine(@"        \______/   |__/|__/      \_/    \_______/|__/  |__/ \_______/");
			Console.WriteLine();
			Console.WriteLine();
			Console.Write("                             ");
			Console.ForegroundColor = ConsoleColor.White;
			string line = "Stocking Machine.....";
			foreach (char letter in line)
			{
				Console.Write(letter);
				System.Threading.Thread.Sleep(75);
			}
			Console.Clear();
		}

		/// <summary>
		/// Handles the Main Menu
		/// </summary>
		private void MainMenu()
		{
			string input;

			do
			{
				input = PrintMainMenu();
				switch (input)
				{
					case "1":
						DisplayStock();
						break;
					case "2":
						PurchaseMenu();
						break;
				}
				if (input != "Q")
				{
					Console.WriteLine("Press any key to return to main menu");
					Console.ReadKey(true);
				}
				Console.Clear();

			} while (input != "Q");

			Console.WriteLine("Have a nice day!");
		}

		/// <summary>
		/// Prints the Main Menu
		/// </summary>
		/// <returns>The user input</returns>
		private string PrintMainMenu()
		{
			string[] validChoices = { "1", "2", "Q" };
			string input = "";

			do
			{
				Console.Clear();
				Console.WriteLine("      MAIN MENU");
				Console.WriteLine("(1) Display Vending Machine Items");
				Console.WriteLine("(2) Purchase");
				Console.WriteLine("(Q) Quit");
				Console.WriteLine();
				Console.Write("    Make a choice: ");
				input = Console.ReadLine().ToUpper();
				Console.Clear();
			}
			while (!validChoices.Contains(input));

			return input;
		}

		/// <summary>
		/// Displays the current Machine stock to the console
		/// </summary>
		private void DisplayStock()
		{
			Console.Clear();

			// Print a header
			Console.WriteLine("Slot  Name                Price   Stock");
			Console.WriteLine("---------------------------------------");

			// Print each item in stock
			// Iterate through the current stock
			foreach (var kvp in this.VendingMachine.VisableStock)
			{
				// Start the line with the slot
				string line = kvp.Key.PadRight(6);

				// If there's nothing in it, show 'SOLD OUT'
				if (this.VendingMachine.SlotIsEmpty(kvp.Key))
				{
					line += "SOLD OUT";
				}
				// Otherwise, show the name and price
				else
				{
					line += kvp.Value[0].Name.PadRight(20);
					line += kvp.Value[0].Price.ToString("C").PadRight(8);
					line += kvp.Value.Count.ToString();
				}
				// Add the line to the output
				Console.WriteLine(line);
			}
		}

		/// <summary>
		/// Handles the Purchase Menu
		/// </summary>
		private void PurchaseMenu()
		{
			string input = "";
			do
			{
				input = PrintPurchaseMenu();
				switch (input)
				{
					case "1":
						FeedMoney();
						break;
					case "2":
						SelectProduct();
						break;
					case "3":
						FinishTransaction();
						break;
				}
			} while (input != "3");
		}

		/// <summary>
		/// Prints the Purchase Menu
		/// </summary>
		/// <returns>The user input</returns>
		private string PrintPurchaseMenu()
		{
			string[] validChoices = { "1", "2", "3" };
			string input = "";

			do
			{
				Console.Clear();
				Console.WriteLine("    PURCHASE MENU");
				Console.WriteLine("(1) Feed Money");
				Console.WriteLine("(2) Select Product");
				Console.WriteLine("(3) Finish Transaction");
				Console.WriteLine($"Current Money Provided: {this.VendingMachine.DepositedFunds.ToString("C")}");
				Console.WriteLine();

				Console.Write("    Make a choice: ");
				input = Console.ReadLine().ToUpper();
				Console.Clear();
			}
			while (!validChoices.Contains(input));

			return input;
		}

		/// <summary>
		/// Allows the user to feed money to the Machine
		/// Note: Only accepts bills
		/// </summary>
		private void FeedMoney()
		{
			string input = "";
			do
			{
				input = PrintFeedMoneyMenu();
				switch (input)
				{
					case "1":
						this.VendingMachine.ReceiveFunds(1);
						break;
					case "2":
						this.VendingMachine.ReceiveFunds(2);
						break;
					case "3":
						this.VendingMachine.ReceiveFunds(5);
						break;
					case "4":
						this.VendingMachine.ReceiveFunds(10);
						break;
				}
			} while (input != "5");
		}

		/// <summary>
		/// Prints the Feed Money Menu
		/// </summary>
		/// <returns>The user input</returns>
		private string PrintFeedMoneyMenu()
		{
			string[] validChoices = { "1", "2", "3", "4", "5" };
			string input = "";

			do
			{
				Console.Clear();
				Console.WriteLine("    FEED MONEY");
				Console.WriteLine("(1) Feed $1 Bill");
				Console.WriteLine("(2) Feed $2 Bill");
				Console.WriteLine("(3) Feed $5 Bill");
				Console.WriteLine("(4) Feed $10 Bill");
				Console.WriteLine("(5) Finish Feeding Money");
				Console.WriteLine($"Current Money Provided: {this.VendingMachine.DepositedFunds.ToString("C")}");
				Console.WriteLine();

				Console.Write("    Make a choice: ");
				input = Console.ReadLine().ToUpper();
				Console.Clear();
			}
			while (!validChoices.Contains(input));

			return input;
		}

		/// <summary>
		/// Allow the user to select a product to purchase
		/// </summary>
		private void SelectProduct()
		{
			DisplayStock();

			// Prompt the user for the slot to vend and take input
			Console.Write("Please enter a slot to vend: ");
			string input = Console.ReadLine().ToUpper();

			// If it is a proper input
			if (this.VendingMachine.ValidSlot(input))
			{
				// If the slot is empty, display Sold Out
				if (this.VendingMachine.SlotIsEmpty(input))
				{
					Console.WriteLine("Item is SOLD OUT.");
				}
				// Otherwise, try to vend it
				else
				{
					VendingItem item = this.VendingMachine.GetItem(input);

					Console.WriteLine($"Item:  {item.Name}");
					Console.WriteLine($"Price: {item.Price:C}");
					Console.WriteLine($"Current Money Provided: {this.VendingMachine.DepositedFunds:C}");
					Console.WriteLine();


					// If this item vends, tell the user
					if (this.VendingMachine.Vend(item))
					{
						string line = $"Vending {item.Name}.....";
						foreach (char letter in line)
						{
							Console.Write(letter);
							System.Threading.Thread.Sleep(55);
						}
						Console.WriteLine();
					}
					// Otherwise, display insufficient funds
					else
					{
						Console.WriteLine("Insufficient Funds");
						Console.Beep(110, 250);
						Console.Beep(98, 175);
					}
				}
			}
			// Otherwise, display invalid selection
			else
			{
				Console.WriteLine("Invalid Selection");
			}
			Console.WriteLine();
			Console.WriteLine("Returning to purchase menu");
			Console.WriteLine("Press any key to continue");
			Console.ReadKey(true);
		}

		/// <summary>
		/// Finishes the transaction and dispenses change
		/// </summary>
		private void FinishTransaction()
		{
			// Get variables from methods
			Change change = this.VendingMachine.MakeChange();
			List<VendingItem> items = this.VendingMachine.ClearTheTray();

			// Print the user's change and fun messages
			Console.Clear();
			Console.WriteLine(change.ToString());
			PrintFunMessages(items);
			Console.WriteLine("Have a nice day!");
		}

		/// <summary>
		/// Make a list of messages relevant to the user's snack choices
		/// </summary>
		/// <param name="items">The user's purchased snacks</param>
		private void PrintFunMessages(List<VendingItem> items)
		{
			foreach (VendingItem item in items)
			{
				if (item.Type == SnackType.Candy)
				{
					Console.WriteLine("Munch Munch, Yum!");
				}
				else if (item.Type == SnackType.Chip)
				{
					Console.WriteLine("Crunch Crunch, Yum!");
				}
				else if (item.Type == SnackType.Drink)
				{
					Console.WriteLine("Glug Glug, Yum!");
				}
				else if (item.Type == SnackType.Gum)
				{
					Console.WriteLine("Chew Chew, Yum!");
				}
			}
		}
	}
}

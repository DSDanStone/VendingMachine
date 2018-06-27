using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
	public class VendingMachine
	{
		/// <summary>
		/// Represents the stock of the vending machine
		/// The key is the slot in the machine
		/// The list is the items sitting in that slot
		/// </summary>
		private Dictionary<string, List<VendingItem>> Stock;
		/// <summary>
		/// Represents the stock as retrivable by the user
		/// </summary>
		public Dictionary<string, List<VendingItem>> VisableStock { get { return new Dictionary<string, List<VendingItem>>(Stock); } }
		/// <summary>
		/// Represents the current funds in the machine
		/// </summary>
		public decimal DepositedFunds { get; private set; }
		/// <summary>
		/// Represents the Vending items that were purchased and are waiting to be picked up
		/// </summary>
		private List<VendingItem> ItemsInTray;

		/// <summary>
		/// Creates a new Vending Machine pulling stock from a file
		/// </summary>
		public VendingMachine()
		{
			this.Stock = Reader.StockMachine();
			this.ItemsInTray = new List<VendingItem>();
		}

		/// <summary>
		/// Creates a new Vending Machine given the dictionary for stock
		/// </summary>
		/// <param name="stock">A dictionary as stock</param>
		public VendingMachine(Dictionary<string, List<VendingItem>> stock)
		{
			this.Stock = stock;
			this.ItemsInTray = new List<VendingItem>();
		}

		/// <summary>
		/// Adds the received funds to deposited funds
		/// </summary>
		/// <param name="funds">The received funds</param>
		public void ReceiveFunds(decimal funds)
		{
			this.DepositedFunds += funds;
			LogTransaction("FEED MONEY:", this.DepositedFunds - funds);
		}

		/// <summary>
		/// Vends the chosen item, if there are sufficient funds
		/// </summary>
		/// <param name="item">The item to vend</param>
		/// <returns>Whether the item was vended</returns>
		public bool Vend(VendingItem item)
		{
			// Initialize output variable
			bool vending = false;

			// If there are sufficient funds,
			// 
			if (item.Price <= this.DepositedFunds)
			{
				this.DepositedFunds -= item.Price;

				this.ItemsInTray.Add(item);
				vending = true;
				LogTransaction($"{item.Name} {item.Slot}", this.DepositedFunds + item.Price);
				UpdateAudit(item.Price, item.Name);
				this.Stock[item.Slot].Remove(item);
			}

			// Return whether the item was vended
			return vending;
		}

		/// <summary>
		/// Depletes the deposited funds in the machine
		/// </summary>
		/// <returns>A string containing the change</returns>
		public Change MakeChange()
		{
			// Hold starting funds to pass to log
			decimal startingFunds = this.DepositedFunds;

			// Create a change object to return
			Change change = new Change(this.DepositedFunds);

			// Reset the depositted funds to 0
			this.DepositedFunds = 0;

			// Log the transaction
			LogTransaction("GIVE CHANGE:", startingFunds);

			// Return the change
			return change;
		}

		/// <summary>
		/// Clears the tray
		/// </summary>
		/// <returns>A list of messages to the user, relevant to their snack selection</returns>
		public List<VendingItem> ClearTheTray()
		{
			// Make a list what's in the tray
			List<VendingItem> temp = new List<VendingItem>(ItemsInTray);

			// Clear the tray
			ItemsInTray.Clear();

			// Return the list of items in the tray
			return temp;
		}

		/// <summary>
		/// Checks whether the given string is a valid slot in the machine
		/// </summary>
		/// <param name="slot">The string to check</param>
		/// <returns>Whether it is a valid slot</returns>
		public bool ValidSlot(string slot)
		{
			return this.Stock.ContainsKey(slot);
		}

		/// <summary>
		/// Checks whether the given slot is empty
		/// </summary>
		/// <param name="slot">The slot to check</param>
		/// <returns>Whether the slot is empty</returns>
		public bool SlotIsEmpty(string slot)
		{
			if (ValidSlot(slot))
			{
				return this.Stock[slot].Count == 0;
			}
			return true;
		}

		/// <summary>
		/// Gets an item from stock
		/// </summary>
		/// <param name="slot">Which slot to pull from</param>
		/// <returns>The item in that slot</returns>
		public VendingItem GetItem(string slot)
		{
			if (ValidSlot(slot))
			{
				return this.Stock[slot][0];
			}
			return null;
		}

		/// <summary>
		/// Logs the transaction
		/// </summary>
		/// <param name="operation">Which operation was performed</param>
		/// <param name="startingFunds">The funds before the operation</param>
		private void LogTransaction(string operation, decimal startingFunds)
		{
			Writer.LogTransaction(operation, startingFunds, this.DepositedFunds);
		}

		/// <summary>
		/// Updates the audit sheet
		/// </summary>
		/// <param name="vendingItemName">The item that was vended</param>
		private void UpdateAudit(decimal vendingItemPrice, string vendingItemName)
		{
			Writer.UpdateAudit(vendingItemPrice, vendingItemName);
		}
	}
}

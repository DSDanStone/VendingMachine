using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
	public class VendingItem
	{
		/// <summary>
		/// Represents the slot in the machine
		/// </summary>
		public string Slot { get; }
		/// <summary>
		/// Represents the name of the item
		/// </summary>
		public string Name { get; }
		/// <summary>
		/// Represents the price of the item
		/// </summary>
		public decimal Price { get; }
		/// <summary>
		/// Represents the type of snack
		/// </summary>
		public SnackType Type { get; }

		/// <summary>
		/// Constructs a Vending Item from a string from file input
		/// </summary>
		/// <param name="input">The standard string formating from the stocking file</param>
		public VendingItem(string input)
		{
			string[] temp = input.Split('|');
			this.Slot = temp[0];
			this.Name = temp[1];
			this.Price = decimal.Parse(temp[2]);
			this.Type = (SnackType)Enum.Parse(typeof(SnackType), temp[3]);
		}

		/// <summary>
		/// Constructs a Vending item, given the individual properties
		/// </summary>
		/// <param name="slot">The item's slot</param>
		/// <param name="name">The item's name</param>
		/// <param name="price">The item's price</param>
		/// <param name="type">The item's type</param>
		public VendingItem(string slot, string name, decimal price, SnackType type)
		{
			this.Slot = slot;
			this.Name = name;
			this.Price = price;
			this.Type = type;
		}
	}
}

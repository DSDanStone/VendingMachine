using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace Capstone.Tests
{
	[TestClass]
	public class VendingItemTests
	{
		[TestMethod]
		public void Constructor_From_String_Works()
		{
			string input = "A1|Potato Crisps|3.05|Chip";

			VendingItem vendingItem = new VendingItem(input);

			Assert.AreEqual("A1", vendingItem.Slot);
			Assert.AreEqual("Potato Crisps", vendingItem.Name);
			Assert.AreEqual(3.05M, vendingItem.Price);
			Assert.AreEqual(SnackType.Chip, vendingItem.Type);
		}

		[DataTestMethod]
		[DataRow("A1", "Potato Crisps", 3.05, SnackType.Chip)]
		[DataRow("A2", "MoonPie", 2.25, SnackType.Candy)]
		[DataRow("A3", "Chaaaaps", 1.45, SnackType.Chip)]
		[DataRow("A4", "Fruity Chewies", 0.85, SnackType.Candy)]
		[DataRow("B1", "Spritz", 1.15, SnackType.Drink)]
		[DataRow("B2", "BubbleBubble", 4.50, SnackType.Gum)]
		public void Constructor_From_Multiple_Inputs_Works(string slot, string name, double price, SnackType type)
		{
			VendingItem vendingItem = new VendingItem(slot, name, (decimal)price, type);

			Assert.AreEqual(slot, vendingItem.Slot);
			Assert.AreEqual(name, vendingItem.Name);
			Assert.AreEqual((decimal)price, vendingItem.Price);
			Assert.AreEqual(type, vendingItem.Type);
		}
	}
}

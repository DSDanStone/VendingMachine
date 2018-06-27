using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace Capstone.Tests
{
	[TestClass]
	public class VendingMachineTests
	{
		Dictionary<string, List<VendingItem>> stock = new Dictionary<string, List<VendingItem>>()
			{
				{"A1", new List<VendingItem>(){new VendingItem("A1|Potato Crisps|3.05|Chip"),new VendingItem("A1|Potato Crisps|3.05|Chip") } },
				{"A2", new List<VendingItem>(){new VendingItem("A2|Stackers|1.45|Chip"),new VendingItem("A2|Stackers|1.45|Chip") } },
				{"A3", new List<VendingItem>(){new VendingItem("A3|Grain Waves|2.75|Chip"),new VendingItem("A3|Grain Waves|2.75|Chip") } },
				{"B1", new List<VendingItem>(){new VendingItem("B1|Moonpie|1.80|Candy"),new VendingItem("B1|Moonpie|1.80|Candy") } },
				{"C2", new List<VendingItem>(){new VendingItem("C2|Dr. Salt|1.50|Drink"), new VendingItem("C2|Dr. Salt|1.50|Drink") } },
				{"D3", new List<VendingItem>(){new VendingItem("D3|Chiclets|0.75|Gum"),new VendingItem("D3|Chiclets|0.75|Gum") } }
			};

		[DataTestMethod]
		[DataRow(10.5)]
		[DataRow(1.0)]
		[DataRow(2.0)]
		[DataRow(5.0)]
		[DataRow(10.0)]
		public void Funds_Are_Accepted(double input)
		{
			VendingMachine vendingMachine = new VendingMachine(stock);

			vendingMachine.ReceiveFunds((decimal)input);

			Assert.AreEqual<decimal>((decimal)input, vendingMachine.DepositedFunds);
		}

		[DataTestMethod]
		[DataRow("A2|Stackers|1.45|Chip", "Crunch Crunch, Yum!")]
		[DataRow("B1|Moonpie|1.80|Candy", "Munch Munch, Yum!")]
		[DataRow("C2|Dr. Salt|1.50|Drink", "Glug Glug, Yum!")]
		[DataRow("D3|Chiclets|0.75|Gum", "Chew Chew, Yum!")]
		public void ClearTheTray_Works(string input, string output)
		{
			VendingItem item = new VendingItem(input);
			VendingMachine vendingMachine = new VendingMachine(stock);
			vendingMachine.ItemsInTray.Add(item);

			List<string> messages = vendingMachine.ClearTheTray();

			Assert.AreEqual<int>(0, vendingMachine.ItemsInTray.Count);
			CollectionAssert.AreEquivalent(new List<string>() { output }, messages);
		}

		[TestMethod]
		public void Correct_Change_is_Given()
		{
			VendingMachine vendingMachine = new VendingMachine(stock);
			vendingMachine.ReceiveFunds((decimal)10.00);
			string output = vendingMachine.MakeChange();

			Assert.AreEqual<string>("Your change is 40 quarters.", output);
			Assert.AreEqual<decimal>(0, vendingMachine.DepositedFunds);
		}

		[TestMethod]
		[DataTestMethod]
		[DataRow("A2|Stackers|1.45|Chip")]
		[DataRow("B1|Moonpie|1.80|Candy")]
		[DataRow("C2|Dr. Salt|1.50|Drink")]
		[DataRow("D3|Chiclets|0.75|Gum")]
		public void Vending_Works_With_Sufficient_Funds(string input)
		{
			VendingItem item = new VendingItem(input);
			VendingMachine vendingMachine = new VendingMachine(stock);
			vendingMachine.ReceiveFunds((decimal)10.00);

			bool vended = (vendingMachine.Vend(item));

			Assert.AreEqual<decimal>(10 - item.Price, vendingMachine.DepositedFunds);
			Assert.IsTrue(vended);
		}

		[DataTestMethod]
		[DataRow("A2|Stackers|1.45|Chip")]
		[DataRow("B1|Moonpie|1.80|Candy")]
		[DataRow("C2|Dr. Salt|1.50|Drink")]
		[DataRow("D3|Chiclets|0.75|Gum")]
		public void Vending_Works_With_Insufficient_Funds(string input)
		{
			VendingItem item = new VendingItem(input);
			VendingMachine vendingMachine = new VendingMachine(stock);
			vendingMachine.ReceiveFunds((decimal).50);

			bool vended = (vendingMachine.Vend(item));

			Assert.AreEqual<decimal>(.5M, vendingMachine.DepositedFunds);
			Assert.IsFalse(vended);
		}

		[TestMethod]
		public void DisplayStock_Works()
		{
			VendingMachine vendingMachine = new VendingMachine(stock);
			List<string> expectedMessages = new List<string>()
			{
				"A1    Potato Crisps       $3.05   2",
				"A2    Stackers            $1.45   2",
				"A3    Grain Waves         $2.75   2",
				"B1    Moonpie             $1.80   2",
				"C2    Dr. Salt            $1.50   2",
				"D3    Chiclets            $0.75   2"
			};

			List<string> actualMessages = vendingMachine.DisplayStock();

			CollectionAssert.AreEquivalent(expectedMessages, actualMessages);
		}

	}
}
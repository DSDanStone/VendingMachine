using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
	public class Change
	{
		/// <summary>
		/// Represents the total amount of money in change
		/// </summary>
		private decimal Total;
		/// <summary>
		/// Represent the coins to make the change
		/// </summary>
		private int QuarterCount;
		private int DimeCount;
		private int NickelCount;
		private int PennyCount;

		/// <summary>
		/// Represents the value of each coin
		/// </summary>
		private const decimal quarterValue = .25M;
		private const decimal dimeValue = .10M;
		private const decimal nickelValue = .05M;
		private const decimal pennyValue = .01M;

		/// <summary>
		/// Creates a change object that holds the coins to make the passed total
		/// </summary>
		/// <param name="amount">The amount to make change for</param>
		public Change(decimal amount)
		{
			this.Total = amount;

			// Count coins away from the current deposited funds in decrementing value
			while (amount >= quarterValue)
			{
				amount -= quarterValue;
				QuarterCount += 1;
			}
			while (amount >= dimeValue)
			{
				amount -= dimeValue;
				DimeCount += 1;
			}
			while (amount >= nickelValue)
			{
				amount -= nickelValue;
				NickelCount += 1;
			}
			while (amount >= pennyValue)
			{
				amount -= pennyValue;
				PennyCount += 1;
			}
		}

		/// <summary>
		/// Returns a string containing the change
		/// </summary>
		public override string ToString()
		{
			// If funds are 0, return a different message
			if (this.Total <=0)
			{
				return "No change due since funds are zero!";
			}

			// Initialize output string
			string output = $"Your change is {this.Total:C} which is";

			// Format the coin counts into a string
			List<string> temp = new List<string>();
			temp.Add((QuarterCount == 0) ? " " : QuarterCount == 1 ? $" {QuarterCount} quarter" : $" {QuarterCount} quarters");
			temp.Add((DimeCount == 0) ? " " : DimeCount == 1 ? $" {DimeCount} dime" : $" {DimeCount} dimes");
			temp.Add((NickelCount == 0) ? " " : NickelCount == 1 ? $" {NickelCount} nickel" : $" {NickelCount} nickels");
			temp.Add((PennyCount == 0) ? " " : PennyCount == 1 ? $" {PennyCount} penny" : $" {PennyCount} pennies");
			temp.RemoveAll(x => x.Equals(" "));

			for (int i = 0; i < temp.Count; i++)
			{
				if (temp.Count >= 3 && i <= temp.Count - 3)
				{
					output += temp[i] + ",";
				}
				else if (temp.Count >= 2 && i == temp.Count - 2)
				{
					output += temp[i] + " and";
				}
				else
				{
					output += temp[i];
				}
			}
			output += ".";

			// Return the string containing the change
			return output;
		}
	}
}

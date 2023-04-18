using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Cases related to children tax exemptions
/// </summary>
public static class Exempt
{
	/// <summary>
	/// Gives you the amount to credit recived for children under 13 up to a certain amount
	/// </summary>
	/// <param name="gross">Gross anual salary</param>
	/// <param name="Children">amount of children</param>
	/// <param name="ChildAges">Ages of each child</param>
	/// <param name="Maintance">Money spend on child care services such as daycare</param>
	/// <param name="TaxOwed">amount of money allocated to taxes out of gross income</param>
	/// <returns>Money exempt from child related services</returns>
	public static double ChildCredit(double gross, int Children, List<uint> ChildAges, List<double> Maintance, double TaxOwed)
	{
		if (ChildAges == null || Maintance == null || Children < 1)
		{
			return 0;
		}
		double Perc = 0.35;
		while (gross >= 15000 && Perc >= 0.20) // for each $1000 over $15000 or until the percentage recived is 20%
		{
			Perc -= 0.01;
			gross -= 1000;
		}
		List<double> returned = new List<double>();
		for (int Child = 0; Child < Children; Child++) 
		{
			if (Maintance[Child] > 3000)
			{
				Maintance[Child] = 3000;
			}
			if (ChildAges[Child] < 13)
			{
				returned.Add(Maintance[Child] * Perc);
			}
		}
		double result1 = returned.Max();// finds first highest number
		returned.Remove(result1);
		double result2 = returned.Max();// finds second highest number
		TaxOwed -= (result1 - result2);
		return TaxOwed;
	}
	/// <summary>
	/// Money recived through dependants under the age of 17 up to $2000 per dependant, if gross is over $200000 (per filer) $50 off each $1000 above
	/// </summary>
	/// <param name="Gross">Yearly Income</param>
	/// <param name="Dependants">Number of dependants</param>
	/// <param name="joint">Is filed Jointly</param>
	/// <returns></returns>
	public static double Dependant(double Gross, uint Dependants, bool joint)
	{
		int AmountSaved = 2000 * (int)Dependants;
		int Max = 0;
		if (joint)
		{
            Max = 200000;
        }
		else
		{
			Max = 400000;
		}
		while (Max < Gross)
		{
			AmountSaved -= 50;
			if (AmountSaved < 0) 
			{
				AmountSaved = 0;
				break;
			}
		}
		return AmountSaved;
	}
}

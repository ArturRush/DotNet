using System;
using System.Collections.Generic;
using Aviation.Aviation;
using Aviation.Factories;

namespace Aviation
{
	class Program
	{
		static void Main(string[] args)
		{
			Aeroport aer = new Aeroport("Останкино");
			aer.FillAeroport(10, 5);
			aer.PrintAviation();

			Console.ReadKey();
		}
	}
}

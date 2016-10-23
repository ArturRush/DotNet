using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aviation;
using Aviation.Factories;

namespace AviationDllTest
{
	class Program
	{
		static void Main(string[] args)
		{
			RussianAviationFactory raf = new RussianAviationFactory();
			var hel = raf.CreateHelicopter();
			hel.MakeFlight(Routs.Cities.Moscow, Routs.Cities.Omsk);
			Console.ReadKey();
		}
	}
}

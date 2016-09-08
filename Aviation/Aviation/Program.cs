using System;
using Aviation.Factories;

namespace Aviation
{
	class Program
	{
		static void Main(string[] args)
		{
			var americatFactory = new AmericanAviationFactory();
			var russianFactory = new RussianAviationFactory();

			var rusHelicopter = russianFactory.CreateHelicopter();
			var rusPlane = russianFactory.CreatePlane();
			var usaPlane = americatFactory.CreatePlane();

			rusHelicopter.PlacePassenger(3);
			rusHelicopter.MakeFlight(Routs.Cities.Cherlak, Routs.Cities.Omsk);
			rusHelicopter.DropOffPassenger();
			rusPlane.PlacePassenger(50);
			rusPlane.SendMessage(rusHelicopter, "Вылетаем");
			rusPlane.MakeFlight(Routs.Cities.Omsk, Routs.Cities.Moscow);
			usaPlane.PlacePassenger(50);
			usaPlane.SendMessage(rusPlane, "Пассажиры приняты на борт");
			usaPlane.MakeFlight(Routs.Cities.Moscow, Routs.Cities.Denver);

			Console.ReadKey();
		}
	}
}

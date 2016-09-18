using System;
using System.Collections.Generic;
using Aviation.Engines;
using Aviation.Aviation;
using Aviation.Factories;

namespace Aviation
{
	class Program
	{
		static void Main(string[] args)
		{
			IAeroport<IPassengerAviation<IEngine>> aer = new Aeroport<IPassengerAviation<IEngine>>("Кольцово");
			FillAeroport(3, 2, aer);
			aer.PrintAviation();
			Console.WriteLine();
			//Вывод количества свободных мест во всех воздушных судах
			Console.WriteLine("Всего свободных мест: " + aer.PrintSomeInfo(FreePlace));
			Console.WriteLine();
			//Вывод количества топлива, необходимого всем судам
			Console.WriteLine("Всего необходимо топлива: " + aer.PrintSomeInfo(FuelNeed) + " литров");
			Console.WriteLine();
			///Проверить все двигатели
			aer.DoSmth(CheckEngine);
			Console.WriteLine();
			Console.WriteLine("Сортировка по модели двигателя:");
			aer.Sort(SortByFreePlaces);
			aer.PrintAviation();
			Console.WriteLine();
			Random r = new Random();
			foreach (var avia in aer)
			{
				
				avia.PlacePassenger(r.Next(20));
			}
			Console.WriteLine();
			Console.WriteLine("Сортировка по количеству свободных мест");
			aer.Sort(SortByFreePlaces);
			aer.PrintAviation();
			Console.ReadKey();
		}

		public static void FillAeroport(int planes, int helicopters, IAeroport<IPassengerAviation<IEngine>> _aviation)
		{
			IAviationFactory aaf = new AmericanAviationFactory();
			IAviationFactory raf = new RussianAviationFactory();

			Random r = new Random();
			int americanPlanes = r.Next(planes);
			int russianHelicopters = r.Next(helicopters);

			for (int i = 0; i < planes - americanPlanes; ++i)
			{
				int reactive = r.Next(2);
				if (reactive == 1)
					_aviation.Add(raf.CreateReactivePlane());
				else
					_aviation.Add(raf.CreateTurbopropPlane());
			}

			for (int i = 0; i < americanPlanes; ++i)
			{
				int reactive = r.Next(2);
				if (reactive == 1)
					_aviation.Add(aaf.CreateReactivePlane());
				else
					_aviation.Add(aaf.CreateTurbopropPlane());
			}

			for (int i = 0; i < russianHelicopters; ++i)
			{
				_aviation.Add(raf.CreateHelicopter());
			}

			for (int i = 0; i < helicopters - russianHelicopters; ++i)
			{
				_aviation.Add(aaf.CreateHelicopter());
			}
		}

		/// <summary>
		/// Проверка двигателя воздушного судна
		/// </summary>
		/// <param name="avia">Воздушное судно</param>
		public static void CheckEngine(IPassengerAviation<IEngine> avia)
		{
			ICheckEngine<IEngine> checker = new EngineChecker();
			checker.Check(avia.Engine);
		}

		/// <summary>
		/// Свободные места судна
		/// </summary>
		/// <param name="avia">Воздушное судно</param>
		/// <returns>Количество свободных мест</returns>
		public static int FreePlace(IPassengerAviation<IEngine> avia)
		{
			return avia.Capacity - avia.Engaged;
		}

		/// <summary>
		/// Количество топлива, необходимое воздушному судну
		/// </summary>
		/// <param name="avia">Воздушное судно</param>
		/// <returns>Количество топлива</returns>
		public static int FuelNeed(IPassengerAviation<IEngine> avia)
		{
			return avia.TankCapacity;
		}

		/// <summary>
		/// Сравнение по количеству свободных мест
		/// </summary>
		public static int SortByFreePlaces(IPassengerAviation<IEngine> a, IPassengerAviation<IEngine> b)
		{
			return FreePlace(a).CompareTo(FreePlace(b));
		}

		/// <summary>
		/// Сравнение по модели двигателя
		/// </summary>
		public static int SortByEngineModel(IPassengerAviation<IEngine> a, IPassengerAviation<IEngine> b)
		{
			return String.Compare(a.Engine.Model, b.Engine.Model, StringComparison.Ordinal);
		}
	}
}

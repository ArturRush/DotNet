using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aviation.Engines;
using Aviation.Aviation;
using Aviation.Factories;
using Aviation.Loggers;
using Aviation.Serializer;
using Newtonsoft.Json;
using NUnit.Framework.Internal;

namespace Aviation
{
	class Program
	{
		private static void FillAer(Aeroport<IPassengerAviation<IEngine>> aer, int planesCount, int heliCount)
		{
			IAviationFactory aaf = new AmericanAviationFactory();
			IAviationFactory raf = new RussianAviationFactory();

			Random r = new Random();
			int americanPlanes = r.Next(planesCount);
			int russianHelicopters = r.Next(heliCount);

			for (int i = 0; i < planesCount - americanPlanes; ++i)
			{
				int reactive = r.Next(2);
				if (reactive == 1)
					aer.Add(raf.CreateReactivePlane());
				else
					aer.Add(raf.CreateTurbopropPlane());
			}

			for (int i = 0; i < americanPlanes; ++i)
			{
				int reactive = r.Next(2);
				if (reactive == 1)
					aer.Add(aaf.CreateReactivePlane());
				else
					aer.Add(aaf.CreateTurbopropPlane());
			}

			for (int i = 0; i < russianHelicopters; ++i)
			{
				aer.Add(raf.CreateHelicopter());
			}

			for (int i = 0; i < heliCount - russianHelicopters; ++i)
			{
				aer.Add(aaf.CreateHelicopter());
			}
		}

		public static void Main(string[] args)
		{
			var aer = new Aeroport<IPassengerAviation<IEngine>>("Кольцово");
			FillAer(aer, 10, 10);

			Console.WriteLine(aer.ConvertToString());


			Console.ReadKey();
		}
	}

	/// <summary>
	/// Класс расширение
	/// </summary>
	public static class AeroportExtension
	{
		/// <summary>
		/// Метод перевода коллекции в json-строку
		/// </summary>
		/// <typeparam name="T">Тип обобощенной коллекции</typeparam>
		/// <param name="aer">Коллекция</param>
		/// <returns>json-строка с объектами коллекции</returns>
		public static string ConvertToString<T>(this Aeroport<T> aer) where T : IPassengerAviation<IEngine>
		{
			ExtensionLogger.Log("Вызван метод конвертации коллекции в строку");
			var wrapper = new AeroportWrapper<T>(aer);
			return JsonConvert.SerializeObject(wrapper, Formatting.Indented);
		}

		/// <summary>
		/// Находит первое вхождение судна, у которого имеется достаточно свободных мест
		/// </summary>
		/// <param name="aer">Коллекция</param>
		/// <returns>Судно, либо null, если таковых не найдено</returns>
		public static IPassengerAviation<IEngine> FindByFreePlaces(this Aeroport<IPassengerAviation<IEngine>> aer, int places)
		{
			ExtensionLogger.Log("Вызван метод поиска пустого судна");
			foreach (var aero in aer)
			{
				if (aero.Capacity - aero.Engaged > places)
					return aero;
			}
			return null;
		}

		/// <summary>
		/// Возвращает все пустые воздушные судна из коллекции
		/// </summary>
		/// <typeparam name="T">тип обобщенной коллекции</typeparam>
		/// <param name="aer">Коллекция пустых самолетов</param>
		/// <returns></returns>
		public static Aeroport<T> FindAllEmpty<T>(this Aeroport<T> aer) where T : IPassengerAviation<IEngine>
		{
			ExtensionLogger.Log("Вызван метод поиска всех пустых суден");
			var res = new Aeroport<T>("temp");
			foreach (var aero in aer)
			{
				if (aero.Engaged == 0)
					res.Add(aero);
			}
			return res;
		} 
	}

	public static class ExtensionLogger
	{
		public static string filePath = "";

		public static void Log(string mes)
		{
			if(filePath=="")
				Console.WriteLine(mes);
			else
			{
				File.AppendAllText(filePath, mes);
			}
		}
	}
}

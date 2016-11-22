using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aviation.Aviation;
using Aviation.Engines;
using Aviation.Factories;
using Newtonsoft.Json;

namespace AviationExtentions
{
	class Program
	{
		/// <summary>
		/// Заполняет коллекццию случайными воздушными судами
		/// </summary>
		/// <param name="aer">Коллекция для заполнения</param>
		/// <param name="planesCount">Необходимое количество самолетов</param>
		/// <param name="heliCount">Необходимое количество вертолетов</param>
		private static void FillAer(Aeroport<IPassengerAviation<IEngine>> aer, int planesCount, int heliCount)
		{
			AmericanAviationFactory aaf = new AmericanAviationFactory();
			RussianAviationFactory raf = new RussianAviationFactory();

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
			FillAer(aer, 15, 10);
			//Посадим в каждое судно случайное количество пассажиров
			Random r = new Random();
			foreach (var avia in aer)
			{
				avia.PlacePassenger(r.Next(0, avia.Capacity));
			}
			//Конвертируем коллекцию в строку
			Console.WriteLine(aer.ConvertToString());
			//Находим, куда можно посадить 3 пассажира
			var tmpAvia = aer.FindByFreePlaces(3);
			if (tmpAvia != null)
				Console.WriteLine("В этом судне достаточно мест:\n" + tmpAvia.ConvertToString());
			//Находим все пустые воздушные суда
			Console.WriteLine(aer.FindAllEmpty().ConvertToString());
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
			return JsonConvert.SerializeObject(aer, Formatting.Indented);
		}

		/// <summary>
		/// Находит первое вхождение судна, у которого имеется достаточно свободных мест
		/// </summary>
		/// <param name="aer">Коллекция</param>
		/// <returns>Судно, либо null, если таковых не найдено</returns>
		public static IPassengerAviation<IEngine> FindByFreePlaces(this Aeroport<IPassengerAviation<IEngine>> aer, int places)
		{
			ExtensionLogger.Log("Вызван метод поиска судна с достаточным количеством мест");
			foreach (var aero in aer)
			{
				if (aero.Capacity - aero.Engaged > places)
					return aero;
			}
			ExtensionLogger.Log("Нет судна с достаточным количеством мест");
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
			var res = new Aeroport<T>(aer.Name);
			foreach (var aero in aer)
			{
				if (aero.Engaged == 0)
					res.Add(aero);
			}
			return res;
		}

		/// <summary>
		/// Конвертирует экземпляр авиации в json-строку
		/// </summary>
		/// <param name="avia">Конвертируемый экземпляр</param>
		/// <returns>json-строка</returns>
		public static string ConvertToString(this IPassengerAviation<IEngine> avia)
		{
			return JsonConvert.SerializeObject(avia, Formatting.Indented);
		}
	}

	/// <summary>
	/// Логгер
	/// </summary>
	public static class ExtensionLogger
	{
		public static string filePath = "";

		public static void Log(string mes)
		{
			if (filePath == "")
				Console.WriteLine(DateTime.Now + " : " + mes);
			else
			{
				File.AppendAllText(DateTime.Now + " : " + filePath, mes);
			}
		}
	}
}

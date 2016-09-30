using System;
using System.Collections.Generic;
using System.IO;
using Aviation.Engines;
using Aviation.Aviation;
using Aviation.Loggers;
using Aviation.Serializer;

namespace Aviation
{
	class Program
	{
		public static void Main(string[] args)
		{
			//====================================== 7 лаба ==================================================
			var aer = new Aeroport<Plane<ReactiveEngine>>("Кольцово");
			FillAerWithReactivePlanes(aer, 10);
			aer.Comparer = SortByFreePlaces;
			var binSer = new BinarySerializer<Plane<ReactiveEngine>>();
			var xmlSer = new XmlCustomSerializer<Plane<ReactiveEngine>>();
			var jsonSer = new JsonSerializer<Plane<ReactiveEngine>>();

			//Сериализация в бинарник
			binSer.Serialize(aer, "aerBinary.dat");
			aer.PrintAviation();
			aer = binSer.Deserialize("aerBinary.dat");
			Console.WriteLine("После десериализации");
			aer.PrintAviation();

			////Сериализация в XML
			xmlSer.Serialize(aer, "aerXML.xml");
			aer.PrintAviation();
			aer = xmlSer.Deserialize("aerXML.xml");
			Console.WriteLine("После десериализации");
			aer.PrintAviation();

			////Сериализация в JSON
			jsonSer.Serialize(aer, "aerjson.json");
			aer.PrintAviation();
			aer = jsonSer.Deserialize("aerjson.json");
			Console.WriteLine("После десериализации");
			aer.PrintAviation();

			Console.ReadKey();
		}

		private static void FillAerWithReactivePlanes(Aeroport<Plane<ReactiveEngine>> aer, int planesCount)
		{
			for (int i = 0; i < planesCount; ++i)
			{
				aer.Add(new Plane<ReactiveEngine>(100, 20000, "SuperJet", new ReactiveEngine("DoublePower")));
			}
		}

		private static void SortProgress(int pr)
		{
			Console.Write("\rПрогресс: {0}%", pr);
		}

		/// <summary>
		/// Метод печати лога
		/// </summary>
		/// <param name="writer">Цель вывода</param>
		/// <param name="avia">Логгируемое судно</param>
		/// <param name="args">Аргументы события</param>
		public static void LogOnLog(TextWriter writer, IPassengerAviation<IEngine> avia, AviaEventArgs args)
		{
			string toPrint = "";
			switch (args.EventType)
			{
				case EventTypes.Flight:
					toPrint = "Судно " + avia.Model + " летит из " + (args as AviaFlightEventArgs).From + " в " + (args as AviaFlightEventArgs).To;
					break;
				case EventTypes.PassIn:
					toPrint = "На судно " + avia.Model + " садятся " + (args as AviaPassInEventArgs).Count + " пассажиров";
					break;
				case EventTypes.PassOut:
					toPrint = "Из " + avia.Model + " вышли все пассажиры";
					break;
				case EventTypes.SendingMessage:
					toPrint = "Судно " + avia.Model + " послало для " + (args as AviaSendMessEventArgs).Target.Model + " сообщение: " +
							  (args as AviaSendMessEventArgs).Message;
					break;
			}
			writer.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " " + toPrint);
		}
		

		///// <summary>
		///// Проверка двигателя воздушного судна
		///// </summary>
		///// <param name="avia">Воздушное судно</param>
		//public static void CheckEngine(IPassengerAviation<IEngine> avia)
		//{
		//	ICheckEngine<IEngine> checker = new EngineChecker();
		//	checker.Check(avia.Engine);
		//}

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
		/// <summary>
		/// Блинная сортировка
		/// </summary>
		/// <typeparam name="T">Обобщение</typeparam>
		/// <param name="arr">Список сравниваемых объектов</param>
		/// <param name="comparer">Метод для сравнения двух элементов</param>
		public static void PancakeSort<T>(List<T> arr, Func<T,T,int> comparer)
		{
			for (int i = arr.Count - 1; i >= 0; --i)
			{
				int pos = i;
				for (int j = 0; j < i; j++)
				{
					if (comparer(arr[j], arr[pos]) > 0)
					{
						pos = j;
					}
				}

				if (pos == i)
				{
					continue;
				}

				if (pos != 0)
				{
					Flip(arr, pos + 1);
				}

				Flip(arr, i + 1);
			}
		}

		private static void Flip<T>(List<T> arr, int n)
		{
			for (int i = 0; i < n; i++)
			{
				--n;
				T tmp = arr[i];
				arr[i] = arr[n];
				arr[n] = tmp;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using Aviation.Engines;
using Aviation.Aviation;
using Aviation.Factories;
using Aviation.Loggers;

namespace Aviation
{
	class Program
	{
		static void Main(string[] args)
		{
			IAeroport<IPassengerAviation<IEngine>> aer = new Aeroport<IPassengerAviation<IEngine>>("Кольцово");
			FillAeroport(3, 0, aer);
			var consLogger = new ConsoleLogger<IPassengerAviation<IEngine>>(aer[0]);
			var fileLogger = new FileLoggerr<IPassengerAviation<IEngine>>(aer[1], "log1.txt");
			var fileLogger2 = new FileLoggerr<IPassengerAviation<IEngine>>(aer[0], "log1.txt");

			consLogger.OnLog += LogOnLog;
			fileLogger.OnLog += LogOnLog;
			fileLogger2.OnLog += LogOnLog;

			aer[0].PlacePassenger(10);
			aer[1].SendMessage(aer[0], "Освобождаю взлетную полосу");
			aer[1].DropOffPassenger();
			aer[0].MakeFlight(Routs.Cities.Moscow, Routs.Cities.Omsk);

			Console.ReadKey();
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
		/// <summary>
		/// Заполнение аэропорта случайными судами по указанному количеству
		/// </summary>
		/// <param name="planes">Количество самолетов</param>
		/// <param name="helicopters">Количество вертолетов</param>
		/// <param name="_aviation">Аэропорт</param>
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

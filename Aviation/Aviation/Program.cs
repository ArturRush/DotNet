using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using Aviation.Engines;
using Aviation.Aviation;
using Aviation.Exeptions;
using Aviation.Factories;
using Aviation.Loggers;

namespace Aviation
{
	class Program
	{
		static void Main(string[] args)
		{
			ExceptionLogger.ToFile = false;
			IAeroport<IPassengerAviation<IEngine>> aer = new Aeroport<IPassengerAviation<IEngine>>("Кольцово");
			FillAeroport(6, 8, aer);
			aer.PrintAviation();
			Console.WriteLine();
			Random r = new Random();
			foreach (var avia in aer)
			{
				avia.PlacePassenger(r.Next(20));
			}
			//aer.PrintAviation();
			Console.WriteLine();
			Console.WriteLine("Сортировка по количеству свободных мест");
			aer.Sorter = PancakeSort;
			//aer.Comparer = SortByFreePlaces;
			aer.Sort();//Системное исключение - Comparer == null
			aer.Comparer = SortByFreePlaces;
			aer.Sort();
			aer.PrintAviation();
			try
			{
				aer[0].PlacePassenger(-5);
			}
			catch (UserException usEx)
			{
				ExceptionLogger.LogUserException(usEx);
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogSystemException(ex);
			}
			try
			{
				aer[0].SendMessage(aer[30], "Не сработает, нет такого элемента, системное исключение");
			}
			catch (UserException usEx)
			{
				ExceptionLogger.LogUserException(usEx);
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogSystemException(ex);
			}
			try
			{
				aer[0].SendMessage(null, "Не сработает, нет  цели, пользовательское исключение");
			}
			catch (UserException usEx)
			{
				ExceptionLogger.LogUserException(usEx);
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogSystemException(ex);
			}
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

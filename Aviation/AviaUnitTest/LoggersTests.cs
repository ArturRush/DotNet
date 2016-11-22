using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aviation;
using Aviation.Loggers;
using Aviation.Aviation;
using Aviation.Engines;
using Aviation.Exeptions;
using NUnit.Framework;

namespace AviaUnitTest
{
	/// <summary>
	///Тестирование логгеров
	/// </summary>
	[TestFixture]
	public class LoggersTests
	{
		/// <summary>
		/// Тестирования создания логгеров
		/// </summary>
		[Test]
		public void LoggerCreateTest()
		{
			var av = new Plane<ReactiveEngine>(10, 20000, "qwe", new ReactiveEngine("dfs"));
			var fl = new FileLogger<IPassengerAviation<IEngine>>(av, "Test11.txt");
			var cl = new ConsoleLogger<IPassengerAviation<IEngine>>(av);
			av.MakeFlight(Routs.Cities.Omsk,Routs.Cities.Cherlak);
			av.SendMessage(av, "dfs");
			av.PlacePassenger(2);
			av.DropOffPassenger();
			File.Delete("Test11.txt");
		}
		/// <summary>
		/// Тестирование логгера исключений
		/// </summary>
		[Test]
		public void ExceptionsTest()
		{
			var av = new Plane<ReactiveEngine>(10, 20000, "qwe", new ReactiveEngine("dfs"));
			try
			{
				Assert.Throws<PassPlaceException>(() => { av.PlacePassenger(-5); });
			}
			catch (UserException usEx)
			{
				ExceptionLogger.LogUserException(usEx);
			}
			IAeroport<IPassengerAviation<IEngine>> aer = new Aeroport<IPassengerAviation<IEngine>>("Кольцово");
			aer.Add(av);
			try
			{
				Assert.Throws<Exception>(()=>
				{
					av.SendMessage(aer[30], "Не сработает, нет такого элемента, системное исключение");
				});
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogSystemException(ex);
			}
			try
			{
				Assert.Throws<NoTargetExeption>(()=> { av.SendMessage(null, "Не сработает, нет  цели, пользовательское исключение"); });
			}
			catch (UserException usEx)
			{
				ExceptionLogger.LogUserException(usEx);
			}
		}
	}
}

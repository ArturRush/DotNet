using System;
using System.IO;
using System.Threading;
using Aviation.Aviation;
using Aviation.Engines;

namespace Aviation.Loggers
{
	/// <summary>
	/// Консольный логгер
	/// </summary>
	public class ConsoleLogger<T> : ILogger<T> where T : IPassengerAviation<IEngine>
	{
		public event Action<TextWriter, T, AviaEventArgs> OnLog;
		/// <summary>
		/// Экземпляр авиации, для которого ведется логгирование
		/// </summary>
		private readonly T _avia;
		/// <summary>
		/// Конструктор логгера с выводом в консоль
		/// </summary>
		/// <param name="avia">Логгируемый экземпляр авиации</param>
		public ConsoleLogger(T avia)
		{
			_avia = avia;
			_avia.OnFlight += AviaEventHandler;
			_avia.OnPassIn += AviaEventHandler;
			_avia.OnPassOff += AviaEventHandler;
			_avia.OnSendingMessage += AviaEventHandler;
		}
		/// <summary>
		/// Объект синхронизации
		/// </summary>
		private readonly object threadLock = new object();

		/// <summary>
		/// Метод обработчика событий
		/// </summary>
		/// <param name="args">Аргументы события</param>
		private void AviaEventHandler(AviaEventArgs args)
		{
			Thread thread = new Thread(() =>
			{
				lock (threadLock)
				{
					using (var writer = Console.Out)
					{
						if (OnLog != null)
						{
							OnLog(writer, _avia, args);
						}
					}
				}
			});
		}
	}
}

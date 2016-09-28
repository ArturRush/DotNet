using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aviation.Aviation;
using Aviation.Engines;

namespace Aviation.Loggers
{
	class FileLoggerr<T> : ILogger<T> where T : IPassengerAviation<IEngine>
	{
		public event Action<TextWriter, T, AviaEventArgs> OnLog;
		/// <summary>
		/// Экземпляр авиации, для которого ведется логгирование
		/// </summary>
		private readonly T _avia;
		/// <summary>
		/// Путь для файла логгирования
		/// </summary>
		private readonly string _filePath;
		/// <summary>
		/// Конструктор логгера с выводом в файл
		/// </summary>
		/// <param name="avia">Логгируемый экземпляр авиации</param>
		/// <param name="filePath">Путь к файлу</param>
		public FileLoggerr(T avia, string filePath)
		{
			_avia = avia;
			_filePath = filePath;
			_avia.OnFlight += AviaEventHandler;
			_avia.OnPassIn += AviaEventHandler;
			_avia.OnPassOff += AviaEventHandler;
			_avia.OnSendingMessage += AviaEventHandler;

			if (!File.Exists(filePath))
			{
				var fs = File.Create(filePath);
				fs.Close();
			}
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
					using (var writer = File.AppendText(_filePath))
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

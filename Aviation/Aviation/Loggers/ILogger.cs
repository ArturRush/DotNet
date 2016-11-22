using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aviation.Aviation;
using Aviation.Engines;

namespace Aviation.Loggers
{
	/// <summary>
	/// Интерфейс для логгера
	/// </summary>
	interface ILogger<out T> where T: IPassengerAviation<IEngine>
	{
		/// <summary>
		/// Событие для внешнего метода печати
		/// </summary>
		event Action<TextWriter, T, AviaEventArgs> OnLog;
	}
}

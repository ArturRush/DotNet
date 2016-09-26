using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading.Tasks;
using Aviation.Aviation;
using Aviation.Engines;

namespace Aviation.Loggers
{
	/// <summary>
	/// Типы событий авиации
	/// </summary>
	public enum EventTypes
	{
		Flight,
		SendingMessage,
		PassIn,
		PassOut
	}
	/// <summary>
	/// Класс события для авиации
	/// </summary>
	public class AviaEventArgs
	{
		/// <summary>
		/// Тип события
		/// </summary>
		public EventTypes EventType { get; private set; }

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="eventType">Тип события</param>
		public AviaEventArgs(EventTypes eventType)
		{
			EventType = eventType;
		}
	}
	/// <summary>
	/// Класс события посадки пассажиров
	/// </summary>
	public class AviaPassInEventArgs : AviaEventArgs
	{
		/// <summary>
		/// Количество пассажиров на посадку
		/// </summary>
		public int Count { get; private set; }
		/// <summary>
		/// Конструктор события
		/// </summary>
		/// <param name="count">Количество пассажиров на посадку</param>
		public AviaPassInEventArgs(int count) : base(EventTypes.PassIn)
		{
			Count = count;
		}

	}
	/// <summary>
	/// Класс события посылки сообщения
	/// </summary>
	public class AviaSendMessEventArgs : AviaEventArgs
	{
		/// <summary>
		/// Цель сообщения
		/// </summary>
		public IPassengerAviation<IEngine> Target { get; private set; }
		/// <summary>
		/// Сообщение
		/// </summary>
		public string Message { get; private set; }
		/// <summary>
		/// Конструктор события
		/// </summary>
		/// <param name="target">Цель сообщения</param>
		/// <param name="message">Сообщение</param>
		public AviaSendMessEventArgs(IPassengerAviation<IEngine> target, string message) : base(EventTypes.SendingMessage)
		{
			Target = target;
			Message = message;
		}
	}
	/// <summary>
	/// Класс события совершения полета
	/// </summary>
	public class AviaFlightEventArgs : AviaEventArgs
	{
		/// <summary>
		/// Город вылета
		/// </summary>
		public Routs.Cities From { get; private set; }
		/// <summary>
		/// Город прилета
		/// </summary>
		public Routs.Cities To { get; private set; }
		/// <summary>
		/// Конструктор события
		/// </summary>
		/// <param name="from">Город вылета</param>
		/// <param name="to">Город прилета</param>
		public AviaFlightEventArgs(Routs.Cities from, Routs.Cities to) : base(EventTypes.Flight)
		{
			From = from;
			To = to;
		}
	}
}
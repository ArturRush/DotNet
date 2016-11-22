using System;
using Aviation.Engines;
using Aviation.Loggers;

namespace Aviation.Aviation
{
	/// <summary>
	/// Интерфейс "авиации"
	/// </summary>
	public interface IPassengerAviation<out T> : ICloneable where T: IEngine
	{
		/// <summary>
		/// Событие совершения полета
		/// </summary>
		event Action<AviaFlightEventArgs> OnFlight;
		/// <summary>
		/// Событие посадки пассажиров
		/// </summary>
		event Action<AviaPassInEventArgs> OnPassIn;
		/// <summary>
		/// Событие Высадки пассажиров
		/// </summary>
		event Action<AviaEventArgs> OnPassOff;
		/// <summary>
		/// Событие посылки сообщения
		/// </summary>
		event Action<AviaSendMessEventArgs> OnSendingMessage;

		/// <summary>
		/// Вместимость салона(пассажиров)
		/// </summary>
		int Capacity { get; }

		/// <summary>
		/// Занято мест
		/// </summary>
		int Engaged { get; }

		/// <summary>
		/// Вместимость бака (литров)
		/// </summary>
		int TankCapacity { get; }
		/// <summary>
		/// Модель воздушного судна
		/// </summary>
		string Model { get; }

		/// <summary>
		/// Двигатель судна
		/// </summary>
		T Engine { get; }

		/// <summary>
		/// Поместить пассажиров в судно
		/// </summary>
		/// <param name="count">Количество сажаемых пассажиров</param>
		void PlacePassenger(int count);

		/// <summary>
		/// Высадить пассажиров
		/// </summary>
		void DropOffPassenger();

		/// <summary>
		/// Отправить сообщение другому судну
		/// </summary>
		/// <param name="target">Цель сообщения</param>
		/// <param name="mes">Текст сообщения</param>
		void SendMessage(IPassengerAviation<IEngine> target, string mes);

		/// <summary>
		/// Получить сообщение с другого судна
		/// </summary>
		/// <param name="mes">Текст сообщения</param>
		void ReceiveMessage(string mes);

		/// <summary>
		/// Отправить судно в полет по заданному маршруту
		/// </summary>
		/// <param name="from">Город отправления</param>
		/// <param name="to">Город прибытия</param>
		void MakeFlight(Routs.Cities from, Routs.Cities to);
	}
}

using System;
using Aviation.Engines;
using Aviation.Exeptions;
using Aviation.Loggers;

namespace Aviation.Aviation
{
	/// <summary>
	/// Базовый класс авиации
	/// </summary>
	[Serializable]
	internal abstract class PassengerAviationBase<T> : IPassengerAviation<T> where T : IEngine
	{
		public virtual event Action<AviaFlightEventArgs> OnFlight;
		public event Action<AviaPassInEventArgs> OnPassIn;
		public event Action<AviaEventArgs> OnPassOff;
		public event Action<AviaSendMessEventArgs> OnSendingMessage;
		public int Capacity { get; set; }
		public int Engaged { get; set; }
		public int TankCapacity { get; set; }
		public string Model { get; set; }

		public T Engine { get; set; }

		public void PlacePassenger(int count)
		{
			if(count<0)
				throw new PassPlaceException("Некорректное количество пассажиров - " + count);
			if (Capacity <= Engaged + count)
			{
				Engaged = Capacity;
				//Console.WriteLine("Судно {0} заполнено!", Model);
			}
			else
			{
				Engaged += count;
				//Console.WriteLine("На судне {0} занято {1} мест из {2}",Model, Engaged, Capacity);
			}
			if (OnPassIn != null)
			{
				OnPassIn(new AviaPassInEventArgs(count));
			}
		}

		public void DropOffPassenger()
		{
			Engaged = 0;
			//Console.WriteLine("Все пассажиры из {0} высажены!", Model);
			if (OnPassOff != null)
			{
				OnPassOff(new AviaEventArgs(EventTypes.PassOut));
			}
		}

		public void SendMessage(IPassengerAviation<IEngine> target, string mes)
		{
			if(target == null)
				throw new NoTargetExeption("Цель сообщения не определена");
			//Console.WriteLine("{0} послал сообщение для {1}", Model, target.Model);
			target.ReceiveMessage(mes);
			if (OnSendingMessage != null)
			{
				OnSendingMessage(new AviaSendMessEventArgs(target, mes));
			}
		}

		public void ReceiveMessage(string mes)
		{
			Console.WriteLine("На судне {0} получено сообщение: {1}", Model, mes);
		}

		public virtual void MakeFlight(Routs.Cities from, Routs.Cities to)
		{
			
		}

		protected PassengerAviationBase(int capacity, int tankCapacity, string model, T engine)
		{
			Capacity = capacity;
			TankCapacity = tankCapacity;
			Model = model;
			Engine = engine;
			Engaged = 0;
		}

		protected PassengerAviationBase()
		{ }
		
		public abstract object Clone();
	}
}

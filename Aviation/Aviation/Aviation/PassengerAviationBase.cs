using System;
using Aviation.Engines;

namespace Aviation.Aviation
{
	/// <summary>
	/// Базовый класс авиации
	/// </summary>
	public abstract class PassengerAviationBase<T> : IPassengerAviation<T> where T: IEngine
	{
		public int Capacity { get; private set; }
		public int Engaged { get; private set; }
		public int TankCapacity { get; private set; }
		public string Model { get; private set; }

		public T Engine { get; private set; }

		public void PlacePassenger(int count)
		{
			if (Capacity <= Engaged + count)
			{
				Engaged = Capacity;
				Console.WriteLine("Судно {0} заполнено!", Model);
			}
			else
			{
				Engaged += count;
				Console.WriteLine("На судне {0} занято {1} мест из {2}",Model, Engaged, Capacity);
			}
		}

		public void DropOffPassenger()
		{
			Engaged = 0;
			Console.WriteLine("Все пассажиры из {0} высажены!", Model);
		}

		public void SendMessage(IPassengerAviation<IEngine> target, string mes)
		{
			Console.WriteLine("{0} послал сообщение для {1}", Model, target.Model);
			target.ReceiveMessage(mes);
		}

		public void ReceiveMessage(string mes)
		{
			Console.WriteLine("На судне {0} получено сообщение: {1}", Model, mes);
		}

		virtual public void MakeFlight(Routs.Cities from, Routs.Cities to)
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

		public abstract object Clone();
	}
}

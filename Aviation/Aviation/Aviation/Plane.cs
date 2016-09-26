﻿using System;
using Aviation.Engines;
using Aviation.Loggers;

namespace Aviation.Aviation
{
	/// <summary>
	/// Самолет
	/// </summary>
	public class Plane<T> : PassengerAviationBase<T>, IPlane<T> where T : IPlaneEngine
	{
		public event Action<AviaFlightEventArgs> OnFlight;

		public Plane(int capacity, int tankCapacity, string model, T engine)
			: base(capacity, tankCapacity, model, engine)
		{
			
		}

		public IEngine Engine
		{
			get { return base.Engine; }
		}

		override public void MakeFlight(Routs.Cities from, Routs.Cities to)
		{
			if (Routs.GetDistance(from, to) >= TankCapacity*100/Engine.Consumption)
			{
				//Console.WriteLine("{0} не хватит топлива до пункта назначения", Model);
				return;
			}
			//Console.WriteLine("{0} готов к взлету", Model);
			//Console.WriteLine("Включить двигатель {0}", Engine.Model);
			Engine.Move();
			//Console.WriteLine("{0} приземляется на взлетную полосу", Model);
			if (OnFlight != null)
			{
				OnFlight(new AviaFlightEventArgs(from,to));
			}
		}

		public override object Clone()
		{
			return new Plane<T>(Capacity, TankCapacity, Model, (T)Engine);
		}
	}
}

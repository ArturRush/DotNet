using System;
using Aviation.Engines;
using Aviation.Loggers;

namespace Aviation.Aviation
{
	/// <summary>
	/// Самолет
	/// </summary>
	[Serializable]
	public class Plane<T> : PassengerAviationBase<T>, IPlane<T> where T : IPlaneEngine
	{
		public event Action<AviaFlightEventArgs> OnFlight;

		public Plane(int capacity, int tankCapacity, string model, T engine)
			: base(capacity, tankCapacity, model, engine)
		{
			
		}
		public Plane()
		{ }

		public IEngine Engine
		{
			get { return base.Engine; }
		}

		override public void MakeFlight(Routs.Cities from, Routs.Cities to)
		{
			if (Routs.GetDistance(from, to) >= TankCapacity*100/Engine.Consumption)
			{
				return;
			}
			Engine.Move();
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

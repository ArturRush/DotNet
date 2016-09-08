using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aviation.Engines;

namespace Aviation.Aviation
{
	/// <summary>
	/// Вертолет
	/// </summary>
	public class Helicopter : PassengerAviationBase
	{
		public Helicopter(int capacity, int tankCapacity, string model, IEngine engine)
			: base(capacity, tankCapacity, model, engine)
		{
			
		}

		override public void MakeFlight(Routs.Cities from, Routs.Cities to)
		{
			if (Routs.GetDistance(from, to) > TankCapacity * 100 / Engine.Consumption)
			{
				Console.WriteLine("{0} не хватит топлива до пункта назначения", Model);
				return;
			}
			Console.WriteLine("{0} готов к взлету", Model);
			Console.WriteLine("Включить двигатель {0}", Engine.Model);
			Engine.Move();
			Console.WriteLine("{0} приземляется", Model);
		}
	}
}

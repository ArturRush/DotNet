using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aviation.Engines
{
	/// <summary>
	/// Самолетный двигатель
	/// </summary>
	[Serializable]
	public class PlaneEngine : EngineBase, IPlaneEngine
	{
		protected PlaneEngine(int consumption, string model, int speed) : base(consumption, model, speed)
		{
		}

		protected PlaneEngine()
		{
			
		}

		override public void Move()
		{
			Console.WriteLine("Двигатель {0} разгоняет судно", Model);
			Console.WriteLine("Судно взлетает");
			Console.WriteLine("Двигатель перемещает судно в пункт назначения");
		}

		public void Fly()
		{
			Console.WriteLine("Самолет летит вперед");
		}
	}
}

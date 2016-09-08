using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aviation.Engines
{
	/// <summary>
	/// Реактивный двигатель
	/// </summary>
	public class ReactiveEngine : EngineBase, IReactiveEngine
	{
		private const int ReactiveEngineSpeed = 1000;
		private const int ReactiveEngineConsumption = 1500;

		public ReactiveEngine(string model) : base(ReactiveEngineConsumption, model, ReactiveEngineSpeed)
		{
		}

		override public void Move()
		{
			Console.WriteLine("Двигатель {0} разгоняет судно", Model);
			Console.WriteLine("Судно взлетает");
			Console.WriteLine("Двигатель перемещает судно в пункт назначения");
		}
	}
}

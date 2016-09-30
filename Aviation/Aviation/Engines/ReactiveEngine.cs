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
	[Serializable]
	public class ReactiveEngine : PlaneEngine, IReactiveEngine
	{
		private const int ReactiveEngineSpeed = 1000;
		private const int ReactiveEngineConsumption = 1500;

		public ReactiveEngine(string model) : base(ReactiveEngineConsumption, model, ReactiveEngineSpeed)
		{
		}

		public ReactiveEngine()
		{
			
		}
	}
}

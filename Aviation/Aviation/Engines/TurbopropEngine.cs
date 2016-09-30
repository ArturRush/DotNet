using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aviation.Engines
{
	/// <summary>
	/// Турбовинтовой двигатель
	/// </summary>
	[Serializable]
	public class TurbopropEngine : PlaneEngine, ITurbopropEngine
	{
		private const int TurbopropEngineSpeed = 650;
		private const int TurbopropEngineConsumption = 1000;

		public TurbopropEngine(string model)
			: base(TurbopropEngineConsumption, model, TurbopropEngineSpeed)
		{
		}
		public TurbopropEngine()
		{
		}
	}
}

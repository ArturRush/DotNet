using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aviation.Engines
{
	/// <summary>
	/// Газотурбинный двигатель
	/// </summary>
	[Serializable]
	internal class GasTurbineEngine : HelicopterEngine, IGasTurbineEngine
	{
		private const int GasTurbineEngineSpeed = 200;
		private const int GasTurbineEngineConsumption = 60;

		public GasTurbineEngine(string model) : base(GasTurbineEngineConsumption, model, GasTurbineEngineSpeed)
		{
		}

		public GasTurbineEngine()
		{
			
		}
	}
}

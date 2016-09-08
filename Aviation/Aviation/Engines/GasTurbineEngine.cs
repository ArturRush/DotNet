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
	public class GasTurbineEngine : EngineBase, IGasTurbineEngine
	{
		private const int GasTurbineEngineSpeed = 200;
		private const int GasTurbineEngineConsumption = 60;

		public GasTurbineEngine(string model) : base(GasTurbineEngineConsumption, model, GasTurbineEngineSpeed)
		{
		}

		override public void Move()
		{
			Console.WriteLine("Двигатель {0} разгоняет винт", Model);
			Console.WriteLine("Судно взлетает");
			Console.WriteLine("Судно прилетает в пункт назначения");
		}
	}
}

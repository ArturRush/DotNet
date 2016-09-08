using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aviation.Aviation;
using Aviation.Engines;

namespace Aviation.Factories
{
	/// <summary>
	/// Фабрика русской авиации
	/// </summary>
	class RussianAviationFactory : IAviationFactory
	{
		readonly IReactiveEngine TUReactiveEngine = new ReactiveEngine("НК-32");
		readonly IGasTurbineEngine MIGasTurbineEngine = new GasTurbineEngine("ДКП-13");

		/// <summary>
		/// Создать русский вертолет
		/// </summary>
		/// <returns>Русский вертолет</returns>
		public IPassengerAviation CreateHelicopter()
		{
			return new Helicopter(4, 500, "Ми-3", (IEngine)MIGasTurbineEngine);
		}

		/// <summary>
		/// Создать русский самолет
		/// </summary>
		/// <returns>Русский самолет</returns>
		public IPassengerAviation CreatePlane()
		{
			return new Plane(120, 50000, "ТУ-160", (IEngine)TUReactiveEngine);
		}
	}
}

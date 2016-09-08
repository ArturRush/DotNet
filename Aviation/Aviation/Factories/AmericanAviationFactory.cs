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
	/// Фабрика американской авиации
	/// </summary>
	class AmericanAviationFactory : IAviationFactory
	{
		readonly IReactiveEngine BoeingReactiveEngine = new ReactiveEngine("GE90");
		readonly IGasTurbineEngine RobinsonGasTurbineEngine = new GasTurbineEngine("RobS5W");

		/// <summary>
		/// Создать американский вертолет
		/// </summary>
		/// <returns>Американский вертолет</returns>
		public IPassengerAviation CreateHelicopter()
		{
			return new Helicopter(4, 500, "R22", (IEngine)RobinsonGasTurbineEngine);
		}

		/// <summary>
		/// Создать американский самолет
		/// </summary>
		/// <returns>Американский самолет</returns>
		public IPassengerAviation CreatePlane()
		{
			return new Plane(120, 50000, "777", (IEngine)BoeingReactiveEngine);
		}
	}
}

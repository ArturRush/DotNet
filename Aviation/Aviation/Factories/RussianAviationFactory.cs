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
		readonly IReactiveEngine _tuReactiveEngine = new ReactiveEngine("НК-32");
		readonly IGasTurbineEngine _miGasTurbineEngine = new GasTurbineEngine("ДКП-13");
		readonly ITurbopropEngine _tuTurboPropEngine = new TurbopropEngine("ДР-13");

		/// <summary>
		/// Создать русский вертолет
		/// </summary>
		/// <returns>Русский вертолет</returns>
		public IPassengerAviation<IHelicopterEngine> CreateHelicopter()
		{
			return new Helicopter<IGasTurbineEngine>(4, 500, "Ми-3", _miGasTurbineEngine);
		}

		/// <summary>
		/// Создать русский реактивный самолет
		/// </summary>
		/// <returns>Русский реактивный самолет</returns>
		public IPassengerAviation<IPlaneEngine> CreateReactivePlane()
		{
			return new Plane<IReactiveEngine>(120, 50000, "ТУ-160", _tuReactiveEngine);
		}

		/// <summary>
		/// Создать русский турбовинтовой самолет
		/// </summary>
		/// <returns>Русский турбовинтовой самолет</returns>
		public IPassengerAviation<IPlaneEngine> CreateTurbopropPlane()
		{
			return new Plane<ITurbopropEngine>(80, 45000, "ТУ-95", _tuTurboPropEngine);
		}
	}
}

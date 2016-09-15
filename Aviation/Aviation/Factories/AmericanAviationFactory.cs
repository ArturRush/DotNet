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
		readonly IReactiveEngine _boeingReactiveEngine = new ReactiveEngine("GE90");
		readonly IGasTurbineEngine _robinsonGasTurbineEngine = new GasTurbineEngine("RobS5W");
		readonly ITurbopropEngine _b2TurbopropEngine = new TurbopropEngine("ST5");

		/// <summary>
		/// Создать американский реактивный самолет
		/// </summary>
		/// <returns>Американский реактивный самолет</returns>
		public IPassengerAviation<IPlaneEngine> CreateReactivePlane()
		{
			return new Plane<IReactiveEngine>(80, 40000, "777", _boeingReactiveEngine);
		}

		/// <summary>
		/// Создать американский турбовинтовой самолет
		/// </summary>
		/// <returns>Американский турбовинтовой самолет</returns>
		public IPassengerAviation<IPlaneEngine> CreateTurbopropPlane()
		{
			return new Plane<ITurbopropEngine>(70, 35000, "B52", _b2TurbopropEngine);
		}


		/// <summary>
		/// Создать американский вертолет
		/// </summary>
		/// <returns>Американский вертолет</returns>
		public IPassengerAviation<IHelicopterEngine> CreateHelicopter()
		{
			return new Helicopter<IGasTurbineEngine>(6, 550, "R22", _robinsonGasTurbineEngine);
		}
	}
}

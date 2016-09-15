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
	/// Фабрика воздушных судов
	/// </summary>
	interface IAviationFactory
	{
		/// <summary>
		/// Создать вертолет
		/// </summary>
		/// <returns></returns>
		IPassengerAviation<IHelicopterEngine>  CreateHelicopter();

		/// <summary>
		/// Создать реактивный самолет
		/// </summary>
		/// <returns></returns>
		IPassengerAviation<IPlaneEngine> CreateReactivePlane();

		/// <summary>
		/// Создать турбовинтовой самолет
		/// </summary>
		/// <returns></returns>
		IPassengerAviation<IPlaneEngine> CreateTurbopropPlane();
	}
}

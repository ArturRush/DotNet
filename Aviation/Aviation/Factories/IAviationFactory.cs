using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aviation.Aviation;

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
		IPassengerAviation CreateHelicopter();

		/// <summary>
		/// Создать самолет
		/// </summary>
		/// <returns></returns>
		IPassengerAviation CreatePlane();
	}
}

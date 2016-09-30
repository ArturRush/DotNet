using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aviation.Aviation;
using Aviation.Engines;
using Aviation.Exeptions;
using Aviation.Loggers;

namespace Aviation.Factories
{
	/// <summary>
	/// Фабрика русской авиации
	/// </summary>
	class RussianAviationFactory : IAviationFactory
	{
		readonly ReactiveEngine _tuReactiveEngine = new ReactiveEngine("НК-32");
		readonly GasTurbineEngine _miGasTurbineEngine = new GasTurbineEngine("ДКП-13");
		readonly TurbopropEngine _tuTurboPropEngine = new TurbopropEngine("ДР-13");
		private readonly List<string> _rusHelicopters = new List<string>();
		private readonly List<string> _rusPlanes = new List<string>();

		/// <summary>
		/// Конструктор по умолчанию, ведется загрузка данных из фалов
		/// </summary>
		public RussianAviationFactory()
		{
			try
			{
				_rusHelicopters = File.ReadLines("RussianHelicopters.txt").ToList();
			}
			catch (UserException userEx)
			{
				ExceptionLogger.LogUserException(userEx);
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogSystemException(ex);
			}

			try
			{
				_rusPlanes = (List<string>)File.ReadLines("RussianPlanes.txt");
			}
			catch (UserException userEx)
			{
				ExceptionLogger.LogUserException(userEx);
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogSystemException(ex);
			}
		}

		/// <summary>
		/// Создать русский вертолет
		/// </summary>
		/// <returns>Русский вертолет</returns>
		public IPassengerAviation<IHelicopterEngine> CreateHelicopter()
		{
			Random r = new Random();
			if (_rusHelicopters.Count <= 0)
				return new Helicopter<GasTurbineEngine>(4, 500, "Ми-3", _miGasTurbineEngine);

			return new Helicopter<GasTurbineEngine>(4, 500, _rusHelicopters[r.Next(_rusHelicopters.Count - 1)], _miGasTurbineEngine);
		}

		/// <summary>
		/// Создать русский реактивный самолет
		/// </summary>
		/// <returns>Русский реактивный самолет</returns>
		public IPassengerAviation<IPlaneEngine> CreateReactivePlane()
		{
			Random r = new Random();
			if (_rusPlanes.Count <= 0)
				return new Plane<ReactiveEngine>(120, 50000, "ТУ-160", _tuReactiveEngine);

			return new Plane<ReactiveEngine>(120, 50000, _rusPlanes[r.Next(_rusPlanes.Count-1)], _tuReactiveEngine);
		}

		/// <summary>
		/// Создать русский турбовинтовой самолет
		/// </summary>
		/// <returns>Русский турбовинтовой самолет</returns>
		public IPassengerAviation<IPlaneEngine> CreateTurbopropPlane()
		{
			Random r = new Random();
			if (_rusPlanes.Count <= 0)
				return new Plane<TurbopropEngine>(80, 45000, "ТУ-95", _tuTurboPropEngine);

			return new Plane<TurbopropEngine>(80, 45000, _rusPlanes[r.Next(_rusPlanes.Count-1)], _tuTurboPropEngine);
		}
	}
}

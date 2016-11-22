using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aviation.Aviation;
using Aviation.Engines;

namespace Aviation.Factories
{
	public static class RandomAviationGenerator
	{
		/// <summary>
		/// Заполнение аэропорта случайными судами по указанному количеству
		/// </summary>
		/// <param name="planes">Количество самолетов</param>
		/// <param name="helicopters">Количество вертолетов</param>
		/// <param name="aviation">Аэропорт</param>
		public static void FillAeroport(int planes, int helicopters, IAeroport<IPassengerAviation<IEngine>> aviation)
		{
			IAviationFactory aaf = new AmericanAviationFactory();
			IAviationFactory raf = new RussianAviationFactory();

			Random r = new Random();
			int americanPlanes = r.Next(planes);
			int russianHelicopters = r.Next(helicopters);

			for (int i = 0; i < planes - americanPlanes; ++i)
			{
				int reactive = r.Next(2);
				if (reactive == 1)
					aviation.Add(raf.CreateReactivePlane());
				else
					aviation.Add(raf.CreateTurbopropPlane());
			}

			for (int i = 0; i < americanPlanes; ++i)
			{
				int reactive = r.Next(2);
				if (reactive == 1)
					aviation.Add(aaf.CreateReactivePlane());
				else
					aviation.Add(aaf.CreateTurbopropPlane());
			}

			for (int i = 0; i < russianHelicopters; ++i)
			{
				aviation.Add(raf.CreateHelicopter());
			}

			for (int i = 0; i < helicopters - russianHelicopters; ++i)
			{
				aviation.Add(aaf.CreateHelicopter());
			}
		}
	}
}

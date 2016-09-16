using System;
using System.Collections.Generic;
using Aviation.Engines;
using Aviation.Aviation;
using Aviation.Factories;

namespace Aviation
{
	class Program
	{
		static void Main(string[] args)
		{
			IAeroport<IPassengerAviation<IEngine>> aer = new Aeroport<IPassengerAviation<IEngine>>("Кольцово");
			FillAeroport(10, 5, aer);
			aer.PrintAviation();

			RussianAviationFactory raf = new RussianAviationFactory();
			IPassengerAviation<IEngine> rus = raf.CreateReactivePlane();//Ковариантность

			ICheckEngine<IEngine> chEng = new EngineChecker();
			ICheckEngine<IPlaneEngine> chPlane = chEng;//Контравариантность
			chPlane.Check(new ReactiveEngine("АБ"));
			chEng.Check(new GasTurbineEngine("Вертолетный"));
			

			Console.ReadKey();
		}

		public static void FillAeroport(int planes, int helicopters, IAeroport<IPassengerAviation<IEngine>> _aviation)
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
					_aviation.Add(raf.CreateReactivePlane());
				else
					_aviation.Add(raf.CreateTurbopropPlane());
			}

			for (int i = 0; i < americanPlanes; ++i)
			{
				int reactive = r.Next(2);
				if (reactive == 1)
					_aviation.Add(aaf.CreateReactivePlane());
				else
					_aviation.Add(aaf.CreateTurbopropPlane());
			}

			for (int i = 0; i < russianHelicopters; ++i)
			{
				_aviation.Add(raf.CreateHelicopter());
			}

			for (int i = 0; i < helicopters - russianHelicopters; ++i)
			{
				_aviation.Add(aaf.CreateHelicopter());
			}
		}
	}
}
